using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UICreditsButton : MonoBehaviour {
    public Image CreditsImage;
    // Use this for initialization
    void Start () {
	//disable CreditsImage
    //Add Listeners to enable image on click
	}
    public void ClickMe()
    {
        CreditsImage.enabled = true;
        CreditsImage.GetComponentInChildren<Text>().enabled = true;
    }
    // Update is called once per frame
    void Update () {
	
	}
    // OnClick function
}
