using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class JsonExample : MonoBehaviour
{
    public TileManager tileManager;

    public GameObject TileAdmin;

    public List<Tile> TileList = new List<Tile>();
    public List<Tile> ReadTileList = new List<Tile>();

    private void Start()
    {
    }

    public void Save()
    {
        for (int i = 0; i < TileAdmin.transform.childCount; i++)
        {
            TileInfo temp = TileAdmin.transform.GetChild(i).GetComponent<TileInfo>();
            TileList.Add(new Tile(temp.MyStage, temp.MyType, temp.MyPos));
        }

        JsonData TestJson = JsonMapper.ToJson(TileList);
        
        File.WriteAllText(Application.dataPath + "./TileData.json", TestJson.ToString());
        Debug.Log("Save");
    }

    public void Load()
    {
        Debug.Log("Load");  
        string JsonString = File.ReadAllText(Application.dataPath + "./TileData.json");

        Debug.Log(JsonString);

        JsonData LoadData = JsonMapper.ToObject(JsonString);

        for (int i = 0; i < LoadData.Count; i++)
        {
            int Stage = (int)LoadData[i]["MyStage"];
            int Type = (int)LoadData[i]["MyType"];
            double X = (double)LoadData[i]["MyPos_x"];
            double Y = (double)LoadData[i]["MyPos_y"];
            Vector2 Pos = new Vector2((float)X, (float)Y);
            ReadTileList.Add(new Tile(Stage, Type, Pos));
        }
        
        SetTile();
    }

    private void SetTile()
    {
        for (int i = 0; i < ReadTileList.Count; i++)
        {
            switch (ReadTileList[i].MyType)
            {
                case 1:
                    tileManager.SetTile_1(new Vector2((float)ReadTileList[i].MyPos_x, (float)ReadTileList[i].MyPos_y));
                    break;
                case 2:
                    tileManager.SetTile_2(new Vector2((float)ReadTileList[i].MyPos_x, (float)ReadTileList[i].MyPos_y));
                    break;
                case 3:
                    tileManager.SetTile_3(new Vector2((float)ReadTileList[i].MyPos_x, (float)ReadTileList[i].MyPos_y));
                    break;
            }
        }
    }
}
