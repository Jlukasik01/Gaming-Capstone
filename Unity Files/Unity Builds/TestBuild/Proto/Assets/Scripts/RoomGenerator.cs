using UnityEngine;
using System.Collections;
using System.Linq;

public class RoomGenerator : MonoBehaviour
{

    public GameObject[,] referenceLevel;
    public GameObject[,] generatedLevel;
    public GameObject roomSpawner; //reference spawner
    public GameObject[] roomsOneDoor;
    public GameObject[] roomsCorner;
    public GameObject[] roomsTwoDoor;
    public GameObject[] roomsThreeDoor;
    public GameObject[] roomsFourDoor;
    public GameObject[] roomsStartingRoom;
    public GameObject currentRoom; //current room being used as origin
    public GameObject generatedRoom; //room that is being created and placed
    public int currentLevel; //What level player is currently on
    public int completedLevels; // How many levels have been completed
    public int averageNumRooms; //Set for number of rooms to spawn
    public int levelheight;
    public int length;
    public int width;
    public int requiredDoorCounter;
    public int optionalDoorCounter;
    public int currentY;
    public int currentX;
    public int levelsCreated;

    // Use this for initialization
    void Start()
    {
        //rooms = Resources.LoadAll<GameObject>("RoomsToLoad");
        // Debug.Log(Resources.LoadAll<GameObject>("RoomsToLoad"));
        //GenerateLevel();
        levelsCreated = 0;
        roomsOneDoor = Resources.LoadAll<GameObject>("RoomsToLoad/OneDoor");
        roomsCorner = Resources.LoadAll<GameObject>("RoomsToLoad/Corner");
        roomsTwoDoor = Resources.LoadAll<GameObject>("RoomsToLoad/TwoDoor");
        roomsThreeDoor = Resources.LoadAll<GameObject>("RoomsToLoad/ThreeDoor");
        roomsFourDoor = Resources.LoadAll<GameObject>("RoomsToLoad/Fourdoor");
        roomsStartingRoom = Resources.LoadAll<GameObject>("RoomsToLoad/StartingRoom");
        newLevel();
    }

    void generateReferenceLevel()
    {
        if (averageNumRooms > 0)
        {
            float roomSize = 22.5f; //how far apart to place rooms
            length = Mathf.RoundToInt(Mathf.Sqrt(averageNumRooms));
            width = Mathf.RoundToInt(Mathf.Sqrt(averageNumRooms));
            referenceLevel = new GameObject[length, width];
            roomSpawner = GameObject.FindGameObjectWithTag("RoomSpawner");

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
        }
    }

    void destroyLevel()
    {
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                Destroy(generatedLevel[x, y]);
            }
        }
    }

    void destroyReferenceLevel()
    {
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                Destroy(referenceLevel[x, y]);
            }
        }
    }

    //destorys old level and creates a new one;
    void newLevel()
    {
        if(levelsCreated > 0)
        {
            destroyReferenceLevel();
            destroyLevel();
        }
        generateReferenceLevel();
        generatedLevel = new GameObject[length, width];
        currentX = Random.Range(1, length - 1);
        currentY = Random.Range(1, width - 1);
        generatedLevel[currentX, currentY] = (GameObject)Instantiate(roomsStartingRoom[0], referenceLevel[currentX, currentY].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
        generateLevel();
        levelsCreated++;
    }

    //Fills referenceLevel with rooms, must be called before generateLevel()
    void generateLevel()
    {
        if (currentRoom == null)
        {
            Debug.Log("Done generating level");
        }
        else
        {
            generateRooms();
            generateLevel();
        }
    }

    //generates up to 4 rooms surrounding currentRoom
    void generateRooms()
    {
        //generate a room in each direction
        for (int i = 0; i < 4; i++)
        {
            switch(i)
            {
                //checking northern side
                case 0:
                    if (currentY != 0 && referenceLevel[currentX, currentY - 1].GetComponent<RoomSpawnerController>().hasRoom == false)
                    {
                        generateIndividualRoom(currentX, currentY - 1);
                    }
                    break;

                //checking western side
                case 1:
			        if (currentX != 0 && referenceLevel[currentX - 1, currentY].GetComponent<RoomSpawnerController>().hasRoom == false)
                    {
                        generateIndividualRoom(currentX - 1, currentY);  
                    }
                    break;

                //checking southern side
                case 2:
                    if (currentY != width && referenceLevel[currentX, currentY + 1].GetComponent<RoomSpawnerController>().hasRoom == false)
                    {
                        generateIndividualRoom(currentX, currentY + 1);
                    }
                    break;

                //checking eastern side
                case 3:
			        if (currentX != length && referenceLevel[currentX + 1, currentY].GetComponent<RoomSpawnerController>().hasRoom == false)
                    {
                        generateIndividualRoom(currentX + 1,currentY);
                    }
                    break;

            }
        }
    currentRoom = findUseableRoom();
    }
       
    //Generates a room based on adjacent doors or empty space at X,Y
    void generateIndividualRoom(int x, int y)
    {
        requiredDoorCounter = 0;
        optionalDoorCounter = 0;
        //Checking for north adjacent
        if ((referenceLevel[x, y - 1] != null) || (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == false))
        {
            optionalDoorCounter++;
        }
        else if ((referenceLevel[x, y - 1] != null) && (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true))
        {
            if (generatedLevel[x, y - 1].GetComponent<RoomController>().hasSouthDoor == true)
            {
                requiredDoorCounter++;
            }
        }

        //checking for south adjacent
        if ((referenceLevel[x, y + 1] != null) || (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == false))
        {
            optionalDoorCounter++;
        }
        else if ((referenceLevel[x, y + 1] != null) && (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == true))
        {
            if (generatedLevel[x, y + 1].GetComponent<RoomController>().hasNorthDoor == true)
            {
                requiredDoorCounter++;
            }
        }

        //checking for west adjacent
        if ((referenceLevel[x - 1, y] != null) || (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == false))
        {
            optionalDoorCounter++;
        }
        else if ((referenceLevel[x - 1, y] != null) && (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == true))
        {
            if (generatedLevel[x - 1, y].GetComponent<RoomController>().hasEastDoor == true)
            {
                requiredDoorCounter++;
            }
        }

        //checking for east adjacent
        if ((referenceLevel[x + 1, y] != null) || (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == false))
        {
            optionalDoorCounter++;
        }
        else if ((referenceLevel[x + 1, y] != null) && (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == true))
        {
            if (generatedLevel[x + 1, y].GetComponent<RoomController>().hasWestDoor == true)
            {
                requiredDoorCounter++;
            }
        }


        //create proper room
        int roomToPick;
        switch (requiredDoorCounter)
        {
            case 0:
                Debug.Log("No doors");
                break;
            case 1:
                if (optionalDoorCounter > 1)
                {
                    //generate 1, 2, 3, or 4 door room
                    

                }
                else
                {
                    //generate a 1 door room
                    roomToPick = Random.Range(0, roomsOneDoor.Length);
                }
                break;
            case 2:
                if (optionalDoorCounter > 2)
                {
                    //generatea 2, 3, or 4 door room
                }
                else
                {
                    //generate a 2 door room
                    
                }
                break;
            case 3:
                if (optionalDoorCounter > 3)
                {
                    //generatea 3 or 4 door room
                }
                else
                {
                    //generate a 3 door room
                    roomToPick = Random.Range(0, roomsThreeDoor.Length);
                }
                break;
            case 4:
                //generate 4 door room
                roomToPick = Random.Range(0, roomsFourDoor.Length);
                break;
        }

        //rotate room into position
        rotateRoom(currentX, currentY);

    }

    //finds and returns a room that is not surrounded by other rooms
    GameObject findUseableRoom()
    {
        int adjacentFound;
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                adjacentFound = 0;
                if (referenceLevel[x, y].GetComponent<RoomSpawnerController>().hasRoom == true);
                {
                    if ((referenceLevel[x, y - 1] == null) || (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true))
                    {
                        adjacentFound++;
                    }
                    if ((referenceLevel[x, y + 1] == null) || (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == true))
                    {
                        adjacentFound++;
                    }
                    if ((referenceLevel[x - 1, y] == null) || (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == true))
                    {
                        adjacentFound++;
                    }
                    if ((referenceLevel[x + 1, y] == null) || (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == true))
                    {
                        adjacentFound++;
                    }
                    if(adjacentFound < 4)
                    {
                        currentX = x;
                        currentY = y;
                        return referenceLevel[x, y];
                    }
                }
            }
        }
        return null;
    }

    //rotates generatedRoom
    void rotateRoom(int x, int y)
    { 
        for(int breakCounter = 0; breakCounter < 4; breakCounter++)
        {
            //checking northern door
            if(generatedLevel[x, y - 1] != null)
            {
                if (generatedRoom.GetComponent<RoomController>().hasNorthDoor != generatedLevel[x, y - 1].GetComponent<RoomController>().hasSouthDoor)
                {
                    Debug.Log("Rotating Room");
                    changeDoorBools();
                    generatedRoom.transform.Rotate(-90, 0, 0, Space.Self);
                    continue;
                }
            }

            //checking southern door
            if (generatedLevel[x, y + 1] != null)
            {
                if (generatedRoom.GetComponent<RoomController>().hasSouthDoor != generatedLevel[x, y + 1].GetComponent<RoomController>().hasNorthDoor)
                {
                    Debug.Log("Rotating Room");
                    changeDoorBools();
                    generatedRoom.transform.Rotate(-90, 0, 0, Space.Self);
                    continue;
                }
            }

            //checking eastern door
            if (generatedLevel[x + 1, y] != null)
            {
                if (generatedRoom.GetComponent<RoomController>().hasEastDoor != generatedLevel[x + 1, y].GetComponent<RoomController>().hasWestDoor)
                {
                    Debug.Log("Rotating Room");
                    changeDoorBools();
                    generatedRoom.transform.Rotate(-90, 0, 0, Space.Self);
                    continue;
                }
            }

            //checking western door
            if (generatedLevel[x - 1, y] != null)
            {
                if (generatedRoom.GetComponent<RoomController>().hasWestDoor != generatedLevel[x - 1, y].GetComponent<RoomController>().hasEastDoor)
                {
                    Debug.Log("Rotating Room");
                    changeDoorBools();
                    generatedRoom.transform.Rotate(-90, 0, 0, Space.Self);
                    continue;
                }
            }
        }
    }

    //based on -90 degree X axis rotation, changes generatedRoom bools based on future 1 rotation
    void changeDoorBools()
    {
        bool futureHasNorthDoor;
        bool futureHasSouthDoor;
        bool futureHasEastDoor;
        bool futureHasWestDoor;

        if (generatedRoom.GetComponent<RoomController>().hasNorthDoor == true)
        {
            futureHasWestDoor = true;
        }
        else
        {
            futureHasWestDoor = false;
        }

        if (generatedRoom.GetComponent<RoomController>().hasSouthDoor == true)
        {
            futureHasEastDoor = true;
        }
        else
        {
            futureHasEastDoor = false;
        }

        if (generatedRoom.GetComponent<RoomController>().hasEastDoor == true)
        {
            futureHasNorthDoor = true;
        }
        else
        {
            futureHasNorthDoor = false;
        }

        if (generatedRoom.GetComponent<RoomController>().hasWestDoor == true)
        {
            futureHasSouthDoor = true;
        }
        else
        {
            futureHasSouthDoor = false;
        }

        generatedRoom.GetComponent<RoomController>().hasNorthDoor = futureHasNorthDoor;
        generatedRoom.GetComponent<RoomController>().hasSouthDoor = futureHasSouthDoor;
        generatedRoom.GetComponent<RoomController>().hasEastDoor = futureHasEastDoor;
        generatedRoom.GetComponent<RoomController>().hasWestDoor = futureHasWestDoor;
    }
}