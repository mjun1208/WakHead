﻿using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class TowerBulletAdmin : MonoBehaviour
{
    public List<GameObject> Bullets = new List<GameObject>();
    public List<TowerBulletMovement> Bullet_Script = new List<TowerBulletMovement>();
    private int NowCount = 0;
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
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Bullets.Add(this.transform.GetChild(i).gameObject);
            Bullet_Script.Add(this.transform.GetChild(i).GetComponent<TowerBulletMovement>());
        }
    }

    public void SpawnBullet(GameObject Target, Vector3 Pos , bool IsRed)
    {
        //Bullet_Script[NowCount].RedTeam = IsRed;
        //Bullet_Script[NowCount].TargetObject = Target;
        //Bullets[NowCount].transform.position = Pos;
        //Bullets[NowCount].SetActive(true);
        //
        //if (++NowCount >= Bullets.Count)
        //    NowCount = 0;

        if (!BoltNetwork.IsServer)
            return;
        GameObject temp = BoltNetwork.Instantiate(BoltPrefabs.TowerBullet, Pos, Quaternion.identity);
        temp.GetComponent<TowerBulletMovement>().state.RedTeam = IsRed;
        temp.GetComponent<TowerBulletMovement>().TargetObject = Target;
    }
}
