using UnityEngine;
using System.Collections;

public class BossRoomController : MonoBehaviour {

    public bool bossAlive;
    public GameObject NewLevelPortal;

	// Use this for initialization
	void Start ()
    {
        bossAlive = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (bossAlive == false)
        {
            Instantiate(NewLevelPortal, transform.position, Quaternion.identity);
        }
	}
}
