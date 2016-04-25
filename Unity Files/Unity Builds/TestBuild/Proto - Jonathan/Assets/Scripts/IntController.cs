using UnityEngine;
using System.Collections;

public class IntController : MonoBehaviour {
    public int inventorySize = 10; //max size of invetory
    public GameObject[] inventory = new GameObject[10];
    public int currentInventorySize = 0; //how big current inventory is
    public int keyPress; //selected inventory spot
    public Transform weaponLoc;
    public GameObject Weapon;

    // Use this for initialization
    void Start()
    {
        if (Weapon == null)
        {
            Weapon = Instantiate(inventory[keyPress] as GameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        getPress();

        //Function to drop currently selected item
        if (Input.GetKeyDown(KeyCode.Q))
        {
            dropItem();
        }

        //Function for using useable items
        if (Input.GetKeyDown(KeyCode.F))
        {
            //Checks it the item is actually useable
            if (inventory[keyPress].GetComponent<ItemController>().useable == true)
            {
                //If it is useable, then does function based on Type in item controller
                inventory[keyPress].GetComponent<SpellController>().CallFunction();
            }
            else
            { }
        }

        if (Weapon.name != inventory[keyPress].name)
        {
            Destroy(Weapon);
            Weapon = Instantiate(inventory[keyPress]);
            Weapon.name = inventory[keyPress].name;
        }

        Weapon.transform.position = weaponLoc.transform.position;
        Weapon.transform.rotation = weaponLoc.transform.rotation;
    }

    public void changeUIcolor(int j, int k)
    {

        GetComponent<UI_Controller>().items[k].color = Color.gray;
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
        //drops selected item and loses its stats
        if (currentInventorySize > 0)
        {
            Weapon.GetComponent<WeaponController>().inInventory = false;
            inventory[keyPress].GetComponent<ItemController>().loseItemModifiers();
            Weapon = null;
            inventory[keyPress] = null;
            currentInventorySize--;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Item" || other.tag == "Weapon")
        {
            Debug.Log("On Loot!!!!!");
            if (Input.GetKey(KeyCode.E))
            {
                //adds item to inventory if there is empty room
                if (findEmptySpot() != -1)
                {
                    keyPress = findEmptySpot();
                    inventory[keyPress] = other.gameObject;
                    Weapon = other.gameObject;
                    Weapon.GetComponent<WeaponController>().inInventory = true;
                    inventory[keyPress].GetComponent<ItemController>().getItemModifiers();
                    currentInventorySize++;
                    Destroy(other);
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
            for (int i = 0; i < inventorySize; i++)
            {
                if (inventory[i] == null)
                {
                    return returnSpot = i;
                }
            }
        }
        return returnSpot = -1;
    }

 
   
   
}
