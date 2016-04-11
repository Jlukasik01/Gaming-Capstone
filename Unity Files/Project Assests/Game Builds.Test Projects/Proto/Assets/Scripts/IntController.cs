using UnityEngine;
using System.Collections;

public class IntController : MonoBehaviour {
    public int inventorySize = 10; //max size of invetory
    public GameObject[] inventory = new GameObject[inventorySize]; 
    public int currentInventorySize = 0; //how big current inventory is
    private int keyPress; //selected inventory spot
    
    
	// Use this for initialization
	void Start ()
    {
        for(int i = 0; i < inventorySize; i++)
        {
            inventory[i] = null;
        }   
	}
	
	// Update is called once per frame
	void Update ()
    {
        getPress();
        Debug.Log("Current Selected Inv Space: ");
        Debug.Log(keyPress);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            dropItem();

        }
	}

    void getPress()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            keyPress = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            keyPress = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            keyPress = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            keyPress = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            keyPress = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            keyPress = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            keyPress = 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            keyPress = 8;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            keyPress = 9;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            keyPress = 0;

        }
    }

    void dropItem()
    {
        if(currentInventorySize > 0)
        {
            Instantiate(inventory[keyPress], gameObject.transform.position, new Quaternion(0, 0, 0, 0));
            inventory[keyPress] = null;
            currentInventorySize--;
        }
    }

    void OnCollisionStay(Collider other)
    {
        if(other.tag== "Loot")
        {
            if(Input.GetKey(KeyCode.E))
            {
                //find empty inv spot, 
             
                if (findEmptySpot() != -1)
                {
                    inventory[] == other.gameObject;
                    currentInventorySize++;
                    Destroy(other.gameObject);
                }
                else
                {
                    //return error of no room in inventory

                }
                
            }
        }
    }


    int findEmptySpot()
    {
        int returnSpot;
        if (currentInventorySize > 0)
        {
            for(int i = 0; i < inventorySize; i++)
            {
                if(inventory[i] == null)
                {
                    return returnSpot = i;
                }
                if(i == inventorySize)
                {
                    return returnSpot = -1;
                }
            }
        }
    }
}
