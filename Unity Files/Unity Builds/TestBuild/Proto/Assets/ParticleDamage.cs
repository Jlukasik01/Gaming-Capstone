using UnityEngine;
using System.Collections;

public class ParticleDamage : MonoBehaviour {
    public int damage;
    public bool takeMana;
	// Use this for initialization
	void Start () {
        takeMana = true;
	}

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().CastandBlast == false)
        {
            Destroy(gameObject);
        }
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().mana == 0)
        {
            Destroy(gameObject);
        }
        //Follow player
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Weapon != null)
        {
            
            transform.position = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Weapon.transform.position;
            transform.rotation = GameObject.FindGameObjectWithTag("Player").transform.rotation;
        }
        else { Destroy(gameObject); }

        if(takeMana)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().mana -= 1;
            StartCoroutine("Mana");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<EnemyController>() != null)
            {
                other.GetComponent<EnemyController>().takeDamage(damage);
            }
        }
    }
    IEnumerator Mana() // Take Damage make invincible
    {
        takeMana = false;
        for (float f = 0f; f <= 1; f += 0.1f)
        {

            yield return new WaitForSeconds(0.1f); // can't take damage until timer ends
        }

        takeMana = true;
    }
}
