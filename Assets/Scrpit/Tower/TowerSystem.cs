using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSystem : MonoBehaviour
{
    public GameObject TowerBullet;
    public GameObject TowerMinion;

    public GameObject TargetObject;
    public bool RedTeam = true;
    public bool isAttack = false;

    float ShootDelay = 100;
    float SpawnDelay = 10;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ShootDelay += Time.deltaTime;
        SpawnDelay += Time.deltaTime;

        if (SpawnDelay >= 16f)
        {
            StartCoroutine(MinionSpawn(4));
            SpawnDelay = 0;
        }

        if (isAttack)
        {
            if (ShootDelay >= 1.2f)
            {
                GameObject temp = Instantiate(TowerBullet, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
                temp.GetComponent<BulletMovement>().TargetObject = TargetObject;
                ShootDelay = 0;
            }
        }

    }

    IEnumerator MinionSpawn(int SpawnCount)
    {
        GameObject temp = Instantiate(TowerMinion, new Vector3(transform.position.x, transform.position.y + Random.Range(-0.5f, 1f), transform.position.z), Quaternion.identity);
        if(temp.GetComponent<Creature>().RedTeam)
            temp.GetComponent<MinionMovement>().TargetObject = GameObject.Find("Tower2");
        else
            temp.GetComponent<MinionMovement>().TargetObject = GameObject.Find("Tower1");
        yield return new WaitForSeconds(0.6f);
        if(SpawnCount != 1)
        StartCoroutine(MinionSpawn(SpawnCount - 1));

    }
}
