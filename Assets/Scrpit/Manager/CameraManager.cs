using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance; 

    public GameObject player;

    public float FollowSpeed = 1.2f;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        if (player != null)
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x, FollowSpeed * BoltNetwork.FrameDeltaTime), transform.position.y, transform.position.z);
    }
}
