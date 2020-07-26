using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAdmin : MonoBehaviour
{
    static public BulletAdmin instance;
    public List<GameObject> Bullets = new List<GameObject>();
    public List<Bullet> Bullet_Script = new List<Bullet>();

    public GameObject BulletGrapic;

    int NowCount = 0;

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        for (int i = 0; i < Bullets.Count; i++)
    //        {
    //            stream.SendNext(Bullets[i].activeSelf);
    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < Bullets.Count; i++)
    //        {
    //            Bullets[i].SetActive((bool)stream.ReceiveNext());
    //        }
    //    }
    //}

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Bullets.Add(this.transform.GetChild(i).gameObject);
            Bullet_Script.Add(this.transform.GetChild(i).GetComponent<Bullet>());
        }
    }

    public void SpawnBullet(Vector3 Pos, int Dir, PlayerMovement player)
    {
        if (BoltNetwork.IsServer)
        {
            GameObject temp = BoltNetwork.Instantiate(BoltPrefabs.Bullet, Pos, Quaternion.identity);
            temp.GetComponent<Bullet>().direction = Dir;
            temp.GetComponent<Bullet>().player = player;
        }
        else
        {
            GameObject temp = Instantiate(BulletGrapic, Pos, Quaternion.identity);
            temp.GetComponent<BulletGrapic>().direction = Dir;
            temp.GetComponent<BulletGrapic>().player = player;
        }
        //Bullets[NowCount].transform.position = Pos;
        //Bullets[NowCount].SetActive(true);
        //Bullet_Script[NowCount].direction = Dir;
        //Bullet_Script[NowCount].player = player;
        //
        //if (++NowCount >= Bullets.Count)
        //    NowCount = 0;
    }
}
