﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    [SerializeField] int value = 0;
    [SerializeField] Sprite sprite;
    [SerializeField] string artifact;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            FindObjectOfType<UIManager>().SetScore(value);
            FindObjectOfType<UIManager>().ShowItemDetail(sprite, artifact);
            Destroy(gameObject);
        }
    }

}
