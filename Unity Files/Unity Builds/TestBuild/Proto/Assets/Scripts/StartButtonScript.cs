using UnityEngine;
using System.Collections;

public class StartButtonScript : MonoBehaviour {

    public GameObject UiOne;
    public GameObject UiTwo;
    public GameObject StartUI;


	// Use this for initialization
	void Start () {
        //UiOne = GameObject.FindGameObjectWithTag("UiOne");
        //UiTwo = GameObject.FindGameObjectWithTag("UiTwo");
        StartUI = GameObject.FindGameObjectWithTag("StartUI");
        //UiOne.GetComponent<Canvas>().enabled = false;
        //UiTwo.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 0;
    }

    public void clickStart()
    {
        Time.timeScale = 1;
        //UiOne.GetComponent<Canvas>().enabled = true;
        //UiTwo.GetComponent<Canvas>().enabled = true;
        //StartUI.GetComponent<Canvas>().enabled = false;
        StartUI.SetActive(false);
    }
}
