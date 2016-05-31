using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ToolTipController : MonoBehaviour {

    public Image ToolTip;
    public Text ToolTipText;
    public bool isActive;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isActive)
            {
                ToolTipText.enabled = false;
                ToolTip.enabled = false;
                isActive = false;
            }
            else
            {
                ToolTipText.enabled = true;
                ToolTip.enabled = true;
                isActive = true;
            }
        }


	}
}
