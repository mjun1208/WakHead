using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class Creature : MonoBehaviourPunCallbacks
{
    protected Animator animator;
    protected SpriteRenderer sprite;
    public float Life = 4;
    public float MoveSpeed = 2;
    public GameObject TargetObject;
    public bool RedTeam = true;

    protected void Awake()
    {
        sprite = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    
    public void KnockBack(float power)
    {
        this.transform.Translate(new Vector3(power, 0, 0));
    }

    protected void Update()
    {
        //if (transform.position.y >= -0.1f)
        //    sprite.sortingOrder = 3;
        //else if (transform.position.y >= -0.2f)
        //    sprite.sortingOrder = 4;
        //else if (transform.position.y >= -0.3f)
        //    sprite.sortingOrder = 5;
        //else if (transform.position.y >= -0.4f)
        //    sprite.sortingOrder = 6;
        //else if (transform.position.y >= -0.5f)
        //    sprite.sortingOrder = 7;
        //else if (transform.position.y >= -0.6f)
        //    sprite.sortingOrder = 8;
        //else if (transform.position.y >= -0.7f)
        //    sprite.sortingOrder = 9;
        //else if (transform.position.y >= -0.8f)
        //    sprite.sortingOrder = 10;
        //else if (transform.position.y >= -0.9f)
        //    sprite.sortingOrder = 11;
        //else if (transform.position.y >= -1f)
        //    sprite.sortingOrder = 12;
    }
}
