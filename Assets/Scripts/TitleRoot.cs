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
    private GameObject windowedSizeSettingObject = null;


    [SerializeField]
    private Slider seSlider = null;
    [SerializeField]
    private Slider bgmSlider = null;


    
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
        soundSettingObject.SetActive(true);
        windowedSizeSettingObject.SetActive(false);
    }
    public void ButtonWindowedSizeSetting()
    {
        soundSettingObject.SetActive(false);
        windowedSizeSettingObject.SetActive(true);
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

    public void SetResolution(int width, int height,FullScreenMode screen)
    {
        // 第3引数にはフルスクリーンかどうかを指定できます。
        // 現在のフルスクリーン状態を維持したい場合はScreen.fullScreenを渡します。
        Screen.SetResolution(width, height, screen);
    }

    public void FullScreen1920()
    {
        SetResolution(1920, 1080,FullScreenMode.FullScreenWindow);
    }

    public void Windowed1280()
    {
        SetResolution(1280, 720, FullScreenMode.Windowed);
    }
}
