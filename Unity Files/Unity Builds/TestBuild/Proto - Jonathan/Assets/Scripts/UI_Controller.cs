using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Controller : MonoBehaviour {

    public Image[] items;
    public Slider healthBarSlider;
    public Slider manaBarSlider;
    public int health;
    public int mana;
    public int maxHealth;
    public int maxMana;

    void OnStart()
    {
        GetComponent<PlayerController>().health = health;
        maxHealth = health;
        healthBarSlider.maxValue = maxHealth;
        //GetComponent<PlayerController>.mana = mana;
        //maxMana = mana;
        //manaBarSlider.maxValue = maxMana;
    }
    public void updateHealth(int h)
    {
        health = h;
        healthBarSlider.value = health;
    }

}
