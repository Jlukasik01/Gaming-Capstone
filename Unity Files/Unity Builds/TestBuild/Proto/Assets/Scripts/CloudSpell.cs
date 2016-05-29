using UnityEngine;
using System.Collections;

public class CloudSpell : MonoBehaviour {
    public GameObject Spell;
    public int AOEAmount = 10;
	// Use this for initialization
	void Start () {
	    for(int i = 0; i < AOEAmount; i ++)
        {
            Instantiate(Spell, transform.position, Quaternion.Euler(0,360/AOEAmount*i,0));
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
