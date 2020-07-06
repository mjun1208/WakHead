using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using UnityEngine.SceneManagement;

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

        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 60;

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
        SceneManager.LoadScene("BangScene");
    }
}
