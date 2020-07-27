using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGrapic : MonoBehaviour
{
    public PlayerMovement player;
    public int direction = 0;

    private void Update()
    {
        transform.Translate(direction * 12 * BoltNetwork.FrameDeltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Minion")
        {
            //Debug.Log("aaa p : " + player.Mycreature.RedTeam + "/ c : " + collision.GetComponent<Creature>().RedTeam);
            if (player.state.RedTeam != collision.GetComponent<Creature>().state.RedTeam)//상대팀인지 식별
            {
                Destroy(this.gameObject);
                //this.gameObject.SetActive(false);
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject != player.gameObject)
            {
                Destroy(this.gameObject);
                //this.gameObject.SetActive(false);
            }
        }
    }
}
