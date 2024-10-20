using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System;
using UnityEngine;

namespace ImmersiveMovement
{
    [BepInPlugin("ImmersiveMovement", "ImmersiveMovement", "1.1.0")]
    public class ImmersiveMovement : BasePlugin
    {
        public static ManualLogSource Logger;
        private Harmony harmony;

        public static ConfigEntry<float> walkSpeedMultiplier;
        public static ConfigEntry<float> runSpeedMultiplier;
        public static ConfigEntry<float> leanSpeedMultiplier;
        public static ConfigEntry<bool> cameraJolt;


        public override void Load()
        {
            Logger = Log;
            Logger.LogInfo("Loading ImmersiveMovement...");

            walkSpeedMultiplier = Config.Bind("Player", "Walk Speed", 1.7f, "Player walking speed. (Game default 4)");
            runSpeedMultiplier = Config.Bind("Player", "Run Speed", 3.0f, "Player running speed. (Game default 8)");

            leanSpeedMultiplier = Config.Bind("Player", "Lean Speed", 2.0f, "Player lean speed. (Game default 10)");
            cameraJolt = Config.Bind("Player", "Camera Jolt", true, "Less camera jolt from falling.");

            Logger.LogInfo($"Walk speed multiplier: {walkSpeedMultiplier.Value}");
            Logger.LogInfo($"Run speed multiplier: {runSpeedMultiplier.Value}");
            Logger.LogInfo($"Lean speed multiplier: {leanSpeedMultiplier.Value}");
            Logger.LogInfo($"Override camera jolt: {cameraJolt.Value}");
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
