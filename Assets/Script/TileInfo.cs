using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public int MyStage;
    public int MyType;
    public Vector2 MyPos;

    private void Awake()
    {
        MyPos = this.transform.position;
    }
}
