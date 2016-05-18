using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICreditImageScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void clickMe()
	{
		//gameObject.SetActive(false);
		GetComponent<Image>().enabled = false;
		GetComponentInChildren<Text> ().enabled = false;
	}
}
