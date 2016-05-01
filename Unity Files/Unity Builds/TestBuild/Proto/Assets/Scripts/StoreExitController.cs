using UnityEngine;
using System.Collections;

public class StoreExitController : MonoBehaviour {
    public Vector3 ExitTeleportPosition;
    public bool inStore = false;
	// Use this for initialization
	void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Input.GetKey(KeyCode.E))
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = ExitTeleportPosition;
                inStore = false;
            }
        }
    }
}
