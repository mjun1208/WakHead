using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class TowerSystem : MonoBehaviourPunCallbacks, IPunObservable 
{
    public GameObject TowerBullet;
    public GameObject TowerMinion;

    public GameObject TargetObject;
    public bool RedTeam = true;
    public bool isAttack = false;

    float ShootDelay = 10;
    float SpawnDelay = 100;

    //private PhotonView photonView_ = GameObject.Find("PhotonController").GetComponent<PhotonView>();

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
        throw new System.NotImplementedException();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        photonView.RPC("Spawn", RpcTarget.AllViaServer, null);
        photonView.RPC("Attack", RpcTarget.AllViaServer, null);
    }

    [PunRPC]
    public void Spawn()
    {
        SpawnDelay += Time.deltaTime;

        if (SpawnDelay >= 16f)
        {
            StartCoroutine(MinionSpawn(4));
            SpawnDelay = 0;
        }
    }
    [PunRPC]
    public void Attack()
    {
        ShootDelay += Time.deltaTime;
        if (isAttack)
        {
            if (ShootDelay >= 1.2f)
            {
                GameObject temp;
                if (RedTeam)
                    temp = PhotonNetwork.Instantiate("TowerBullet", new Vector3(transform.position.x, transform.position.y + Random.Range(-0.5f, 1f), transform.position.z), Quaternion.identity);
                else
                    temp = PhotonNetwork.Instantiate("TowerBullet2", new Vector3(transform.position.x, transform.position.y + Random.Range(-0.5f, 1f), transform.position.z), Quaternion.identity);
                //GameObject temp = Instantiate(TowerBullet, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
                temp.GetComponent<BulletMovement>().TargetObject = TargetObject;
                ShootDelay = 0;
            }
        }
    }

    IEnumerator MinionSpawn(int SpawnCount)
    {
        GameObject temp;

        if (RedTeam)
            temp = PhotonNetwork.Instantiate("Minion", new Vector3(transform.position.x, transform.position.y + Random.Range(-0.5f, 1f), transform.position.z), Quaternion.identity);  
        else
            temp = PhotonNetwork.Instantiate("Minion2", new Vector3(transform.position.x, transform.position.y + Random.Range(-0.5f, 1f), transform.position.z), Quaternion.identity);
        //GameObject temp = Instantiate(TowerMinion, new Vector3(transform.position.x, transform.position.y + Random.Range(-0.5f, 1f), transform.position.z), Quaternion.identity);
        if (RedTeam)
            temp.GetComponent<MinionMovement>().TargetObject = GameObject.Find("Tower2");
        else
            temp.GetComponent<MinionMovement>().TargetObject = GameObject.Find("Tower1");
        yield return new WaitForSeconds(0.6f);
        if(SpawnCount != 1)
        StartCoroutine(MinionSpawn(SpawnCount - 1));

    }
}
