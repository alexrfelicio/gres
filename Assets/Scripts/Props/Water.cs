using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {
    private enum Directions {None, Left, Right, Top, Bottom }
    [SerializeField] Directions direction;
    [SerializeField] bool oneStepAllowed = false;

    private bool haveCrate = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && !haveCrate && !oneStepAllowed) {
            Vector2 target = GetNextPosition();
            collision.GetComponent<Player>().SetWaterMove(target);
        } else if (collision.tag == "Crate") {
            haveCrate = true;
        } else if (collision.tag == "Player" && oneStepAllowed) {
            oneStepAllowed = false;
        }
    }

    private Vector2 GetNextPosition() {
        switch(direction) {
            case Directions.Left:
                return new Vector2(transform.position.x - 1, transform.position.y);
            case Directions.Right:
                return new Vector2(transform.position.x + 1, transform.position.y);
            case Directions.Top:
                return new Vector2(transform.position.x, transform.position.y + 1);
            case Directions.Bottom:
                return new Vector2(transform.position.x, transform.position.y - 1);
            default:
                return transform.position;
        }
    }

}
