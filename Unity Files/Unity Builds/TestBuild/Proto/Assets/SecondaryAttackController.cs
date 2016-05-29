using UnityEngine;
using System.Collections;

public class SecondaryAttackController : MonoBehaviour {
    public bool isActive;
    public int damage;
    public GameObject Spell;
	// Use this for initialization
	void Start () {
        isActive = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void callSpell()
    {
        if (Spell != null)
        {
            Instantiate(Spell, transform.position, transform.rotation);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(isActive)
            {
                other.GetComponent<PlayerController>().TakeDamage(damage);
                isActive = false;

            }
        }
    }
}
