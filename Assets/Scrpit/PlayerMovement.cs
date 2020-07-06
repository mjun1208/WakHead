using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Rendering;

public class PlayerMovement : Creature, IPunObservable
{
    public bool IsLocalPlayer = false;
    Vector3 OldPos;
    Vector3 CurPos;

    float CurrentSpeed = 0;
    float AttackSpeed = 0;
    int isLeft = 1;

    public float MinYPos = -5;
    public float MaxYPos = 0;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(CurPos);
            stream.SendNext(this.transform.localScale);
            stream.SendNext(this.RedTeam);
        }
        else
        {
            CurPos = (Vector3)stream.ReceiveNext();
            this.transform.localScale = (Vector3)stream.ReceiveNext();
            this.RedTeam = (bool)stream.ReceiveNext();
        }
    }
    private void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
            RedTeam = true;
        else
            RedTeam = false;

        animator = transform.GetChild(0).GetComponent<Animator>();
        CurPos = this.transform.position;
        OldPos = CurPos;

        CurrentSpeed = MoveSpeed;
        AttackSpeed = MoveSpeed * 0.2f;

        if (photonView.IsMine)
            CameraManager.instance.player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PhotonNetwork.Time);
        //Debug.Log(PhotonNetwork.ServerTimestamp);

        base.Update();
        //sprite.sortingOrder = sprite.sortingOrder + 1;//미니언보다 한층 더 높은 레이어를 사용하여 왁굳형의 가시성을 올린다.

        if (!photonView.IsMine) {
            this.transform.position = Vector3.Lerp(this.transform.position, CurPos, 5.0f);
            
            return;
        }

        //Move();   
        Move_Input();
    }
    void Move()
    {
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * MoveSpeed;

        transform.Translate(dir.x, dir.y, 0);

        CurPos = this.transform.position;

        if (CurPos != OldPos)
        {
            //네트워크에 적용
            OldPos = CurPos;
        }
    }

    void Move_Input()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
            animator.SetBool("Walk", true);

        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
            animator.SetBool("Walk", false);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-CurrentSpeed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(CurrentSpeed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.position.y <= MinYPos)
                transform.position = new Vector3(transform.position.x, MinYPos, transform.position.z);
            else
                transform.Translate(0, -CurrentSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (transform.position.y >= MaxYPos)
                transform.position = new Vector3(transform.position.x, MaxYPos, transform.position.z);
            else
                transform.Translate(0, CurrentSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("Attack", true);
            CurrentSpeed = AttackSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            animator.SetBool("Attack", false);
            CurrentSpeed = MoveSpeed;
        }

        CurPos = this.transform.position;
        if (CurPos != OldPos)
        {
            OldPos = CurPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Bullet")
        //{
        //    Life -= 2f;
        //    Destroy(collision.gameObject);
        //}
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Destroy(collision.gameObject);
    //    if (collision.gameObject.tag == "Bullet")
    //        {
    //            Life -= 2f;
    //            Destroy(collision.gameObject);
    //        }
    //}
}
