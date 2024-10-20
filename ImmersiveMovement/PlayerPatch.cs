using HarmonyLib;
using UnityEngine;
using System.Reflection;
using UnityStandardAssets.Characters.FirstPerson;

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
                    controls.playerWalkSpeed = ImmersiveMovement.walkSpeedMultiplier.Value;
                    controls.playerRunSpeed = ImmersiveMovement.runSpeedMultiplier.Value;

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

        [HarmonyPatch(typeof(FirstPersonController), "Update")]
        public class LeanSpeedPatch
        {
            private static bool logSent = false;
            static void Postfix(FirstPersonController __instance)
            {
                if (!logSent) 
                {
                    ImmersiveMovement.Logger.LogInfo("Player lean speed modified.");
                    logSent = true;
                }
                __instance.leanSpeed = ImmersiveMovement.leanSpeedMultiplier.Value;               
            }

            [HarmonyPatch(typeof(FirstPersonController), "JoltCamera")]
            static void Prefix(ref float amplitude, Vector3 direction, float speed)
            {
                if (ImmersiveMovement.cameraJolt.Value == true)
                {
                    amplitude = Mathf.Min(amplitude, 30f);
                }
            }
        }
    }
}
