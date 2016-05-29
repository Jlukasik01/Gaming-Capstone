using UnityEngine;
using System.Collections;

public class KittenSpellController : MonoBehaviour {

    public float speed;
 
    // Use this for initialization
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {

        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);



        StartCoroutine("Destroy", 5f);
    }
    IEnumerator Destroy(float Time) // Destory object in time
    {

        for (float f = 0.0f; f <= Time; f += 0.1f)
        {
            yield return new WaitForSeconds(0.1f); // wait for animation to be in the position to do damage.
        }
       
    }
    void OnTriggerEnter(Collider other)
    {
    
            if (other.gameObject.tag != "Enemy" && other.gameObject.tag != "Weapon" && other.gameObject.tag != "Item")
            {
                Destroy(gameObject);
            }
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerController>().TakeDamage(5);
                Destroy(gameObject);
            }
        

    }
}
