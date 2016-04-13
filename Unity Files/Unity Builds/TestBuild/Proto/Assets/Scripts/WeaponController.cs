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

}
