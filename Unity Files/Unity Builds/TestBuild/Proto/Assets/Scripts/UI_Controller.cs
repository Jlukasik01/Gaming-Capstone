using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Controller : MonoBehaviour
{

    public Slider healthSlider;
    //public Slider manaBarSlider;
    public Text SoulsCounter;
    public Image[] items;
    public int highlighted;

    void Start()
    {
        highlight(1, 1);
        updateHealth();
    }

    void Update()
    {
        if (highlighted != GetComponent<IntController>().keyPress)
        {
            highlight(highlighted, -1);
            highlight(GetComponent<IntController>().keyPress, 1);
        }
        healthSlider.maxValue = GetComponent<PlayerController>().maxHealth;
        healthSlider.value = GetComponent<PlayerController>().health;
        SoulsCounter.text = "Souls: " + GetComponent<PlayerController>().souls;
    }

    public void highlight(int a, int b)
    {
        if (b == -1)
            GetComponent<UI_Controller>().items[a].color = Color.gray;
        else {
            GetComponent<UI_Controller>().items[a].color = Color.yellow;
            highlighted = a;
        }
    }

    public void updateHealth()
    {
        healthSlider.maxValue = GetComponent<PlayerController>().maxHealth;
        healthSlider.value = GetComponent<PlayerController>().health;
    }
}