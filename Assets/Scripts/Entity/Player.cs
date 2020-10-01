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
    private Vector2 waterMove;
    private UIManager uiManager;
    
    private bool isDead;
    private bool isMoving;
    private bool isWaterMoving;
    private int battery = 50;
    private int step = 0;
    private float moveSpeed = 2f;
    private float waterTime = 2f;

    private void Start() {
        waterMove = Vector2.zero;
        animator = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            FindObjectOfType<UIManager>().ShowQuitModal();
        }
        if (battery == 0 || isDead) {
            StartCoroutine(Death());
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
                ManageBatteryAndSteps();
                controller.MoveEnemies();
                controller.CheckFires(step);
                waterMove = Vector2.zero;
            } else if (waterMove != Vector2.zero && (waterTime <= 0 || isWaterMoving)) {
                var targetPos = waterMove;
                if (!IsWalkable(targetPos)) return;
                StartCoroutine(Move(targetPos));
                waterMove = Vector2.zero;
                waterTime = 2f;
            }
        }
        waterTime -= Time.deltaTime;
    }

    private void ManageBatteryAndSteps() {
        step++;
        battery--;
    }

    IEnumerator Move(Vector3 targetPos) {
        isMoving = true;
        animator.SetBool("IsMoving", isMoving);
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) {
            transform.position =
                Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
        isWaterMoving = false;
        animator.SetBool("IsMoving", isMoving);
    }

    IEnumerator Death() {
        animator.SetBool("IsDead", isDead);
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsDead", false);
    }

    public void SetWaterMove(Vector2 move) {
        waterMove = move;
        isWaterMoving = (!isWaterMoving) ? true : false;
    }

    private void IsMoveable(Vector3 target) {
        if (Physics2D.OverlapCircle(target, 0.3f, interactable) != null) {
            battery--;
        }
    }

    private bool IsWalkable(Vector3 target) {
        return (Physics2D.OverlapCircle(target, 0.01f, walkable) != null) ? true : false;
    }

    public void Dead() {
        isDead = true;
    }
}
