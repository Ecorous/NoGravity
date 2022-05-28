using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace NoGravity;

[BepInAutoPlugin("com.exo.examplemod", "NoGravity")]
public partial class NoGravityPlugin : BaseUnityPlugin
{
    public const string Author  = "Exo";

    internal Harmony Harmony { get; } = new(Id);
        
    internal void Awake()
    {
        // Applying patches
        Harmony.PatchAll();
        Logger.LogInfo($"{Name} successfully loaded! Made by {Author}");
    }
}
/* Harmony patches modify the game code at runtime
 * Official website: https://harmony.pardeike.net/
 * Introduction: https://harmony.pardeike.net/articles/intro.html
 * API Documentation: https://harmony.pardeike.net/api/index.html
 */

// Here's the example of harmony patch
[HarmonyPatch(typeof(VersionNumberTextMesh), nameof(VersionNumberTextMesh.Start))]
/* We're patching the method "Start" of class VersionNumberTextMesh
* The first argument can typeof(class) or class name (string). Warning: it's case-sensitive
* The second argument is our method. It can be a nameof(class.method) or method name (string). Also case-sensitive
* So, for example, patch can look like this:
* [HarmonyPatch("VersionNumberTextMesh", "Start")]
* Or like this:
* [HarmonyPatch(typeof(VersionNumberTextMesh), nameof(VersionNumberTextMesh.Start))
* But the method Start is private and can't be accesed like that. So we have to use the method name (string) instead.
*/
public class VersionNumberTextMeshPatch
{
    // Postfix is called after executing target method's code.
    public static void Postfix(VersionNumberTextMesh __instance)
    {
        // We're adding new line to version text.
        __instance.textMesh.text +=
            $"\n<color=blue>{NoGravityPlugin.Name} v{NoGravityPlugin.Version} by {NoGravityPlugin.Author}</color>";
    }
}

[HarmonyPatch(typeof(SpiderController), nameof(SpiderController.AirControl))]
public class SpiderControllerPatch
{
    public static void Prefix()
    {
        Physics2D.gravity = Vector2.zero;
    }
}
