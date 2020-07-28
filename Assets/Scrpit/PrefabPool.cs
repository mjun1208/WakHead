using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPool : Bolt.IPrefabPool
{
    public GameObject Instantiate(PrefabId prefabId, Vector3 position, Quaternion rotation)
    {
        GameObject gameObject;

        gameObject = GameObject.Instantiate(LoadPrefab(prefabId), position, rotation);
        gameObject.GetComponent<BoltEntity>().enabled = true;

        return gameObject;
    }

    public void Destroy(GameObject gameObject)
    {
        GameObject.Destroy(gameObject);
    }


    public GameObject LoadPrefab(PrefabId prefabId)
    {
        return PrefabDatabase.Find(prefabId);
    }
}
