using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal_WakGood_Skill_2 : Skill_2_Base
{
    public GameObject Bullet;


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

        if (BoltNetwork.IsServer)
        {
            var shoot = ShootEvent.Create(entity);
            shoot.Send();
        }
    }

    public override void OnEvent(ShootEvent evnt)
    {
        Shoot();
    }

    void Shoot()
    {
        BulletAdmin.instance.SpawnBullet(player.transform.position, (int)player.transform.localScale.x, player);
    }
}
