using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System;
using UnityEngine;

namespace ImmersiveMovement
{
    [BepInPlugin("ImmersiveMovement", "ImmersiveMovement", "1.0.1")]
    public class ImmersiveMovement : BasePlugin
    {
        public static ManualLogSource Logger;
        private Harmony harmony;

        public static ConfigEntry<float> walkSpeedMultiplier;
        public static ConfigEntry<float> runSpeedMultiplier;

        public override void Load()
        {
            Logger = Log;
            Logger.LogInfo("Loading ImmersiveMovement...");

            walkSpeedMultiplier = Config.Bind("Player", "Walk Speed", 1.7f, "Player walking speed. (Game default 4)");
            runSpeedMultiplier = Config.Bind("Player", "Run Speed", 3.0f, "Player for player running speed. (Game default 8)");

            Logger.LogInfo($"Walk speed multiplier: {walkSpeedMultiplier.Value}");
            Logger.LogInfo($"Run speed multiplier: {runSpeedMultiplier.Value}");

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
