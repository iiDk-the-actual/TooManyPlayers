using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Collections;
using UnityEngine.Jobs;

namespace TooManyPlayers.Patches
{
    [HarmonyPatch(typeof(PlayerCosmeticsSystem))]
    [HarmonyPatch("Awake")]
    internal class CosmeticsSystem
    {
        private static void Prefix(PlayerCosmeticsSystem __instance)
        {
            PlayerCosmeticsSystem.playersToLookUp = new Queue<NetPlayer>(255);
            PlayerCosmeticsSystem.userCosmeticCallback = new Dictionary<int, IUserCosmeticsCallback>(255);
            PlayerCosmeticsSystem.userCosmeticsWaiting = new Dictionary<int, string>(127);

            PlayerCosmeticsSystem.playerIDsList = new List<string>(255);

            PlayerCosmeticsSystem.playerActorNumberList = new List<int>(255);
        }
    }
}
