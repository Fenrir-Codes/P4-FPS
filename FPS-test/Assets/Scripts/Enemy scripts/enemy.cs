
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    public float health = 100f;

    public float xPosition = 0f;
    public float zPosition = 0f;

    //private float calmSpeed = 0;
    public float wanderingSpeed = 0.2f;
    public float chasingSpeed = 8f;

    private NavMeshAgent agent = null;

    public Transform Player;
    public Transform Enemy;
    
    Animator animator;

    public Text DistanceText;

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
        if (health <= 0f)
        {
            StartCoroutine(Dying());
        }
    }

    IEnumerator Dying()
    {
        animator.SetBool("ZombieDying", true);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        Destroy(Enemy);
    }

    void movetotarget()
    {
        if (Enemy != null)
        {

            var distance = Vector3.Distance(Enemy.position, Player.position);

            DistanceText.text = distance.ToString();

            if (distance > 20f)
            {
                wandering();
            }
            if (distance < 20f)
            {
                chaseing();
            }
            if (distance < 4f)
            {
                attack();
            }

        }     

    }

    void wandering()
    {
        xPosition = Random.Range(80, 500);
        zPosition = Random.Range(80, 500);

        animator.SetBool("Wandering", true);
        animator.SetBool("ZombieRun", false);
        animator.SetBool("ZombieAttack", false);
        agent.SetDestination(new Vector3(xPosition, 11f, zPosition));
        agent.speed = wanderingSpeed;
    }

    void chaseing()
    {
        animator.SetBool("ZombieRun", true);
        animator.SetBool("Wandering", false);
        animator.SetBool("ZombieAttack", false);
        agent.speed = chasingSpeed;
        agent.SetDestination(Player.position);
    }

    void attack()
    {

        animator.SetBool("ZombieAttack", true);
        animator.SetBool("Wandering", false);
        animator.SetBool("ZombieRun", false);
    }
}
