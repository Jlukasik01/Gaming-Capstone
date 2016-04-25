using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {
    public GameObject floor;
    public GameObject wall;
    public GameObject door;
    public int roomSize =1;
    int roomX = Random.Range(3, 6);
    int roomY = Random.Range(3, 6);
    Quaternion ButtomWall = new Quaternion(0, 0, 0, 0);
    Quaternion TopWall = new Quaternion(-0, -0, 0, 0);
    Quaternion LeftWall = new Quaternion(0, 45, 45, 0);
    Quaternion RightWall = new Quaternion(0, -45, -45, 0);


    // Use this for initialization
    void Start () {
	    for(int x = 0; x < roomX*10; x=x+10)
        {
            
            for(int y = 0; y < roomY*10; y=y+10)
            {
                Instantiate(floor, new Vector3( x,0, y), new Quaternion());
                if (x == 0)
                {
                    Instantiate(wall, new Vector3(x-5, 5, y), ButtomWall);
                }
                if (x == (roomX-1) * 10)
                {
                    Instantiate(wall, new Vector3(x +5, 5, y), TopWall);
                }
                if (y == 0)
                {
                    Instantiate(wall, new Vector3(x, 5, y-5), LeftWall);
                }
                if (y == (roomY-1) * 10)
                {
                    Instantiate(wall, new Vector3(x, 5, y+5), RightWall);
                }
            }
        }
           
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(roomX);
	}
}
