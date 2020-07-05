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
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.localScale = (Vector3)stream.ReceiveNext();
        }
    }

    void Start()
    {
        base.Start();

        animator = transform.GetChild(0).GetComponent<Animator>();
    }
    

    void Update()
    {
        base.Update();

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

        if (!isAttack)
        {
            Vector3 Temp = new Vector3(TargetObject.transform.position.x - transform.position.x, TargetObject.transform.position.y - transform.position.y, 0);
            Temp = Vector3.Normalize(Temp);
            transform.Translate(Temp * MoveSpeed * Time.deltaTime);
        }

        if (Life <= 0)
        {
            Destroy(gameObject);//원래는 죽는 아니메로 이동
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
