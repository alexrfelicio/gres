using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour {

    [Header("Environment Objects")]
    [SerializeField] GameObject fireObj;
    [Header("Environment Configs")]
    [SerializeField] private Enemy[] enemies;
    [SerializeField] private Fires[] fires;

    public void MoveEnemies() {
        for (int i = 0; i < enemies.Length; i++) {
            enemies[i].Move();
        }
    }

    public void checkFires(int move) {
        Transform firesObject = transform.Find("Fires");
        for(int i = 0; i < fires.Length; i++) {
            if (fires[i].move == move) {
                Instantiate(
                    fireObj,
                    new Vector2(fires[i].posX,fires[i].posY),
                    transform.rotation,
                    firesObject);
            }
        }
    }

    [Serializable]
    public class Fires {
        public int move;
        public float posX;
        public float posY;
    }
}
