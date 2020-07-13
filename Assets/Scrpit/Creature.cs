using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bolt;
using UdpKit.Platform;
using UnityEngine.Rendering;

public class Creature : Bolt.EntityBehaviour<IPlayerState>
{
    public Animator animator;
    public SpriteRenderer sprite;
    public float Life = 4;
    public float MoveSpeed = 2;
    public GameObject TargetObject;
    public bool RedTeam = true;

    public bool CanMove = true;
    public bool IsDead = false;

    Vector3 TargetPos;
    public override void Attached()
    {
        state.OnDoGrab = DoGrab;
    }

    public void KnockBack(float power)
    {
        CanMove = false;
        StartCoroutine(DoKnockBack(power));
    }

    public IEnumerator DoKnockBack(float power)
    {
        Vector3 Target = this.transform.position + new Vector3(power, 0, 0);
        TargetPos = Target;
        while (Vector3.Distance(this.transform.position, TargetPos) > 0.1f) 
        {
            state.DoGrab();
            //photonView.RPC("DoGrab", RpcTarget.All, TargetPos);
            yield return null;
        }
        CanMove = true;
        yield return null;
    }

    public IEnumerator Grab(Vector3 Target)
    {
        TargetPos = Target;
        while (Vector3.Distance(this.transform.position, TargetPos) > 0.45f)
        {
            state.DoGrab();
            //photonView.RPC("DoGrab", RpcTarget.All, Target);
            yield return null;
        }

        CanMove = true;
        yield return null;
    }

    public void DoGrab()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, TargetPos, 10.0f * Time.deltaTime);
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
