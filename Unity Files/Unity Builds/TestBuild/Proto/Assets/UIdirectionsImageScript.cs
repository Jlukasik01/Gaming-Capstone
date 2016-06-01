using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIdirectionsImageScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }
    public void clickMe()
    {
        //gameObject.SetActive(false);
        GetComponentInChildren<Text>().enabled = false;
        GetComponent<Image>().enabled = false;

    }
    // Update is called once per frame
    void Update()
    {

    }
}
