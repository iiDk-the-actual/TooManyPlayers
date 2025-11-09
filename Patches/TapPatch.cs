using GorillaTagScripts;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TooManyPlayers.Patches
{
    [HarmonyPatch(typeof(GorillaTagger))]
    [HarmonyPatch("ProcessHandTapping")]
    internal class TapPatch
    {
        private static bool Prefix(GorillaTagger __instance) =>
            false;
    }
}
