using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameOverSprites
{
    public Vector2 stageValue = new(1, 1);
    public List<Sprite> gameOverSpritesValue = new();
}
[System.Serializable]
public class StageContents
{
    public Vector2 stageValue = new(1, 1);
    public Sprite nameSprite = null;
    public Sprite explanationSprite = null;
    public Sprite iconSprite = null;
    public Sprite stageSprite = null;
    public Sprite[] storySprites = new Sprite[3];
}
[CreateAssetMenu(fileName = "DataScriptableObject", menuName = "DataScriptableObject", order = 0)]
public class DataScriptableObject : ScriptableObject
{
    public Vector2 selectStageValue = Vector2.one;

    public float seVolume = 0.5f;
    public float bgmVolume = 0.5f;
    public List<GameOverSprites> gameOverSprites = new();
    public List<StageContents> stageContents = new();
}