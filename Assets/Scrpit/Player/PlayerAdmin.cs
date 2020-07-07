using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Rendering;

public class PlayerAdmin : MonoBehaviourPunCallbacks , IPunObservable
{
    public List<GameObject> Players = new List<GameObject>();
    public List<PlayerMovement> Player_Script = new List<PlayerMovement>();

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
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Players.Add(this.transform.GetChild(i).gameObject);
            Player_Script.Add(this.transform.GetChild(i).GetComponent<PlayerMovement>());
        }

        photonView.RPC("SpawnPlayer", RpcTarget.All, null);
        //PhotonNetwork.Instantiate("IsMine", Vector3.zero, Quaternion.identity);
    }

    [PunRPC]
    public void SpawnPlayer()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Players[0].SetActive(true);
            Player_Script[0].IsLocalPlayer = true;
        }
        else
        {
            Players[1].SetActive(true);
            Player_Script[1].IsLocalPlayer = true;
        }
    }
}
