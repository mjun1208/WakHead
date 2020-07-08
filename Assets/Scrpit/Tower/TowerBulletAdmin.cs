using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class TowerBulletAdmin : MonoBehaviourPunCallbacks, IPunObservable
{
    public List<GameObject> Bullets = new List<GameObject>();
    public List<BulletMovement> Bullet_Script = new List<BulletMovement>();
    private int NowCount = 0;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                stream.SendNext(Bullets[i].activeSelf);
            }
        }
        else
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                Bullets[i].SetActive((bool)stream.ReceiveNext());
            }
        }
    }

    private void Awake()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Bullets.Add(this.transform.GetChild(i).gameObject);
            Bullet_Script.Add(this.transform.GetChild(i).GetComponent<BulletMovement>());
        }
    }

    public void SpawnBullet(GameObject Target, Vector3 Pos , bool IsRed)
    {
        Bullet_Script[NowCount].RedTeam = IsRed;
        Bullet_Script[NowCount].TargetObject = Target;
        Bullets[NowCount].transform.position = Pos;
        Bullets[NowCount].SetActive(true);

        if (++NowCount >= Bullets.Count)
            NowCount = 0;
    }
}
