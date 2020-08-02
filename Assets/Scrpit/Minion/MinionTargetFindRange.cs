﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionTargetFindRange : MonoBehaviour
{
    public MinionMovement minion_script;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (minion_script.Mycreature.TargetObject == null)
        {
            if (collision.tag == "Player" || collision.tag == "Minion")
            {
                if (minion_script.Mycreature.state.RedTeam != collision.GetComponent<Creature>().state.RedTeam) //상대팀일때
                {
                    minion_script.Mycreature.TargetObject = collision.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == minion_script.Mycreature.TargetObject)
        {
            minion_script.isAttack = false;
            minion_script.Mycreature.TargetObject = null;
        }
    }
}
