using UnityEngine;
using System.Collections;
using System.Linq;

public class RoomGenerator : MonoBehaviour
{

    public GameObject[,] referenceLevel;               //************why have a reference lvl?
    public GameObject[,] generatedLevel;
    public GameObject roomSpawner;
    public GameObject[] rooms;
    public int currentLevel; //What level player is currently on
    public int completedLevels; // How many levels have been completed
    public int averageNumRooms; //Set for number of rooms to spawn
    public int length; //Do NOT set in inspector
    public int width;// Do NOT set in inspector
    public int levelheight;

    // Use this for initialization
    void Start()
    {
        rooms = Resources.LoadAll<GameObject>("RoomsToLoad"); //**********Good Idea
        Debug.Log(Resources.LoadAll<GameObject>("RoomsToLoad"));
        GenerateLevel();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateLevel()
    {
        if (averageNumRooms > 0)
        {
            GameObject generatedRoom; //room that is being created and placed
            float roomSize = 22.5f; //how far apart to place rooms
            length = Mathf.RoundToInt(Mathf.Sqrt(averageNumRooms));
            width = Mathf.RoundToInt(Mathf.Sqrt(averageNumRooms));
            referenceLevel = new GameObject[length, width];
            generatedLevel = new GameObject[length, width];
            roomSpawner = GameObject.FindGameObjectWithTag("RoomSpawner");    //**************** if your spawning these down below, just use a prefab to spawn all of them, not one in the scene

            //Generates empty objects to reference for rooms
            for (int x = 0; x < length; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    Vector3 pos = new Vector3(x * roomSize, 0, y * roomSize);
                    referenceLevel[x, y] = (GameObject)Instantiate(roomSpawner, pos, Quaternion.identity);
                    referenceLevel[x, y].gameObject.GetComponent<RoomSpawnerController>().pos = pos;
                    Debug.Log("GENERATING SPAWNER");
                }
            }

            Debug.Log("LOADING ROOMS NOW");
            //Assign rooms to references
            for (int x = 0; x < length; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    //TEST GENERATED ROOM
                    generatedRoom = Instantiate(rooms[0].gameObject, referenceLevel[x, y].gameObject.GetComponent<RoomSpawnerController>().pos, Quaternion.identity) as GameObject;


                    Debug.Log("LOADING ROOM");
                    int doorCounter = 0;
                    //checking for northern door
                    if (generatedLevel[x, y - 1] != null && generatedLevel[x, y - 1].GetComponent<RoomController>().hasNorthDoor == true)
                    {
                        doorCounter++;
                    }
                    //checking for southern door
                    if (generatedLevel[x, y + 1] != null && generatedLevel[x, y + 1].GetComponent<RoomController>().hasSouthDoor == true)
                    {
                        doorCounter++;
                    }
                    //checking for eastern door
                    if (generatedLevel[x + 1, y] != null && generatedLevel[x + 1, y].GetComponent<RoomController>().hasEastDoor == true)
                    {
                        doorCounter++;
                    }
                    //checking for western door
                    if (generatedLevel[x - 1, y] != null && generatedLevel[x - 1, y].GetComponent<RoomController>().hasWestDoor == true)
                    {
                        doorCounter++;
                    }


                    //**********************My suggetion would be to not rotate rooms on the fly, instead have versions of each room already in the resources rotated
                    // and just redo the bools for the doors for each rotated prefab. The code could just find random rooms to place next to already existing rooms with doors, 
                    //EI this room as a east room place a random room to the east with a west door

                    switch (doorCounter)
                    {
                        case 0:
                            Debug.Log("No doors.....");
                            break;
                        case 1:
                            //1 door, generate 1 door room, check for rotations
                            rotateRoom(generatedRoom,x,y);
                            break;
                        case 2:
                            //2 door, check if corner or against wall, generate 2 door room, check for rotations
                            rotateRoom(generatedRoom,x,y);
                            break;
                        case 3:
                            // 3 door, generate 3 door room, check for rotations
                            rotateRoom(generatedRoom,x,y);
                            break;
                        case 4:
                            // 4 door, generate 4 door room, check for rotations
                            rotateRoom(generatedRoom,x,y);
                            break;
                    }
                }

            }

            Debug.Log("DONE WITH ROOMS");
        }
    }

    void DestroyLevel()
    {
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                Destroy(generatedLevel[x, y]);
                Destroy(referenceLevel[x, y]);
            }
        }
    }

    void rotateRoom(GameObject generatedRoom, int x, int y)
    { 
        for(int breakCounter = 0; breakCounter < 4; breakCounter++)
        {
            //checking northern door
            if (generatedRoom.GetComponent<RoomController>().hasNorthDoor != generatedLevel[x, y - 1].GetComponent<RoomController>().hasNorthDoor)
            {
                Debug.Log("Rotating Room");
                //rotate generatedRoom
            }
            //checking southern door
            else if (generatedRoom.GetComponent<RoomController>().hasSouthDoor != generatedLevel[x, y + 1].GetComponent<RoomController>().hasSouthDoor)
            {
                Debug.Log("Rotating Room");
                //rotate generatedRoom
            }
            //checking eastern door
            else if (generatedRoom.GetComponent<RoomController>().hasEastDoor != generatedLevel[x + 1, y].GetComponent<RoomController>().hasEastDoor)
            {
                Debug.Log("Rotating Room");
                //rotate generatedRoom
            }
            //checking western door
            else if (generatedRoom.GetComponent<RoomController>().hasWestDoor != generatedLevel[x - 1, y].GetComponent<RoomController>().hasWestDoor)
            {
                Debug.Log("Rotating Room");
                //rotate generatedRoom
            }
            else
                Debug.Log("Room fits/rotated 4 times");
                break;
        }
    }
}