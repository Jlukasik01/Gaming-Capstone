using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StoreButtonController : MonoBehaviour {
    public GameObject Item;
    public int Cost;
    public string discription;
    public Transform spawnLoc; // in store
    public GameObject Player;
	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(OnClick);  
        
	}

	// Update is called once per frame
	void Update () {
     if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
	}
    public void OnClick()
    {
        if (Player.GetComponent<PlayerController>().souls >= Cost)
        { Instantiate(Item, spawnLoc.position, spawnLoc.rotation); }
    }
}
