using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapse : MonoBehaviour {

    [SerializeField] Hole hole;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            StartCoroutine(StartCollapse());
        }
    }

    IEnumerator StartCollapse() {
        Transform holesObj = transform.parent.Find("Holes");
        yield return new WaitForSeconds(1f);
        Instantiate(
            hole,
            new Vector2(transform.position.x, transform.position.y),
            transform.rotation,
            holesObj
            );
        Destroy(gameObject);
    }
}
