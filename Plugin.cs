using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace SRXDPostProcessing;

[BepInPlugin("SRXD.PostProcessing", "PostProcessing", "1.0.0.0")]
internal class Plugin : BaseUnityPlugin {
    internal static CustomPostProcessFeature PostProcessFeature { get; private set; }
    
    private void Awake() {
        var harmony = new Harmony("PostProcessTest");
        
        harmony.PatchAll(typeof(Patches));
        PostProcessFeature = ScriptableObject.CreateInstance<CustomPostProcessFeature>();
    }
}