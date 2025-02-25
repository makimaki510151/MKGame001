using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSlectRoot : MonoBehaviour
{
    [SerializeField]
    private DataScriptableObject dataScriptableObject = null;
    // [SerializeField]
    // private GameObject firstObject = null;
    // [SerializeField]
    // private GameObject secondObject = null;
    [SerializeField]
    private GameObject thirdObject = null;

    [SerializeField]
    private RectTransform thirdMainRectTransform = null;
    [SerializeField]
    private RectTransform thirdStoryRectTransform = null;

    [SerializeField]
    private float thirdUIMoveSpeed = 0.05f;

    [SerializeField]
    private Image thirdStageImage = null;
    [SerializeField]
    private Image thirdNameImage = null;
    [SerializeField]
    private Image thirdExplanationImage = null;
    [SerializeField]
    private Image thirdIconImage = null;
    [SerializeField]
    private Image[] thirdStoryImages = new Image[7];



    // [SerializeField]
    // private List<GameObject> categoryObjects = new();

    private Vector3 thirdMainPosBefore = new(-350, 0, 0);
    private Vector3 thirdMainPosAfter = new(-350, 950, 0);
    private Vector3 thirdStoryPosBefore = new(-350, -1225, 0);
    private Vector3 thirdStoryPosAfter = new(-350, -32, 0);


    enum UIMoveUpDown
    {
        Up,
        Down
    }
    UIMoveUpDown upDown = UIMoveUpDown.Up;
    float lerpFloat = 0;

    int selectCategory = 1;
    int selectStageValue = 0;

    private bool isUIMove = false;
    private bool isButtonGo;

    private void Start()
    {
        Time.timeScale = 1;
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (isUIMove)
        {
            if (upDown == UIMoveUpDown.Up)
            {
                lerpFloat += thirdUIMoveSpeed;
                thirdMainRectTransform.localPosition = Vector3.Lerp(thirdMainPosBefore, thirdMainPosAfter, lerpFloat);
                thirdStoryRectTransform.localPosition = Vector3.Lerp(thirdStoryPosBefore, thirdStoryPosAfter, lerpFloat);
                if (thirdMainRectTransform.localPosition == thirdMainPosAfter)
                {
                    isButtonGo = false;
                    isUIMove = false;
                }
            }
            else
            {
                lerpFloat += thirdUIMoveSpeed;
                thirdMainRectTransform.localPosition = Vector2.Lerp(thirdMainPosAfter, thirdMainPosBefore, lerpFloat);
                thirdStoryRectTransform.localPosition = Vector2.Lerp(thirdStoryPosAfter, thirdStoryPosBefore, lerpFloat);
                if (thirdMainRectTransform.localPosition == thirdMainPosBefore)
                {
                    isButtonGo = false;
                    isUIMove = false;
                }
            }
        }
    }
    public void ButtonGameStart()
    {
        if (isButtonGo)
        {
            return;
        }
        isButtonGo = true;
        dataScriptableObject.selectStageValue = new Vector2(selectCategory, selectStageValue);
        StartCoroutine(LoadYourAsyncScene("Stage"));
    }
    IEnumerator LoadYourAsyncScene(string name)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void ButtonMainStorySwitching(bool value)
    {
        if (isButtonGo)
        {
            return;
        }
        isButtonGo = true;
        if (value)
        {
            isButtonGo = true;
            isUIMove = true;
            upDown = UIMoveUpDown.Up;
            lerpFloat = 0f;
        }
        else
        {
            isButtonGo = true;
            isUIMove = true;
            upDown = UIMoveUpDown.Down;
            lerpFloat = 0f;
        }
    }

    public void ButtonSelectStageValue(int value)
    {
        if (isButtonGo)
        {
            return;
        }
        selectStageValue = value;
        thirdMainRectTransform.localPosition = thirdMainPosBefore;
        thirdStoryRectTransform.localPosition = thirdStoryPosBefore;
        thirdObject.SetActive(true);
        for (int i = 0; i < dataScriptableObject.stageContents.Count; i++)
        {
            if (dataScriptableObject.stageContents[i].stageValue == new Vector2(1, selectStageValue))
            {
                Debug.Log("?¿½Ä‚Ño?¿½?¿½");
                thirdStageImage.sprite = dataScriptableObject.stageContents[i].stageSprite;
                thirdNameImage.sprite = dataScriptableObject.stageContents[i].nameSprite;
                thirdExplanationImage.sprite = dataScriptableObject.stageContents[i].explanationSprite;
                thirdIconImage.sprite = dataScriptableObject.stageContents[i].iconSprite;

                for (int j = 0; j < dataScriptableObject.stageContents[i].stageProgressFlags.Length; j++)
                {
                    switch (j)
                    {
                        case 0:
                            if (dataScriptableObject.stageContents[i].stageProgressFlags[j])
                            {
                                thirdStoryImages[4].sprite = dataScriptableObject.stageContents[i].storySprites[4];
                            }
                            else
                            {
                                thirdStoryImages[4].sprite = null;
                            }
                            break;
                        case 1:
                            if (dataScriptableObject.stageContents[i].stageProgressFlags[j])
                            {
                                thirdStoryImages[1].sprite = dataScriptableObject.stageContents[i].storySprites[1];
                                thirdStoryImages[5].sprite = dataScriptableObject.stageContents[i].storySprites[5];
                            }
                            else
                            {
                                thirdStoryImages[1].sprite = null;
                                thirdStoryImages[5].sprite = null;
                            }
                            break; 
                        case 2:
                            if (dataScriptableObject.stageContents[i].stageProgressFlags[j])
                            {
                                thirdStoryImages[2].sprite = dataScriptableObject.stageContents[i].storySprites[2];
                                thirdStoryImages[6].sprite = dataScriptableObject.stageContents[i].storySprites[6];
                            }
                            else
                            {
                                thirdStoryImages[2].sprite = null;
                                thirdStoryImages[6].sprite = null;
                            }
                            break;
                    }
                    
                }
                thirdStoryImages[0].sprite = dataScriptableObject.stageContents[i].storySprites[0];
                if (dataScriptableObject.stageContents[i].stageClear)
                {
                    thirdStoryImages[3].sprite = dataScriptableObject.stageContents[i].storySprites[3];
                }
                else
                {
                    thirdStoryImages[3].sprite = null;
                }
            }
        }

    }

    public void BackStageSelect(){
        if (isButtonGo){
            return;
        };
        thirdObject.SetActive(false);
    }

    // public void ButtonCategorySelectForward(int value)
    // {
    //     if (isButtonGo)
    //     {
    //         return;
    //     }
    //     selectCategory = value;
    //     secondObject.SetActive(true);
    //     categoryObjects[selectCategory].SetActive(true);
    //     firstObject.SetActive(false);
    // }
    // public void ButtonCategorySelectBack()
    // {
    //     if (isButtonGo)
    //     {
    //         return;
    //     }
    //     firstObject.SetActive(true);
    //     for (int i = 0; i < categoryObjects.Count; i++)
    //     {
    //         categoryObjects[i].SetActive(false);
    //     }
    //     secondObject.SetActive(false);
    // }
}
