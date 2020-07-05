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
        if (TargetObject == null)
        {
            Destroy(this.gameObject);
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
                 Destroy(gameObject);
            }

        }
    }
}
