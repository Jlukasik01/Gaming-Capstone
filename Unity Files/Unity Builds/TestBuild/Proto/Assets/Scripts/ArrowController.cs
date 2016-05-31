using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {
    public float speed;
    public bool isActive;
    public int Damage;
    public GameObject Player;
	// Use this for initialization
	void Start ()
    {
        isActive = true;
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(isActive)
        {
            gameObject.transform.Translate(Vector3.right* Time.deltaTime * speed);
            // gameObject.transform.Rotate(Vector3.right);
        }
        StartCoroutine("Destroy", 5f);
    }

    IEnumerator Destroy(float Time) // Destory object in time
    {
        
        for (float f = 0.0f; f <= Time; f += 0.1f)
        {
         
            yield return new WaitForSeconds(0.1f); // wait for animation to be in the position to do damage.
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag != "Player")
        {
            if (other.gameObject.tag == "Enemy" && isActive)
            {
                other.gameObject.GetComponent<EnemyController>().health -= (Damage + Player.GetComponent<IntController>().Weapon.GetComponent<WeaponController>().damage);
            }
            isActive = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponentInChildren<Animator>().speed = 0f;
            
        }

    }
}
