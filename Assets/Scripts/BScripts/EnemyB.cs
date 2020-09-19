using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : MonoBehaviour
{
    [SerializeField] GameObject mWaypoints;
    [SerializeField] bool mRepeatMovement;
    private int mCurrentWaypoint = 0;
    private bool mForward = true;


    public Vector3 GetDestination()
    {
        int max = mWaypoints.transform.childCount;

        Debug.Log("mCurrentWaypoint: " + mCurrentWaypoint);

        if (mCurrentWaypoint == max - 1)
        {
            mForward = false;
            if (!mRepeatMovement)
            {
                mCurrentWaypoint = 0;
            }
        }

        if (mCurrentWaypoint == 0)
        {
            mForward = true;
        }

        if (mRepeatMovement)
        {
            if (mForward)
            {
                mCurrentWaypoint++;
            }
            else
            {
                mCurrentWaypoint--;
            }
        }
        else
        {
            mCurrentWaypoint++;
        }

        return mWaypoints.transform.GetChild(mCurrentWaypoint).transform.position;
    }
}
