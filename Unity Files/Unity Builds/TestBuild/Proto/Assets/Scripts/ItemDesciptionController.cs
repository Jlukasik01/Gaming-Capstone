using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ItemDesciptionController : MonoBehaviour {
    GameObject player;
    Text text;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        if (player.GetComponent<IntController>().inventory[player.GetComponent<IntController>().keyPress] != null)
        {
            text.text = player.GetComponent<IntController>().inventory[player.GetComponent<IntController>().keyPress].GetComponent<ItemController>().description;
            if(player.GetComponent<IntController>().inventory[player.GetComponent<IntController>().keyPress].GetComponent<WeaponController>() != null)
            {
               text.text += "\nDamage: " + player.GetComponent<IntController>().inventory[player.GetComponent<IntController>().keyPress].GetComponent<WeaponController>().damage.ToString();
                text.text += "\nWeapon Speed: " + player.GetComponent<IntController>().inventory[player.GetComponent<IntController>().keyPress].GetComponent<WeaponController>().attackSpeed.ToString();

            }
            text.text += "\nValue: " + player.GetComponent<IntController>().inventory[player.GetComponent<IntController>().keyPress].GetComponent<ItemController>().value + " Souls";
            text.text += "\nHealth Modifier: " + player.GetComponent<IntController>().inventory[player.GetComponent<IntController>().keyPress].GetComponent<ItemController>().maxHealthModifier;
            text.text += "\nMana Modifier: " + player.GetComponent<IntController>().inventory[player.GetComponent<IntController>().keyPress].GetComponent<ItemController>().maxManaModifier;
            text.text += "\nStamina Modifier: " + player.GetComponent<IntController>().inventory[player.GetComponent<IntController>().keyPress].GetComponent<ItemController>().maxStaminaModifier;
            text.text += "\nWeapon Damage Modifier: " + player.GetComponent<IntController>().inventory[player.GetComponent<IntController>().keyPress].GetComponent<ItemController>().weaponDamageModifier;
            text.text += "\nSpell Damage Modifier: " + player.GetComponent<IntController>().inventory[player.GetComponent<IntController>().keyPress].GetComponent<ItemController>().spellDamageModifier;
            text.text += "\nDefense Modifier: " + player.GetComponent<IntController>().inventory[player.GetComponent<IntController>().keyPress].GetComponent<ItemController>().defenseModifier;
            text.text += "\nSpeed Modifier: " + player.GetComponent<IntController>().inventory[player.GetComponent<IntController>().keyPress].GetComponent<ItemController>().speedModifier;
            text.text += "\nStackable Item: " + player.GetComponent<IntController>().inventory[player.GetComponent<IntController>().keyPress].GetComponent<ItemController>().stackable;
            text.text += "\nPress Space To Hide ToolTip";
        }
    }
}
