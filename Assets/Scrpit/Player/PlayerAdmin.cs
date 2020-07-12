using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bolt;

[BoltGlobalBehaviour]
public class PlayerAdmin : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string scene)
    {
        BoltNetwork.Instantiate(BoltPrefabs.Player, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
