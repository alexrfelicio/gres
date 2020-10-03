using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour {

    [SerializeField] EnvironmentController controller;
    [SerializeField] LayerMask interactable;
    [SerializeField] LayerMask mud;
    [SerializeField] LayerMask walkable;
    [SerializeField] AudioClip mudSFX;
    [SerializeField] AudioClip walkSFX;
    [SerializeField] AudioClip waterSFX;

    private Animator animator;
    private Vector2 input;
    private Vector2 waterMove;
    private UIManager uiManager;
    
    private bool isDead;
    private bool isMoving;
    private bool isWaterMoving;
    private bool isFinished;
    private int battery = 50;
    private int step = 0;
    private float moveSpeed = 2f;
    private float SFXVolume;
    private float waterTime = 2f;
    private Death death;

    private void Start() {
        waterMove = Vector2.zero;
        animator = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
        uiManager.SetBattery(battery);
        SFXVolume = GamePersist.Instance.sfx;
        animator.SetFloat("Vertical", -1);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            FindObjectOfType<UIManager>().ShowQuitModal();
        }

        if (battery == 0) {
            this.death = global::Death.Battery;
            isDead = true;
        }

        if (isDead) {
            StartCoroutine(Death());
            return;
        }

        if (isFinished) {
            uiManager.ShowWinModal(battery);
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

                var crateFuturePos = transform.position;
                crateFuturePos.x += (input.x * 2);
                crateFuturePos.y += (verticalMovement * 2);

                if (!IsWalkable(targetPos)) return;

                if (IsMoveable(targetPos)) {
                    if (!IsWalkable(crateFuturePos)) return;
                    if (!IsBlocked(crateFuturePos)) return;
                    battery--;
                }
                if (IsMud(targetPos)) {
                    battery--;
                    AudioSource.PlayClipAtPoint(mudSFX, targetPos, SFXVolume);
                }
                AudioSource.PlayClipAtPoint(walkSFX, targetPos, SFXVolume);
                StartCoroutine(Move(targetPos));
                ManageBatteryAndSteps();
                uiManager.SetBattery(battery);
                controller.MoveEnemies();
                controller.CheckFires(step);
                waterMove = Vector2.zero;
            } else if (waterMove != Vector2.zero && (waterTime <= 0 || isWaterMoving)) {
                var targetPos = waterMove;
                AudioSource.PlayClipAtPoint(waterSFX, targetPos, SFXVolume);
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
        uiManager.ShowGameOverModal(this.death);
    }

    public void SetWaterMove(Vector2 move) {
        isWaterMoving = (!isWaterMoving) ? true : false;
        waterMove = move;
    }

    private bool IsMoveable(Vector3 target) {
        return (Physics2D.OverlapCircle(target, 0.3f, interactable) != null);
    }

    private bool IsMud(Vector3 target) {
        return (Physics2D.OverlapCircle(target, 0.3f, mud) != null);
    }

    private bool IsWalkable(Vector3 target) {
        return (Physics2D.OverlapCircle(target, 0.01f, walkable) != null) ? true : false;
    }

    private bool IsBlocked(Vector3 target) {
        Debug.Log("IsBlocked?");
        Debug.Log((Physics2D.OverlapCircle(target, 0.01f, interactable) != null) ? true : false);
        return (Physics2D.OverlapCircle(target, 0.01f, interactable) != null) ? false : true;
    }

    public void Dead(Death death) {
        isDead = true;
        this.death = death;
    }

    public void Win() {
        isFinished = true;
    }

    public void ChargeBattery(int amount) {
        battery += amount;
        uiManager.SetBattery(battery);
    }

}
