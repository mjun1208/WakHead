using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : Bolt.EntityEventListener<IMinionState>
{
    public bool isAttack = false;
    public Creature Mycreature;
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(this.transform.position);
    //        stream.SendNext(this.transform.localScale);
    //
    //        stream.SendNext(RedTeam);
    //        stream.SendNext(this.Life);
    //        stream.SendNext(this.CanMove);
    //    }
    //    else
    //    {
    //        this.transform.position = (Vector3)stream.ReceiveNext();
    //        this.transform.localScale = (Vector3)stream.ReceiveNext();
    //
    //        RedTeam = (bool)stream.ReceiveNext();
    //        this.Life = (float)stream.ReceiveNext();
    //        this.CanMove = (bool)stream.ReceiveNext();
    //    }
    //}

    public override void Attached()
    {
        state.SetTransforms(state.CreatureTransform, transform);

        if (entity.IsOwner)
        {
            state.LocalScale = this.transform.localScale;
            state.RedTeam = Mycreature.RedTeam;
        }

        state.AddCallback("LocalScale", ScaleChange);
        state.AddCallback("RedTeam", RedTeamChange);
    }

    void ScaleChange()
    {
        transform.localScale = state.LocalScale;
    }

    void RedTeamChange()
    {
        Mycreature.RedTeam = state.RedTeam;
    }

    void Update()
    {
         ChangeTeam();
         Dead();

         if (!BoltNetwork.IsServer)
            return;

         if (Mycreature.CanMove && !Mycreature.Stun)
         {
             Attack();
             Move();
         }
         else if (Mycreature.Stun)
         {
             isAttack = false;
             Mycreature.animator.SetBool("Attack", false);
         }
    }

    void ChangeTeam()
    {
        if (state.RedTeam)
        {
            this.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            Mycreature.sprite.color = new Color(1, 0, 0, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(-1.2f, 1.2f, 1.2f);
            Mycreature.sprite.color = new Color(0, 0, 1, 1);
        }
    }

    void Dead()
    {
        if (Mycreature.Life <= 0)
        {
            ParticleAdmin.instance.SpawnParticle(this.gameObject.transform.position);
            BoltNetwork.Destroy(this.gameObject);
            //this.gameObject.SetActive(false);//원래는 죽는 아니메로 이동
        }
    }
    void Attack()
    {
        if (Vector3.Distance(Mycreature.TargetObject.transform.position, transform.position) <= 1.5f)
        {
            isAttack = true;
            Mycreature.animator.SetBool("Attack", true);
        }
        else
        {
            isAttack = false;
            Mycreature.animator.SetBool("Attack", false);
        }
    }

    void Move()
    {
        if (!isAttack)
        {
            Vector3 Temp = new Vector3(Mycreature.TargetObject.transform.position.x - transform.position.x, Mycreature.TargetObject.transform.position.y - transform.position.y, 0);
            Temp = Vector3.Normalize(Temp);
            transform.Translate(Temp * Mycreature.MoveSpeed * BoltNetwork.FrameDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Bullet")
        //{
        //    Life -= 1f;
        //    Destroy(collision.gameObject);
        //}
    }
}
