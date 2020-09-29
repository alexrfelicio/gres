using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour {

    [Header("Environment Objects")]
    [SerializeField] GameObject fireObj;
    [SerializeField] GameObject mudObj;
    [Header("Environment Configs")]
    [SerializeField] private Enemy[] enemies;
    [SerializeField] private Fires[] fires;
    [SerializeField] private Mud[] muds;

    private int currentPlayerMovement = 0;
    private List<int> fireSpawns = new List<int>();
    private float fireSpawnTime = 3f;

    private void Start() {
        for (int i = 0; i < fires.Length; i++) {
            if (!fireSpawns.Contains(fires[i].move)) {
                fireSpawns.Add(fires[i].move);
            }
        }
    }

    private void Update() {
        if (fireSpawnTime <= 0) {
            currentPlayerMovement++;
            CheckFires(currentPlayerMovement);
        } else {
            fireSpawnTime -= Time.deltaTime;
        }
    }

    public void MoveEnemies() {
        for (int i = 0; i < enemies.Length; i++) {
            enemies[i].Move();
        }
    }

    public void CheckFires(int move) {
        if (fireSpawns.Contains(move)) {
            Transform firesObject = transform.Find("Fires");
            for (int i = 0; i < fires.Length; i++) {
                if (fires[i].move == move) {
                    Instantiate(
                        fireObj,
                        new Vector2(fires[i].posX, fires[i].posY),
                        transform.rotation,
                        firesObject);
                }
            }
            fireSpawnTime = 3f;
        }
        fireSpawns.Remove(move);
        currentPlayerMovement = move;
    }

    public void CheckMuds(int move) {
        if (fireSpawns.Contains(move)) {
            Transform firesObject = transform.Find("Muds");
            for (int i = 0; i < fires.Length; i++) {
                if (fires[i].move == move) {
                    Instantiate(
                        fireObj,
                        new Vector2(fires[i].posX, fires[i].posY),
                        transform.rotation,
                        firesObject);
                }
            }
            fireSpawnTime = 3f;
        }
        fireSpawns.Remove(move);
        currentPlayerMovement = move;
    }

    [Serializable]
    public class Fires {
        public int move;
        public float posX;
        public float posY;
    }
}
