using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIdirImageControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public void clickMe()
    {
        //gameObject.SetActive(false);
        GetComponent<Image>().enabled = false;
		GetComponentInChildren<Text> ().enabled = false;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
