using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_1 : Bolt.EntityBehaviour<IPlayerState>
{
    public PlayerMovement player;
    public Skill_1_Base skill_1;

    public override void Attached()
    {
        state.OnDoSkill_1  = DoSkill_1;
    }

    public void Skill_1()
    {
        if (entity.IsOwner)
            state.DoSkill_1();
    }

    public void DoSkill_1()
    {
        skill_1.Skill_1();
        //이제 이걸 싹 바꾸면 됨
    }
}
