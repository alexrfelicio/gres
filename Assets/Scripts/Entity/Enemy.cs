using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject waypoints;
    [SerializeField] bool repeatMovement;
    private int currentWaypoint = 0;
    private bool forward = true;
    private Vector3 destination;
    private float speed = 3f;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<Player>().Dead(Death.Enemy);
        }
    }

    private void Start() {
        destination = transform.position;
    }

    private void Update() {
        transform.position =
            Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }

    public void Move() {
        int max = waypoints.transform.childCount;

        if (currentWaypoint == max - 1) {
            forward = false;
            if (!repeatMovement) {
                currentWaypoint = 0;
            }
        }

        if (currentWaypoint == 0) {
            forward = true;
        }

        if (repeatMovement) {
            if (forward) {
                currentWaypoint++;
            } else {
                currentWaypoint--;
            }
        } else {
            currentWaypoint++;
        }
        
        destination = waypoints.transform.GetChild(currentWaypoint).transform.position;
    }
}
