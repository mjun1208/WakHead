using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAdmin : Bolt.GlobalEventListener
{
    public List<GameObject> Minions = new List<GameObject>();
    public List<MinionMovement> Minion_Script = new List<MinionMovement>();
    private int NowCount = 0;

    float SpawnDelay = 15;

    public GameObject Tower_Red;
    public GameObject Tower_Blue;
    //bool RedTeam;
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        for (int i = 0; i < Minions.Count; i++)
    //        {
    //            stream.SendNext(Minions[i].activeSelf);
    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < Minions.Count; i++)
    //        {
    //            Minions[i].SetActive((bool)stream.ReceiveNext());
    //        }
    //    }
    //}

    private void Awake()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Minions.Add(this.transform.GetChild(i).gameObject);
            Minion_Script.Add(this.transform.GetChild(i).GetComponent<MinionMovement>());
        }
    }

    private void Update()
    {
        if (!BoltNetwork.IsServer)
            return;
        Spawn();
    }

    public void Spawn()
    {
        SpawnDelay += BoltNetwork.FrameDeltaTime;

        if (SpawnDelay > 15.0f)
        {
            var minionSpawn = SpawnMinionEvent.Create();
            minionSpawn.Send();
            //StartCoroutine(MinionSpawn(4));
            SpawnDelay = -9999999999;
        }
    }

    public override void OnEvent(SpawnMinionEvent evnt)
    {
        StartCoroutine(MinionSpawn(1));
    }

    IEnumerator MinionSpawn(int SpawnCount)
    {
        //SpawnMinion(Tower_Blue, new Vector3(Tower_Red.transform.position.x, Tower_Red.transform.position.y + Random.Range(-0.5f, 1f), 0), true);
        SpawnMinion(Tower_Red, new Vector3(Tower_Blue.transform.position.x, Tower_Blue.transform.position.y + Random.Range(-0.5f, 1f), 0), false);
        //if (RedTeam)
        //    Minion.SpawnMinion(GameObject.Find("Tower2"), new Vector3(transform.position.x, transform.position.y + Random.Range(-0.5f, 1f), 0), RedTeam);
        //else
        //    Minion.SpawnMinion(GameObject.Find("Tower1"), new Vector3(transform.position.x, transform.position.y + Random.Range(-0.5f, 1f), 0), RedTeam);

        yield return new WaitForSeconds(0.6f);
        if (SpawnCount != 1)
            StartCoroutine(MinionSpawn(SpawnCount - 1));

    }

    public void SpawnMinion(GameObject Target, Vector3 Pos, bool IsRed)
    {
        //Minion_Script[NowCount].Life = 4;
        //Minion_Script[NowCount].TargetObject = Target;
        //Minion_Script[NowCount].RedTeam = IsRed;
        //Minions[NowCount].transform.position = Pos;
        //Minions[NowCount].SetActive(true);
        //
        //if (++NowCount >= Minions.Count)
        //    NowCount = 0;
        if (!BoltNetwork.IsServer)
            return;
        GameObject temp = BoltNetwork.Instantiate(BoltPrefabs.Minion, Pos, Quaternion.identity);
        temp.GetComponent<MinionMovement>().Mycreature.Life = 4;
        temp.GetComponent<MinionMovement>().Mycreature.TargetObject = Target;
        temp.GetComponent<MinionMovement>().Mycreature.RedTeam = IsRed;
    }

    public void GetEnemy()
    {

    }
}
