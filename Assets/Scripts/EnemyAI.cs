using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
    public NavMeshAgent agent;
    public GameObject player;
    public LayerMask whatIsGround, whatIsPlayer;
    private EnemyAnimator enemyAnimator;
    [SerializeField] private AudioSource swingSound;
    [SerializeField] private int damage; 

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    public float attackTime;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<EnemyAnimator>();
    }

    private void Update() {
        enemyAnimator.WalkAnim(1f);
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }
    
    private void Patroling() {
        // If a walkpoint isnt set, set a new one
        if (!walkPointSet) SetWalkPoint();

        // If walkpoint is set, make the object go to it
        if (walkPointSet)
            agent.SetDestination(walkPoint);
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // If the player isn't moving, clear the walkpoint set, so a new one can be set
        if (distanceToWalkPoint.magnitude < 0.1f)
            walkPointSet = false;
    }

    private void SetWalkPoint() {
        // Picks a random point withing the walkPointRange
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer() {
        agent.SetDestination(player.transform.position);
    }

    private void AttackPlayer() {
        agent.SetDestination(transform.position);
        transform.LookAt(player.transform);
        
        if (alreadyAttacked) return;

        enemyAnimator.AttackAnim(); // Player the attack animation
        StartCoroutine(AttackCoroutine()); // Start the attack coroutine
        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks); // Start a cooldown function
    }

    private IEnumerator AttackCoroutine() {
        yield return new WaitForSeconds(attackTime);
        player.GetComponent<PlayerHealth>().TakeDamage(damage);
        swingSound.Play();
    }

    private void ResetAttack() {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected() {
        // Only visible in scene view
        // Shows the sight and attack range as spheres
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}