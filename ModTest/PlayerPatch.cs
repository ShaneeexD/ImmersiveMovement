using HarmonyLib;
using UnityEngine;
using System.Reflection;

namespace ImmersiveMovement
{
    [HarmonyPatch(typeof(Player), "FixedUpdate")] 
    public class PlayerPatch
    {
        private static bool logSent = false; 

        [HarmonyPrefix]
        public static void Prefix(Player __instance)
        {
            if (__instance != null)
            {
                GameplayControls controls = GameplayControls.Instance;

                if (controls != null)
                {
                    controls.playerWalkSpeed = 1.5f;
                    controls.playerRunSpeed = 2.8f;

                    if (!logSent)
                    {
                        logSent = true;
                        ImmersiveMovement.Logger.LogInfo("Player speed modified.");
                    }
                }
                else
                {
                    ImmersiveMovement.Logger.LogWarning("GameplayControls instance is null.");
                }
            }
            else
            {
                ImmersiveMovement.Logger.LogWarning("Player instance is null.");
            }
        }
    }
}
