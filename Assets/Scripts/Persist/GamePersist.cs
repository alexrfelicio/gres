using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePersist : MonoBehaviour {

    public static GamePersist Instance { get; private set; }
    public float volume = 1f;
    public int language = 0;
    public Dictionary<int, int[]> levels;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
            ResumeGame();
        } else {
            Destroy(gameObject);
            Instance.Load();
            Instance.ResumeGame();
        }
    }

    public void Load() {
        levels = new Dictionary<int, int[]>();
        var optionsData = GameSystem.LoadOptionsData();
        if (optionsData != null) {
            volume = optionsData.volume;
            language = optionsData.language;
        }

        if (language == 0) {
            LangResolver.Instance.SetSystemLanguage(SystemLanguage.Portuguese);
        } else {
            LangResolver.Instance.SetSystemLanguage(SystemLanguage.English);
        }

        for (int i = 1; i <= 5; i++) {
            var levelData = GameSystem.LoadLevelData(i);
            if (levelData != null) {
                int[] aux = { levelData.score, BoolToInt(levelData.artefato_1),
                    BoolToInt(levelData.artefato_2), BoolToInt(levelData.artefato_3),
                    BoolToInt(levelData.artefato_4) };

                levels[i] = aux;
            } else {
                levels[i] = null;
            }
        }
    }

    private int BoolToInt(bool value) {
        return (value) ? 1 : 0;
    }

    public void PauseGame() {
        Time.timeScale = 0f;
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
    }
}
