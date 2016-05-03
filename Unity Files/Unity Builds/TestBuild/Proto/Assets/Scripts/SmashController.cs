using UnityEngine;
using System.Collections;

public class SmashController : MonoBehaviour {
    public int damage;
    public bool canDoDamage;
    // Use this for initialization
    void Start()
    {
        StartCoroutine("Destroy", 10f);
        canDoDamage = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Destroy(float Time) // Destory object in time
    {

        for (float f = 0.0f; f <= Time; f += 0.1f)
        {
            if (f > 1)
            {
                canDoDamage = false;
            }
            yield return new WaitForSeconds(0.1f); // wait for animation to be in the position to do damage.
        }
        Destroy(gameObject);
    }
    void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "Player") && canDoDamage == true)
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            canDoDamage = false;
        }
    }
}
