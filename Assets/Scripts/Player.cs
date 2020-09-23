using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour {

    private int mBattery = 20;
    private bool mDead = false;
    private Vector3 mDestination;
    private float mSpeed = 3f;

    private void Start() {
        mDestination = transform.position;
    }

    private void Update() {
        transform.position =
            Vector3.MoveTowards(transform.position, mDestination, mSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("LOSE!");
        mDead = true;
    }

    public void SetDestination(Vector3 destination) {
        mDestination = destination;
    }

    public bool AllowedToMove() {
        Debug.Log("Battery remaining: " + mBattery);
        bool allow = (mBattery > 0);
        mBattery--;
        return allow && !mDead;
    }
}
