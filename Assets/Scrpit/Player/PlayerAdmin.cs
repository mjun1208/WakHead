using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bolt;

[BoltGlobalBehaviour]
public class PlayerAdmin : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string scene)
    {
        BoltNetwork.SetPrefabPool(new PrefabPool());
        //AnimalCrossing
        //Normal_WakGood
        if (BoltNetwork.IsServer)
        {
            BoltNetwork.Instantiate(BoltPrefabs.Normal_WakGood, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else {
            BoltNetwork.Instantiate(BoltPrefabs.AnimalCrossing, new Vector3(0, 0, 0), Quaternion.identity);
        }

    }
}
