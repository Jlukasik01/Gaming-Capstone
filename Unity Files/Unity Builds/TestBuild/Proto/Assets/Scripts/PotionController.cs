using UnityEngine;
using System.Collections;

public class PotionController : MonoBehaviour {

	int healing = 10;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			Destroy(gameObject);
		}
		if(other.gameObject.tag == "Player")
		{

			if (other.gameObject.GetComponent<PlayerController>().health+healing > other.gameObject.GetComponent<PlayerController>().maxHealth)
					other.gameObject.GetComponent<PlayerController>().health = other.gameObject.GetComponent<PlayerController>().maxHealth;
			else
					other.gameObject.GetComponent<PlayerController>().health += healing;
			Destroy(gameObject);
		}

	}
}
