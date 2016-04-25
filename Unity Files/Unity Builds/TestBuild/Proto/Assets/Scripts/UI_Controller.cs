using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Controller : MonoBehaviour {

    public Slider healthSlider;
    //public Slider manaBarSlider;
    public Image[] items;
    //public int health;
    //public int mana;
    //public int maxHealth;
    //public int maxMana;
    private GameObject player;

    void OnStart()
    {

        player = GameObject.FindGameObjectWithTag("Player");

        //health = player.GetComponent<PlayerController>().health;
        //maxHealth = health;
        //updateHealth(health, maxHealth);
        //GetComponent<PlayerController>.mana = mana;
        //maxMana = mana;
        //manaBarSlider.maxValue = maxMana;
        //float maxHealth = (GetComponent<PlayerController>().health) *1.0f;
        //healthSlider.maxValue = maxHealth;
    }

    public void updateHealth(int h, float m)
    {
        healthSlider.maxValue = m;
        healthSlider.value = h;
    }
    /*
    public void changeImage()
    {
        
    }*/
}