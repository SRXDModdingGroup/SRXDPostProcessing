using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace SRXDPostProcessing; 

internal class CustomPostProcessFeature : ScriptableRendererFeature {
    private static readonly int ASPECT = Shader.PropertyToID("_Aspect");
    
    private CustomPostProcessPass pass;

    public override void Create() {
        pass = new CustomPostProcessPass();
        pass.renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) => renderer.EnqueuePass(pass);

    private class CustomPostProcessPass : ScriptableRenderPass {
        private RenderTargetIdentifier cameraColorTarget;
        private RenderTargetHandle tempTexture1;
        private RenderTargetHandle tempTexture2;
        private float aspect;
        private bool shouldProcess;
        private PostProcessingLayer targetLayer;

        public CustomPostProcessPass() {
            tempTexture1.Init("_tempTexture1");
            tempTexture2.Init("_tempTexture2");
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData) {
            var mainCamera = MainCamera.Instance;
            var cameraData = renderingData.cameraData;
            var camera = cameraData.camera;
            
            shouldProcess = mainCamera != null && (camera == mainCamera.CurrentCamera || camera == mainCamera.backgroundCamera);
            
            if (!shouldProcess)
                return;
            
            cameraColorTarget = cameraData.renderer.cameraColorTarget;
            aspect = camera.aspect;

            if (camera == mainCamera.CurrentCamera)
                targetLayer = PostProcessingLayer.Foreground;
            else
                targetLayer = PostProcessingLayer.Background;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) {
            if (!shouldProcess)
                return;
            
            var cmd = CommandBufferPool.Get("CustomRenderFeature");
            var descriptor = renderingData.cameraData.cameraTargetDescriptor;

            descriptor.depthBufferBits = 0;
            cmd.GetTemporaryRT(tempTexture1.id, descriptor, FilterMode.Bilinear);
            cmd.GetTemporaryRT(tempTexture2.id, descriptor, FilterMode.Bilinear);
            Blit(cmd, cameraColorTarget, tempTexture1.Identifier());

            var source = tempTexture1.Identifier();
            var destination = tempTexture2.Identifier();

            foreach (var instance in PostProcessingManager.PostProcessingInstances) {
                if (!instance.Enabled || instance.Layer != targetLayer)
                    continue;

                var material = instance.Material;
                
                material.SetFloat(ASPECT, aspect);
                Blit(cmd, source, destination, material);
                (source, destination) = (destination, source);
            }

            Blit(cmd, source, cameraColorTarget);
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd) {
            if (!shouldProcess)
                return;
            
            cmd.ReleaseTemporaryRT(tempTexture1.id);
            cmd.ReleaseTemporaryRT(tempTexture2.id);
        }
    }
}