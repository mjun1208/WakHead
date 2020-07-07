using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class MinionMovement : Creature, IPunObservable
{
    public bool isAttack = false;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.transform.position);
            stream.SendNext(this.transform.localScale);

            stream.SendNext(RedTeam);
            stream.SendNext(this.Life);
            stream.SendNext(this.CanMove);
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.localScale = (Vector3)stream.ReceiveNext();

            RedTeam = (bool)stream.ReceiveNext();
            this.Life = (float)stream.ReceiveNext();
            this.CanMove = (bool)stream.ReceiveNext();
        }
    }

    private void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {

        base.Update();

        ChangeTeam();
        Dead();

        if (!PhotonNetwork.IsMasterClient)
            return;

        if (CanMove)
        {
            Attack();
            Move();
        }
    }

    void ChangeTeam()
    {
        if (RedTeam)
        {
            this.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            sprite.color = new Color(1, 0, 0, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(-1.2f, 1.2f, 1.2f);
            sprite.color = new Color(0, 0, 1, 1);
        }
    }

    void Dead()
    {
        if (Life <= 0)
        {
            ParticleAdmin.instance.SpawnParticle(this.gameObject.transform.position);
            this.gameObject.SetActive(false);//원래는 죽는 아니메로 이동
        }
    }
    void Attack()
    {
        if (Vector3.Distance(TargetObject.transform.position, transform.position) <= 1.5f)
        {
            isAttack = true;
            animator.SetBool("Attack", true);
        }
        else
        {
            isAttack = false;
            animator.SetBool("Attack", false);
        }
    }

    void Move()
    {

        if (!isAttack)
        {
            Vector3 Temp = new Vector3(TargetObject.transform.position.x - transform.position.x, TargetObject.transform.position.y - transform.position.y, 0);
            Temp = Vector3.Normalize(Temp);
            transform.Translate(Temp * MoveSpeed * Time.deltaTime);
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
