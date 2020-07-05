using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonInit : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1.0";
    public string userId = "Wak";
    public byte MaxPlayer = 2;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        OnConnectedToMaster();
    }

    private void Start()
    {
        PhotonNetwork.GameVersion = this.gameVersion;
        PhotonNetwork.NickName = userId;

        PhotonNetwork.SendRate = 140;
        PhotonNetwork.SerializationRate = 140;

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.MaxPlayer });
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity);
    }
}
