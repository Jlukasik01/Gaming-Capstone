using UnityEngine;
using System.Collections;

public class UI_SwitchScript : MonoBehaviour {
    public GameObject playerHolder;

    public void switchHighlight()
    {
        int temp2 = playerHolder.GetComponent<IntController>().keyPress;
        int temp = temp2;
        temp++;
        if (temp > 9) { temp = 0; }
        playerHolder.GetComponent<IntController>().changeUIcolor(temp, temp2);
        playerHolder.GetComponent<IntController>().keyPress = temp;
    }

	// Use this for initialization
	void Start () {
        playerHolder = GameObject.FindGameObjectWithTag("Player");
    }
}