using HarmonyLib;
using modweaver.core;
using UnityEngine;

namespace NoGravity;

[ModMainClass]
public class NoGravityPlugin : Mod {
    public override void Init() {
        Logger.Info("Loading {0} v{1} by {2}", Metadata.title, Metadata.version, string.Join(", ", Metadata.authors));
        // Applying patches
        Logger.Debug("Setting up patcher...");
        Harmony harmony = new Harmony(Metadata.id); 
        Logger.Debug("Patching...");
        harmony.PatchAll();
    }

    public override void Ready() {
        Logger.Info("Loaded {0}!", Metadata.title);
    }

    public override void OnGUI(ModsMenuPopup ui) { }
}


[HarmonyPatch(typeof(SpiderController), nameof(SpiderController.AirControl))]
public class SpiderControllerPatch {
    public static void Prefix() {
        Physics2D.gravity = Vector2.zero;
    }
}