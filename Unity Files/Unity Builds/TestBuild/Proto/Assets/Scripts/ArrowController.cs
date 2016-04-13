using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Translate(Vector3.right* Time.deltaTime * speed);
       // gameObject.transform.Rotate(Vector3.right);

        StartCoroutine("Destory", 5);
    }
    IEnumerator Destroy(float Time) // Attack
    {
        
        for (float f = 0.0f; f <= Time; f += 0.1f)
        {
            Destroy(gameObject);
            yield return new WaitForSeconds(0.1f); // wait for animation to be in the position to do damage.
        }
    }
}
