﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Bolt.EntityBehaviour<IPlayerState>
{
    public PlayerMovement player;
    public GameObject Bullet;
    public List<GameObject> TargetObject = new List<GameObject>();

    public override void Attached()
    {
        state.OnDoAttack = DoAttack;
        state.OnDoAttack2 = DoAttack2;
    }

    public void Attack()
    {
        //if (entity.IsOwner)
        //    state.DoAttack();
        //if (PhotonNetwork.IsMasterClient)
        //    photonView.RPC("DoAttack", RpcTarget.AllViaServer, null);
    }
    
    public void Attack2()
    {
        if (entity.IsOwner)
            state.DoAttack2();
        //if (PhotonNetwork.IsMasterClient)
        //    photonView.RPC("DoAttack2", RpcTarget.AllViaServer, null);
    }

    public void DoAttack()
    {
        for (int i = 0; i < TargetObject.Count; i++)
        {
            Creature tempCreatureScript = TargetObject[i].GetComponent<Creature>();

            tempCreatureScript.OnDamage(0.3f);
            //if (TargetObject[i].tag != "Player")
            Vector2 Dir = TargetObject[i].transform.position - player.transform.position;
            Dir.Normalize();
            float power;
            if (Dir.x > 0)
                power = 0.5f;
            else
                power = -0.5f;
            tempCreatureScript.KnockBack(power);
        }
    }

    public void DoAttack2()
    {
        BulletAdmin.instance.SpawnBullet(player.transform.position, (int)player.transform.localScale.x, player);
        //GameObject bul = Instantiate(Bullet, player.transform.position, Quaternion.identity);
        //bul.GetComponent<Bullet>().direction = (int)player.transform.localScale.x;
        //bul.GetComponent<Bullet>().player = player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (entity.IsOwner)
        {
            if (collision.gameObject.tag == "Minion")
            {
                if (player.Mycreature.RedTeam != collision.GetComponent<Creature>().RedTeam)//상대팀인지 식별
                {
                    TargetObject.Add(collision.gameObject);
                }
            }
            else if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject != player.gameObject)
                    TargetObject.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (entity.IsOwner)
        {
            if (collision.gameObject.tag == "Minion")
            {
                if (player.Mycreature.RedTeam != collision.GetComponent<Creature>().RedTeam)
                {
                    TargetObject.Remove(collision.gameObject);
                }
            }
            else if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject != player.gameObject)
                    TargetObject.Remove(collision.gameObject);
            }
        }
    }
}
