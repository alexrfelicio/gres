using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour{
    private readonly string START_SCENE = "StartScene";
    private readonly string OPTIONS_SCENE = "OptionsScene";
    private readonly string LEVEL_PREFFIX = "Level ";

    private int currentIndex = 1;

    public void LoadStartScene() {
        SceneManager.LoadScene(START_SCENE);
    }

    public void BackOptionsScene() {
        var volume = FindObjectOfType<OptionsController>().GetCurrentVolume();
        var language = FindObjectOfType<OptionsController>().GetCurrentLanguage();
        GameSystem.SaveOptionsData(volume, language);
        SceneManager.LoadScene(START_SCENE);
    }

    public void LoadOptionsScene() {
        SceneManager.LoadScene(OPTIONS_SCENE);
    }

    public void LoadLevel() {
        var level = LEVEL_PREFFIX + currentIndex.ToString();
        currentIndex++;
        SceneManager.LoadScene(level);
    }

    public void ResetLevel() {
        SceneManager.LoadScene("Level 1");
    }

    public void SetPortuguse() {
        FindObjectOfType<ButtonManager>().SelectedPTBR();
        LangResolver.Instance.SetSystemLanguage(SystemLanguage.Portuguese);
        FindObjectOfType<OptionsController>().SetCurrentLanguage(0);
    }

    public void SetEnglish() {
        FindObjectOfType<ButtonManager>().SelectedENUS();
        LangResolver.Instance.SetSystemLanguage(SystemLanguage.English);
        FindObjectOfType<OptionsController>().SetCurrentLanguage(1);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
