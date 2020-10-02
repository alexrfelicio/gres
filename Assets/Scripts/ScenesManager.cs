using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour{
    private readonly string START_SCENE = "StartScene";
    private readonly string OPTIONS_SCENE = "OptionsScene";
    private readonly string LEVEL_PREFFIX = "Level ";

    public void LoadStartScene() {
        SceneManager.LoadScene(START_SCENE);
    }

    public void StartLevel() {
        Time.timeScale = 1f;
        string level = LEVEL_PREFFIX + FindObjectOfType<PageSwiper>().GetCurrentLevel();
        SceneManager.LoadScene(level);        
    }

    public void BackOptionsScene() {
        var volume = FindObjectOfType<OptionsController>().GetCurrentVolume();
        var language = FindObjectOfType<OptionsController>().GetCurrentLanguage();
        GameSystem.SaveOptionsData(volume, language);
        FindObjectOfType<GamePersist>().Load();
        SceneManager.LoadScene(START_SCENE);
    }

    public void LoadOptionsScene() {
        SceneManager.LoadScene(OPTIONS_SCENE);
    }

    public void ResetLevel1() {
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1f;
    }

    public void ResetLevel2() {
        SceneManager.LoadScene("Level 2");
        Time.timeScale = 1f;
    }

    public void ResetLevel3() {
        SceneManager.LoadScene("Level 3");
        Time.timeScale = 1f;
    }

    public void ResetLevel4() {
        SceneManager.LoadScene("Level 4");
        Time.timeScale = 1f;
    }

    public void ResetLevel5() {
        SceneManager.LoadScene("Level 5");
        Time.timeScale = 1f;
    }

    public void LoadLevel() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelScene");
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
