using UnityEngine;
using System.Collections;

public class SpellController : MonoBehaviour
{
    public string spellName;
    public string spellType; //What type of spell, not needed if spell is an item like a potion. Ex - Item type = staff, Spell type = fire 
    public string description; //What it does
    public int manaCost; //Mana cost of spell
    public int staminaCost; //Stamina cost of spell
    public bool canUse;

    //Permanent modifiers
    public int healthModifier; //Used for damaging or healing the player
    public int manaModifier; //modifier for players current mana
    public int staminaModifier; //modifier for player's current stamina

    //Temporary modifiers
    public int manaRegenModifier; //Mana regen modifier for player
    public int staminaRegenModifier; //Stamnina regen modifier for player
    public int spellDamageModifier; //modifier for player's spell damage
    public int weaponDamageModifier; //modifier for player's weapon damage
    public int defenseModifier; //General modifier for damage player takes
	public int speedModifier; //General modifier for player speed
    public float modifyTime; //time how long the mana and stamina regen modifier is affected

    //Dont set in prefab
    public bool modifyingStats; //DO NOT SET IN PREFAB, used to control how long modifiers last on player
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
        //General Stat Modifying Potion
        if (GetComponent<ItemController>().type == "Potion")
        {
            Player.GetComponent<PlayerController>().health += healthModifier;
            Player.GetComponent<PlayerController>().mana += manaModifier;
            Player.GetComponent<PlayerController>().stamina += staminaModifier;
            modifyingStats = true;
            GetComponent<ItemController>().count--;
            StartCoroutine("Timer");
            if (GetComponent<ItemController>().count == 0)
            {
                Destroy(gameObject);
                Player.GetComponent<IntController>().inventory[Player.GetComponent<IntController>().keyPress] = null;
            }

            if (modifyingStats == true)
            {
                Player.GetComponent<PlayerController>().manaRegenRate += manaRegenModifier;
                Player.GetComponent<PlayerController>().staminaRegenRate += staminaRegenModifier;
                Player.GetComponent<PlayerController>().spellDamageModifier += spellDamageModifier;
                Player.GetComponent<PlayerController>().weaponDamageModifier += weaponDamageModifier;
                Player.GetComponent<PlayerController>().defense += defenseModifier;
                Player.GetComponent<PlayerController>().moveSpeed += speedModifier;
                StartCoroutine("modifyTimer");
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
            Destroy(gameObject);
            Player.GetComponent<IntController>().inventory[Player.GetComponent<IntController>().keyPress] = null;
        }

        else if(GetComponent<ItemController>().type == "Teleport") // Teleport for store
        {
            if (GameObject.FindGameObjectWithTag("StoreExit").GetComponent<StoreExitController>().inStore == false) //sets players teleport back positions
            {
                GameObject.FindGameObjectWithTag("StoreExit").GetComponent<StoreExitController>().ExitTeleportPosition = Player.transform.position;
                GameObject.FindGameObjectWithTag("StoreExit").GetComponent<StoreExitController>().inStore = true;
            }
            Player.transform.position = GameObject.FindGameObjectWithTag("StoreTeleport").transform.position;
            Destroy(gameObject);
            Player.GetComponent<IntController>().inventory[Player.GetComponent<IntController>().keyPress] = null;
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

    //modifies players stats for regenTime then sets back to normal
    IEnumerator modifyTimer()
    { 
        yield return new WaitForSeconds(modifyTime);
        Player.GetComponent<PlayerController>().manaRegenRate -= manaRegenModifier;
        Player.GetComponent<PlayerController>().staminaRegenRate -= staminaRegenModifier;
        Player.GetComponent<PlayerController>().spellDamageModifier -= spellDamageModifier;
        Player.GetComponent<PlayerController>().weaponDamageModifier -= weaponDamageModifier;
        Player.GetComponent<PlayerController>().defense -= defenseModifier;
        Player.GetComponent<PlayerController>().moveSpeed -= speedModifier;
        modifyingStats = false;
    }
}
