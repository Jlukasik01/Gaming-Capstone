using UnityEngine;
using System.Collections;


public class LootController : MonoBehaviour {

    public GameObject[] inventory;
    private int maxSize;
    public void Start()
    {
        //int maxSize;
        maxSize = inventory.Length;
    }
    
    
    public GameObject dropItem()
    {
        GameObject temp;
        int pick = Random.Range(0, maxSize);
        temp = inventory[pick];
        return temp;
    }
}
