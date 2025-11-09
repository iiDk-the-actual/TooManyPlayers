using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Collections;
using UnityEngine.Jobs;

namespace TooManyPlayers.Patches
{
    [HarmonyPatch(typeof(VRRig))]
    [HarmonyPatch("RequestCosmetics")]
    internal class RequestCosmetics
    {
        private static bool Prefix(PhotonMessageInfoWrapped info) =>
            false;
    }
}
