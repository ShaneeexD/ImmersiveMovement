using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System;
using UnityEngine;

namespace ImmersiveMovement
{
    [BepInPlugin("ImmersiveMovement", "ImmersiveMovement", "1.0.0")]
    public class ImmersiveMovement : BasePlugin
    {
        public static ManualLogSource Logger;
        private Harmony harmony;

        public override void Load()
        {
            // Initialize the logger
            Logger = Log;
            Logger.LogInfo("Loading ImmersiveMovement...");

            try
            {
                harmony = new Harmony("ImmersiveMovement");
                Logger.LogInfo("Harmony instance created.");

                harmony.PatchAll();
                Logger.LogInfo("All patches applied.");

            }
            catch (Exception ex)
            {
                Logger.LogError($"Error during Load: {ex}");
            }
        }
    }    
}
