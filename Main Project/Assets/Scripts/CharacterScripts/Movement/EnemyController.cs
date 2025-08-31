using oxi;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    AnimatorHandler animatorHandler;
    Transform player;
    EnemyCombatController combatController;

    public LayerMask whatIsGround, whatIsPlayer;

    [Header("Patrol Settings")]
    bool walkPointSet;
    public Vector3 walkPoint;
    public float walkPointRange;

    [Header("Attack Settings")]
    bool alreadyAttacked;
    public float timeBetweenAttacks;
    public float sightRange, attackRange;

    //States
    [Header("Range")]
    public bool playerInSightRange, playerInAttackRange;

    [Header("DEV Tool")]
    public bool isDummy;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
        animator = animatorHandler.anim;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        combatController = GetComponent<EnemyCombatController>();
        animatorHandler.Initialize();
    }

    private void Update()
    {
        if (isDummy) return;
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        bool isRunning = agent.velocity.magnitude > 0.1f;
        animator.SetBool("run", isRunning);
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX,
                                transform.position.y,
                                transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            combatController.PerformAttack();
            animator.SetTrigger("attack");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
