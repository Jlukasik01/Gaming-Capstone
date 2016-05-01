using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_buttonBehavior : MonoBehaviour
{

    private GameObject player;
    private GameObject item;
    private Sprite def;
    public int invSlot;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        def = GetComponent<Image>().sprite;
    }
    public void changeKey(int a)
    {
        player.GetComponent<IntController>().keyPress = a;
    }

    void Update()
    {
        if (player.GetComponent<IntController>().inventory[invSlot] != null) { }
        // GetComponent<Image>().sprite = player.GetComponent<IntController>().inventory[invSlot].GetComponent<ItemController>().ImageUI;
        else
            GetComponent<Image>().sprite = def;
    }
}