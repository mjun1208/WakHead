using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TowerBulletMovement : Bolt.EntityBehaviour<ITowerBulletState>
{
    public GameObject TargetObject;
    public bool RedTeam = true;
    public SpriteRenderer renderer;

    public Sprite RedBullet;
    public Sprite BlueBullet;

    public TrailRenderer trail;

    public Material Redmaterial;
    public Material Bluematerial;
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //        stream.SendNext(RedTeam);
    //    else
    //        RedTeam = (bool)stream.ReceiveNext();
    //}

    public override void Attached()
    {
        state.SetTransforms(state.TowerBulletTransform ,this.transform);
    }


    // Update is called once per frame
    void Update()
    {
        if (state.RedTeam)
        {
            renderer.sprite = RedBullet;
            trail.material = Redmaterial;
        }
        else
        {
            renderer.sprite = BlueBullet;
            trail.material = Bluematerial;
        }


        if (!BoltNetwork.IsServer)
            return;

        if (TargetObject == null)
        {
            BoltNetwork.Destroy(this.gameObject);
        }

        Vector3 Temp = new Vector3(TargetObject.transform.position.x - transform.position.x, TargetObject.transform.position.y - transform.position.y, 0);
        Temp = Vector3.Normalize(Temp);
        transform.Translate(Temp * 5 * BoltNetwork.FrameDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!BoltNetwork.IsServer)
            return;

        if (collision.gameObject == TargetObject)
        {
            if (collision.tag == "Minion" || collision.tag == "Player")
            {
                collision.gameObject.GetComponent<Creature>().Life -= 2.6f;
                BoltNetwork.Destroy(this.gameObject);
            }

        }
    }
}
