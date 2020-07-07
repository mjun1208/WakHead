using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class TowerSystem : MonoBehaviourPunCallbacks, IPunObservable 
{
    public GameObject TargetObject;
    public bool RedTeam = true;
    public bool isAttack = false;
    public float TowerHp = 100;
    public SpriteRenderer TowerSprite;
    public GameObject EndPanel;
    public Text EndingText;

    public BulletAdmin Bullet;
    public MinionAdmin Minion;



    int CurShootDelay = 0;
    int CurSpawnDelay = 0;
    
    int OldShootDelay = 0;
    int OldSpawnDelay = 0;

    public Animator anime;

    //private PhotonView photonView_ = GameObject.Find("PhotonController").GetComponent<PhotonView>();

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new System.NotImplementedException();
    }


    void Start()
    {
        OldSpawnDelay = PhotonNetwork.ServerTimestamp - 15 * 1000;
        OldShootDelay = PhotonNetwork.ServerTimestamp - 2 * 1000;
    }

    // Update is called once per frame
    void Update()
    {
        Hp();
        ChangeTeam();

        if (!PhotonNetwork.IsMasterClient)
            return;
        photonView.RPC("Spawn", RpcTarget.AllViaServer, null);
        //photonView.RPC("Attack", RpcTarget.AllViaServer, null);
    }
    void ChangeTeam()
    {
        anime.SetBool("RedTeam", RedTeam);
    }

    [PunRPC]
    public void Spawn()
    {
        CurSpawnDelay = PhotonNetwork.ServerTimestamp;

        if (CurSpawnDelay - OldSpawnDelay > 15 * 1000)
        {
            StartCoroutine(MinionSpawn(4));
            OldSpawnDelay = CurSpawnDelay;
        }
    }
    [PunRPC]
    public void Attack()
    {
        CurShootDelay = PhotonNetwork.ServerTimestamp;
        if (isAttack)
        {
            if (CurShootDelay - OldShootDelay > 2 * 1000)
            {
                Bullet.SpawnBullet(TargetObject, new Vector3(transform.position.x, transform.position.y + Random.Range(-0.5f, 1f), 0), RedTeam);
                OldShootDelay = CurShootDelay;
            }
        }
    }

    public void Hp()
    {
        if (TowerHp <= 0)
        {
            TowerHp = 0;
            if(RedTeam)
                EndingText.text = "블루팀 왁굳 승리!";
            else
                EndingText.text = "레드팀 왁굳 승리!";
            EndPanel.SetActive(true);
        }
    }

    IEnumerator MinionSpawn(int SpawnCount)
    {

        if (RedTeam)
            Minion.SpawnMinion(GameObject.Find("Tower2"), new Vector3(transform.position.x, transform.position.y + Random.Range(-0.5f, 1f), 0), RedTeam);
        else
            Minion.SpawnMinion(GameObject.Find("Tower1"), new Vector3(transform.position.x, transform.position.y + Random.Range(-0.5f, 1f), 0), RedTeam);
        
        yield return new WaitForSeconds(0.6f);
        if(SpawnCount != 1)
        StartCoroutine(MinionSpawn(SpawnCount - 1));

    }

    public IEnumerator Hit()
    {
        TowerSprite.color = new Color(255,0,0,255);
        yield return new WaitForSeconds(0.05f);
        TowerSprite.color = new Color(255,255,255,255);
    }
}
