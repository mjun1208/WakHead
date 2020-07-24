using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGrapic : MonoBehaviour
{
    public PlayerMovement player;
    public int direction = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * 12 * BoltNetwork.FrameDeltaTime, 0, 0);

        if (transform.position.x >= 50 || transform.position.x <= -50)
            this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (BoltNetwork.IsServer)
        //{
            if (collision.gameObject.tag == "Minion")
            {
                if (player.Mycreature.RedTeam != collision.GetComponent<Creature>().RedTeam)//상대팀인지 식별
            {
                this.gameObject.SetActive(false);
                //CollObject = collision.gameObject;
                //state.Coll();
            }
            }
            else if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject != player.gameObject)
                {

                this.gameObject.SetActive(false);
                //CollObject = collision.gameObject;
                //state.Coll();
            }
            }
        //}
    }
}
