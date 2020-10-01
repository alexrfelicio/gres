using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageSwiper : MonoBehaviour {

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
            currentPanel--;
            rightMove = new Vector3(transform.position.x + panelWidth, transform.position.y, transform.position.z);
            StartCoroutine(SmoothMove(transform.position, rightMove, easing));
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && currentPanel+1 < 6) {
            currentPanel++;
            leftMove = new Vector3(transform.position.x - panelWidth, transform.position.y, transform.position.z);
            StartCoroutine(SmoothMove(transform.position, leftMove, easing));
        }
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
