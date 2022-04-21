
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public float health = 100f;

    private NavMeshAgent agent = null;
    public Transform Player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
    }


    void movetotarget()
    {
        agent.SetDestination(Player.position);
    }
}
