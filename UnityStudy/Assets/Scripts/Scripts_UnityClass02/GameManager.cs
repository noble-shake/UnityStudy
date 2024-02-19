using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Enemy Spawner")]
    [SerializeField] bool isSpawn = false;
    [SerializeField] List<GameObject> listEnemy;

    [SerializeField, Range(0.1f, 2.0f)] float spawnTime = 1.0f;
    float sTimer = 0.0f; // timer


    void Start()
    {
        
    }

    void Update()
    {
        checkSpawn();   
    }

    /// <summary>
    /// EnemySpawn Spawnable Check
    /// </summary>
    private void checkSpawn() {
        if (!isSpawn) return;

        sTimer += Time.deltaTime;
        if (sTimer > spawnTime) { 
            sTimer = 0.0f;
            // Spawn
            spawnEnemy();
        }

    }

    private void spawnEnemy() { 
        float rand = Random.Range(0, listEnemy.Count); // 0 ~ count - 1..
        //Random.Range(0.0f, 100.0f); // 0.0 ~ 100.0..
        // 0.0 ~ 50.0 -> Enemy A
        // 50.00 ~ 75.0 -> Enemy B
        // 75.0 ~ 100.0 -> Enemy C
        GameObject objEnemy = null;
        if (rand < 50.0)
        {
            //Enemy A
            objEnemy = listEnemy[0];
        }
        else if (rand < 75.0) {
            //Enemy B
            objEnemy = listEnemy[1];
        }
        else
        {
            //Enemy C
            objEnemy = listEnemy[2];
        }

        Instantiate(objEnemy);
    }
}
