using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class BulletMovement : MonoBehaviourPunCallbacks , IPunObservable
{
    public GameObject TargetObject;
    public bool RedTeam = true;
    public SpriteRenderer renderer;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
            stream.SendNext(RedTeam);
        else
            RedTeam = (bool)stream.ReceiveNext();
    }

    private void Awake()
    {
        renderer = this.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
    }

    private void OnEnable()
    {
        if (RedTeam)
        {
            renderer.color = new Color(1, 0, 0, 1);
        }
        else
        {
            renderer.color = new Color(0, 0, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetObject == null)
        {
            this.gameObject.SetActive(false);
        }

        Vector3 Temp = new Vector3(TargetObject.transform.position.x - transform.position.x, TargetObject.transform.position.y - transform.position.y, 0);
        Temp = Vector3.Normalize(Temp);
        transform.Translate(Temp * 5 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == TargetObject)
        {
            if (collision.tag == "Minion" || collision.tag == "Player")
            {
                collision.gameObject.GetComponent<Creature>().Life -= 2.6f;
                this.gameObject.SetActive(false);
            }

        }
    }
}
