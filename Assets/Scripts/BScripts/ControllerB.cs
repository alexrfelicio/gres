using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerB : MonoBehaviour
{
    [SerializeField] PlayerB mPlayer;
    [SerializeField] EnemyB[] enemies;

    private bool mIsMoving = false;

    private int mCameraMovement = 50;

    private float mSpeed = 2f;

    private Camera mCamera;
    private Vector3 mDestination;


    private void Start()
    {
        mDestination = mPlayer.transform.position;
        mCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Moving())
        {
            float xPos = mPlayer.transform.position.x;
            float yPos = mPlayer.transform.position.y;

            if (Input.GetKeyDown(KeyCode.S))
            {
                mDestination = new Vector3(xPos - 1, yPos - 0.5f, mPlayer.transform.position.z);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                mDestination = new Vector3(xPos + 1, yPos - 0.5f, mPlayer.transform.position.z);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                mDestination = new Vector3(xPos - 1, yPos + 0.5f, mPlayer.transform.position.z);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                mDestination = new Vector3(xPos + 1, yPos + 0.5f, mPlayer.transform.position.z);
            }

            if (mPlayer.AllowedToMove())
            {
                mPlayer.transform.position = mDestination;
            }

            for (int i = 0; i < enemies.Length; i++)
            {
                Vector3 destination = enemies[i].GetDestination();
                enemies[i].transform.position = destination;
            }
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mCamera.transform.position = Vector3.MoveTowards(
                mCamera.transform.position,
                new Vector3(mCamera.transform.position.x - mCameraMovement, mCamera.transform.position.y, mCamera.transform.position.z),
                mSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            mCamera.transform.position = Vector3.MoveTowards(
                mCamera.transform.position,
                new Vector3(mCamera.transform.position.x + mCameraMovement, mCamera.transform.position.y, mCamera.transform.position.z),
                mSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            mCamera.transform.position = Vector3.MoveTowards(
                mCamera.transform.position,
                new Vector3(mCamera.transform.position.x, mCamera.transform.position.y + mCameraMovement, mCamera.transform.position.z),
                mSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            mCamera.transform.position = Vector3.MoveTowards(
                mCamera.transform.position,
                new Vector3(mCamera.transform.position.x, mCamera.transform.position.y - mCameraMovement, mCamera.transform.position.z),
                mSpeed * Time.deltaTime);
        }

    }
   
    private bool Moving()
    {
        return (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D));
    }
}
