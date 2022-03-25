using System;
using UnityEngine;

namespace SRXDPostProcessing; 

/// <summary>
/// An instance of a post processing effect
/// </summary>
public class PostProcessingInstance : IComparable<PostProcessingInstance> {
    /// <summary>
    /// Set this to enable or disable the effect
    /// </summary>
    public bool Enabled { get; set; }
    
    /// <summary>
    /// The material to apply to the render target
    /// </summary>
    /// <remarks>Shader used by the material must have a _MainTex texture parameter as input, and may also have an _Aspect float parameter to get the camera's aspect ratio</remarks>
    public Material Material { get; }
    
    /// <summary>
    /// The layer to apply the effect to
    /// </summary>
    public PostProcessingLayer Layer { get; }
    
    /// <summary>
    /// The priority of the effect. Effects with a lower priority value will be applied first
    /// </summary>
    public int Priority { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="material">The material to apply to the render target</param>
    /// <param name="enabled">If true, the effect will be enabled immediately</param>
    /// <param name="layer">The layer to apply the effect to</param>
    /// <param name="priority">The priority of the effect. Effects with a lower priority value will be applied first</param>
    /// <remarks>Shader used by the material must have a _MainTex texture parameter as input, and may also have an _Aspect float parameter to get the camera's aspect ratio</remarks>
    public PostProcessingInstance(Material material, bool enabled = false, PostProcessingLayer layer = PostProcessingLayer.Foreground, int priority = 0) {
        Material = material;
        Enabled = enabled;
        Layer = layer;
        Priority = priority;
    }

    public int CompareTo(PostProcessingInstance other) => Priority.CompareTo(other.Priority);
}