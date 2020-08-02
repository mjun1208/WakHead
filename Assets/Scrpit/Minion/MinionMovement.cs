using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : Bolt.EntityEventListener<IMinionState>
{
    public bool isAttack = false;
    public Creature Mycreature;
    public Rigidbody2D rigid;

    public GameObject EnemyTower;
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
        //state.LocalScale = transform.localScale;
    }

    void RedTeamChange()
    {
        Mycreature.RedTeam = state.RedTeam;
        //state.RedTeam = Mycreature.RedTeam;
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
            Mycreature.sprite.color = new Color(1, 0, 0, 1);
        }
        else
        {
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
        if (Mycreature.TargetObject != null)
        {
            if (Vector3.Distance(Mycreature.TargetObject.transform.position, transform.position) <= 0.7f)
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
        else
        {
            if (Vector3.Distance(EnemyTower.transform.position, transform.position) <= 1.5f)
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
    }

    void Move()
    {
        if (Mycreature.TargetObject != null)
        {
            if (Mycreature.TargetObject.transform.position.x < this.transform.position.x)
                state.LocalScale = new Vector3(-1.2f, 1.2f, 1.2f);
            else
                state.LocalScale = new Vector3(1.2f, 1.2f, 1.2f);

            if (!isAttack)
            {
                Vector3 Temp = new Vector3(Mycreature.TargetObject.transform.position.x - transform.position.x, Mycreature.TargetObject.transform.position.y - transform.position.y, 0);
                Temp = Vector3.Normalize(Temp);
                //rigid.MovePosition(this.transform.position + (Temp * Mycreature.MoveSpeed * BoltNetwork.FrameDeltaTime));
                transform.Translate(Temp * Mycreature.MoveSpeed * BoltNetwork.FrameDeltaTime);
            }
        }
        else
        {
            if (EnemyTower.transform.position.x < this.transform.position.x)
                state.LocalScale = new Vector3(-1.2f, 1.2f, 1.2f);
            else
                state.LocalScale = new Vector3(1.2f, 1.2f, 1.2f);

            if (!isAttack)
            {
                Vector3 Temp = new Vector3(EnemyTower.transform.position.x - transform.position.x, EnemyTower.transform.position.y - transform.position.y, 0);
                Temp = Vector3.Normalize(Temp);
                //rigid.MovePosition(this.transform.position + (Temp * Mycreature.MoveSpeed * BoltNetwork.FrameDeltaTime));
                transform.Translate(Temp * Mycreature.MoveSpeed * BoltNetwork.FrameDeltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
