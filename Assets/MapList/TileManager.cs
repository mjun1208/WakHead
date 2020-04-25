using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject Tile_1;
    public GameObject Tile_2;
    public GameObject Tile_3;

    List<GameObject> Tile_1_Objects = new List<GameObject>();
    List<GameObject> Tile_2_Objects = new List<GameObject>();
    List<GameObject> Tile_3_Objects = new List<GameObject>();

    int NowTile_1 = 0;
    int NowTile_2 = 0;
    int NowTile_3 = 0;

    private void Awake()
    {
        for (int i = 0; i < Tile_1.transform.childCount; i++)
        {
            Tile_1_Objects.Add(Tile_1.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < Tile_2.transform.childCount; i++)
        {
            Tile_2_Objects.Add(Tile_2.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < Tile_3.transform.childCount; i++)
        {
            Tile_3_Objects.Add(Tile_3.transform.GetChild(i).gameObject);
        }
    }

    public void SetTile_1(Vector2 Pos)
    {
        Tile_1_Objects[NowTile_1].SetActive(true);
        Tile_1_Objects[NowTile_1].transform.position = Pos;

        if (NowTile_1++ >= Tile_1.transform.childCount - 1)
        {
            NowTile_1 = 0;
        }
    }

    public void SetTile_2(Vector2 Pos)
    {
        Tile_2_Objects[NowTile_2].SetActive(true);
        Tile_2_Objects[NowTile_2].transform.position = Pos;

        if (NowTile_2++ >= Tile_2.transform.childCount - 1)
        {
            NowTile_2 = 0;
        }
    }

    public void SetTile_3(Vector2 Pos)
    {
        Tile_3_Objects[NowTile_3].SetActive(true);
        Tile_3_Objects[NowTile_3].transform.position = Pos;

        if (NowTile_3++ >= Tile_3.transform.childCount - 1)
        {
            NowTile_3 = 0;
        }
    }
}
