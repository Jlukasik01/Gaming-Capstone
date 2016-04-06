using UnityEngine;
using System.Collections;


public class LootController : MonoBehaviour {

    public GameObject[] inventory;
    public void Start()
    {
        int maxSize;
        maxSize = inventory.Length;
    }
    
    
    public GameObject dropItem()
    {
        GameObject temp;
        int pick = Random.Range(0, 10);
        temp = inventory[pick];
        return temp;
    }
}
