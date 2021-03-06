﻿using UnityEngine;
using System.Collections;

//responsible for instantiating a enemy at self position
public class EnemySpawner : MonoBehaviour {

    public GameObject spawningEnemy;
    public GameObject enemySpawnerController;
    public bool canSpawn;

    // Use this for initialization
    void Start()
    {
        enemySpawnerController = GameObject.FindGameObjectWithTag("EnemySpawnerController");
        canSpawn = true;
       
    }

    void Update()
    {
        if(canSpawn == true)
        {
            spawningEnemy = enemySpawnerController.GetComponent<EnemySpawnerController>().spawnEnemy();
            Instantiate(spawningEnemy, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
