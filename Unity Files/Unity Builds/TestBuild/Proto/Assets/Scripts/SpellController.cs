using UnityEngine;
using System.Collections;

public class SpellController : MonoBehaviour
{
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
    public bool canUse;
    public GameObject Player;
    public GameObject LootTable;


    // Use this for initialization
    void Start()
    {
        canUse = true;
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
    void Update()
    {

    }

    public void CallFunction()
    {
        Debug.Log("TRIED TO USE SPELL");
        //Health Modifying Potion
        if (GetComponent<ItemController>().type == "HealthPotion")
        {
            Player.GetComponent<PlayerController>().health += healthModifier;
            GetComponent<ItemController>().count--;
            StartCoroutine("Timer");
            if (GetComponent<ItemController>().count == 0)
            {
                Destroy(gameObject);
                Player.GetComponent<IntController>().inventory[Player.GetComponent<IntController>().keyPress] = null;
            }
        }


        //Randomizes current inventory
        else if (GetComponent<ItemController>().type == "Dice")
        {
            for (int x = 0; x < Player.GetComponent<IntController>().inventorySize; x++)
            {
                if (Player.GetComponent<IntController>().inventory[x] != null)
                {

                    Player.GetComponent<IntController>().inventory[x] = LootTable.GetComponent<LootController>().dropItem();
                }

            }
        }
    }
    IEnumerator Timer() // Take Damage make invinvible
    {
        canUse = false;
        for (float f = 0f; f <= 1; f += 0.1f)
        {

            yield return new WaitForSeconds(1f); // can't take damage until timer ends
        }

        canUse = true;
    }
}
