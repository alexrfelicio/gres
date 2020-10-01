using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] GameObject itemDetail;
    [SerializeField] GameObject gameOverModal;
    [SerializeField] GameObject winModal;
    [SerializeField] GameObject quitModal;

    [SerializeField] Sprite fire;
    [SerializeField] Sprite hole;
    [SerializeField] Sprite battery;
    [SerializeField] Sprite enemy;
    [SerializeField] Sprite empty;

    private int currentScore = 0;

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

    /*public void ShowGameOverModal(Death death) {
        StartCoroutine(Death(death))
    }*/

    public void ShowQuitModal() {
        quitModal.SetActive(true);
        Time.timeScale = 0f;
    }

    IEnumerator RestartLevel() {
        yield return new WaitForSeconds(3);
        FindObjectOfType<ScenesManager>().ResetLevel();
    }

    /*IEnumerator Death(Death death) {
        yield return new WaitForSeconds(3);
        Time.timeScale = 0f;
        var imageObj = itemDetail.transform.Find("Artifact Image");
        var titleObj = itemDetail.transform.Find("Artifact Title");
        var textObj = itemDetail.transform.Find("Artifact Text");
        imageObj.GetComponent<Image>().sprite = image;
        titleObj.GetComponent<Text>().text = LangResolver.Instance.GetTextByKey(artifactTitle);
        textObj.GetComponent<Text>().text = LangResolver.Instance.GetTextByKey(artifact);
        itemDetail.SetActive(true);
    }*/

    public void ReturnToLevelSelect() {
        FindObjectOfType<ScenesManager>().LoadLevel();
    }

    public void CloseModal() {
        Time.timeScale = 1f;
        itemDetail.SetActive(false);
        //gameOverModal.SetActive(false);
        //winModal.SetActive(false);
        quitModal.SetActive(false);
    }

}
