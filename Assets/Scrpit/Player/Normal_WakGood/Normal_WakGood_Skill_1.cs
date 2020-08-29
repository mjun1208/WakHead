using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal_WakGood_Skill_1 : Skill_1_Base
{
    private void Update()
    {
        if (player.Mycreature.animator.GetCurrentAnimatorStateInfo(0).IsName("Skill_1") && player.Mycreature.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            player.Mycreature.animator.Rebind();
            player.Mycreature.animator.SetBool("Skill_1", false);
            player.Skill_CanMove = true;
        }
    }

    public override void Skill_1()
    {
        for (int i = 0; i < TargetObject.Count; i++)
        {
            Creature tempCreatureScript = TargetObject[i].GetComponent<Creature>();
        
            tempCreatureScript.OnDamage(1.0f);
            tempCreatureScript.CanMove = false;
            tempCreatureScript.Grab(player.transform.position, true);
            //if (TargetObject[i].tag != "Player")
            //tempCreatureScript.KnockBack(TargetObject[i].transform.position.x - player.transform.position.x);
        }
    }
}
