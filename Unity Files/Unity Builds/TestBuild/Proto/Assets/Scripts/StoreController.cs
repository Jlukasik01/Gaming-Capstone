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
            if (Input.GetKeyDown(KeyCode.E)) // Open Store if not Open
            {
                if (!storeIsActive) { storeIsActive = true; }
                else if(storeIsActive) { storeIsActive = false; }
            }
            if (Input.GetKeyDown(KeyCode.F)) //Sell Current Item In player inventory
            {
                if (!storeIsActive) {
                    if(player.GetComponent<IntController>().inventory[player.GetComponent<IntController>().keyPress] != null) // check to make sure player is holding something
                    {
                        player.GetComponent<PlayerController>().souls += player.GetComponent<IntController>().inventory[player.GetComponent<IntController>().keyPress].GetComponent<ItemController>().value; // get Item Value
                        player.GetComponent<IntController>().inventory[player.GetComponent<IntController>().keyPress] = null;
                        Destroy(player.GetComponent<IntController>().Weapon);
                    }
                }
            }
        }

    }
    
}
