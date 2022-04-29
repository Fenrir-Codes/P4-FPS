
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    public float maxHealt = 100f;
    public float health = 100f;

    public float walkRadius = 0f;

    public float wanderingSpeed = 0.2f;
    public float chasingSpeed = 6f;

    private NavMeshAgent agent = null;


    public Slider slider;
    public Transform Player;
    public Transform Enemy;

    Animator animator;

    public Text DistanceText;

    private void Awake()
    {
       // Player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = maxHealt;
    }

    private void Update()
    {
        calculateHealth();
        movetotarget();
    }

    void calculateHealth()
    {
        slider.value = health;
    }

    #region Take damage script
    public void takeDamage(float amount)
    {
        Debug.Log("Taken damage:  "+amount);
        health-= amount;
        if (health <= 0f)
        {
            StartCoroutine(Dying());
        }
    }
    #endregion

    #region IEnumerator Dying script
    IEnumerator Dying()
    {
        agent.isStopped = true;
        animator.SetBool("ZombieDying", true);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        Destroy(Enemy);
    }
    #endregion

    #region move to target function
    void movetotarget()
    {
        var distance = Vector3.Distance(Enemy.position, Player.position);
        DistanceText.text = distance.ToString();

        if (agent != null && distance > 20f && agent.remainingDistance <= agent.stoppingDistance)
        {
            wandering();
        }
        if (distance < 18f)
        {
            chaseing();
        }
        if (distance < 4f)
        {
            attack();
        }
    }
    #endregion

    #region wandering script
    void wandering()
    {
        animator.SetBool("Wandering", true);
        animator.SetBool("ZombieRun", false);
        animator.SetBool("ZombieAttack", false);
        agent.SetDestination(WanderingAgents());
        agent.speed = wanderingSpeed;
    }
    #endregion

    #region chaseing script
    void chaseing()
    {
        animator.SetBool("ZombieRun", true);
        animator.SetBool("Wandering", false);
        animator.SetBool("ZombieAttack", false);
        agent.speed = chasingSpeed;
        agent.SetDestination(Player.position);
    }
    #endregion

    #region attack script
    void attack()
    {

        animator.SetBool("ZombieAttack", true);
        animator.SetBool("Wandering", false);
        animator.SetBool("ZombieRun", false);
    }
    #endregion

    #region vecro3 setting wanderind destination
    public Vector3 WanderingAgents()
    {
        walkRadius = Random.Range(1, 500);
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;

        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
    #endregion

}
