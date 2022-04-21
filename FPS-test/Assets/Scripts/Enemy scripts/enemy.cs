
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public float health = 100f;
    private NavMeshAgent agent = null;

    public Transform Player;
    public Transform ZombieRig;
    public Transform Enemy;
    
    Animator animator;

    private void Awake()
    {
       // Player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movetotarget();
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        Debug.Log("taken: " + amount);
        if (health <= 0f)
        {
            Die();
        }
 
    }

    void Die()
    {
        Destroy(gameObject);
        Destroy(Enemy);
    }


    void movetotarget()
    {

        if (Enemy != null)
        {
            var distance = Vector3.Distance(ZombieRig.position, Player.position);

            if (distance < 20f && agent.velocity != null)
            {
                animator.SetBool("EnemyRun", true);
                agent.speed = 10f;

                agent.SetDestination(Player.position);
            }
            else
            {
                animator.SetBool("EnemyRun", false);
            }
            if (distance > 40f)
            {
                animator.SetBool("EnemyWalk", true);

            }
            else
            {
                animator.SetBool("EnemyWalk", false);
            }
            if (distance < 10f)
            {
                animator.SetBool("EnemyAttack", true);

            }
            else
            {
                animator.SetBool("EnemyAttack", false);
            }
            if (distance > 60f)
            {
                animator.SetBool("EnemyAttack", false);
                animator.SetBool("EnemyIdle", false);
                animator.SetBool("EnemyWalk", true);
            }

        }

       




    }
}
