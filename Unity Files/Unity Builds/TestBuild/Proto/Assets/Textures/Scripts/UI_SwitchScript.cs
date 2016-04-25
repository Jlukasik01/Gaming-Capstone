using UnityEngine;
using System.Collections;

public class UI_SwitchScript : MonoBehaviour {
    public GameObject player;


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        switchHighlight();
    }

    public void switchHighlight()
    {
        int temp = player.GetComponent<IntController>().keyPress;
        for (int i = 0; i < 10; i++)
        {
            temp++;
            if (temp > 9) temp = 0;
            if (player.GetComponent<IntController>().inventory[temp] != null)
            { //changeUIcolor highlights the first paramater, and unhighlights the second
                player.GetComponent<IntController>().changeUIcolor(temp, player.GetComponent<IntController>().keyPress);
                player.GetComponent<IntController>().keyPress = temp;
                return;
            }
        }

    }
}