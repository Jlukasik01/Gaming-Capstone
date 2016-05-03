using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Controller : MonoBehaviour
{

    public Slider healthSlider;
    public Slider StanimaSlider;
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
        StanimaSlider.maxValue = GetComponent<PlayerController>().maxStamina;
        StanimaSlider.value = GetComponent<PlayerController>().stamina;
        SoulsCounter.text = "Souls: " + GetComponent<PlayerController>().souls;
    }



    public void updateHealth()
    {
        healthSlider.maxValue = GetComponent<PlayerController>().maxHealth;
        healthSlider.value = GetComponent<PlayerController>().health;
    }
}