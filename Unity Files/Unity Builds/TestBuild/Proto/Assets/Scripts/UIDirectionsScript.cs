using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIDirectionsScript : MonoBehaviour {
    public Image DirectionsImage;
	// Use this for initialization
	void Start () {
      // disable Image
	  // add onclick listener
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ClickMe()
    {
        DirectionsImage.enabled = true;
		DirectionsImage.GetComponentInChildren<Text> ().enabled = true;
    }

    //add activate image
}
