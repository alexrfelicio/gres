using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageSwiper : MonoBehaviour {

    [SerializeField] GameObject leftArrow;
    [SerializeField] GameObject rightArrow;

    private int currentPanel = 1;
    private float easing = 0.5f;
    private float panelWidth;
    private Vector3 rightMove;
    private Vector3 leftMove;

    void Start() {
        RectTransform rt = (RectTransform) transform.Find("Level 1");
        panelWidth = rt.rect.width;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentPanel-1 > 0) {
            MoveRight();
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && currentPanel+1 < 6) {
            MoveLeft();
        }

        if (currentPanel == 1) {
            leftArrow.GetComponent<Button>().interactable = false;
        } else if (currentPanel == 5) {
            rightArrow.GetComponent<Button>().interactable = false;
        } else {
            leftArrow.GetComponent<Button>().interactable = true;
            rightArrow.GetComponent<Button>().interactable = true;
        }
    }

    public void MoveLeft() {
        currentPanel++;
        leftMove = new Vector3(transform.position.x - panelWidth, transform.position.y, transform.position.z);
        StartCoroutine(SmoothMove(transform.position, leftMove, easing));
    }

    public void MoveRight() {
        currentPanel--;
        rightMove = new Vector3(transform.position.x + panelWidth, transform.position.y, transform.position.z);
        StartCoroutine(SmoothMove(transform.position, rightMove, easing));
    }

    IEnumerator SmoothMove(Vector3 source, Vector3 target, float seconds) {
        float t = 0f;
        while (t <= 1.0f) {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(source, target, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    public int GetCurrentLevel() {
        return currentPanel;
    }
}