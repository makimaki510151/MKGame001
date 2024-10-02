using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSlectRoot : MonoBehaviour
{
    [SerializeField]
    private DataScriptableObject dataScriptableObject = null;
    [SerializeField]
    private GameObject firstObject = null;
    [SerializeField]
    private GameObject secondObject = null;
    [SerializeField]
    private GameObject thirdObject = null;

    [SerializeField]
    private List<GameObject> categoryObjects = new();


    private bool isButtonGo;
    public void ButtonStageSelect(int value)
    {
        if (isButtonGo)
        {
            return;
        }
        isButtonGo = true;
        dataScriptableObject.selectStageValue = new Vector2(1, value);
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

    public void ButtonCategorySelectForward(int value)
    {
        secondObject.SetActive(true);
        categoryObjects[value].SetActive(true);
        firstObject.SetActive(false);
    }
    public void ButtonCategorySelectBack()
    {
        firstObject.SetActive(true);
        for(int i = 0; i < categoryObjects.Count; i++)
        {
            categoryObjects[i].SetActive(false);
        }
        secondObject.SetActive(false);
    }
}
