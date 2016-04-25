using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_buttonBehavior : MonoBehaviour {

    private GameObject image;
    private GameObject playerHolder;
    private int temp;

    public void intSelect(int k)
    {
        temp = playerHolder.GetComponent<IntController>().keyPress;
        playerHolder.GetComponent<IntController>().changeUIcolor(k, temp);
        playerHolder.GetComponent<IntController>().keyPress = k;
    }

    public void setImage()
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
        playerHolder = GameObject.FindGameObjectWithTag("Player");
    }
}