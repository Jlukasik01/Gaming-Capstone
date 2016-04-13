using UnityEngine;
using System.Collections;

public class IntController : MonoBehaviour {
    public int inventorySize = 10; //max size of invetory
    public GameObject[] inventory = new GameObject[4]; 
    public int currentInventorySize = 0; //how big current inventory is
    private int keyPress; //selected inventory spot
    public Transform weaponLoc;
    public GameObject Weapon;
    
	// Use this for initialization
	void Start ()
    {
        Weapon = Instantiate(inventory[keyPress] as GameObject);
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
        if(Weapon.name != inventory[keyPress].name)
        {
            Destroy(Weapon);
            Weapon = Instantiate(inventory[keyPress]);
            Weapon.name = inventory[keyPress].name;
        }

        Weapon.transform.position = weaponLoc.transform.position;
        Weapon.transform.rotation = weaponLoc.transform.rotation;
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
            Weapon.GetComponent<WeaponController>().inInventory = false;
            Weapon = null;
            inventory[keyPress] = null;
            currentInventorySize--;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag== "Loot")
        {
            Debug.Log("On Loot!!!!!");
            if(Input.GetKey(KeyCode.E))
            {
               
             
                if (findEmptySpot() != -1)
                {
                    keyPress = findEmptySpot();
                    inventory[keyPress] = other.gameObject;
                    Weapon = other.gameObject;
                    Weapon.GetComponent<WeaponController>().inInventory = true;
                    currentInventorySize++; 
                }
                else
                {
                    //return error of no room in inventoryaw
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
            }
        }
        return returnSpot = -1;
    }
}
