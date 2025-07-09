using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace TooManyPlayers.Patches
{
    [HarmonyPatch(typeof(NetworkSystemPUN))]
    [HarmonyPatch("Initialise")]
    internal class InitialisePatch
    {
        private static void Prefix(NetworkSystemPUN __instance)
        {
            __instance.regionNames = new string[] { "USW" };
        }
    }
}
