using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

    private Vector2 destination;
    private bool onWater = false;
    private float speed = 3f;

    private void Start() {
        destination = transform.position;
    }

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && !onWater) {
            HitDirection direction = ReturnDirection(collision.gameObject, gameObject);
            switch (direction) {
                case HitDirection.Right:
                    destination = new Vector2(transform.position.x - 1, transform.position.y);
                    break;
                case HitDirection.Left:
                    destination = new Vector2(transform.position.x + 1, transform.position.y);
                    break;
                case HitDirection.Top:
                    destination = new Vector2(transform.position.x, transform.position.y - 1);
                    break;
                case HitDirection.Bottom:
                    destination = new Vector2(transform.position.x, transform.position.y + 1);
                    break;
                default:
                    break;
            }
        } else if (collision.tag == "Water") {
            onWater = true;
        }
    }

    private enum HitDirection { None, Top, Bottom, Left, Right }
    private HitDirection ReturnDirection(GameObject collider, GameObject objectHit) {
        if (collider.transform.position.x > objectHit.transform.position.x) {
            return HitDirection.Right;
        } else if (collider.transform.position.x < objectHit.transform.position.x) {
            return HitDirection.Left;
        } else if (collider.transform.position.y > objectHit.transform.position.y) {
            return HitDirection.Top;
        } else if (collider.transform.position.y < objectHit.transform.position.y) {
            return HitDirection.Bottom;
        } else {
            return HitDirection.None;
        }
    }
}
