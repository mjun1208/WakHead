using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int direction = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * 12 * Time.deltaTime,0,0);
        if (transform.position.x >= 50 || transform.position.x <= -50)
            Destroy(gameObject);
    }
}
