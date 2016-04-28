using UnityEngine;
using System.Collections;

public class StoreController : MonoBehaviour {
    public bool storeIsActive;
    public bool playerInCollider;
    public GameObject player;
    public 
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null)
        { player = GameObject.FindWithTag("Player"); }
        checkActive();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInCollider = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInCollider = false;
            storeIsActive = false;
        }
    }
    void checkActive()
    {
        if(playerInCollider)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!storeIsActive) { storeIsActive = true; }
                else if(storeIsActive) { storeIsActive = false; }
            }
        }

    }
    
}
