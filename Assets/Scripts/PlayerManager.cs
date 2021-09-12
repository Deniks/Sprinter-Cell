using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    InputManager inputManager;
    CameraManager cameraManager;
    PlayerLocomotion playerLocomotion;
    EnemyAnimatorController enemyAnimatorController;
    public bool isInteracting;

    [SerializeField]
    Text coinText;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        enemyAnimatorController = FindObjectOfType<EnemyAnimatorController>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        if (GameManager.isPaused || GameManager.isGameOver)
        {
            playerLocomotion.rigidbody.isKinematic = true;
            animator.enabled = false;
        }
        else
        {
            playerLocomotion.HandleAllMovement();
            animator.enabled = true;
        }
    }

    private void LateUpdate()
    {
        if (!(GameManager.isPaused || GameManager.isGameOver))
        {
            cameraManager.HandleAllCameraMovement();

            isInteracting = animator.GetBool("isInteracting");
            playerLocomotion.isJumping = animator.GetBool("isJumping");
            animator.SetBool("isGrounded", playerLocomotion.isGrounded);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyAnimatorController.UpdateAnimatorValues("isPlayerCaught", true);
        }



    }

    [SerializeField]


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyAnimatorController.UpdateAnimatorValues("isPlayerCaught", false);
        }

        if (other.CompareTag("Coin"))
        {
            GameManager.playerCoins += 1;
            coinText.text = $": {GameManager.playerCoins}";
        }
    }

    public void setPlayerPosition(Vector3 positon)
    {
        gameObject.transform.position = positon;
    }
}
