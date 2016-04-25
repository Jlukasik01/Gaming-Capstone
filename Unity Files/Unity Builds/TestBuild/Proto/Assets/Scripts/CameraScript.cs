using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    public GameObject player;
    Vector3 Distance;
    public float dampPercentage = 0.98f;
	// Use this for initialization
	void Start () {
        Distance = player.transform.position  + Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Camera.main.transform.position = (player.transform.position + Distance)*dampPercentage;
	}
}
