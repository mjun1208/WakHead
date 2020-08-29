using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalCrossing_Skill_2 : Skill_2_Base
{
    private float WaveSize = 1;
    public SpriteRenderer renderer; 

    private void Update()
    {
        if (player.Mycreature.animator.GetCurrentAnimatorStateInfo(0).IsName("Skill_2") && player.Mycreature.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            player.Mycreature.animator.Rebind();
            player.Mycreature.animator.SetBool("Skill_2", false);
            player.Skill_CanMove = true;
        }
    }

    public override void Skill_2()
    {
        base.Skill_2();
        renderer.enabled = true;
        StartCoroutine(WaveAttack());
    }

    IEnumerator WaveAttack()
    {
        while (WaveSize < 10.0f)
        {
            WaveSize += 50.0f * BoltNetwork.FrameDeltaTime;
            this.transform.localScale = new Vector3(WaveSize, WaveSize, 1);

            //for (int i = 0; i < TargetObject.Count; i++)
            //{
            while (TargetObject.Count > 0)
            {
                Creature tempCreatureScript = TargetObject[0].GetComponent<Creature>();

                tempCreatureScript.CanMove = false;

                Vector3 Dir = TargetObject[0].transform.position - this.transform.position;
                Dir.Normalize();
                tempCreatureScript.KnockBack(2.0f, Dir, true);

                tempCreatureScript.OnDamage(1.0f);
                TargetObject.RemoveAt(0);
            }
            //}

            yield return null;
        }

        WaveSize = 1;
        this.transform.localScale = new Vector3(1, 1, 1);

        renderer.enabled = false;
        yield return null;
    }
}
