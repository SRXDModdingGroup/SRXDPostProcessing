using System.Linq;
using HarmonyLib;
using UnityEngine.Rendering.Universal;

namespace SRXDPostProcessing;

internal class Patches {
    [HarmonyPatch(typeof(UniversalRendererData), "Create"), HarmonyPrefix]
    private static void UniversalRendererData_Create_Prefix(UniversalRendererData __instance) {
        var features = __instance.rendererFeatures;

        if (!features.Any(feature => feature is CustomPostProcessFeature))
            features.Add(Plugin.PostProcessFeature);
    }
}