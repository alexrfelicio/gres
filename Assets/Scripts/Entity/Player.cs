using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour {

    [SerializeField] EnvironmentController controller;
    [SerializeField] LayerMask interactable;
    [SerializeField] LayerMask walkable;

    private Animator animator;
    private Vector2 input;
    private UIManager uiManager;
    
    private bool isDead;
    private bool isMoving;
    private int battery = 50;
    private int step = 0;
    private float moveSpeed = 2f;

    private void Start() {
        animator = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
        uiManager.SetBattery(battery);
    }

    private void Update() {
        if (battery == 0 || isDead) {
            uiManager.LoseGame();
            return;
        }
        if (!isMoving) {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxis("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero) {
                float verticalMovement = 0;
                if (input.y > 0) {
                    verticalMovement = Vector2.up.y;
                } else if (input.y < 0) {
                    verticalMovement = Vector2.down.y;
                } else {
                    verticalMovement = 0;
                }
                animator.SetFloat("Horizontal", input.x);
                animator.SetFloat("Vertical", verticalMovement);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += verticalMovement;

                if (!IsWalkable(targetPos)) return;
                IsMoveable(targetPos);
                StartCoroutine(Move(targetPos));
                controller.MoveEnemies();
                controller.CheckFires(step);
            }
        }
    }

    IEnumerator Move(Vector3 targetPos) {
        step++;
        isMoving = true;
        animator.SetBool("IsMoving", isMoving);
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) {
            transform.position =
                Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        battery--;
        uiManager.SetBattery(battery);
        isMoving = false;
        animator.SetBool("IsMoving", isMoving);
    }

    private void IsMoveable(Vector3 target) {
        if (Physics2D.OverlapCircle(target, 0.3f, interactable) != null) {
            battery--;
        }
    }

    private bool IsWalkable(Vector3 target) {
        return (Physics2D.OverlapCircle(target, 0.3f, walkable) != null) ? true : false;
    }

    public void Dead() {
        isDead = true;
    }
}
