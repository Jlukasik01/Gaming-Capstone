using UnityEngine;
using System.Collections;

public class BossRoomController : MonoBehaviour {

    public bool bossAlive;
    public GameObject NewLevelPortal;
    public int arrayIndex;
    bool spawnedPortal;

	// Use this for initialization
	void Start ()
    {
        bossAlive = true;
        spawnedPortal = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (bossAlive == false && spawnedPortal == false)
        {
            Instantiate(NewLevelPortal, transform.position, Quaternion.identity);
            spawnedPortal = true;
            Debug.Log("SPAWNED PORTAL");
        }
	}
}
