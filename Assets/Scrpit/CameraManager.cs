using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject player;
    public float FollowSpeed = 1.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x,player.transform.position.x, FollowSpeed * Time.deltaTime), transform.position.y, transform.position.z);
    }
}
