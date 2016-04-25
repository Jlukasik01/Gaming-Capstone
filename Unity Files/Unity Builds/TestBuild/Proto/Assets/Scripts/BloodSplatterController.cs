using UnityEngine;
using System.Collections;

public class BloodSplatterController : MonoBehaviour {

    public GameObject[] bloodSplatter = new GameObject[3];
    
	// Use this for initialization
	void Start () {
        StartCoroutine("BloodSpawn", 3);
    }
	
	// Update is called once per frame
	void Update () {
        
	}
    IEnumerator BloodSpawn(float spawnTime) // Take Damage make invinvible
    {
       
        for (float f = 0f; f <= spawnTime; f += 
            1)
        {
            Instantiate(bloodSplatter[1], new Vector3(gameObject.transform.position.x, 0.2f, gameObject.transform.position.z), new Quaternion(0,0,0,0));
            yield return new WaitForSeconds(1f); // can't take damage until timer ends
        }

    
    }
}
