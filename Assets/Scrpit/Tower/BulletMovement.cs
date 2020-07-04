using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public GameObject TargetObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 Temp = new Vector3(TargetObject.transform.position.x - transform.position.x, TargetObject.transform.position.y - transform.position.y, 0);
        Temp = Vector3.Normalize(Temp);
        transform.Translate(Temp * 5 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Minion" || collision.tag == "Player")
        //{
        //    Destroy(gameObject);
        //}
    }
}
