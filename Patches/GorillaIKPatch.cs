using HarmonyLib;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace TooManyPlayers.Patches
{
    [HarmonyPatch(typeof(GorillaIKMgr))]
    [HarmonyPatch("Awake")]
    internal class GorillaIKPatch
    {
        private static bool Prefix(GorillaIKMgr __instance)
        {
            GorillaIKMgr._instance = __instance;

            __instance.ikList = new List<GorillaIK>(256);
            __instance.firstFrame = true;
            __instance.tAA = new TransformAccessArray(512, 512);
            __instance.transformList = new List<Transform>();
            __instance.job = new GorillaIKMgr.IKJob
            {
                constantInput = new NativeArray<GorillaIKMgr.IKConstantInput>(512, Allocator.Persistent, NativeArrayOptions.ClearMemory),
                input = new NativeArray<GorillaIKMgr.IKInput>(512, Allocator.Persistent, NativeArrayOptions.ClearMemory),
                output = new NativeArray<GorillaIKMgr.IKOutput>(512, Allocator.Persistent, NativeArrayOptions.ClearMemory)
            };
            __instance.jobXform = new GorillaIKMgr.IKTransformJob
            {
                transformRotations = new NativeArray<Quaternion>(512, Allocator.Persistent, NativeArrayOptions.ClearMemory)
            };

            return false;
        }
    }

    [HarmonyPatch(typeof(GorillaIKMgr))]
    [HarmonyPatch("LateUpdate")]
    internal class GorillaIKPatch2
    {
        private static bool Prefix(GorillaIKMgr __instance)
        {
            if (!__instance.firstFrame)
            {
                __instance.jobXformHandle.Complete();
            }
            __instance.CopyInput();
            __instance.jobHandle = __instance.job.Schedule(__instance.actualListSz, 256, default(JobHandle));
            __instance.jobHandle.Complete();
            __instance.CopyOutput();
            __instance.jobXformHandle = __instance.jobXform.Schedule(__instance.tAA, default(JobHandle));
            __instance.firstFrame = false;
            return false;
        }
    }
}
