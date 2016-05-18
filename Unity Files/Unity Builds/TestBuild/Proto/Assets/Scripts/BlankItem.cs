using UnityEngine;
using System.Collections;
//blank item to be used in loot table. Destroys itself on frame 1
public class BlankItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Destroy(gameObject);
	}
}
