using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviourPunCallbacks
{
    public PlayerMovement player;
    public GameObject Bullet;
    public List<GameObject> TargetObject = new List<GameObject>();

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void Attack()
    {
        if (PhotonNetwork.IsMasterClient)
            photonView.RPC("DoAttack", RpcTarget.AllViaServer, null);
    }

    public void Attack2()
    {
        if (PhotonNetwork.IsMasterClient)
            photonView.RPC("DoAttack2", RpcTarget.AllViaServer, null);
    }


    [PunRPC]
    public void DoAttack()
    {
        for (int i = 0; i < TargetObject.Count; i++)
        {
            Creature tempCreatureScript = TargetObject[i].GetComponent<Creature>();

            tempCreatureScript.OnDamage(0.3f);
            //if (TargetObject[i].tag != "Player")
            Vector2 Dir = TargetObject[i].transform.position - player.transform.position;
            Dir.Normalize();
            float power;
            if (Dir.x > 0)
                power = 0.5f;
            else
                power = -0.5f;
            tempCreatureScript.KnockBack(power);
        }
    }

    [PunRPC]
    public void DoAttack2()
    {
        BulletAdmin.instance.SpawnBullet(player.transform.position, (int)player.transform.localScale.x, player);
        //GameObject bul = Instantiate(Bullet, player.transform.position, Quaternion.identity);
        //bul.GetComponent<Bullet>().direction = (int)player.transform.localScale.x;
        //bul.GetComponent<Bullet>().player = player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (collision.gameObject.tag == "Minion")
            {
                if (player.RedTeam != collision.GetComponent<Creature>().RedTeam)//상대팀인지 식별
                {
                    TargetObject.Add(collision.gameObject);
                }
            }
            else if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject != player.gameObject)
                    TargetObject.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (collision.gameObject.tag == "Minion")
            {
                if (player.RedTeam != collision.GetComponent<Creature>().RedTeam)
                {
                    TargetObject.Remove(collision.gameObject);
                }
            }
            else if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject != player.gameObject)
                    TargetObject.Remove(collision.gameObject);
            }
        }
    }
}
