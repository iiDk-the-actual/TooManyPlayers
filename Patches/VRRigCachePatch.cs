using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Collections;
using UnityEngine.Jobs;

namespace TooManyPlayers.Patches
{
    [HarmonyPatch(typeof(VRRigCache))]
    [HarmonyPatch("Awake")]
    internal class VRRigCachePatch
    {
        private static void Prefix(VRRigCache __instance)
        {
            __instance.rigAmount = 254;
            VRRigCache.freeRigs = new Queue<RigContainer>(255);
            VRRigCache.rigsInUse = new Dictionary<NetPlayer, RigContainer>(255);
        }
    }

    [HarmonyPatch(typeof(VRRigJobManager))]
    [HarmonyPatch("Awake")]
    internal class VRRigJobPatch
    {
        private static void Postfix(VRRigJobManager __instance)
        {
            __instance.cachedInput = new NativeArray<VRRigJobManager.VRRigTransformInput>(254, Allocator.Persistent, NativeArrayOptions.ClearMemory);
            __instance.tAA = new TransformAccessArray(254, 255);
            __instance.rigList = new List<VRRig>(254);
        }
    }
}
