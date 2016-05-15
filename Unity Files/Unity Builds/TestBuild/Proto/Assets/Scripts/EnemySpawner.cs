using UnityEngine;
using System.Collections;

//responsible for instantiating a enemy at self position
public class EnemySpawner : MonoBehaviour {

    public GameObject spawningEnemy;
    public GameObject enemySpawnerController;

    // Use this for initialization
    void Start ()
    {
        enemySpawnerController = GameObject.FindGameObjectWithTag("EnemySpawnerController");
        spawningEnemy = enemySpawnerController.GetComponent<EnemySpawnerController>().spawnEnemy();
        Instantiate(spawningEnemy, gameObject.transform.position, Quaternion.identity);
    }
	
}
