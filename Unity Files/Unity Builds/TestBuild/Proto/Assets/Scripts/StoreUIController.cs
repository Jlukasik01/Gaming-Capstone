using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StoreUIController : MonoBehaviour {
    public Button StoreSlots;
    public bool ShowStore;
    public GameObject Store;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ShowStore = Store.GetComponent<StoreController>().storeIsActive;
        if(ShowStore)
        {
            GetComponent<Canvas>().enabled = true;
        }
        else if(!ShowStore)
        {
            GetComponent<Canvas>().enabled = false;
        }      
	}

}
