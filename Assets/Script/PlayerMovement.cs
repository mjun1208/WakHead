using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public bool IsGround = false;
    [HideInInspector] public float JumpCount = 0;
    private Rigidbody2D rigid;
    private Animator anime;
    private float MoveSpeed = 10;
    private float JumpPower = 12;


    

    enum PlayerState
    {
        Idle,
        Attack
    }

    PlayerState state;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anime = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        IsGround = false;
        state = PlayerState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case PlayerState.Idle:
                Attack();
                break;
            case PlayerState.Attack:
                if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && anime.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    anime.SetBool("Attack", false);
                    anime.Rebind();
                    state = PlayerState.Idle;
                }
                break;
        }
        Walk(new Vector3(Input.GetAxis("Horizontal"), 0, 0));
        Jump();
    }

    void Walk(Vector3 Dir)
    {
        if (Dir != Vector3.zero)
        {
            if (state != PlayerState.Attack)
            {
                if (Dir.x > 0)
                    this.transform.localScale = new Vector3(1, 1, 1);
                else
                    this.transform.localScale = new Vector3(-1, 1, 1);

            }
            else
                Dir /= 2;
            anime.SetBool("Walk", true);
            transform.Translate(Dir * MoveSpeed * Time.deltaTime);
        }
        else
            anime.SetBool("Walk", false);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.C) && !Input.GetKey(KeyCode.DownArrow) && JumpCount < 1)
        {
            JumpCount++;
            rigid.velocity = Vector3.zero;
            rigid.AddForce(Vector3.up * JumpPower, ForceMode2D.Impulse);
            //rigid.MovePosition(this.transform.position + (Vector3.up * JumpPower * Time.deltaTime));
        }
        else if (Input.GetKeyDown(KeyCode.C) && Input.GetKey(KeyCode.DownArrow) && IsGround)
        {
            transform.Translate(Vector3.down * 25 * Time.deltaTime);
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            state = PlayerState.Attack;
            anime.SetBool("Attack", true);
        }
    }
}
