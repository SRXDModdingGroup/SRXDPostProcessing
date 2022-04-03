using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace SRXDPostProcessing;

[BepInPlugin("SRXD.PostProcessing", "PostProcessing", "1.0.0.0")]
internal class Plugin : BaseUnityPlugin {
    public new static ManualLogSource Logger { get; private set; }
    
    internal static CustomPostProcessFeature PostProcessFeature { get; private set; }
    
    private void Awake() {
        Logger = base.Logger;
        
        var harmony = new Harmony("PostProcessTest");
        
        harmony.PatchAll(typeof(Patches));
        PostProcessFeature = ScriptableObject.CreateInstance<CustomPostProcessFeature>();
    }
}