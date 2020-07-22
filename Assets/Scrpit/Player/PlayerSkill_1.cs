using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_1 : Bolt.EntityBehaviour<IPlayerState>
{
    public PlayerMovement player;
    public List<GameObject> TargetObject = new List<GameObject>();

    public override void Attached()
    {
        state.OnDoSkill_1  = DoSkill_1;
    }

    public void Skill_1()
    {
        if (entity.IsOwner)
            state.DoSkill_1();
    }

    public void DoSkill_1()
    {
        for (int i = 0; i < TargetObject.Count; i++)
        {
            Creature tempCreatureScript = TargetObject[i].GetComponent<Creature>();

            tempCreatureScript.OnDamage(1.0f);
            tempCreatureScript.CanMove = false;
            tempCreatureScript.Grab(player.transform.position, true);
            //if (TargetObject[i].tag != "Player")
            //tempCreatureScript.KnockBack(TargetObject[i].transform.position.x - player.transform.position.x);
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
