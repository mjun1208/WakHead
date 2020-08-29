using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalCrossing_Skill_1 : Skill_1_Base
{
    private float CurBiteTime = 0;
    private float RandBiteTime = 0;


    private void Update()
    {
        if (!entity.IsOwner)
            return;

        if (player.Mycreature.animator.GetCurrentAnimatorStateInfo(0).IsName("Skill_1_1") && player.Mycreature.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            CurBiteTime += BoltNetwork.FrameDeltaTime;
            if (CurBiteTime > RandBiteTime)
            {
                player.Mycreature.animator.SetBool("Bite", true);
            }
        }

        if (player.Mycreature.animator.GetCurrentAnimatorStateInfo(0).IsName("Skill_1_2"))
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                player.Mycreature.animator.SetBool("PullUp", true);
            }
        }

        if (player.Mycreature.animator.GetCurrentAnimatorStateInfo(0).IsName("Skill_1_3") && player.Mycreature.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            player.Mycreature.animator.Rebind();
            player.Mycreature.animator.SetBool("Skill_1", false);
            player.Mycreature.animator.SetBool("Bite", false);
            player.Mycreature.animator.SetBool("PullUp", false);
            player.Skill_CanMove = true;
        }
    }

    public override void Skill_1()
    {
        base.Skill_1();

        RandBiteTime = Random.Range(0.0f, 5.0f);
        CurBiteTime = 0;

        //player.Mycreature.animator.SetBool("Bite", true);
        //player.Mycreature.animator.SetBool("PullUp", true);
        //player.Mycreature.animator.SetBool("Skill_1", false);
    }
}
