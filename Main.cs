using BepInEx;
using GorillaNetworking;
using System;
using Photon.Pun;
using UnityEngine;
using System.Collections;
using Classes;

namespace TooManyPlayers
{
    public class Main : MonoBehaviour
    {
        private float isOpenDelay;
        public void Start()
        {
            NetworkSystem.Instance.OnJoinedRoomEvent += OnJoinRoom;
        }

        public void OnJoinRoom() =>
            CoroutineManager.instance.StartCoroutine(OnJoinRoomCoroutine());

        public IEnumerator OnJoinRoomCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            if (!NetworkSystem.Instance.GameModeString.Contains($"<TOOMANYPLAYERS{PluginInfo.Version}>"))
                NetworkSystem.Instance.ReturnToSinglePlayer();
        }

        public void Update()
        {
            if (PhotonNetwork.IsConnectedAndReady)
            {
                if (GorillaComputer.instance.troopName != $"<TOOMANYPLAYERS{PluginInfo.Version}>")
                {
                    GorillaComputer.instance.currentTroopPopulation = -1;
                    GorillaComputer.instance.troopName = $"<TOOMANYPLAYERS{PluginInfo.Version}>";

                    GorillaComputer.instance.currentQueue = GorillaComputer.instance.GetQueueNameForTroop($"<TOOMANYPLAYERS{PluginInfo.Version}>");

                    GorillaComputer.instance.JoinTroopQueue();
                }
            }

            if (PhotonNetwork.InRoom)
            {
                if (!PhotonNetwork.CurrentRoom.IsOpen && PhotonNetwork.IsMasterClient && Time.time > isOpenDelay)
                {
                    isOpenDelay = Time.time + 5f;
                    PhotonNetwork.CurrentRoom.IsOpen = true;
                }

                if (PhotonNetwork.CurrentRoom.MaxPlayers != 255)
                    PhotonNetwork.CurrentRoom.MaxPlayers = 255;

                if (PhotonNetwork.CurrentRoom.PlayerTtl != 0)
                    PhotonNetwork.CurrentRoom.PlayerTtl = 0;
            }
        }
    }
}
