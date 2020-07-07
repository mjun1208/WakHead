using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Rendering;

public class PlayerAdmin : MonoBehaviourPunCallbacks , IPunObservable
{
    public static PlayerAdmin instance;

    public List<GameObject> Players = new List<GameObject>();
    public List<PlayerMovement> Player_Script = new List<PlayerMovement>();
    private int NowCount = 0;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            for (int i = 0; i < Players.Count; i++)
            {
                stream.SendNext(Players[i].activeSelf);
            }
        }
        else
        {
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].SetActive((bool)stream.ReceiveNext());
            }
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            Players.Add(this.transform.GetChild(i).gameObject);
            Player_Script.Add(this.transform.GetChild(i).GetComponent<PlayerMovement>());
        }
        //SpawnPlayer();
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }
}
