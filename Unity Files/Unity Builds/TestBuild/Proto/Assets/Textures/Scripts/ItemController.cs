using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {
    public string ItemName;
    public string Type; //potion, spell, booster
    public int Value; // how much
    public int HealthBoost; // potion
    public int DamageBoost; // booster
    public int DefenseBoost; // booster
    public string Description; // discription
    public bool Stackable; // checks if you can stack
    public GameObject Player;
    public int count = 1;
    void Start () {
	    if(Player==null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
	}
    public void CallFunction() // each TYPE would have its own (IF)
    {
       if(Type == "Potion")
        {
            Player.GetComponent<PlayerController>().health += HealthBoost;
            count--;
            if(count == 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public bool Stack(GameObject other)
    {
        if (other.tag == "Item")
        {
            if (other.GetComponent<ItemController>().Type == Type)
            {
                count += 1;
                return true; // true, this is same type as new item, so this would return true and inventory would delete the new one.
            }
        }
        return false; // says this inventory slot is not of same type, so inventory will check other spots
    }
}
