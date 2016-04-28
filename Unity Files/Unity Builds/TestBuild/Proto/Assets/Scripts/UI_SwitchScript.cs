using UnityEngine;
using System.Collections;

public class UI_SwitchScript : MonoBehaviour
{
    public GameObject player;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void switchHighlight()
    {
        if(player.GetComponent<IntController>().keyPress == 9)
            player.GetComponent<IntController>().keyPress = 0;
        else
            player.GetComponent<IntController>().keyPress++;

    }
}