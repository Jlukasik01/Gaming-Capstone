using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour
{
    public string itemName;
    public string type; //potion, spell, booster
    public string description; // description
    public int value; // how much item is worth
    public int maxHealthModifier; // Permanent modifier to player's max health as long as item is in inventory
    public int maxManaModifier; //Permanend modifier to player's man mana as long as item is in inventory
    public int maxStaminaModifier; //Permanent modifier to player's max stamina as long as item is in inventory
    public int weaponDamageModifier; // Permanent modifier to player's weapon damage as long as item is in inventory
    public int spellDamageModifier; // Permanent modifier to player's spell damage as long as item is in inventory
    public int defenseModifier; // Permanent modifier to player's defense as long as item is in inventory
    public int speedModifier; //Permanent modifier to player's speed as long as item is in inventory
    public bool stackable; // checks if you can stack
    public bool useable; // checks whether or not the item is a on use item (ex - health potion)
    public GameObject Player;
    public int count = 1; //How many there are, mainly used for potions
    public bool inInventory = false; //Set to TRUE if the player starts with this item in the inventory, otherwise set false

    void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    void Update()
    {
        if (Player.GetComponent<IntController>().Weapon == gameObject && gameObject.tag != "Weapon")
        {
            GetComponent<Collider>().isTrigger = true;
            GetComponent<Rigidbody>().useGravity = false;
        }
        else if(Player.GetComponent<IntController>().Weapon != gameObject && gameObject.tag != "Weapon") { GetComponent<Collider>().isTrigger = false; GetComponent<Rigidbody>().useGravity = true; }
    }

    public bool Stack(GameObject other)
    {
        if (other.tag == "Item")
        {
            if (other.GetComponent<ItemController>().type == type)
            {
                count += 1;
                return true; // true, this is same type as new item, so this would return true and inventory would delete the new one.
            }
         
        }
        return false; // says this inventory slot is not of same type, so inventory will check other spots
    }

    //Adds item's modifiers to player's stats, use when picking up or gaining an item
    public void getItemModifiers()
    {
        Player.GetComponent<PlayerController>().maxHealth += maxHealthModifier;
        Player.GetComponent<PlayerController>().maxMana += maxManaModifier;
        Player.GetComponent<PlayerController>().maxStamina += maxStaminaModifier;
        Player.GetComponent<PlayerController>().defense += defenseModifier;
        Player.GetComponent<PlayerController>().spellDamage += spellDamageModifier;
        Player.GetComponent<PlayerController>().weaponDamage += weaponDamageModifier;
        Player.GetComponent<PlayerController>().moveSpeed += speedModifier;
    }

    //Subtracts item's modifiers to player's stats, use when dropping or losing an item
    public void loseItemModifiers()
    {
        Player.GetComponent<PlayerController>().maxHealth -= maxHealthModifier;
        Player.GetComponent<PlayerController>().maxMana -= maxManaModifier;
        Player.GetComponent<PlayerController>().maxStamina -= maxStaminaModifier;
        Player.GetComponent<PlayerController>().defense -= defenseModifier;
        Player.GetComponent<PlayerController>().spellDamage -= spellDamageModifier;
        Player.GetComponent<PlayerController>().weaponDamage -= weaponDamageModifier;
        Player.GetComponent<PlayerController>().moveSpeed -= speedModifier;
    }

}
