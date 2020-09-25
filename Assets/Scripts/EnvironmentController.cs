using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour {

    [SerializeField] private Enemy[] enemies;

    public void MoveEnemies() {
        for (int i = 0; i < enemies.Length; i++) {
            enemies[i].Move();
        }
    }
}
