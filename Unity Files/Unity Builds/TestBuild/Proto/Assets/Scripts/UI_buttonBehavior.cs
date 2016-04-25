using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_buttonBehavior : MonoBehaviour {

    private GameObject player;
    private GameObject item;
    private Sprite def;
    public int invSlot;


    // Used to highlight selected item
    public void intSelect(int k)
    {
        if (player.GetComponent<IntController>().inventory[k] != null)
        {
            if (k == player.GetComponent<IntController>().keyPress)
                player.GetComponent<IntController>().changeUIcolor(k, 10);
            else
                player.GetComponent<IntController>().changeUIcolor(k, player.GetComponent<IntController>().keyPress);
            player.GetComponent<IntController>().keyPress = k;
        }
    }

    public void switchImage()
    {
        if (GetComponent<Image>().enabled)
        {
            GetComponent<Image>().enabled = false;
        }
        else
            GetComponent<Image>().enabled = true;
    }

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        def = GetComponent<Image>().sprite;
    }

    void Update ()
    {
        if (player.GetComponent<IntController>().inventory[invSlot] != null)
            GetComponent<Image>().sprite = player.GetComponent<IntController>().inventory[invSlot].GetComponent<WeaponController>().ImageUI;
        
        else
        {
            GetComponent<Image>().sprite = def;
            player.GetComponent<IntController>().changeUIcolor(invSlot, invSlot);
        }
    }
}