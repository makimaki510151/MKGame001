using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameOverSprites
{
    public Vector2 stgaeValue = new(1, 1);
    public List<Sprite> gameOverSpritesValue = new();
}
[CreateAssetMenu(fileName = "DataScriptableObject", menuName = "DataScriptableObject", order = 0)]
public class DataScriptableObject : ScriptableObject
{
    public Vector2 selectStageValue = Vector2.one;

    public float seVolume = 0.5f;
    public float bgmVolume = 0.5f;
    public List<GameOverSprites> gameOverSprites = new();

}