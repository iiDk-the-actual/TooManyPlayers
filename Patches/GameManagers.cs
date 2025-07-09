using GorillaTagScripts;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TooManyPlayers.Patches
{
    [HarmonyPatch(typeof(GorillaTagManager))]
    [HarmonyPatch("Awake")]
    internal class TagAwake
    {
        private static void Postfix(GorillaTagManager __instance)
        {
            __instance.currentInfected = new List<NetPlayer>(255);
            __instance.currentInfectedArray = new int[255];

            for (int i = 0; i < __instance.currentInfectedArray.Length; i++)
                __instance.currentInfectedArray[i] = -1;
        }
    }

    [HarmonyPatch(typeof(GorillaFreezeTagManager))]
    [HarmonyPatch("Awake")]
    internal class FreezeAwake
    {
        private static void Postfix(GorillaFreezeTagManager __instance)
        {
            __instance.currentRoundInfectedPlayers = new List<NetPlayer>(255);
            __instance.lastRoundInfectedPlayers = new List<NetPlayer>(255);
            __instance.currentFrozen = new Dictionary<NetPlayer, float>(255);
        }
    }

    [HarmonyPatch(typeof(GorillaPropHuntGameManager))]
    [HarmonyPatch("Awake")]
    internal class PropHuntAwake
    {
        private static void Postfix(GorillaFreezeTagManager __instance)
        {
            typeof(GorillaFreezeTagManager).GetField("_g_ph_activePlayerRigs", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).SetValue(null, new List<VRRig>(255));
        }
    }

    [HarmonyPatch(typeof(GorillaTagManager), "OnSerializeWrite", new Type[] { typeof(PhotonStream), typeof(PhotonMessageInfo) })]
    internal class OnSerializeWrite
    {
        private static bool Prefix(GorillaTagManager __instance, PhotonStream stream, PhotonMessageInfo info)
        {
            __instance.CopyInfectedListToArray();
            stream.SendNext(__instance.isCurrentlyTag);
            stream.SendNext((__instance.currentIt != null) ? __instance.currentIt.ActorNumber : (-1));
            stream.SendNext(__instance.currentInfectedArray);
            __instance.WriteLastTagged(stream);
            return false;
        }
    }

    [HarmonyPatch(typeof(GorillaTagManager), "OnSerializeRead", new Type[] { typeof(PhotonStream), typeof(PhotonMessageInfo) })]
    internal class OnSerializeRead
    {
        private static bool Prefix(GorillaTagManager __instance, PhotonStream stream, PhotonMessageInfo info)
        {
            __instance.isCurrentlyTag = (bool)stream.ReceiveNext();
            __instance.tempItInt = (int)stream.ReceiveNext();
            __instance.currentIt = (__instance.tempItInt != -1) ? NetworkSystem.Instance.GetPlayer(__instance.tempItInt) : null;
            __instance.currentInfectedArray = (int[])stream.ReceiveNext();
            __instance.ReadLastTagged(stream);
            __instance.CopyInfectedArrayToList();
            return false;
        }
    }
}
