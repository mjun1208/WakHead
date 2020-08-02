using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bolt;
using UdpKit.Platform;
using UnityEngine.Rendering;

public class Creature : Bolt.EntityEventListener<ICreatureState>
{
    public Animator animator;
    public SpriteRenderer sprite;
    public float Life = 4;
    public float MoveSpeed = 2;
    public GameObject TargetObject;
    public bool RedTeam = true;

    public bool CanMove = true;
    public bool IsDead = false;

    public bool Stun = false;

    Vector3 TargetPos;

    IEnumerator MoveCoroutine;


    public override void Attached()
    {
    }

    public void KnockBack(float power, bool stun)
    {
        Vector3 Target = this.transform.position + new Vector3(power, 0, 0);
        TargetPos = Target;

        var forcedmovement = ForcedMovementEvent.Create(entity);
        forcedmovement.Target = TargetPos;
        forcedmovement.Stun = stun;
        forcedmovement.Send();
    }

    public void Grab(Vector3 target, bool stun)
    {
        TargetPos = target;

        var forcedmovement = ForcedMovementEvent.Create(entity);
        forcedmovement.Target = TargetPos;
        forcedmovement.Stun = stun;
        forcedmovement.Send();
    }

    public override void OnEvent(ForcedMovementEvent evnt)
    {
        TargetPos = evnt.Target;

        if (entity.IsOwner)
        {
            Stun = evnt.Stun;
            CanMove = false;
            if (MoveCoroutine != null)
                StopCoroutine(MoveCoroutine);
            MoveCoroutine = null;

            MoveCoroutine = ForcedMovement(TargetPos);
            StartCoroutine(MoveCoroutine);
        }
    }

    public IEnumerator ForcedMovement(Vector3 Target)
    {
        TargetPos = Target;
        while (Vector3.Distance(this.transform.position, TargetPos) > 0.5f)
        {
            DoForcedMovement();
            yield return null;
        }

        Stun = false;
        CanMove = true;
        yield return null;
    }

    public void DoForcedMovement()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, TargetPos, 6.0f * BoltNetwork.FrameDeltaTime);
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
