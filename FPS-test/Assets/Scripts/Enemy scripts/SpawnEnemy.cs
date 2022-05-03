using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject Enemy;
    public float xPosition;
    public float zPosition;
    public int spawned = 1;
    public int maxEnemy = 10;

    private void Awake()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
    }

    private void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        while (spawned < maxEnemy)
        {
            //xPosition = Random.Range(227, 240);
            //zPosition = Random.Range(128,138);
            //Instantiate(Enemy, new Vector3(xPosition, 0.1f, zPosition), Quaternion.identity);
            //yield return new WaitForSeconds(0.1f);
            //spawned += 1;

            int spawn = Random.Range(0, spawnPoints.Length);
            yield return new WaitForSeconds(0.1f);
            GameObject.Instantiate(Enemy, spawnPoints[spawn].transform.position, Quaternion.identity);
            spawned += 1;
        }
    }
}
