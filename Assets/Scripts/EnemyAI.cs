using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //PLayer and NavMeshAgent agent:
    public NavMeshAgent agent;
    public Transform player;

    //Layer Masks for ground and player
    public LayerMask whatIsGround, whatIsPlayer;

    //Health Variables (Health of enemy):
     public float health = 10f;

    //Patroling Variables (Walkpoint, walkpoint range, and if walkpoint is set):
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking player variables (Damage, time between attacks, and if enemy has already attacked):
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States of enemy (Patrol, Chase, Attack) and sight and attack range of enemy:
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // Start is called before the first frame update (Find player and agent): 
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame (Check for sight and attack range of player):
    void Update()
    {
    //Check for sight of player and attack range of player (If player is in sight range and not in attack range, patrol, if player is in sight range and
    //in attack range, attack player, if player is not in sight range, patrol):
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached if distance is less than 1 meter (Set walkpoint to false):
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range of enemy:
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Ensure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code here (ANY ATTACK I WANT SWINGING SWORD, SHOOTING, ENERGY BLAST):
             Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            
            //Applying Force of projectile to move forward (32f is the speed of projectile):
             rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //Upward force to make projectile go up (8f is the speed of projectile going up):
             rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            //player.GetComponent<Health>().TakeDamage(attackDamage);
             alreadyAttacked = true;
            //Resets attack function
             Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    //Destroy enemy:
    private void DestroyEnemy()
    {
      Destroy(gameObject);
    }

    //Visualize enemy sight and attack range in editor:
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    
}
