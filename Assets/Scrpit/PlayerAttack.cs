using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerMovement player;
    public List<GameObject> TargetObject = new List<GameObject>();


    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void Attack()
    {
        for (int i = 0; i < TargetObject.Count; i++)
        {
            TargetObject[i].transform.Translate((TargetObject[i].transform.position.x - player.transform.position.x) * 0.3f, 0, 0);
            TargetObject[i].GetComponent<Creature>().Life -= 0.3f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Minion")
        {
            if (player.RedTeam != collision.GetComponent<Creature>().RedTeam)//상대팀인지 식별
            {
                TargetObject.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Minion")
        {
            if (player.RedTeam != collision.GetComponent<Creature>().RedTeam)
            {
                TargetObject.Remove(collision.gameObject);
            }
        }
    }
}
