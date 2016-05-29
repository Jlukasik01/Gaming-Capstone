using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class QuitButtonController : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(() => onClick());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void onClick()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
