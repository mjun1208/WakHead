﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//시간없으니까 공격을 일케하는데 나중에 바꿨으면 함
public class MinionAttack : Bolt.EntityEventListener<IMinionState>
{
    public MinionMovement minionMovement;
    void Start()
    {
        //minion = gameObject.transform.parent.GetComponent<MinionMovement>();
    }

    void Update()
    {
        
    }

    void Attack()
    {
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    minionMovement.TargetObject.GetComponent<TowerSystem>().OnDamage(1f);
        //    minionMovement.TargetObject.GetComponent<TowerSystem>().Hit();
        //}
    }
}
