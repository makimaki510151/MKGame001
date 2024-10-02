using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class ValueData
{
    public int objectID;
    public enum SetType
    {
        None,
        Tile,
        Prefab,
        Player
    }
    public SetType type;
    public GameObject setPrefab;
    public Tilemap setTilemap;
    public Tile setTile;
}
[System.Serializable]
public class StageCsv
{
    public Vector2 stgaeValue;
    public TextAsset csvFile;
}

public class StageCreat : MonoBehaviour
{
    [SerializeField]
    private List<StageCsv> stageCsvList = new();
    private TextAsset csvFile;
    [SerializeField]
    private List<ValueData> valueDatas = new();

    private DataScriptableObject dataScriptableObject = null;

    private List<string[]> csvDatas = new List<string[]>();
    void Start()
    {
        dataScriptableObject = StageRoot.Instance.dataScriptableObject;

        csvFile = stageCsvList[0].csvFile;
        for (int i = 0; i < stageCsvList.Count; i++)
        {
            if (stageCsvList[i].stgaeValue == dataScriptableObject.selectStageValue)
            {
                csvFile = stageCsvList[i].csvFile;
            }
        }

        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        for (int i = 0; i < csvDatas.Count; i++)
        {
            for (int j = 0; j < csvDatas[i].Length; j++)
            {
                for(int k = 0; k < valueDatas.Count; k++)
                {
                    if (valueDatas[k].objectID == int.Parse(csvDatas[i][j]))
                    {
                        switch (valueDatas[k].type)
                        {
                            case ValueData.SetType.None:
                                break;
                            case ValueData.SetType.Tile:
                                valueDatas[k].setTilemap.SetTile(new Vector3Int(j, i, 0), valueDatas[k].setTile);
                                if (k == 6)
                                {
                                    Debug.Log("ŒÄ‚Î‚ê‚Ä‚¢‚é");
                                }
                                break;
                            case ValueData.SetType.Prefab:
                                Instantiate(valueDatas[k].setPrefab, new Vector3(j, i, 0), Quaternion.identity);
                                break;
                            case ValueData.SetType.Player:
                                StageRoot.Instance.SetStartPlayerPos(new Vector3(j, i, 0));
                                break;
                        }
                    }
                }
            }
        }
    }
}
