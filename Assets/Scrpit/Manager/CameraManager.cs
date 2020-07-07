using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance; 

    public GameObject player;
    public float FollowSpeed = 1.2f;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        //PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity);
    }

    private void FixedUpdate()
    {
        if (player != null)
            transform.position = new Vector3(Mathf.Lerp(transform.position.x,player.transform.position.x, FollowSpeed * Time.deltaTime), transform.position.y, transform.position.z);
    }
}
