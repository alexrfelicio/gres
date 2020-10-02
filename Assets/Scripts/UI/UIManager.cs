using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private const int BATTERY_SCORE = 50;
    private const int ARTIFACT_SCORE = 750;
    private const int ALL_ARTIFACTS_SCORE = 250;

    [SerializeField] int currentLevel;
    [SerializeField] GameObject itemDetail;
    [SerializeField] GameObject gameOverModal;
    [SerializeField] GameObject winModal;
    [SerializeField] GameObject quitModal;

    [SerializeField] Sprite fire;
    [SerializeField] Sprite hole;
    [SerializeField] Sprite battery;
    [SerializeField] Sprite enemy;
    [SerializeField] Sprite empty;

    [SerializeField] Artifacts[] artifacts;

    private int highestScore = 0;
    private int currentScore = 0;
    private int artifactsFounded = 0;

    private void Awake() {
        if (GamePersist.Instance.levels != null) {
            highestScore = GamePersist.Instance.levels[currentLevel][0];
        }
    }

    public void ShowItemDetail(Sprite image, string artifactTitle, string artifact) {
        Time.timeScale = 0f;
        var imageObj = itemDetail.transform.Find("Artifact Image");
        var titleObj = itemDetail.transform.Find("Artifact Title");
        var textObj = itemDetail.transform.Find("Artifact Text");
        imageObj.GetComponent<Image>().sprite = image;
        titleObj.GetComponent<Text>().text = LangResolver.Instance.GetTextByKey(artifactTitle);
        textObj.GetComponent<Text>().text = LangResolver.Instance.GetTextByKey(artifact);
        itemDetail.SetActive(true);
    }

    public void ShowGameOverModal(Death death) {
        StartCoroutine(Death(death));
    }

    public void ShowWinModal(int battery) {
        Time.timeScale = 0f;
        Color enabled = new Color(1f, 1f, 1f);
        Color disabled = new Color(0.1f, 0.1f, 0.1f);
        int batteryValue = 0;
        int artifactValue = 0;
        int allArtifactValue = 0;
        var batteryObj = winModal.transform.Find("Battery Text");
        var artifactObj = winModal.transform.Find("Artifact Text");
        var allArtifactObj = winModal.transform.Find("All Artifacts Text");
        var totalObj = winModal.transform.Find("Total");

        string batteryText = LangResolver.Instance.GetTextByKey("win_battery");
        string artifactText = LangResolver.Instance.GetTextByKey("win_artifact");
        string allArtifactsText = LangResolver.Instance.GetTextByKey("win_all_artifact");
        string scoreText = (currentScore > highestScore) ?
            LangResolver.Instance.GetTextByKey("win_score") :
            LangResolver.Instance.GetTextByKey("win_high_score");

        batteryValue = battery * BATTERY_SCORE;
        batteryText += " " + battery.ToString() + "x" + BATTERY_SCORE.ToString() + " = +" + batteryValue.ToString();
        batteryObj.GetComponent<Text>().text = batteryText;

        artifactValue = artifactsFounded * ARTIFACT_SCORE;
        artifactText += " " + artifactsFounded.ToString() + "x" + ARTIFACT_SCORE.ToString() + " = +" + artifactValue.ToString();
        artifactObj.GetComponent<Text>().text = artifactText;

        if (artifactsFounded == artifacts.Length) {
            allArtifactValue = artifactsFounded * ALL_ARTIFACTS_SCORE;
            allArtifactsText += " " + artifactsFounded.ToString() + "x" + ALL_ARTIFACTS_SCORE.ToString() + " = +" + allArtifactValue.ToString();
            allArtifactObj.GetComponent<Text>().text = allArtifactsText;
            allArtifactObj.gameObject.SetActive(true);
        }

        int scoreValue = batteryValue + artifactValue + allArtifactValue;
        scoreText += " " + scoreValue.ToString();
        totalObj.GetComponent<Text>().text = scoreText;
        for (int i = 0; i < artifacts.Length; i++) {
            var artifactSpriteObj = winModal.transform.Find("Artifact " + (i+1));
            artifactSpriteObj.GetComponent<Image>().sprite = artifacts[i].sprite;
            artifactSpriteObj.GetComponent<Image>().color = (artifacts[i].found) ? enabled : disabled;
        }
        winModal.SetActive(true);
        GameSystem.SaveLevelData(currentLevel, scoreValue, artifacts[0].found, artifacts[1].found, artifacts[2].found, artifacts[3].found);
    }

    public void ShowQuitModal() {
        quitModal.SetActive(true);
        Time.timeScale = 0f;
    }

    IEnumerator Death(Death death) {
        yield return new WaitForSeconds(2f);
        var imageObj = gameOverModal.transform.Find("Image Reason");
        var textObj = gameOverModal.transform.Find("Game Over Reason");
        imageObj.GetComponent<Image>().sprite = GetDeathSprite(death);
        textObj.GetComponent<Text>().text = LangResolver.Instance.GetTextByKey(GetDeathReason(death));
        gameOverModal.SetActive(true);
    }

    public void ReturnToLevelSelect() {
        FindObjectOfType<ScenesManager>().LoadLevel();
    }

    public void CloseModal() {
        Time.timeScale = 1f;
        itemDetail.SetActive(false);
        gameOverModal.SetActive(false);
        winModal.SetActive(false);
        quitModal.SetActive(false);
    }

    private Sprite GetDeathSprite(Death death) {
        switch(death) {
            case global::Death.Fire:
                return fire;
            case global::Death.Hole:
                return hole;
            case global::Death.Battery:
                return battery;
            case global::Death.Enemy:
                return enemy;
            case global::Death.Empty:
                return empty;
            default:
                return null;
        }
    }

    private string GetDeathReason(Death death) {
        switch (death) {
            case global::Death.Fire:
                return "fire_death";
            case global::Death.Hole:
                return "hole_death";
            case global::Death.Battery:
                return "battery_death";
            case global::Death.Enemy:
                return "enemy_death";
            case global::Death.Empty:
                return "empty_death";
            default:
                return null;
        }
    }

    public void MarkAsFounded(string id) {
        for (int i = 0; i < artifacts.Length; i++) {
            if (artifacts[i].id == id) {
                artifacts[i].found = true;
                artifactsFounded++;
            }
        }
    }

    [System.Serializable]
    public class Artifacts {
        public string id;
        public Sprite sprite;
        public bool found = false;
    }
}
