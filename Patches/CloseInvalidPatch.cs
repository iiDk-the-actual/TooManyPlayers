using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace TooManyPlayers.Patches
{
    [HarmonyPatch(typeof(GorillaNot))]
    [HarmonyPatch("CloseInvalidRoom")]
    internal class CloseInvalidPatch
    {
        private static bool Prefix(GorillaNot __instance)
        {
            return false;
        }
    }
}
