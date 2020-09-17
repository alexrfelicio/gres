using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour {

    private int mBattery = 20;
    private bool mDead = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("LOSE!");
        mDead = true;
    }

    public bool AllowedToMove() {
        Debug.Log("Battery remaining: " + mBattery);
        bool allow = (mBattery > 0);
        mBattery--;
        return allow && !mDead;
    }
}
