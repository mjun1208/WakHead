using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_2 : Bolt.EntityEventListener<IPlayerState>
{
    public PlayerMovement player;
    public Skill_2_Base skill_2;

    public override void Attached()
    {
        state.OnDoSkill_2 = DoSkill_2;
    }

    public void Skill_2()
    {
        if (entity.IsOwner)
            state.DoSkill_2();
    }

    public void DoSkill_2()
    {
        skill_2.Skill_2();
        //이제 이걸 싹 바꾸면 됨
    }
}
