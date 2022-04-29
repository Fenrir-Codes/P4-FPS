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
            xPosition = Random.Range(230, 240);
            zPosition = Random.Range(145,170);
            Instantiate(Enemy, new Vector3(xPosition, 0.1f, zPosition), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            spawned += 1;
        }
    }
}
