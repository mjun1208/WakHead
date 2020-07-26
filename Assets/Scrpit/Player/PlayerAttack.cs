using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bolt;

public class PlayerAttack : Bolt.EntityEventListener<IPlayerState>
{
    public PlayerMovement player;
    public GameObject Bullet;
    public List<GameObject> TargetObject = new List<GameObject>();

    public override void Attached()
    {
        state.OnDoAttack = DoAttack;
        state.OnDoSkill_2 = DoSkill_2;
    }

    public void Attack()
    {
        if (entity.IsOwner)
            state.DoAttack();
    }
    
    public void Attack2()
    {
        if (BoltNetwork.IsServer)
        {
            var shoot = ShootEvent.Create(entity);
            shoot.Send();
        }
        //DoSkill_2();
    }

    public override void OnEvent(ShootEvent evnt)
    {
        DoSkill_2();
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
                power = 1.0f;
            else
                power = -1.0f;

            //tempCreatureScript.transform.position += new Vector3(10, 0, 0);
            tempCreatureScript.KnockBack(power, false);
        }
    }

    public void DoSkill_2()
    {
        BulletAdmin.instance.SpawnBullet(player.transform.position, (int)player.transform.localScale.x, player);
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
