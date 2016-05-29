using UnityEngine;
using System.Collections;

//master controller for all enemy spawners. Included to reduce load time on enemy list instead of loading it on every spawner
public class EnemySpawnerController : MonoBehaviour {

    public GameObject[] enemyList;
    public GameObject spawningEnemy;
    public GameObject roomGenerator;
    public int selectedEnemyIndex;

	// Use this for initialization
	void Start ()
    {
        enemyList = Resources.LoadAll<GameObject>("EnemiesToLoad");
        roomGenerator = GameObject.FindGameObjectWithTag("RoomGenerator");

        GameObject[] tempArray;
        tempArray = new GameObject[enemyList.Length];
        int arrayIndexToFind = 0;
        Debug.Log("Starting to sort enemyList");
        for(int i = 0; i < enemyList.Length; i++)
        {
            if (enemyList[i].GetComponent<EnemyController>().arrayIndex == arrayIndexToFind)
            {
                tempArray[arrayIndexToFind] = enemyList[i];
                Debug.Log("Index:" + i + " tempArray:" + tempArray[arrayIndexToFind]);
                arrayIndexToFind++;
                i = -1;
            }
        }
        Debug.Log("Done sorting");
        Debug.Log("Assinging enemyList to sorted array");
        for(int i = 0; i < enemyList.Length; i++)
        {  
            enemyList[i] = tempArray[i];
            Debug.Log("Index:" + i + " enemyList:" + enemyList[i]);
        }
        //tempArray.CopyTo(enemyList, 0);
       

    }
	
	public GameObject spawnEnemy()
    {
        int rangeIncreaseLv = 1; //how often a new enemy can appear on spawn list
        int maxRange = 2 + Mathf.RoundToInt(roomGenerator.GetComponent<RoomGenerator>().levelsCreated / rangeIncreaseLv)+5; //max range of indexs of enemies that can spawn
        //makes sure maxRange stays within array size
        if(maxRange > enemyList.Length)
        {
            maxRange = enemyList.Length;
        }
        //first few levels can spawn anything, due to limited choices
        if(maxRange > 4)
        {
            //75% of the time, select enemy in last 75% of enemyList
            if (Random.Range(0, 101) < 75)
            {
                selectedEnemyIndex = Random.Range(Mathf.RoundToInt(maxRange / 4), maxRange);
            }
            //25% of the time, select enemy in first 25% of enemyList
            else
            {
                selectedEnemyIndex = Random.Range(0, Mathf.RoundToInt(maxRange / 4));
            }
        }
        else
        {
            selectedEnemyIndex = Random.Range(0, maxRange);
        }     
        spawningEnemy = enemyList[selectedEnemyIndex];
        scaleStats();
        makeAlpha();
        return spawningEnemy;
    }

    //gets chance to make a monster "Alpha", drastically increases stats
    void makeAlpha()
    {
        if(Random.Range(0, 101) < roomGenerator.GetComponent<RoomGenerator>().alphaMonsterChance)
        {
            //increase all monster's stats by double
            spawningEnemy.GetComponent<EnemyController>().health *= 2;
            spawningEnemy.GetComponent<EnemyController>().damage *= 2;
        }
    }

    //scales spawningEnemy's stats to the appropriate level
    void scaleStats()
    {
        //increases enemy's health by 10% each level
        spawningEnemy.GetComponent<EnemyController>().health = Mathf.RoundToInt(spawningEnemy.GetComponent<EnemyController>().baseHealth * (1 + (roomGenerator.GetComponent<RoomGenerator>().currentLevel/10)));
        //increases enemy's damage by 10% each level
        spawningEnemy.GetComponent<EnemyController>().damage = Mathf.RoundToInt(spawningEnemy.GetComponent<EnemyController>().baseDamage * (1 + (roomGenerator.GetComponent<RoomGenerator>().currentLevel / 10)));
       
    }

}
