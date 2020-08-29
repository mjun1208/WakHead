using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : Bolt.EntityEventListener<IMinionState>
{
    public bool isAttack = false;
    public Creature Mycreature;
    public Rigidbody2D rigid;

    public GameObject EnemyTower;
    public bool CanAttack = false;

    bool IsDead = false;
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
        state.SetAnimator(Mycreature.animator);

        if (entity.IsOwner)
        {
            state.LocalScale = this.transform.localScale;
            state.RedTeam = Mycreature.RedTeam;
        }

        state.AddCallback("LocalScale", ScaleChange);
        state.AddCallback("RedTeam", RedTeamChange);

        state.Animator.applyRootMotion = entity.IsOwner;
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

        if (!IsDead)
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
            IsDead = true;
            ParticleAdmin.instance.SpawnParticle(this.gameObject.transform.position);
            StartCoroutine(DestroyObject());
            //BoltNetwork.Destroy(this.gameObject);
        }
    }

    IEnumerator DestroyObject()
    {
        for (int i = 0; i < this.transform.childCount; i++)
            this.transform.GetChild(i).gameObject.SetActive(false);

        this.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(BoltNetwork.FrameDeltaTime);
        BoltNetwork.Destroy(this.gameObject);
    }

    void Attack()
    {
        if (Mycreature.TargetObject != null)
        {
            if (CanAttack/*Vector3.Distance(Mycreature.TargetObject.transform.position, transform.position) <= 0.7f*/)
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
            isAttack = false;

            if (CanAttack/*Vector3.Distance(EnemyTower.transform.position, transform.position) <= 1.5f*/)
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

            if (!CanAttack)
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

            if (!CanAttack)
            {
                Vector3 Temp = new Vector3(EnemyTower.transform.position.x - transform.position.x, EnemyTower.transform.position.y - transform.position.y, 0);
                Temp = Vector3.Normalize(Temp);
                //rigid.MovePosition(this.transform.position + (Temp * Mycreature.MoveSpeed * BoltNetwork.FrameDeltaTime));
                transform.Translate(Temp * Mycreature.MoveSpeed * BoltNetwork.FrameDeltaTime);
            }
        }
        CanAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
