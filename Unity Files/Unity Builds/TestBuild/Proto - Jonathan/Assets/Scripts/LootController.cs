using UnityEngine;
using System.Collections;


public class LootController : MonoBehaviour {

    public GameObject[] MasterLootTable;
    private int maxSize;
    public void Start()
    {
        //int maxSize;
        maxSize = MasterLootTable.Length;
       
    }
    
    //returns random game object from MasterLootTable 
    public GameObject dropItem()
    {
        GameObject temp;
        int pick = Random.Range(0, maxSize);
        temp = MasterLootTable[pick];
        return temp;
    }
}
