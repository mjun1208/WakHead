using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviourPunCallbacks
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
        transform.Translate(direction * 12 * Time.deltaTime,0,0);
        if (transform.position.x >= 50 || transform.position.x <= -50)
            this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (collision.gameObject.tag == "Minion")
            {
                if (player.RedTeam != collision.GetComponent<Creature>().RedTeam)//상대팀인지 식별
                {
                    Creature tempCreatureScript = collision.gameObject.GetComponent<Creature>();

                    tempCreatureScript.OnDamage(1.0f);

                    Vector2 Dir = collision.gameObject.transform.position - player.transform.position;
                    Dir.Normalize();
                    float power;
                    if (Dir.x > 0)
                        power = 2f;
                    else
                        power = -2f;
                    tempCreatureScript.KnockBack(power);


                    this.gameObject.SetActive(false);
                }
            }
            else if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject != player.gameObject)
                {
                    Creature tempCreatureScript = collision.gameObject.GetComponent<Creature>();

                    tempCreatureScript.OnDamage(1.0f);

                    Vector2 Dir = collision.gameObject.transform.position - player.transform.position;
                    Dir.Normalize();
                    float power;
                    if (Dir.x > 0)
                        power = 2f;
                    else
                        power = -2f;
                    tempCreatureScript.KnockBack(power);

                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
