using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//시간없으니까 공격을 일케하는데 나중에 바꿨으면 함
public class MinionAttack : Bolt.EntityEventListener<IMinionState>
{
    public MinionMovement minion_script;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void Attack()
    {
        if (!BoltNetwork.IsServer)
            return;

        if (minion_script.Mycreature.TargetObject == null)
        {
            minion_script.EnemyTower.GetComponent<TowerSystem>().OnDamage(1f);
            minion_script.EnemyTower.GetComponent<TowerSystem>().Hit();
        }
        else
        {
            minion_script.Mycreature.TargetObject.GetComponent<Creature>().Life -= 1.0f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!BoltNetwork.IsServer)
            return;

        if (minion_script.Mycreature.TargetObject != null)
        {
            if (collision.gameObject == minion_script.Mycreature.TargetObject)
            {
                minion_script.CanAttack = true;
            }
        }
        else
        {
            if (Vector3.Distance(minion_script.EnemyTower.transform.position, this.transform.position) <= 1.5f)
            {
                minion_script.CanAttack = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!BoltNetwork.IsServer)
            return;

        if (minion_script.Mycreature.TargetObject != null)
        {
            if (collision.gameObject == minion_script.Mycreature.TargetObject)
            {
                minion_script.CanAttack = false;
                minion_script.isAttack = false;
            }
        }
        else
        {

        }
    }
}
