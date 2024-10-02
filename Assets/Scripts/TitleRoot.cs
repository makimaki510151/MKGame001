using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleRoot : MonoBehaviour
{
    [SerializeField]
    private DataScriptableObject dataScriptableObject = null;

    [SerializeField]
    private GameObject mainPageObject = null;
    [SerializeField]
    private GameObject settingPageObject = null;
    
    [SerializeField]
    private GameObject soundSettingObject = null;
    [SerializeField]
    private Slider seSlider = null;
    [SerializeField]
    private Slider bgmSlider = null;


    [SerializeField]
    private GameObject keyConfigSettingObject = null;

    private bool isButtonGo = false;

    private void Start()
    {
        seSlider.value = dataScriptableObject.seVolume;
        bgmSlider.value = dataScriptableObject.bgmVolume;
    }

    public void ButtonStart()
    {
        if(isButtonGo)
        {
            return;
        }
        isButtonGo = true;
        StartCoroutine(LoadYourAsyncScene("StageSelect"));
    }

    public void ButtonSetting()
    {
        mainPageObject.SetActive(false);
        settingPageObject.SetActive(true);
        ButtonSoundSetting();
    }

    public void ButtonGameEnd()
    {

    }

    public void ButtonSoundSetting()
    {
        keyConfigSettingObject.SetActive(false);
        soundSettingObject.SetActive(true);
    }

    public void SliderChangedSe()
    {
        dataScriptableObject.seVolume = seSlider.value;
    }

    public void SliderChangedBgm()
    {
        dataScriptableObject.bgmVolume = bgmSlider.value;
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
}
