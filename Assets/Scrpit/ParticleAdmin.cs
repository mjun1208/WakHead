using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAdmin : MonoBehaviour
{
    public static ParticleAdmin instance;

    public List<GameObject> Particles = new List<GameObject>();
    public int NowCount = 0;

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        for (int i = 0; i < Particles.Count; i++)
    //        {
    //            stream.SendNext(Particles[i].activeSelf);
    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < Particles.Count; i++)
    //        {
    //            Particles[i].SetActive((bool)stream.ReceiveNext());
    //        }
    //    }
    //}

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Particles.Add(this.transform.GetChild(i).gameObject);
        }
    }

    public void SpawnParticle(Vector3 Pos)
    {
        //photonView.RPC("RPCSpawnParticle", RpcTarget.All, Pos);
    }

    public void RPCSpawnParticle(Vector3 Pos)
    {
        Particles[NowCount].transform.position = Pos;
        Particles[NowCount].gameObject.SetActive(true);

        if (++NowCount >= Particles.Count)
        {
            NowCount = 0;
        }
    }
}
