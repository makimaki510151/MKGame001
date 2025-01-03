using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StageContents
{
    public Vector2 stageValue = new(1, 1);
    public Sprite nameSprite = null;
    public Sprite explanationSprite = null;
    public Sprite iconSprite = null;
    public Sprite stageSprite = null;
    public Sprite[] storySprites = new Sprite[3];
    public List<Sprite> gameOverSpritesValue = new();
    public bool isGetScroll = false;
    public bool[] stageProgressFlags = new bool[3] {false,false,false};
    public bool stageClear = false;
}
[CreateAssetMenu(fileName = "DataScriptableObject", menuName = "DataScriptableObject", order = 0)]
public class DataScriptableObject : ScriptableObject
{
    public Vector2 selectStageValue = Vector2.one;

    public float seVolume = 0.5f;
    public float bgmVolume = 0.5f;
    public List<StageContents> stageContents = new();
}