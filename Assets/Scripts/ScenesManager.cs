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
        MusicTheme.Instance.StopAudio();
        string level = LEVEL_PREFFIX + FindObjectOfType<PageSwiper>().GetCurrentLevel();
        SceneManager.LoadScene(level);        
    }

    public void BackOptionsScene() {
        var volume = FindObjectOfType<OptionsController>().GetCurrentVolume();
        var language = FindObjectOfType<OptionsController>().GetCurrentLanguage();
        var sfx = FindObjectOfType<OptionsController>().GetCurrentSFX();
        GameSystem.SaveOptionsData(volume, sfx, language);
        FindObjectOfType<GamePersist>().Load();
        SceneManager.LoadScene(START_SCENE);
    }

    public void LoadOptionsScene() {
        SceneManager.LoadScene(OPTIONS_SCENE);
    }

    public void ResetLevel1() {
        GamePersist.Instance.ResumeGame();
        SceneManager.LoadScene("Level 1");
    }

    public void ResetLevel2() {
        GamePersist.Instance.ResumeGame();
        SceneManager.LoadScene("Level 2");
    }

    public void ResetLevel3() {
        GamePersist.Instance.ResumeGame();
        SceneManager.LoadScene("Level 3");
    }

    public void ResetLevel4() {
        GamePersist.Instance.ResumeGame();
        SceneManager.LoadScene("Level 4");
    }

    public void ResetLevel5() {
        SceneManager.LoadScene("Level 5");
    }

    public void LoadLevel() {
        if (!MusicTheme.Instance.IsPlaying()) {
            MusicTheme.Instance.PlayAudio();
        }
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
