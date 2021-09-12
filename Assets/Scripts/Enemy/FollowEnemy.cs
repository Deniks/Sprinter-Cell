using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowEnemy : MonoBehaviour
{

    [SerializeField]
    NavMeshAgent agent;

    [SerializeField]
    EnemyAnimatorController enemyAnimatorController;
    
    public bool setFollow = true;
    public bool isAboutToBeCaught = false;
    public bool isJumping = false;

    public GameObject playerObject;

    public Vector3 offset;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimatorController = GetComponent<EnemyAnimatorController>();
        enemyAnimatorController.PlayTargetAnimation("Sad Idle");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (setFollow)
        {
            agent.destination = playerObject.transform.position + offset;
            enemyAnimatorController.PlayTargetAnimation("Run");
        }
        if (agent.isOnOffMeshLink)
        {
            Debug.Log("is jumping");
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerObject)
        {
            isAboutToBeCaught = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject == playerObject)
        {
            isAboutToBeCaught = false;
        }
    }


}
