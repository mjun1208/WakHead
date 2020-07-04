using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Creature
{
    public bool IsLocalPlayer = false;
    Vector2 OldPos;
    Vector2 CurPos;

    float CurrentSpeed = 0;
    float AttackSpeed = 0;
    int isLeft = 1;

    public float MinYPos = -5;
    public float MaxYPos = 0;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        animator = transform.GetChild(0).GetComponent<Animator>();
        CurPos = this.transform.position;
        OldPos = CurPos;

        CurrentSpeed = MoveSpeed;
        AttackSpeed = MoveSpeed * 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        sprite.sortingOrder = sprite.sortingOrder + 1;

        if (!IsLocalPlayer) {
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

        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetBool("Attack", true);
            CurrentSpeed = AttackSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            animator.SetBool("Attack", false);
            CurrentSpeed = MoveSpeed;
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
