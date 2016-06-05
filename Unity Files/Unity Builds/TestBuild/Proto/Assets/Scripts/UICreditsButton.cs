using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICreditsButton : MonoBehaviour {

    public Image CreditsImage;

    public void ClickMe()
    {
        CreditsImage.enabled = true;
        CreditsImage.GetComponentInChildren<Text>().enabled = true;
    }
}
