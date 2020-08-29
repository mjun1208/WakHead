using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bolt;

public class PlayerAttack : Bolt.EntityEventListener<IPlayerState>
{
    public PlayerMovement player;
    public List<GameObject> TargetObject = new List<GameObject>();

    public override void Attached()
    {
        state.OnDoAttack = DoAttack;
    }

    public void Attack()
    {
        if (entity.IsOwner)
            state.DoAttack();
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

            //tempCreatureScript.transform.position += new Vector3(10, 0, 0);
            tempCreatureScript.KnockBack(1.0f, new Vector2(Dir.x, 0), false);
        }
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
