using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bolt;
public class Bullet : Bolt.EntityEventListener<IBulletState>
{
    public PlayerMovement player;   
    public int direction = 0;
    // Start is called before the first frame update
    private GameObject CollObject = null;

    public override void Attached()
    {
        state.SetTransforms(state.BulletTransform, transform);
        state.OnColl = Coll;

        //state.CollObject = CollObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * 12 * BoltNetwork.FrameDeltaTime, 0,0);
        if (transform.position.x >= 50 || transform.position.x <= -50)
            this.gameObject.SetActive(false);
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
        tempCreatureScript.KnockBack(power);


        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (BoltNetwork.IsServer)
        {
            if (collision.gameObject.tag == "Minion")
            {
                if (player.Mycreature.RedTeam != collision.GetComponent<Creature>().RedTeam)//상대팀인지 식별
                {
                    CollObject = collision.gameObject;
                    state.Coll();
                }
            }
            else if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject != player.gameObject)
                {
                    CollObject = collision.gameObject;
                    state.Coll();
                }
            }
        }
    }
}
