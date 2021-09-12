using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private EnemyAnimatorController enemyAnimatorController;

    [SerializeField]
    private FollowEnemy followEnemy;

   

    public float agentSpeed;
    void Start()
    {

        followEnemy = GetComponent<FollowEnemy>();
        enemyAnimatorController = GetComponent<EnemyAnimatorController>();
        agent = GetComponent<NavMeshAgent>();
        agentSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        enemyAnimatorController.UpdateAnimatorValues("isPlayerFound", followEnemy.setFollow);
        enemyAnimatorController.UpdateAnimatorValues("isPlayerAboutToBeCaught", followEnemy.isAboutToBeCaught);
        enemyAnimatorController.UpdateAnimatorValues("isJumping", followEnemy.isJumping);
    }
}
