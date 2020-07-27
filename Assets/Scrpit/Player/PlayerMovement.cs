using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bolt;

public class PlayerMovement : Bolt.EntityBehaviour<IPlayerState>
{
    public Creature Mycreature;

    public bool IsLocalPlayer = false;
    Vector3 OldPos;
    Vector3 CurPos;

    float CurrentSpeed = 0;
    float AttackSpeed = 0;
    int isLeft = 1;

    public float MinYPos = -5;
    public float MaxYPos = 0;

    public GameObject Grapic;

    private int CurReSpawnTime = 0;
    private int OldReSpawnTime = 0;

    public Collider2D collider;
     
    private bool Skill_CanMove = true;

    public SpriteRenderer PlayerArrow;

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(CurPos);
    //        stream.SendNext(this.transform.localScale);
    //        stream.SendNext(this.RedTeam);
    //        //stream.SendNext(this.Life);
    //        stream.SendNext(Grapic.activeSelf);
    //
    //        stream.SendNext(!this.IsLocalPlayer);
    //        stream.SendNext(this.CanMove);
    //    }
    //    else
    //    {
    //        CurPos = (Vector3)stream.ReceiveNext();
    //        this.transform.localScale = (Vector3)stream.ReceiveNext();
    //        this.RedTeam = (bool)stream.ReceiveNext();
    //        //this.Life = (float)stream.ReceiveNext();
    //        Grapic.SetActive((bool)stream.ReceiveNext());
    //
    //        this.IsLocalPlayer = (bool)stream.ReceiveNext();
    //        this.CanMove = (bool)stream.ReceiveNext();
    //    }
    //}

    public override void Attached()
    {
        if (entity.IsOwner)
            CameraManager.instance.player = this.gameObject;

        state.SetTransforms(state.CreatureTransform, transform);
        state.SetAnimator(Mycreature.animator);

        if (entity.IsOwner)
        {
            if (BoltNetwork.IsServer)
            {
                Mycreature.RedTeam = true;
            }
            else
            {
                Mycreature.RedTeam = false;
            }
            state.LocalScale = this.transform.localScale;
            state.RedTeam = Mycreature.RedTeam;
            //state.CanMove = Mycreature.CanMove;
            //state.Stun = Mycreature.Stun;
        }

        state.AddCallback("LocalScale", ScaleChange);
        state.AddCallback("RedTeam", RedTeamChange);
        //state.AddCallback("CanMove", CanMoveChange);
        //state.AddCallback("Stun", StunChange);
        state.Animator.applyRootMotion = entity.IsOwner;
    }
    
    void ScaleChange()
    {
        transform.localScale = state.LocalScale;
    }

    void RedTeamChange()
    {
        Mycreature.RedTeam = state.RedTeam;
    }

    //void CanMoveChange()
    //{
    //    Mycreature.CanMove = state.CanMove;
    //}

    //void StunChange()
    //{
    //    Mycreature.Stun = state.Stun;
    //}

    public override void SimulateOwner()
    {
        if (Mycreature.Life <= 0)
        {
            if (!Mycreature.IsDead)
            {
                collider.enabled = false;
                Mycreature.IsDead = true;
                ParticleAdmin.instance.SpawnParticle(this.gameObject.transform.position);
                Grapic.SetActive(false);
                //OldReSpawnTime = PhotonNetwork.ServerTimestamp;
            }
            else
            {
                //CurReSpawnTime = PhotonNetwork.ServerTimestamp;
                int NowTime = CurReSpawnTime - OldReSpawnTime;
                if (NowTime > 3 * 1000)
                {
                    Mycreature.Life = 4;
                    //photonView.RPC("ApplyLife", RpcTarget.Others, Life);

                    transform.position = Vector3.zero;

                    Mycreature.animator.SetBool("Skill_1", false);
                    Mycreature.animator.SetBool("Skill_2", false);
                    Mycreature.animator.SetBool("Attack", false);
                    Mycreature.animator.SetBool("Walk", false);
                }

                return;
            }
        }
        else
        {
            collider.enabled = true;
            Mycreature.IsDead = false;
            Grapic.SetActive(true);
        }

        CurPos = this.transform.position;

        //Move();   
        if (Mycreature.animator.GetBool("Skill_1") && Mycreature.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            Mycreature.animator.SetBool("Skill_1", false);
            Skill_CanMove = true;
        }

        if (Mycreature.animator.GetBool("Skill_2") && Mycreature.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            Mycreature.animator.SetBool("Skill_2", false);
            Skill_CanMove = true;
        }

        if (Mycreature.CanMove && Skill_CanMove && !Mycreature.Stun)
        {
            Move_Input();
            Skill_1();
            Skill_2();
        }
        else if (Mycreature.Stun)
        {
            Skill_CanMove = true;

            Mycreature.animator.SetBool("Attack", false);
            Mycreature.animator.SetBool("Skill_1", false);
            Mycreature.animator.SetBool("Skill_2", false);
            CurrentSpeed = Mycreature.MoveSpeed;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Mycreature.animator = transform.GetChild(0).GetComponent<Animator>();
        if (state.RedTeam)
            PlayerArrow.color = new Color(1, 0, 0, 1);
        else
            PlayerArrow.color = new Color(0, 0, 1, 1);

        CurPos = this.transform.position;
        OldPos = CurPos;

        CurrentSpeed = Mycreature.MoveSpeed;
        AttackSpeed = Mycreature.MoveSpeed * 0.2f;
    }

    void Move_Input()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
            Mycreature.animator.SetBool("Walk", true);
        else
            Mycreature.animator.SetBool("Walk", false);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-CurrentSpeed * BoltNetwork.FrameDeltaTime, 0, 0);
            state.LocalScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(CurrentSpeed * BoltNetwork.FrameDeltaTime, 0, 0);
            state.LocalScale = new Vector3(1, 1, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.position.y <= MinYPos)
                transform.position = new Vector3(transform.position.x, MinYPos, transform.position.z);
            else
                transform.Translate(0, -CurrentSpeed * BoltNetwork.FrameDeltaTime, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (transform.position.y >= MaxYPos)
                transform.position = new Vector3(transform.position.x, MaxYPos, transform.position.z);
            else
                transform.Translate(0, CurrentSpeed * BoltNetwork.FrameDeltaTime, 0);
        }


        if (Input.GetKey(KeyCode.Z))
        {
            Mycreature.animator.SetBool("Attack", true);
            CurrentSpeed = AttackSpeed;
        }
        else
        {
            Mycreature.animator.SetBool("Attack", false);
            CurrentSpeed = Mycreature.MoveSpeed;
        }
    }

    public void Skill_1()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Skill_CanMove = false;
            Mycreature.animator.SetBool("Skill_1", true);
        }
    }

    public void Skill_2()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Skill_CanMove = false;
            Mycreature.animator.SetBool("Skill_2", true);
        }
    }
}
