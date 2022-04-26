using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject Enemy;
    public float xPosition;
    public float zPosition;
    public int spawned = 1;
    public int maxEnemy = 10;


    private void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        while (spawned < maxEnemy)
        {
            xPosition = Random.Range(100, 150);
            zPosition = Random.Range(100, 250);
            Instantiate(Enemy, new Vector3(xPosition, 11, zPosition), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            spawned += 1;
        }
    }
}
