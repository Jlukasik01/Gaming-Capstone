using UnityEngine;
using System.Collections;

public class SpellController : MonoBehaviour {
    public string spellName;
    public string spellType; //What type of spell, not needed if spell is an item like a potion. Ex - Item type = staff, Spell type = fire 
    public string description; //What it does
    public int manaCost; //Mana cost of spell
    public int staminaCost; //Stamina cost of spell
    public int manaRegenModifier; //Mana regen modifier for player
    public int staminaRegenModifier; //Stamnina regen modifier for player
    public int healthModifier; //Used for damaging or healing the player
    public int damageValue; //how hard the spell hits
    public int attackModifier; //General modifier for damage player deals
    public int defenseModifier; //General modifier for damage player takes
    public GameObject Player;
    public GameObject LootTable;


	// Use this for initialization
	void Start ()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        if (LootTable == null)
        {
            LootTable = GameObject.FindGameObjectWithTag("LootTable");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void CallFunction()
    {
        //Health Modifying Potion
        if (GetComponent<ItemController>().type == "HealthPotion")
        {
            Player.GetComponent<PlayerController>().health += healthModifier;
            Player.GetComponent<ItemController>().count--;
            if (Player.GetComponent<ItemController>().count == 0)
            {
                Destroy(gameObject);
            }
        }

        //Randomizes current inventory
        else if (GetComponent<ItemController>().type == "Dice")
        {
            for(int x = 0; x < Player.GetComponent<IntController>().currentInventorySize; x++)
            {
                Player.GetComponent<IntController>().inventory[x] = LootTable.GetComponent<LootController>().dropItem();
            }
            Destroy(gameObject);
        }
    }
}
