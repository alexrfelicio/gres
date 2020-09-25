using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    [SerializeField] Text scoreText;
    [SerializeField] Text batteryText;
    [SerializeField] Text loseText;

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
        Invoke("ResetGame", 3f);
    }

    private void ResetGame() {
        SceneManager.LoadScene(0);
    }
}
