using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {
    public float timer;
	// Use this for initialization
	void Start () {
        StartCoroutine("Destroy", timer);
	}
	
	// Update is called once per frame
	void Update () {
	   
	}
    IEnumerator Destroy(float Time) // Destory object in time
    {

        for (float f = 0.0f; f <= Time; f += 0.1f)
        {
            yield return new WaitForSeconds(0.1f); // wait for animation to be in the position to do damage.
        }
        Destroy(gameObject);
    }
}
