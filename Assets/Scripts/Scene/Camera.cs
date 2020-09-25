using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    private int cameraMovement = 50;
    private float speed = 3f;

    void Update() {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(transform.position.x - cameraMovement, transform.position.y, transform.position.z),
                speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(transform.position.x + cameraMovement, transform.position.y, transform.position.z),
                speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.UpArrow)) {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(transform.position.x, transform.position.y + cameraMovement, transform.position.z),
                speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(transform.position.x, transform.position.y - cameraMovement, transform.position.z),
                speed * Time.deltaTime);
        }
    }
}
