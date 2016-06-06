using UnityEngine;
using System.Collections;

public class NewLevel : MonoBehaviour {

    public GameObject TeleportSpell;
    public GameObject RoomGenerator;
    // Use this for initialization
    void Start ()
    {

        Instantiate(TeleportSpell, transform.position, Quaternion.identity);
        if(RoomGenerator == null)
        {
            RoomGenerator = GameObject.FindGameObjectWithTag("RoomGenerator");
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("PORTAL COLLIDED");
        if (other.gameObject.tag == "Player")
        {
           
            RoomGenerator.GetComponent<RoomGenerator>().newLevel();
            RoomGenerator.GetComponent<RoomGenerator>().currentLevel++;
            Destroy(gameObject);
        }
        
    }

}
