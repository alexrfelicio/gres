using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject mWaypoints;
    [SerializeField] bool mRepeatMovement;
    private int mCurrentWaypoint = 0;
    private bool mForward = true;
    private Vector3 mDestination;
    private float mSpeed = 3f;

    private void Start() {
        mDestination = transform.position;
    }

    private void Update() {
        transform.position =
            Vector3.MoveTowards(transform.position, mDestination, mSpeed * Time.deltaTime);
    }

    public void SetDestination() {
        int max = mWaypoints.transform.childCount;

        Debug.Log("mCurrentWaypoint: " + mCurrentWaypoint);
        
        if (mCurrentWaypoint == max - 1) {
            mForward = false;
            if (!mRepeatMovement) {
                mCurrentWaypoint = 0;
            }
        }

        if (mCurrentWaypoint == 0) {
            mForward = true;
        }

        if (mRepeatMovement) {
            if (mForward) {
                mCurrentWaypoint++;
            } else {
                mCurrentWaypoint--;
            }
        } else {
            mCurrentWaypoint++;
        }
        
        mDestination = mWaypoints.transform.GetChild(mCurrentWaypoint).transform.position;
    }
}
