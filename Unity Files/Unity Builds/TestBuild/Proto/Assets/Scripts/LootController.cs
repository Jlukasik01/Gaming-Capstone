using UnityEngine;
using System.Collections;


public class LootController : MonoBehaviour
{
    public GameObject[] commonItems;
    public GameObject[] uncommonItems;
    public GameObject[] rareItems;
    public GameObject[] ultraRareItems;
    public GameObject droppedItem;
    public GameObject blankItem; 

    public int chanceToDropItem;


    private int maxSize;
    public void Start()
    {
        blankItem = GameObject.FindGameObjectWithTag("blankItem");
    }

    //returns random game object from MasterLootTable 
    public GameObject dropItem()
    {
        if(Random.Range(0, 101) <= chanceToDropItem)
        {
            //picks a number between 0 and 100
            int pick = Random.Range(0, 101);
            Debug.Log("Picking item " + pick);
            //50% chance to select common item
            if (pick <= 50)
            {
                droppedItem = commonItems[Random.Range(0, commonItems.Length)];
            }
            //30% chance to select uncommon item
            else if (pick <= 80 && pick > 50)
            {
                droppedItem = uncommonItems[Random.Range(0, uncommonItems.Length)];
            }
            //15% chance to select rare item
            else if (pick <= 90 && pick > 75)
            {
                droppedItem = rareItems[Random.Range(0, rareItems.Length)];
            }
            //5% chance to select ultraRareItem
            else if (pick <= 100 && pick > 90)
            {
                droppedItem = ultraRareItems[Random.Range(0, ultraRareItems.Length)];
            }

            return droppedItem;
        }
        return blankItem;
        
    }
}
