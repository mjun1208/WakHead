using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSystem : Bolt.EntityEventListener
{
    public GameObject TargetObject;
    public bool RedTeam = true;
    public bool isAttack = false;
    public float TowerHp = 100;
    public SpriteRenderer TowerSprite;
    public GameObject Panel;
    public Text Paneltext;

    public TowerBulletAdmin Bullet;
    public MinionAdmin Minion;

    float ShootDelay = 2;

    public Animator anime;

    void Start()
    {
        //OldSpawnDelay = PhotonNetwork.ServerTimestamp - 15 * 1000;
        //OldShootDelay = PhotonNetwork.ServerTimestamp - 2 * 1000;
    }

    // Update is called once per frame
    void Update()
    {
        Hp();
        ChangeTeam();
    }

    void ChangeTeam()
    {
        anime.SetBool("RedTeam", RedTeam);
    }

    void Hp()
    {
        if (TowerHp <= 0)
        {
            TowerHp = 0;
            if (RedTeam)
                Paneltext.text = "블루팀 왁굳 승리!";
            else
                Paneltext.text = "레드팀 왁굳 승리!";
            Panel.SetActive(true);
        }
    }

    public void Attack()
    {
        ShootDelay += BoltNetwork.FrameDeltaTime;
        if (isAttack)
        {
            if (ShootDelay > 2.0f)
            {
                var towerBulletShoot = ShootTowerBulletEvent.Create(entity);
                towerBulletShoot.Send();
                ShootDelay = 0;
            }
        }
    }
    public override void OnEvent(ShootTowerBulletEvent evnt)
    {
        Bullet.SpawnBullet(TargetObject, new Vector3(transform.position.x, transform.position.y + 6, 0), RedTeam);
    }


    public void OnDamage(float Damage)
    {
       // TowerHp -= Damage;
       // photonView.RPC("ApplyLife", RpcTarget.Others, TowerHp);
    }

    public void ApplyLife(float life)
    {
        TowerHp = life;
    }

    public IEnumerator Hit()
    {
        TowerSprite.color = new Color(255,0,0,255);
        yield return new WaitForSeconds(0.05f);
        TowerSprite.color = new Color(255,255,255,255);
    }
}
