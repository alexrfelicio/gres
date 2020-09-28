using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] Text scoreText;
    [SerializeField] Text batteryText;
    [SerializeField] Text loseText;
    [SerializeField] GameObject itemDetail;

    private int currentScore = 0;

    public void SetScore(int value) {
        currentScore += value;
        string text = "Score: " + currentScore;
        scoreText.text = text;
    }

    public void SetBattery(int value) {
        string text = "Battery: " + value;
        batteryText.text = text;
    }

    public void LoseGame() {
        loseText.gameObject.SetActive(true);
        StartCoroutine(RestartLevel());
    }

    public void ShowItemDetail(Sprite image, string artifact) {
        var imageObj = itemDetail.transform.Find("Artifact Image");
        var textObj = itemDetail.transform.Find("Artifact Panel").Find("Artifact Text");
        imageObj.GetComponent<Image>().sprite = image;
        textObj.GetComponent<Text>().text = LangResolver.Instance.GetTextByKey(artifact);
        itemDetail.SetActive(true);
        StartCoroutine(RemoveItemDetail());
    }

    IEnumerator RestartLevel() {
        yield return new WaitForSeconds(3);
        FindObjectOfType<ScenesManager>().ResetLevel();
    }

    IEnumerator RemoveItemDetail() {
        yield return new WaitForSeconds(3);
        itemDetail.SetActive(false);
    }

}
