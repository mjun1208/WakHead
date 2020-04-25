using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public int MyStage;
    public int MyType;
    public double MyPos_x;
    public double MyPos_y;

    public Tile(int Stage, int Type, Vector2 Pos)
    {
        MyStage = Stage;
        MyType = Type;
        MyPos_x = (double)Pos.x;
        MyPos_y = (double)Pos.y;
    }
}
