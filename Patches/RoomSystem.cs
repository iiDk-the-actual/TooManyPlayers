using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace TooManyPlayers.Patches
{
    [HarmonyPatch(typeof(RoomSystem))]
    [HarmonyPatch("GetRoomSize")]
    internal class GetRoomSize
    {
        private static bool Prefix(ref byte __result)
        {
            __result = 255;
            return false;
        }
    }

    [HarmonyPatch(typeof(RoomSystem))]
    [HarmonyPatch("GetRoomSizeForCreate")]
    internal class GetRoomSizeForCreate
    {
        private static bool Prefix(ref byte __result, string gameMode = "")
        {
            __result = 255;
            return false;
        }
    }

    [HarmonyPatch(typeof(RoomConfig), "AnyPublicConfig")]
    internal class AnyPublicConfig
    {
        private static bool Prefix(ref RoomConfig __result)
        {
            __result = new RoomConfig
            {
                isPublic = true,
                isJoinable = true,
                createIfMissing = true,
                MaxPlayers = 255
            };
            return false;
        }
    }
}
