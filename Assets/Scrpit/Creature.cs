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

    public bool CanMove = true;
    public bool IsDead = false;

    protected void Awake()
    {
        sprite = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    
    public void KnockBack(float power)
    {
        CanMove = false;
        StartCoroutine(DoKnockBack(power));
        //photonView.RPC("DoKnockBack", RpcTarget.All, power);
        //this.transform.Translate(new Vector3(power, 0, 0));
    }

    public IEnumerator DoKnockBack(float power)
    {
        Vector3 TargetPos = this.transform.position + new Vector3(power, 0, 0);

        while (Vector3.Distance(this.transform.position, TargetPos) > 0.1f) {
            photonView.RPC("DoGrab", RpcTarget.All, TargetPos);
            yield return null;
        }
        CanMove = true;
        yield return null;
    }

    public IEnumerator Grab(Vector3 Target)
    {
        while (Vector3.Distance(this.transform.position, Target) > 0.45f)
        {
            photonView.RPC("DoGrab", RpcTarget.All, Target);
            //this.transform.position = Vector3.Lerp(this.transform.position, Target, 10.0f * Time.deltaTime);
            yield return null;
        }

        CanMove = true;
        yield return null;
    }

    [PunRPC]
    public void DoGrab(Vector3 Target)
    {
        this.transform.position = Vector3.Lerp(this.transform.position, Target, 10.0f * Time.deltaTime);
    }

    public void OnDamage(float damage)
    {
        Life -= damage; 
        photonView.RPC("ApplyLife", RpcTarget.Others, Life);
    }

    [PunRPC]
    public void ApplyLife(float life)
    {
        Life = life;
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
