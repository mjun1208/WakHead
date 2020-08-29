using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_1_Base : Bolt.EntityEventListener<IPlayerState>
{
    public PlayerMovement player;
    public List<GameObject> TargetObject = new List<GameObject>();

    public virtual void Skill_1()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (entity.IsOwner)
        {
            if (collision.gameObject.tag == "Minion")
            {
                if (player.Mycreature.state.RedTeam != collision.GetComponent<Creature>().state.RedTeam)//상대팀인지 식별
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
                if (player.Mycreature.state.RedTeam != collision.GetComponent<Creature>().state.RedTeam)
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
