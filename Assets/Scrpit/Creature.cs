using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer sprite;
    public float Life = 4;
    public float MoveSpeed = 2;
    public GameObject TargetObject;
    public bool RedTeam = true;

    public bool CanMove = true;
    public bool IsDead = false;
    
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
            //photonView.RPC("DoGrab", RpcTarget.All, TargetPos);
            yield return null;
        }
        CanMove = true;
        yield return null;
    }

    public IEnumerator Grab(Vector3 Target)
    {
        while (Vector3.Distance(this.transform.position, Target) > 0.45f)
        {
            //photonView.RPC("DoGrab", RpcTarget.All, Target);
            //this.transform.position = Vector3.Lerp(this.transform.position, Target, 10.0f * Time.deltaTime);
            yield return null;
        }

        CanMove = true;
        yield return null;
    }

    public void DoGrab(Vector3 Target)
    {
        this.transform.position = Vector3.Lerp(this.transform.position, Target, 10.0f * Time.deltaTime);
    }

    public void OnDamage(float damage)
    {
        Life -= damage; 
        ///photonView.RPC("ApplyLife", RpcTarget.Others, Life);
    }

    public void ApplyLife(float life)
    {
        Life = life;
    }
}
