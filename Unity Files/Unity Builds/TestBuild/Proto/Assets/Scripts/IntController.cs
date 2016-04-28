using UnityEngine;
using System.Collections;

public class IntController : MonoBehaviour {
    public int inventorySize = 10; //max size of invetory
    public GameObject[] inventory = new GameObject[10];
    public int currentInventorySize = 0; //how big current inventory is
    public int keyPress; //selected inventory spot
    public Transform weaponLoc;
    public GameObject Weapon;
    public bool onLoot = false;

    // Use this for initialization
    void Start()
    {
        keyPress = 1 ;
        Weapon = Instantiate(inventory[1]) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        getPress();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            dropItem();
        }
        //Function for using useable items
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("PRESSING Y");
            useItem();
        }

        if (inventory[keyPress] != null)
        {
            if (Weapon == null)
            {
                Weapon = Instantiate(inventory[keyPress] as GameObject);
                changeUIcolor(keyPress, keyPress);
            }
            if (Weapon.name != inventory[keyPress].name)
            {
                Weapon.SetActive(false);
                Weapon = Instantiate(inventory[keyPress] as GameObject);
                Weapon.name = inventory[keyPress].name;
                Weapon.SetActive(true);
            }
        }
        if (Weapon != null)
        {
            Weapon.transform.position = weaponLoc.transform.position;
            Weapon.transform.rotation = weaponLoc.transform.rotation;
        }

    }

    public void changeUIcolor(int j, int k)
    {
        if (k != -1)
            GetComponent<UI_Controller>().items[k].color = Color.gray;
        if (inventory[j] != null)
            GetComponent<UI_Controller>().items[j].color = Color.yellow;
    }

    void getPress()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            changeUIcolor(1, keyPress);
            keyPress = 1;

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            changeUIcolor(2, keyPress);
            keyPress = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            changeUIcolor(3, keyPress);
            keyPress = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            changeUIcolor(4, keyPress);
            keyPress = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            changeUIcolor(5, keyPress);
            keyPress = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            changeUIcolor(6, keyPress);
            keyPress = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            changeUIcolor(7, keyPress);
            keyPress = 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            changeUIcolor(8, keyPress);
            keyPress = 8;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            changeUIcolor(9, keyPress);
            keyPress = 9;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            changeUIcolor(0, keyPress);
            keyPress = 0;
        }
    }

    void dropItem()
    {
        if (currentInventorySize > 0)
        {
            Instantiate(inventory[keyPress], transform.position, transform.rotation);
            inventory[keyPress] = null;
            Weapon = null;
            currentInventorySize--;
            GetComponent<PlayerController>().equip();
        }
    }

    void pickUpItem(Collider other)
    {
        if ((other.tag == "Item" || other.tag == "Weapon") && other.GetComponent<ItemController>().inInventory == false)
        {
            if (other.tag == "Weapon")
            {
                keyPress = findEmptySpot();
                inventory[keyPress] = other.gameObject;
                other.gameObject.SetActive(false);
            }
            if (other.tag == "Item")
            {
                if(other.gameObject.GetComponent<ItemController>().stackable)
                {
                    Debug.Log("StackCheck");
                    if (ItemCheckStack(other.gameObject)) //check to see if this is already in stock
                    {
                        Destroy(other.gameObject); //stack count went up delete new one
                      
                    }
         
                    else{ // stackable but doesnt already exist in inventory
                        keyPress = findEmptySpot();
                        inventory[keyPress] = other.gameObject;
                        other.gameObject.SetActive(false);
                    }

                    }
                else // non-stackable add to inv
                {

                    keyPress = findEmptySpot();
                    inventory[keyPress] = other.gameObject;
                    other.gameObject.SetActive(false);

                }
            }
        }
    }
    bool ItemCheckStack(GameObject other)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventory[i] != null)
            {
                if (inventory[i].tag == "Item")
                {
                    if (inventory[i].GetComponent<ItemController>().Stack(other))
                    { return true; }
                }
            }
        }
        return false;
    }
    public void useItem()
    {
        Debug.Log("IN useItem()");
        //Checks it the item is actually useable
        if (inventory[keyPress] != null && inventory[keyPress].GetComponent<ItemController>().useable == true)
        {
            Debug.Log("TRYING TO CALL FUNCTION");
            //If it is useable, then does function based on Type in item controller
            inventory[keyPress].GetComponent<SpellController>().CallFunction();
        }
    }

    
    void OnTriggerStay(Collider other)
    {
        //used for testing purposes
       if ((other.tag == "Item" || other.tag == "Weapon") && other.GetComponent<ItemController>().inInventory == false)
       {
           onLoot = true;
       }
     
       if (Input.GetKeyDown(KeyCode.E))
       {
           pickUpItem(other);
       }
       
    }

    //Used for testing purposes
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item" || other.tag == "Weapon")
        {
            onLoot = false;
        }
    }

    public int findEmptySpot()
    {
        if (0 < inventorySize)
        {
            for(int i = 0; i < inventorySize; i++)
            {
                if(inventory[i] == null)
                {
                    return i;
                }
            }
        }
        return -1;
    }
}
