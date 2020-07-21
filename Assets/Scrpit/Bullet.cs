using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bolt;
using System.Diagnostics.Tracing;

public class Bullet : GlobalEventListener
{
    public PlayerMovement player;   
    public int direction = 0;
    // Start is called before the first frame update
    private GameObject CollObject = null;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * 12 * BoltNetwork.FrameDeltaTime, 0, 0);

        if (transform.position.x >= 50 || transform.position.x <= -50)
        {
            var objectSetActive = ObjectSetActive.Create(Bolt.GlobalTargets.Everyone);
            objectSetActive.Send();
        }
    }

    void Coll()
    {
        Creature tempCreatureScript = CollObject.GetComponent<Creature>();

        tempCreatureScript.OnDamage(1.0f);

        Vector2 Dir = CollObject.transform.position - player.transform.position;
        Dir.Normalize();
        float power;
        if (Dir.x > 0)
            power = 2f;
        else
            power = -2f;
        tempCreatureScript.KnockBack(power, true);
        
        var objectSetActive = ObjectSetActive.Create(Bolt.GlobalTargets.Everyone);
        objectSetActive.Send();
    }

    public override void OnEvent(ObjectSetActive evnt)
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.entity.IsOwner)
        {
            if (collision.gameObject.tag == "Minion")
            {
                if (player.Mycreature.RedTeam != collision.GetComponent<Creature>().RedTeam)//상대팀인지 식별
                {
                    CollObject = collision.gameObject;
                    Coll();
                }
            }
            else if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject != player.gameObject)
                {
                    CollObject = collision.gameObject;
                    Coll();
                }
            }
        }
    }
}
