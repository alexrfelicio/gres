using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    [SerializeField] int value = 0;
    private void OnTriggerEnter2D(Collider2D collision) {
        FindObjectOfType<UIManager>().SetScore(value);
        Destroy(gameObject);
    }

}
