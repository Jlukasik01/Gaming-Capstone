using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponController : MonoBehaviour {
    public string WeaponType; 
    public int damage; 
    public Sprite ImageUI;
    public bool inInventory;
    public GameObject projectile;
    Rigidbody body;
    Collider coll;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
	}
	
	void Update()
    {
        if (GetComponent<ItemController>().inInventory == false)
        {
            body.useGravity = true;
            coll.isTrigger = false;
        }
        if(GetComponent<ItemController>().inInventory == true && WeaponType == "Bow")
        {
            coll.isTrigger = true;
            body.useGravity = false;
        }
    }

    public void ActivateCollider()
    {
        StartCoroutine("Activation");
    }

    IEnumerator Activation()
    {
        GetComponent<Collider>().isTrigger = false;
        for(float i = 0f; i < 0.7f; i += 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
        }
        GetComponent<Collider>().isTrigger = true;
    }
}
