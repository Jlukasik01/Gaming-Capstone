using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Controller : MonoBehaviour
{

    public Slider healthSlider;
    //public Slider manaBarSlider;
    public Text SoulsCounter;
    public Image[] items;
    

    void Start()
    {
        
        updateHealth();
    }

    void Update()
    {
     
        healthSlider.maxValue = GetComponent<PlayerController>().maxHealth;
        healthSlider.value = GetComponent<PlayerController>().health;
        SoulsCounter.text = "Souls: " + GetComponent<PlayerController>().souls;
    }



    public void updateHealth()
    {
        healthSlider.maxValue = GetComponent<PlayerController>().maxHealth;
        healthSlider.value = GetComponent<PlayerController>().health;
    }
}