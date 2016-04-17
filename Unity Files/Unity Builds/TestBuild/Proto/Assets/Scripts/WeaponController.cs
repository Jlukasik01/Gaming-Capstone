using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
    public string WeaponType;
    public int damage;
    public Texture ImageUI;
    public bool inInventory;
    Rigidbody body;
    Collider coll;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
	}
	
	void Update()
    {
        
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
