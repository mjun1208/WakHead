using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class MinionAdmin : MonoBehaviourPunCallbacks , IPunObservable
{
    public List<GameObject> Minions = new List<GameObject>();
    public List<MinionMovement> Minion_Script = new List<MinionMovement>();
    private int NowCount = 0;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            for (int i = 0; i < Minions.Count; i++)
            {
                stream.SendNext(Minions[i].activeSelf);
            }
        }
        else
        {
            for (int i = 0; i < Minions.Count; i++)
            {
                Minions[i].SetActive((bool)stream.ReceiveNext());
            }
        }
    }
    private void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Minions.Add(this.transform.GetChild(i).gameObject);
            Minion_Script.Add(this.transform.GetChild(i).GetComponent<MinionMovement>());
        }
    }

    public void SpawnMinion(GameObject Target, Vector3 Pos, bool IsRed)
    {
        Minion_Script[NowCount].TargetObject = Target;
        Minion_Script[NowCount].RedTeam = IsRed;
        Minions[NowCount].transform.position = Pos;
        Minions[NowCount].SetActive(true);

        if (++NowCount >= Minions.Count)
            NowCount = 0;
    }

    public void GetEnemy()
    {

    }
}
