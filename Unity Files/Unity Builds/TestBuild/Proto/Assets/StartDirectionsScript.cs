using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartDirectionsScript : MonoBehaviour {

    public Image DirectionsImage;

    public void ClickMe()
    {
        DirectionsImage.enabled = true;
        DirectionsImage.GetComponentInChildren<Text>().enabled = true;
    }
}
