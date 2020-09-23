using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    [SerializeField] Player mPlayer;
    [SerializeField] Enemy[] enemies;

    private bool mIsMoving = false;

    private int mCameraMovement = 50;

    private float mSpeed = 2f;

    private Camera mCamera;
    private Vector3 mDestination;


    private void Start() {
        mDestination = mPlayer.transform.position;
        mCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update() {
        if (Moving() && PlayerMoved(mPlayer.transform.position)) {
            float xPos = mPlayer.transform.position.x;
            float yPos = mPlayer.transform.position.y;
 
            if (Input.GetKeyDown(KeyCode.Keypad1)) {
                mDestination = new Vector3(xPos - 1, yPos - 0.5f, mPlayer.transform.position.z);
            } else if (Input.GetKeyDown(KeyCode.Keypad3)) {
                mDestination = new Vector3(xPos + 1, yPos - 0.5f, mPlayer.transform.position.z);
            } else if (Input.GetKeyDown(KeyCode.Keypad7)) {
                mDestination = new Vector3(xPos - 1, yPos + 0.5f, mPlayer.transform.position.z);
            } else if (Input.GetKeyDown(KeyCode.Keypad9)) {
                mDestination = new Vector3(xPos + 1, yPos + 0.5f, mPlayer.transform.position.z);
            }

            
            if (mPlayer.AllowedToMove()) {
                mPlayer.SetDestination(mDestination);
            }

            for (int i = 0; i < enemies.Length; i++) {
                enemies[i].SetDestination();
            }
        }


        if (Input.GetKey(KeyCode.LeftArrow)) {
            mCamera.transform.position = Vector3.MoveTowards(
                mCamera.transform.position,
                new Vector3(mCamera.transform.position.x - mCameraMovement, mCamera.transform.position.y, mCamera.transform.position.z),
                mSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            mCamera.transform.position = Vector3.MoveTowards(
                mCamera.transform.position,
                new Vector3(mCamera.transform.position.x + mCameraMovement, mCamera.transform.position.y, mCamera.transform.position.z),
                mSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.UpArrow)) {
            mCamera.transform.position = Vector3.MoveTowards(
                mCamera.transform.position,
                new Vector3(mCamera.transform.position.x, mCamera.transform.position.y + mCameraMovement, mCamera.transform.position.z),
                mSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            mCamera.transform.position = Vector3.MoveTowards(
                mCamera.transform.position,
                new Vector3(mCamera.transform.position.x, mCamera.transform.position.y - mCameraMovement, mCamera.transform.position.z),
                mSpeed * Time.deltaTime);
        }

    }

    private bool Moving() {
        return (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Keypad3)
            || Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Keypad9));
    }

    private bool PlayerMoved(Vector3 currentPosition) {
        return (Vector3.Distance(currentPosition, mDestination) < 0.1f);
    }
}
