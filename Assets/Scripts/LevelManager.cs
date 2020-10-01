using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    [SerializeField] GameObject panel;

    private void Awake() {
        if (GamePersist.Instance.levels != null) {
            Color enabled = new Color(1f, 1f, 1f);
            Color disabled = new Color(0.1f, 0.1f, 0.1f);
            for (int i = 1; i <= 5; i++) {
                int[] data = GamePersist.Instance.levels[i];
                var scoreText = GetScoreText(i).GetComponent<Text>();
                scoreText.text = data[0].ToString();

                var artifact1 = GetArtifact(i, 1).GetComponent<Image>();
                var artifact2 = GetArtifact(i, 2).GetComponent<Image>();
                var artifact3 = GetArtifact(i, 3).GetComponent<Image>();
                var artifact4 = GetArtifact(i, 4).GetComponent<Image>();

                artifact1.color = (data[1] == 1) ? enabled : disabled;
                artifact2.color = (data[2] == 1) ? enabled : disabled;
                artifact3.color = (data[3] == 1) ? enabled : disabled;
                artifact4.color = (data[4] == 1) ? enabled : disabled;
            }
        }
    }

    private Transform GetScoreText(int id) {
        string level = "Level " + id;
        return panel.transform.Find(level).Find("Score").Find("Text");
    }

    private Transform GetArtifact(int id, int artifactId) {
        string level = "Level " + id;
        string artifact = "Artifact " + artifactId;
        return panel.transform.Find(level).Find(artifact).Find("Image");
    }
}

