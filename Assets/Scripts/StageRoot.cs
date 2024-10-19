using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
class DisplayStage
{
    public Vector2 stageValue = new(1, 1);
    public GameObject stageObject = null;
}

[System.Serializable]
class DestroyItem
{
    public GameObject item;
    public Vector2 stageValue = Vector2.zero;
}
public class StageRoot : MonoBehaviour
{
    [SerializeField, Tooltip("デバッグ")]
    private bool isDebug = false;
    [SerializeField]
    private Vector2 debugVector2 = new(1, 1);

    [Header("通常")]

    public DataScriptableObject dataScriptableObject = null;

    [SerializeField]
    private Transform playerTransform = null;
    [SerializeField]
    private SpriteRenderer playerSpriteRenderer = null;
    [SerializeField]
    private Transform playerSpriteTransform = null;
    [SerializeField]
    private Transform coreTransform = null;
    [SerializeField]
    private bool isCameraPlayer = true;
    [SerializeField]
    private float xPosGap = 12;
    [SerializeField]
    private float cameraYPos = 10;
    [SerializeField]
    private GameObject stageGoalUIObject = null;
    [SerializeField]
    private List<TMP_Text> resultTextList = new();
    [SerializeField]
    private GameObject gameOverUIObject = null;
    [SerializeField]
    private GameObject mainCanvas = null;
    [SerializeField]
    private Image gameOverImage = null;

    [SerializeField]
    private List<DisplayStage> displayStages = new();
    [SerializeField]
    private List<DestroyItem> itemObjects = new();

    private Transform cameraTransform = null;
    private bool isButtonGo = false;

    public static StageRoot Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (!isDebug)
        {
            for (int i = 0; i < displayStages.Count; i++)
            {
                displayStages[i].stageObject.SetActive(false);
                if (displayStages[i].stageValue == dataScriptableObject.selectStageValue)
                {
                    displayStages[i].stageObject.SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < displayStages.Count; i++)
            {
                displayStages[i].stageObject.SetActive(false);
                if (displayStages[i].stageValue == debugVector2)
                {
                    displayStages[i].stageObject.SetActive(true);
                }
            }
        }


        for (int i = 0; i < dataScriptableObject.stageContents.Count; i++)
        {
            if (dataScriptableObject.selectStageValue == dataScriptableObject.stageContents[i].stageValue)
            {
                playerSpriteRenderer.sprite = dataScriptableObject.stageContents[i].iconSprite;
                if (dataScriptableObject.stageContents[i].isGetScroll)
                {
                    for (int j = 0; j < itemObjects.Count; j++)
                    {
                        if (itemObjects[j].stageValue == dataScriptableObject.selectStageValue)
                        {
                            Destroy(itemObjects[j].item);
                        }
                    }
                }
            }
        }
        Time.timeScale = 1;
        cameraTransform = Camera.main.transform;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        playerSpriteTransform.rotation = Quaternion.Euler(0, 0, 0);

        if (Time.timeScale == 1)
        {
            if (isCameraPlayer)
            {
                var playerPos = playerTransform.position;
                playerPos.x += xPosGap;
                playerPos.y = cameraYPos;
                playerPos.z = -10;
                cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, playerPos, 0.01f);
            }
            else
            {
                var tempPos = coreTransform.position;
                tempPos.y = cameraYPos;
                tempPos.z = -10;
                cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, tempPos, 0.01f);
            }
        }
    }

    public void SetStartPlayerPos(Vector3 vector3)
    {
        vector3.x += 1;
        coreTransform.position = vector3;
        vector3.x -= 1;
        playerTransform.position = vector3;
        vector3.z = -10;
        cameraTransform.position = vector3;

    }
    public void StgaeGoal(int lClickCount, int invertCount, float clearTime, float slowTime)
    {
        SetResult(lClickCount, invertCount, clearTime, slowTime);

        Time.timeScale = 0;

        stageGoalUIObject.SetActive(true);
    }

    public void GameOver(int value)
    {
        Time.timeScale = 0;

        for (int j = 0; j < dataScriptableObject.stageContents.Count; j++)
        {
            if (dataScriptableObject.selectStageValue == dataScriptableObject.stageContents[j].stageValue)
            {
                gameOverImage.sprite = dataScriptableObject.stageContents[j].gameOverSpritesValue[value];
            }
        }

        gameOverUIObject.SetActive(true);
    }
    public void ButtonStageSelect()
    {
        if (isButtonGo)
        {
            return;
        }
        isButtonGo = true;
        StartCoroutine(LoadYourAsyncScene("StageSelect"));
        Time.timeScale = 1;
    }
    public void ButtonStgae()
    {
        if (isButtonGo)
        {
            return;
        }
        isButtonGo = true;
        StartCoroutine(LoadYourAsyncScene("Stage"));
        Time.timeScale = 1;
    }
    IEnumerator LoadYourAsyncScene(string name)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public Transform GetPlayer()
    {
        return playerTransform;
    }

    public void SetResult(int lClickCount, int invertCount, float clearTime, float slowTime)
    {
        resultTextList[0].text = lClickCount.ToString();
        resultTextList[1].text = invertCount.ToString();
        resultTextList[2].text = clearTime.ToString("f2");
        resultTextList[3].text = slowTime.ToString("f2");
    }
    public void PlayerOff()
    {
        playerTransform.gameObject.SetActive(false);
        coreTransform.gameObject.SetActive(false);
        mainCanvas.SetActive(false);
    }
    public void GetItem(GameObject _gameObject)
    {
        for (int i = 0; i < dataScriptableObject.stageContents.Count; i++)
        {
            if (dataScriptableObject.selectStageValue == dataScriptableObject.stageContents[i].stageValue)
            {
                dataScriptableObject.stageContents[i].isGetScroll = true;
                Destroy(_gameObject);
            }
        }
    }
}
