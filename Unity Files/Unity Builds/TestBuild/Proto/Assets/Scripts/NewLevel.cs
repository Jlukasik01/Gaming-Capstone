using UnityEngine;
using System.Collections;

public class NewLevel : MonoBehaviour {

    public GameObject TeleportSpell;
    // Use this for initialization
    void Start ()
    {
        Instantiate(TeleportSpell, transform.position, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            GetComponent<RoomGenerator>().newLevel();
        }

    }

}
