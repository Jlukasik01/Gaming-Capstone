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
            float roomSize = 30.0f; //how far apart to place rooms
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
        referenceLevel[currentX, currentY].GetComponent<RoomSpawnerController>().hasRoom = true;
        currentRoom = generatedLevel[currentX, currentY];
        generateLevel();
        levelsCreated++;
    }

    //Fills referenceLevel with rooms, must be called before generateLevel()
    void generateLevel()
    {
        Debug.Log("generateLevel()");
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

            if(i == 0)
            {
                Debug.Log("generateRoom(), checking north for room");
                if (currentY > 0 && referenceLevel[currentX, currentY - 1].GetComponent<RoomSpawnerController>().hasRoom == false)
                {
                    generateIndividualRoom(currentX, currentY - 1);
                   
                }
                i++;
            }
            if(i == 1)
            {
                Debug.Log("generateRoom(), checking west for room");
                if (currentX > 0 && referenceLevel[currentX - 1, currentY].GetComponent<RoomSpawnerController>().hasRoom == false)
                {
                    generateIndividualRoom(currentX - 1, currentY);
                }
                i++;
            }
            if(i == 2)
            {
                Debug.Log("generateRoom(), checking south for room");
                if (currentY < width - 1 && referenceLevel[currentX, currentY + 1].GetComponent<RoomSpawnerController>().hasRoom == false)
                {
                    generateIndividualRoom(currentX, currentY + 1);
                }
                i++;
            }

            if(i == 3)
            {
                Debug.Log("generateRoom(), checking east for room");
                if (currentX < length - 1&& referenceLevel[currentX + 1, currentY].GetComponent<RoomSpawnerController>().hasRoom == false)
                {
                    generateIndividualRoom(currentX + 1, currentY);
                }
                i++;
            }
        }
        Debug.Log("generateRoom(), setting currentRoom");
    currentRoom = findUseableRoom();
        Debug.Log("generateRoom(), current room =");
        Debug.Log(currentRoom);
    }
       
    //Generates a room based on adjacent doors or empty space at X,Y
    void generateIndividualRoom(int x, int y)
    {
        bool usingNorthDoor = false;
        bool usingSouthDoor = false;
        bool usingEastDoor = false;
        bool usingWestDoor = false;

        requiredDoorCounter = 0;
        optionalDoorCounter = 0;

        //if on north border, check south, east, west
        if (y == 0)
        {
            //In north west corner, check south, east
            if (x == 0)
            {
                //checking south
                if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if(generatedLevel[x, y + 1].GetComponent<RoomController>().hasNorthDoor == true)
                    {
                        requiredDoorCounter++;
                    }
                }
                else if(referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == false)
                {
                    optionalDoorCounter++;
                }

                //checking east
                if (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if (generatedLevel[x + 1, y].GetComponent<RoomController>().hasWestDoor == true)
                    {
                        requiredDoorCounter++;
                    }
                }
                else if (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == false)
                {
                    optionalDoorCounter++;
                }
            }
            // in north east corner, check south, west
            else if (x == length - 1)
            {

                //checking south
                if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if (generatedLevel[x, y + 1].GetComponent<RoomController>().hasNorthDoor == true)
                    {
                        requiredDoorCounter++;
                    }
                }
                else if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == false)
                {
                    optionalDoorCounter++;
                }

                //checking west
                if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if (generatedLevel[x - 1, y].GetComponent<RoomController>().hasEastDoor == true)
                    {
                        requiredDoorCounter++;
                    }
                }
                else if(referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == false)
                {
                    optionalDoorCounter++;
                }
            }
            //not on corner, check south, east, west
            else
            {
                //checking south
                if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if (generatedLevel[x, y + 1].GetComponent<RoomController>().hasNorthDoor == true)
                    {
                        requiredDoorCounter++;
                    }
                }
                else if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == false)
                {
                    optionalDoorCounter++;
                }

                //checking east
                if (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if (generatedLevel[x + 1, y].GetComponent<RoomController>().hasWestDoor == true)
                    {
                        requiredDoorCounter++;
                    }
                }
                else if (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == false)
                {
                    optionalDoorCounter++;
                }

                //checking west
                if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if (generatedLevel[x - 1, y].GetComponent<RoomController>().hasEastDoor == true)
                    {
                        requiredDoorCounter++;
                    }
                }
                else if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == false)
                {
                    optionalDoorCounter++;
                }
            }
        }
        //if on south border, check north, east, west
        else if (y == width - 1)
        {
            
            //on south west corner, check north, east
            if (x == 0)
            {
                //checking north
                if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if (generatedLevel[x, y - 1].GetComponent<RoomController>().hasWestDoor == true)
                    {
                        requiredDoorCounter++;
                    }
                }
                else if(referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    optionalDoorCounter++;
                }

                //checking east
                if (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if (generatedLevel[x + 1, y].GetComponent<RoomController>().hasWestDoor == true)
                    {
                        requiredDoorCounter++;
                    }
                }
                else if (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == false)
                {
                    optionalDoorCounter++;
                }
            }

            //on south east corner, check north, west
            else if (x == length - 1)
            {

                //checking north
                if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if (generatedLevel[x, y - 1].GetComponent<RoomController>().hasWestDoor == true)
                    {
                        requiredDoorCounter++;
                    }
                }
                else if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    optionalDoorCounter++;
                }

                //checking west
                if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if (generatedLevel[x - 1, y].GetComponent<RoomController>().hasEastDoor == true)
                    {
                        requiredDoorCounter++;
                    }
                }
                else if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == false)
                {
                    optionalDoorCounter++;
                }
            }

            //not on corner, check north, east, west
            else
            {
                //checking north
                if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if (generatedLevel[x, y - 1].GetComponent<RoomController>().hasWestDoor == true)
                    {
                        requiredDoorCounter++;
                    }
                }
                else if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    optionalDoorCounter++;
                }

                //checking west
                if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if (generatedLevel[x - 1, y].GetComponent<RoomController>().hasEastDoor == true)
                    {
                        requiredDoorCounter++;
                    }
                }
                else if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == false)
                {
                    optionalDoorCounter++;
                }
                //checking east
                if (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if (generatedLevel[x + 1, y].GetComponent<RoomController>().hasWestDoor == true)
                    {
                        requiredDoorCounter++;
                    }
                }
                else if (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == false)
                {
                    optionalDoorCounter++;
                }
            }
        }

        //if on east border, check north, south, west, already checked for corner
        else if (x == length - 1)
        {

            //checking north
            if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                if (generatedLevel[x, y - 1].GetComponent<RoomController>().hasWestDoor == true)
                {
                    requiredDoorCounter++;
                }
            }
            else if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                optionalDoorCounter++;
            }

            //checking south
            if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                if (generatedLevel[x, y + 1].GetComponent<RoomController>().hasNorthDoor == true)
                {
                    requiredDoorCounter++;
                }
            }
            else if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == false)
            {
                optionalDoorCounter++;
            }

            //checking west
            if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                if (generatedLevel[x - 1, y].GetComponent<RoomController>().hasEastDoor == true)
                {
                    requiredDoorCounter++;
                }
            }
            else if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == false)
            {
                optionalDoorCounter++;
            }

        }

        //if on west border, check north, south, east, already checked for corner
        else if (x == 0)
        {

            //checking north
            if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                if (generatedLevel[x, y - 1].GetComponent<RoomController>().hasWestDoor == true)
                {
                    requiredDoorCounter++;
                }
            }
            else if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                optionalDoorCounter++;
            }

            //checking south
            if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                if (generatedLevel[x, y + 1].GetComponent<RoomController>().hasNorthDoor == true)
                {
                    requiredDoorCounter++;
                }
            }
            else if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == false)
            {
                optionalDoorCounter++;
            }

            //checking east
            if (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                if (generatedLevel[x + 1, y].GetComponent<RoomController>().hasWestDoor == true)
                {
                    requiredDoorCounter++;
                }
            }
            else if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == false)
            {
                optionalDoorCounter++;
            }
        }

        //else in center, check north, south, east, west
        else
        {
            //checking north
            if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                if (generatedLevel[x, y - 1].GetComponent<RoomController>().hasWestDoor == true)
                {
                    requiredDoorCounter++;
                }
            }
            else if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                optionalDoorCounter++;
            }

            //checking south
            if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                if (generatedLevel[x, y + 1].GetComponent<RoomController>().hasNorthDoor == true)
                {
                    requiredDoorCounter++;
                }
            }
            else if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == false)
            {
                optionalDoorCounter++;
            }

            //checking east
            if (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if (generatedLevel[x + 1, y].GetComponent<RoomController>().hasWestDoor == true)
                    {
                        requiredDoorCounter++;
                    }
                }
                else if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == false)
                {
                    optionalDoorCounter++;
                }
            //checking west
            if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                if (generatedLevel[x - 1, y].GetComponent<RoomController>().hasEastDoor == true)
                {
                    requiredDoorCounter++;
                }
            }
            else if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == false)
            {
                optionalDoorCounter++;
            }
        }










        /*
        //Checking for north adjacent
        if (y > 0)
        {
            if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == false)
            {
                optionalDoorCounter++;
                usingNorthDoor = true;
            }
            else if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                if (generatedLevel[x, y - 1].GetComponent<RoomController>().hasSouthDoor == true)
                {
                    requiredDoorCounter++;
                    usingNorthDoor = true;
                }
            }
        }
        

        //checking for south adjacent
        if(y < width - 1)
        {
            if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == false)
            {
                optionalDoorCounter++;
                usingSouthDoor = true;
            }
            else if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                if (generatedLevel[x, y + 1].GetComponent<RoomController>().hasNorthDoor == true)
                {
                    requiredDoorCounter++;
                    usingSouthDoor = true;
                }
            }
        }
       

        //checking for west adjacent
        if(x > 0)
        {
            if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == false)
            {
                optionalDoorCounter++;
                usingWestDoor = true;
            }
            else if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                if (generatedLevel[x - 1, y].GetComponent<RoomController>().hasEastDoor == true)
                {
                    requiredDoorCounter++;
                    usingWestDoor = true;
                }
            }
        }
        

        //checking for east adjacent
        if(x < length - 1)
        {
            if (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == false)
            {
                optionalDoorCounter++;
                usingEastDoor = true;
            }
            else if (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                if (generatedLevel[x + 1, y].GetComponent<RoomController>().hasWestDoor == true)
                {
                    requiredDoorCounter++;
                    usingEastDoor = true;
                }
            }
        }
        */

        //create proper room
        int roomToPick;
        switch (requiredDoorCounter)
        {
            case 0:
                Debug.Log("No required doors");
                //generate a 1, 2, 3, or 4 door room
                switch (Random.Range(1, 5))
                {
                    case 1:
                        //generate 1 door room

                        roomToPick = Random.Range(0, roomsOneDoor.Length);
                        generatedLevel[x, y] = (GameObject)Instantiate(roomsOneDoor[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                        break;
                    case 2:
                        if ((usingNorthDoor == true || usingSouthDoor == true) && (usingEastDoor == true || usingWestDoor == true))
                        {
                            //generate corner
                            roomToPick = Random.Range(0, roomsCorner.Length);
                            generatedLevel[x, y] = (GameObject)Instantiate(roomsCorner[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                        }
                        else
                        {
                            //generate hallway
                            roomToPick = Random.Range(0, roomsTwoDoor.Length);
                            generatedLevel[x, y] = (GameObject)Instantiate(roomsTwoDoor[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                        }
                        break;
                    case 3:
                        //generate 3 door room
                        roomToPick = Random.Range(0, roomsThreeDoor.Length);
                        generatedLevel[x, y] = (GameObject)Instantiate(roomsThreeDoor[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                        break;
                    case 4:
                        //generate 4 door room
                        roomToPick = Random.Range(0, roomsFourDoor.Length);
                        generatedLevel[x, y] = (GameObject)Instantiate(roomsFourDoor[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                        break;
                }
                break;
            case 1:
                if (optionalDoorCounter > 1)
                {
                    //generate a 1, 2, 3, or 4 door room
                    switch(Random.Range(1,5))
                    {
                        case 1:
                            //generate 1 door room
                            
                            roomToPick = Random.Range(0, roomsOneDoor.Length);
                            generatedLevel[x, y] = (GameObject)Instantiate(roomsOneDoor[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                            break;
                        case 2:
                            if((usingNorthDoor == true || usingSouthDoor == true) && (usingEastDoor == true || usingWestDoor == true))
                            {
                                //generate corner
                                roomToPick = Random.Range(0, roomsCorner.Length);
                                generatedLevel[x, y] = (GameObject)Instantiate(roomsCorner[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                            }
                            else
                            {
                                //generate hallway
                                roomToPick = Random.Range(0, roomsTwoDoor.Length);
                                generatedLevel[x, y] = (GameObject)Instantiate(roomsTwoDoor[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                            }
                            break;
                        case 3:
                            //generate 3 door room
                            roomToPick = Random.Range(0, roomsThreeDoor.Length);
                            generatedLevel[x, y] = (GameObject)Instantiate(roomsThreeDoor[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                            break;
                        case 4:
                            //generate 4 door room
                            roomToPick = Random.Range(0, roomsFourDoor.Length);
                            generatedLevel[x, y] = (GameObject)Instantiate(roomsFourDoor[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                            break;
                    }

                }
                else
                {
                    //generate a 1 door room
                    roomToPick = Random.Range(0, roomsOneDoor.Length);
                    generatedLevel[x, y] = (GameObject)Instantiate(roomsOneDoor[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                }
                break;
            case 2:
                if (optionalDoorCounter > 2)
                {
                    //generate a 2, 3, or 4 door room
                    switch (Random.Range(2, 5))
                    {

                        case 2:
                            if ((usingNorthDoor == true || usingSouthDoor == true) && (usingEastDoor == true || usingWestDoor == true))
                            {
                                //generate corner
                                roomToPick = Random.Range(0, roomsCorner.Length);
                                generatedLevel[x, y] = (GameObject)Instantiate(roomsCorner[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                            }
                            else
                            {
                                //generate hallway
                                roomToPick = Random.Range(0, roomsTwoDoor.Length);
                                generatedLevel[x, y] = (GameObject)Instantiate(roomsTwoDoor[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                            }
                            break;
                        case 3:
                            //generate 3 door room
                            roomToPick = Random.Range(0, roomsThreeDoor.Length);
                            generatedLevel[x, y] = (GameObject)Instantiate(roomsThreeDoor[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                            break;
                        case 4:
                            //generate 4 door room
                            roomToPick = Random.Range(0, roomsFourDoor.Length);
                            generatedLevel[x, y] = (GameObject)Instantiate(roomsFourDoor[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                            break;
                    }
                }
                else if ((usingNorthDoor == true || usingSouthDoor == true) && (usingEastDoor == true || usingWestDoor == true))
                {
                    //generate corner
                    roomToPick = Random.Range(0, roomsCorner.Length);
                    generatedLevel[x, y] = (GameObject)Instantiate(roomsCorner[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                }
                else
                {
                    //generate a 2 door room
                    roomToPick = Random.Range(0, roomsTwoDoor.Length);
                    generatedLevel[x, y] = (GameObject)Instantiate(roomsTwoDoor[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);

                }
                break;
            case 3:
                if (optionalDoorCounter > 3)
                {
                    //generatea 3 or 4 door room
                    switch (Random.Range(3, 5))
                    {

                        case 3:
                            //generate 3 door room
                            roomToPick = Random.Range(0, roomsThreeDoor.Length);
                            generatedLevel[x, y] = (GameObject)Instantiate(roomsThreeDoor[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                            break;
                        case 4:
                            //generate 4 door room
                            roomToPick = Random.Range(0, roomsFourDoor.Length);
                            generatedLevel[x, y] = (GameObject)Instantiate(roomsFourDoor[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                            break;
                    }
                }
                else
                {
                    //generate a 3 door room
                    roomToPick = Random.Range(0, roomsThreeDoor.Length);
                    generatedLevel[x, y] = (GameObject)Instantiate(roomsThreeDoor[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                }
                break;
            case 4:
                //generate 4 door room
                roomToPick = Random.Range(0, roomsFourDoor.Length);
                generatedLevel[x, y] = (GameObject)Instantiate(roomsFourDoor[roomToPick], referenceLevel[x, y].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                break;
        }

        referenceLevel[x, y].GetComponent<RoomSpawnerController>().hasRoom = true;
        generatedRoom = generatedLevel[x, y];
        //rotate room into position
        rotateRoom(x, y);

    }

    //finds and returns a room that is not surrounded by other rooms
    GameObject findUseableRoom()
    {
        int adjacentFound;
        for (int x = 0; x < length - 1; x++)
        {
            for (int y = 0; y < width - 1; y++)
            {
                adjacentFound = 0;
                if (referenceLevel[x, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    //if on north border, check south, east, west
                    if (y == 0)
                    {
                        adjacentFound++;
                        //In north west corner, check south, east
                        if (x == 0)
                        {
                            adjacentFound++;
                            //checking south
                            if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                            {
                                adjacentFound++;
                            }
                            //checking east
                            if (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                            {
                                adjacentFound++;
                            }
                        }
                        // in north east corner, check south, west
                        else if (x == length - 1)
                        {
                            adjacentFound++;
                            //checking south
                            if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                            {
                                adjacentFound++;
                            }
                            //checking west
                            if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                            {
                                adjacentFound++;
                            }
                        }
                        //not on corner, check south, east, west
                        else
                        {
                            //checking south
                            if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                            {
                                adjacentFound++;
                            }
                            //checking east
                            if (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                            {
                                adjacentFound++;
                            }
                            //checking west
                            if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                            {
                                adjacentFound++;
                            }
                        }
                    }
                    //if on south border, check north, east, west
                    else if (y == width - 1)
                    {
                        adjacentFound++;
                        //on south west corner, check north, east
                        if(x == 0)
                        {
                            adjacentFound++;
                            //checking north
                            if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                            {
                                adjacentFound++;
                            }
                            //checking east
                            if (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                            {
                                adjacentFound++;
                            }
                        }

                        //on south east corner, check north, west
                        else if(x == length - 1)
                        {
                            adjacentFound++;
                            //checking north
                            if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                            {
                                adjacentFound++;
                            }
                            //checking west
                            if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                            {
                                adjacentFound++;
                            }
                        }

                        //not on corner, check north, east, west
                        else
                        {
                            //checking north
                            if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                            {
                                adjacentFound++;
                            }
                            //checking west
                            if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                            {
                                adjacentFound++;
                            }
                            //checking east
                            if (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                            {
                                adjacentFound++;
                            } 
                        }
                    }

                    //if on east border, check north, south, west, already checked for corner
                    else if (x == length - 1)
                    {
                        adjacentFound++;
                        //checking north
                        if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                        {
                            adjacentFound++;
                        }
                        //checking south
                        if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                        {
                            adjacentFound++;
                        }
                        //checking west
                        if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                        {
                            adjacentFound++;
                        }

                    }

                    //if on west border, check north, south, east, already checked for corner
                    else if(x == 0)
                    {
                        adjacentFound++;
                        //checking north
                        if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                        {
                            adjacentFound++;
                        }
                        //checking south
                        if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                        {
                            adjacentFound++;
                        }
                        //checking east
                        if (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                        {
                            adjacentFound++;
                        }
                    }

                    //else in center, check north, south, east, west
                    else
                    {
                        //checking north
                        if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                        {
                            adjacentFound++;
                        }
                        //checking south
                        if (referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                        {
                            adjacentFound++;
                        }
                        //checking east
                        if (referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                        {
                            adjacentFound++;
                        }
                        //checking west
                        if (referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                        {
                            adjacentFound++;
                        }
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
            if(y > 0)
            {
                if (referenceLevel[x, y - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if (generatedRoom.GetComponent<RoomController>().hasNorthDoor != generatedLevel[x, y - 1].GetComponent<RoomController>().hasSouthDoor)
                    {
                        Debug.Log("Rotating Room");
                        changeDoorBools();
                        generatedRoom.transform.Rotate(0, -90,0,Space.Self);
                        continue;
                    }
                }
                
            }

            //checking southern door
            if (y < width - 1)
            {
                if(referenceLevel[x, y + 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if (generatedRoom.GetComponent<RoomController>().hasSouthDoor != generatedLevel[x, y + 1].GetComponent<RoomController>().hasNorthDoor)
                    {
                        Debug.Log("Rotating Room");
                        changeDoorBools();
                        generatedRoom.transform.Rotate(0, -90, 0, Space.Self);
                        continue;
                    }
                }
                
            }

            //checking eastern door
            if (x < length - 1)
            {
                if(referenceLevel[x + 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if (generatedRoom.GetComponent<RoomController>().hasEastDoor != generatedLevel[x + 1, y].GetComponent<RoomController>().hasWestDoor)
                    {
                        Debug.Log("Rotating Room");
                        changeDoorBools();
                        generatedRoom.transform.Rotate(0, -90, 0, Space.Self);
                        continue;
                    }
                }
               
            }

            //checking western door
            if (x > 0)
            {
                if(referenceLevel[x - 1, y].GetComponent<RoomSpawnerController>().hasRoom == true)
                {
                    if (generatedRoom.GetComponent<RoomController>().hasWestDoor != generatedLevel[x - 1, y].GetComponent<RoomController>().hasEastDoor)
                    {
                        Debug.Log("Rotating Room");
                        changeDoorBools();
                        generatedRoom.transform.Rotate(0, -90, 0, Space.Self);
                        continue;
                    }
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
        futureHasNorthDoor = generatedRoom.GetComponent<RoomController>().hasEastDoor;
        futureHasSouthDoor = generatedRoom.GetComponent<RoomController>().hasWestDoor;
        futureHasEastDoor = generatedRoom.GetComponent<RoomController>().hasSouthDoor;
        futureHasWestDoor = generatedRoom.GetComponent<RoomController>().hasNorthDoor;

        /*
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
        */
        generatedRoom.GetComponent<RoomController>().hasNorthDoor = futureHasNorthDoor;
        generatedRoom.GetComponent<RoomController>().hasSouthDoor = futureHasSouthDoor;
        generatedRoom.GetComponent<RoomController>().hasEastDoor = futureHasEastDoor;
        generatedRoom.GetComponent<RoomController>().hasWestDoor = futureHasWestDoor;
    }

    
}