using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Controller : MonoBehaviour {

    public Slider healthSlider;
    //public Slider manaBarSlider;
    public Image[] items;

    public void updateHealth(int h, float m)
    {
        healthSlider.maxValue = m;
        healthSlider.value = h;
    }
}