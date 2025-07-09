using GorillaGameModes;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace TooManyPlayers.Patches
{
    [HarmonyPatch(typeof(GameMode))]
    [HarmonyPatch("Awake")]
    internal class AwakeGameMode
    {
        private static void Prefix(GameMode __instance)
        {
            typeof(GameMode).GetField("optOutPlayers", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).SetValue(null, new HashSet<int>(255));
            typeof(GameMode).GetField("_participatingPlayers", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).SetValue(null, new List<NetPlayer>(255));
            typeof(GameMode).GetField("_oldPlayersBuffer", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).SetValue(null, new NetPlayer[255]);
            typeof(GameMode).GetField("_tempAddedPlayers", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).SetValue(null, new List<NetPlayer>(255));
            typeof(GameMode).GetField("_tempRemovedPlayers", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).SetValue(null, new List<NetPlayer>(255));
        }
    }

    [HarmonyPatch(typeof(GameMode))]
    [HarmonyPatch("RefreshPlayers")]
    internal class RefreshPlayersGameMode
    {
        private static bool Prefix(GameMode __instance)
        {
            GameMode._oldPlayersCount = GameMode._participatingPlayers.Count;

            for (int i = 0; i < GameMode._oldPlayersCount; i++)
                GameMode._oldPlayersBuffer[i] = GameMode._participatingPlayers[i];

            GameMode._participatingPlayers.Clear();

            for (int j = 0; j < RoomSystem.PlayersInRoom.Count; j++)
            {
                if (GameMode.CanParticipate(RoomSystem.PlayersInRoom[j]))
                    GameMode.ParticipatingPlayers.Add(RoomSystem.PlayersInRoom[j]);
            }

            GameMode._tempRemovedPlayers.Clear();

            for (int k = 0; k < GameMode._oldPlayersCount; k++)
            {
                NetPlayer p = GameMode._oldPlayersBuffer[k];

                if (!GameMode.ContainsNetPlayer(GameMode._participatingPlayers, p))
                    GameMode._tempRemovedPlayers.Add(p);
            }

            GameMode._tempAddedPlayers.Clear();

            for (int l = 0; l < GameMode._participatingPlayers.Count; l++)
            {
                NetPlayer pp = GameMode._participatingPlayers[l];
                if (!GameMode.ContainsNetPlayer(GameMode._oldPlayersBuffer, pp, GameMode._oldPlayersCount))
                    GameMode._tempAddedPlayers.Add(pp);
            }

            if (GameMode._tempAddedPlayers.Count > 0 || GameMode._tempRemovedPlayers.Count > 0)
            {
                Action action = typeof(GameMode).GetField("ParticipatingPlayersChanged", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null) as Action;
                action?.Invoke();
            }

            return false;
        }
    }
}
