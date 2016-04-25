using UnityEngine;
using System.Collections;


public class LootController : MonoBehaviour {

    public GameObject[] MasterLootTable;
    private int maxSize;
    public void Start()
    {
        //int maxSize;
        maxSize = MasterLootTable.Length;'
       
    }
    
    
    public GameObject dropItem()
    {
        GameObject temp;
        int pick = Random.Range(0, maxSize);
        temp = MasterLootTable[pick];
        return temp;
    }
    public void populateTable()
    {
        //test function to fill the masterloottable with swords
        for (int x = 0; x < maxSize; x++)
        {
            MasterLootTable.Add("Bow")
        }
    }
}
