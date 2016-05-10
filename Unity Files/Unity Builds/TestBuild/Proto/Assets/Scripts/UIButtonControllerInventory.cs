using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIButtonControllerInventory : MonoBehaviour {
    Image image;
    public int inventoryNumber;
    public GameObject player;
    public GameObject InventoryObject;
    public Sprite UIImage;
    public Button invImage;
    public bool ClickedDown;
    public Sprite defualtSprite;

    void Start()
    {
        image = GetComponent<Image>();
        invImage = GetComponentInChildren<Button>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        InventoryObject = player.GetComponent<IntController>().inventory[inventoryNumber];
        if (player.GetComponent<IntController>().inventory[inventoryNumber] != null)
        {
            if (player.GetComponent<IntController>().inventory[inventoryNumber].GetComponent<ItemController>().UIImage != null)
            { UIImage = player.GetComponent<IntController>().inventory[inventoryNumber].GetComponent<ItemController>().UIImage; }
            if (UIImage != null) { invImage.image.sprite = UIImage; }
            else { invImage.image.sprite = defualtSprite; }
        }


        if (player.GetComponent<IntController>().keyPress == inventoryNumber)
        {
            image.color = Color.yellow;
        }
        else
        {
            image.color = Color.grey;

        }
    }
    
}
