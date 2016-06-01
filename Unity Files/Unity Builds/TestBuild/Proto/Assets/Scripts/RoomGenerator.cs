using UnityEngine;
using System.Collections;
using System.Linq;

//Responsible for generating the rooms in the game level
public class RoomGenerator : MonoBehaviour
{

    public GameObject[,] referenceLevel;
    public GameObject[,] generatedLevel;
    public GameObject roomSpawner; //reference spawner
    public GameObject[] roomsZeroDoor;
    public GameObject[] roomsOneDoor;
    public GameObject[] roomsCorner;
    public GameObject[] roomsTwoDoor;
    public GameObject[] roomsThreeDoor;
    public GameObject[] roomsFourDoor;
    public GameObject[] roomsStartingRoom;
    public GameObject[] roomsBossRoom;
    public GameObject currentRoom; //current room being used as origin
    public GameObject generatedRoom; //room that is being created and placed
    public GameObject player;
    public GameObject enemyList;
    public int currentLevel; //What level player is currently on
    public int completedLevels; // How many levels have been completed
    public int averageNumRooms; //Set for number of rooms to spawn
    public int levelheight;
    public int length;
    public int width;
    public int requiredDoorCounter;
    public int optionalDoorCounter;
    public int currentZ;
    public int currentX;
    public int levelsCreated;
    public int alphaMonsterChance; //how much of a chance a enemy spawner has to spawn a alpha version of a enemy
    public int alphaMonsterIncrease; //how much alphaMonsterChance increases each level;
    public int roomNumIncrease; //how much averageNumRooms is increased each level;
    public bool continueGeneration;
    
    
   
    // Use this for initialization
    void Start()
    {
        //levelsCreated = 0;
        roomsZeroDoor = Resources.LoadAll<GameObject>("RoomsToLoad/ZeroDoor");
        roomsOneDoor = Resources.LoadAll<GameObject>("RoomsToLoad/OneDoor");
        roomsCorner = Resources.LoadAll<GameObject>("RoomsToLoad/Corner");
        roomsTwoDoor = Resources.LoadAll<GameObject>("RoomsToLoad/TwoDoor");
        roomsThreeDoor = Resources.LoadAll<GameObject>("RoomsToLoad/ThreeDoor");
        roomsFourDoor = Resources.LoadAll<GameObject>("RoomsToLoad/Fourdoor");
        roomsStartingRoom = Resources.LoadAll<GameObject>("RoomsToLoad/StartingRoom");
        roomsBossRoom = Resources.LoadAll<GameObject>("RoomsToLoad/BossRoom");
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        
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
                for (int z = 0; z < width; z++)
                {
                    Vector3 pos = new Vector3(x * roomSize, 0, z * roomSize);
                    referenceLevel[x, z] = (GameObject)Instantiate(roomSpawner, pos, Quaternion.identity);
                    referenceLevel[x, z].gameObject.GetComponent<RoomSpawnerController>().pos = pos;
                    Debug.Log("GENERATING SPAWNER");
                }
            }
        }
    }

    void destroyLevel()
    {
        for (int x = 0; x < length; x++)
        {
            for (int z = 0; z < width; z++)
            {
                Destroy(generatedLevel[x, z]);
            }
        }
    }

    void destroyReferenceLevel()
    {
        for (int x = 0; x < length; x++)
        {
            for (int z = 0; z < width; z++)
            {
                Destroy(referenceLevel[x, z]);
            }
        }
    }

    //destroys old level and creates a new one;
    void newLevel()
    {
        if(levelsCreated > 0)
        {
            destroyReferenceLevel();
            destroyLevel();
            alphaMonsterChance += alphaMonsterIncrease;
            averageNumRooms += roomNumIncrease;
        }
        levelsCreated++;
        generateReferenceLevel();
        generatedLevel = new GameObject[length, width];

        //Generate player starting room
        currentX = Random.Range(1, length - 1);
        currentZ = Random.Range(1, width - 1);
        generatedLevel[currentX, currentZ] = (GameObject)Instantiate(roomsStartingRoom[0], referenceLevel[currentX, currentZ].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
        referenceLevel[currentX, currentZ].GetComponent<RoomSpawnerController>().hasRoom = true;
        currentRoom = generatedLevel[currentX, currentZ];
        generatedRoom = currentRoom;
        player.transform.position = Vector3.Lerp(player.transform.position, referenceLevel[currentX, currentZ].GetComponent<RoomSpawnerController>().pos, 1);
        
        //generate boss room
        findRandomEmptyRoom();
        if (levelsCreated > GetComponent<EnemySpawnerController>().enemyList.Length)
        {
            
            //generatedLevel[currentX, currentZ] = (GameObject)Instantiate(roomsBossRoom[Mathf.RoundToInt(levelsCreated / GetComponent<EnemySpawnerController>().enemyList.Length)], referenceLevel[currentX, currentZ].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
            referenceLevel[currentX, currentZ].GetComponent<RoomSpawnerController>().hasRoom = true;
        }
        else
        {
            generatedLevel[currentX, currentZ] = (GameObject)Instantiate(roomsBossRoom[levelsCreated - 1], referenceLevel[currentX, currentZ].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
            referenceLevel[currentX, currentZ].GetComponent<RoomSpawnerController>().hasRoom = true;
        }
        

        continueGeneration = true;
        generateLevel();
        
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
            //generateLevel();
        }
    }

    //generates up to 4 rooms surrounding currentRoom
    void generateRooms()
    {
        //generate a room in each direction
        Debug.Log("Currently at: " + currentX + " " + currentZ);
        if (currentZ > 0)
        {
            if(referenceLevel[currentX, currentZ - 1].GetComponent<RoomSpawnerController>().hasRoom == false)
            {
                Debug.Log("Generating room at " + currentX + " " + currentZ);
                generateIndividualRoom(currentX, currentZ - 1);
                Debug.Log(generatedRoom);
                currentRoom = generatedRoom;
                //currentZ -= 1;
                generateRooms();
            }
            
        }
        if (currentX > 0)
        {
            if(referenceLevel[currentX - 1, currentZ].GetComponent<RoomSpawnerController>().hasRoom == false)
            {
                Debug.Log("Generating room at " + currentX + " " + currentZ);
                generateIndividualRoom(currentX - 1, currentZ);
                currentRoom = generatedRoom;
                Debug.Log(generatedRoom);
                //currentX -= 1;
                generateRooms();

            }
            
        }
        if (currentZ < width - 1)
        {
            if(referenceLevel[currentX, currentZ + 1].GetComponent<RoomSpawnerController>().hasRoom == false)
            {
                Debug.Log("Generating room at " + currentX + " " + currentZ);
                generateIndividualRoom(currentX, currentZ + 1);
                currentRoom = generatedRoom;
                Debug.Log(generatedRoom);
                //currentZ += 1;  
                generateRooms();
            }
            
        }
        if (currentX < length - 1)
        {
            if(referenceLevel[currentX + 1, currentZ].GetComponent<RoomSpawnerController>().hasRoom == false)
            {
                Debug.Log("Generating room at " + currentX + " " + currentZ);
                generateIndividualRoom(currentX + 1, currentZ);
                currentRoom = generatedRoom;
                Debug.Log(generatedRoom);
                //currentX += 1;
                generateRooms();
            }
           
        }

        if(continueGeneration == true)
        {
            //worst case, look for room
            currentRoom = findUseableRoom();
            if (currentRoom != null)
            {
                Debug.Log("Surrounded, looking for new room.");
                generateRooms();
            }
            else
            {
                continueGeneration = false;
                Debug.Log("Failed to find new room in findUseableRoom(). Level done.");
            }
            
        }
       
        
      
    }
       
    //Generates a room based on adjacent doors or emptz space at x,z
    void generateIndividualRoom(int x, int z)
    {
        bool usingNorthDoor = false;
        bool usingSouthDoor = false;
        bool usingEastDoor = false;
        bool usingWestDoor = false;

        int usingDoors = 0; //counter for how manz door room to spawn
        int extraDoorsWeight = 75; //weight from 0 (never) to 100 (always) to use optional doors on blank rooms, setting to lower results in unaccessible areas and null errors
        int roomToPick;

        if(z > 0)
        {
            //checking South
            if (referenceLevel[x, z - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                if (generatedLevel[x, z - 1].GetComponent<RoomController>().hasNorthDoor == true)
                {
                    usingSouthDoor = true;
                    usingDoors++;
                }
            }
            else if (referenceLevel[x, z - 1].GetComponent<RoomSpawnerController>().hasRoom == false)
            {
                if (Random.Range(1, 100) < extraDoorsWeight)
                {
                    usingSouthDoor = true;
                    usingDoors++;
                }
            }
        }
        if(z < width - 1)
        {
            //checking north
            if (referenceLevel[x, z + 1].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                if (generatedLevel[x, z + 1].GetComponent<RoomController>().hasSouthDoor == true)
                {
                    usingNorthDoor = true;
                    usingDoors++;
                }
            }
            else if (referenceLevel[x, z + 1].GetComponent<RoomSpawnerController>().hasRoom == false)
            {
                if (Random.Range(1, 100) < extraDoorsWeight)
                {
                    usingNorthDoor = true;
                    usingDoors++;
                }
            }
        }
        if(x > 0)
        {
            //checking west
            if (referenceLevel[x - 1, z].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                if (generatedLevel[x - 1, z].GetComponent<RoomController>().hasEastDoor == true)
                {
                    usingWestDoor = true;
                    usingDoors++;
                }
            }
            else if (referenceLevel[x - 1, z].GetComponent<RoomSpawnerController>().hasRoom == false)
            {
                if (Random.Range(1, 100) < extraDoorsWeight)
                {
                    usingWestDoor = true;
                    usingDoors++;
                }
            }
        }
        if(x < length - 1)
        {
            //checking east
            if (referenceLevel[x + 1, z].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                if (generatedLevel[x + 1, z].GetComponent<RoomController>().hasWestDoor == true)
                {
                    usingEastDoor = true;
                    usingDoors++;
                }
            }
            else if (referenceLevel[x + 1, z].GetComponent<RoomSpawnerController>().hasRoom == false)
            {
                if (Random.Range(1, 100) < extraDoorsWeight)
                {
                    usingEastDoor = true;
                    usingDoors++;
                }
            }
        }

        switch (usingDoors)
        {
            case 0:
                //0 adjacent doors, failed all randoms
                //Debug.Log("usingDoors = 0");
                int zeroDoorCounter = 0;
                
                if(z > 0)
                {
                    if (referenceLevel[x, z - 1].GetComponent<RoomSpawnerController>().hasRoom == false)
                    {
                        zeroDoorCounter++;
                        usingSouthDoor = true;
                    }
                }

                if(z < width - 1)
                {
                    if (referenceLevel[x, z + 1].GetComponent<RoomSpawnerController>().hasRoom == false)
                    {
                        zeroDoorCounter++;
                        usingNorthDoor = true;
                    }
                }
                if(x > 0)
                {
                    if (referenceLevel[x - 1, z].GetComponent<RoomSpawnerController>().hasRoom == false)
                    {
                        zeroDoorCounter++;
                        usingWestDoor = true;
                    }
                }
                if(x < length - 1)
                {
                    if (referenceLevel[x + 1, z].GetComponent<RoomSpawnerController>().hasRoom == false)
                    {
                        zeroDoorCounter++;
                        usingEastDoor = true;
                    }
                }
               

                switch(zeroDoorCounter)
                {
                    case 0:
                        //Debug.Log("zeroDoorCounter == 0");
                        roomToPick = Random.Range(0, roomsZeroDoor.Length);
                        generatedLevel[x, z] = (GameObject)Instantiate(roomsZeroDoor[roomToPick], referenceLevel[x, z].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                        usingDoors = 1;
                        break;
                    case 1:
                       // Debug.Log("zeroDoorCounter == 1");
                        roomToPick = Random.Range(0, roomsOneDoor.Length);
                        generatedLevel[x, z] = (GameObject)Instantiate(roomsOneDoor[roomToPick], referenceLevel[x, z].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                        usingDoors = 1;
                        break;
                    case 2:
                        //Debug.Log("zeroDoorCounter == 2");
                        if (((usingNorthDoor == true) && (usingSouthDoor == true)) || ((usingEastDoor == true) && (usingWestDoor == true)))
                        {
                            roomToPick = Random.Range(0, roomsTwoDoor.Length);
                            generatedLevel[x, z] = (GameObject)Instantiate(roomsTwoDoor[roomToPick], referenceLevel[x, z].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                        }
                        else if (((usingNorthDoor == true) || (usingSouthDoor == true)) && ((usingEastDoor == true) || (usingWestDoor == true)))
                        {
                            roomToPick = Random.Range(0, roomsCorner.Length);
                            generatedLevel[x, z] = (GameObject)Instantiate(roomsCorner[roomToPick], referenceLevel[x,z ].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                        }
                        usingDoors = 2;
                        break;
                    case 3:
                        //Debug.Log("zeroDoorCounter == 3");
                        roomToPick = Random.Range(0, roomsThreeDoor.Length);
                        generatedLevel[x, z] = (GameObject)Instantiate(roomsThreeDoor[roomToPick], referenceLevel[x, z].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                        usingDoors = 3;
                        break;
                    case 4:
                        //Debug.Log("zeroDoorCounter == 4");
                        roomToPick = Random.Range(0, roomsFourDoor.Length);
                        generatedLevel[x, z] = (GameObject)Instantiate(roomsFourDoor[roomToPick], referenceLevel[x, z].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                        usingDoors = 4;
                        break;
                }
                break;

            case 1:
                //generate a 1 door room
                roomToPick = Random.Range(0, roomsOneDoor.Length);
                generatedLevel[x, z] = (GameObject)Instantiate(roomsOneDoor[roomToPick], referenceLevel[x, z].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                //Debug.Log("usingDoors = 1");
                break;

            case 2:
                //check corner or 2 door room
                if(((usingNorthDoor == true) && (usingSouthDoor == true)) || ((usingEastDoor == true) && (usingWestDoor == true)))
                {
                    roomToPick = Random.Range(0, roomsTwoDoor.Length);
                    generatedLevel[x, z] = (GameObject)Instantiate(roomsTwoDoor[roomToPick], referenceLevel[x, z].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                }
                else if (((usingNorthDoor == true) || (usingSouthDoor == true)) && ((usingEastDoor == true) || (usingWestDoor == true)))
                {
                    roomToPick = Random.Range(0, roomsCorner.Length);
                    generatedLevel[x, z] = (GameObject)Instantiate(roomsCorner[roomToPick], referenceLevel[x, z].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                }
               // Debug.Log("usingDoors = 2");
                break;

            case 3:
                roomToPick = Random.Range(0, roomsThreeDoor.Length);
                generatedLevel[x, z] = (GameObject)Instantiate(roomsThreeDoor[roomToPick], referenceLevel[x, z].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
               // Debug.Log("usingDoors = 3");
                //generate 3 door room
                break;

            case 4:
                roomToPick = Random.Range(0, roomsFourDoor.Length);
                generatedLevel[x, z] = (GameObject)Instantiate(roomsFourDoor[roomToPick], referenceLevel[x, z].GetComponent<RoomSpawnerController>().pos, Quaternion.identity);
                //Debug.Log("usingDoors = 4");
                //generate 4 door room
                break;
        }
        referenceLevel[x, z].GetComponent<RoomSpawnerController>().hasRoom = true;
        generatedRoom = generatedLevel[x, z];
        /*
        Debug.Log("X -");
        Debug.Log(x);
        Debug.Log("z - ");
        Debug.Log(z);
        Debug.Log("Generatedroom - ");
        Debug.Log(generatedRoom);
        Debug.Log("usingNorthDoor - ");
        Debug.Log(usingNorthDoor);
        Debug.Log("usingSouthDoor - ");
        Debug.Log(usingSouthDoor);
        Debug.Log("usingEastDoor - ");
        Debug.Log(usingEastDoor);
        Debug.Log("usingWestDoor - ");
        Debug.Log(usingWestDoor);*/
        //rotate room into position
        rotateRoom(x, z, usingNorthDoor, usingSouthDoor, usingEastDoor, usingWestDoor);
        currentX = x;
        currentZ = z;
    }

    //finds and returns a room that is not surrounded by other rooms
    GameObject findUseableRoom()
    {
        int adjacentFound;
        for (int x = 0; x < length; x++)
        {
            for (int z = 0; z < width; z++)
            {
                adjacentFound = 0;

                //checking borders
                if (x == 0)
                {
                    adjacentFound++;
                }
                if (x == length - 1)
                {
                    adjacentFound++;
                }
                if (z == 0)
                {
                    adjacentFound++;
                }
                if (z == width - 1)
                {
                    adjacentFound++;
                }

                //checking rest of sides
                if (x > 0)
                {
                    //checking west
                    if (referenceLevel[x - 1, z].GetComponent<RoomSpawnerController>().hasRoom == true)
                    {
                        adjacentFound++;
                    }
                }
                if (x < length - 1)
                {
                    //checking east
                    if (referenceLevel[x + 1, z].GetComponent<RoomSpawnerController>().hasRoom == true)
                    {
                        adjacentFound++;
                    }
                }
                if(z > 0)
                {
                    //checking south
                    if (referenceLevel[x, z - 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                    {
                        adjacentFound++;
                    }
                }
                if(z < width - 1)
                {
                    //checking north
                    if (referenceLevel[x, z + 1].GetComponent<RoomSpawnerController>().hasRoom == true)
                    {
                        adjacentFound++;
                    }
                }

                if (adjacentFound < 4)
                {
                    currentX = x;
                    currentZ = z;
                    return referenceLevel[x, z];
                }   
            }
        }
        return null;
    }

    //rotates generatedRoom  90 degrees
    void rotateRoom(int x, int y, bool usingNorthDoor, bool usingSouthDoor, bool usingEastDoor, bool usingWestDoor)
    {
            if ((usingNorthDoor == generatedRoom.GetComponent<RoomController>().hasNorthDoor) && (usingSouthDoor == generatedRoom.GetComponent<RoomController>().hasSouthDoor) && (usingEastDoor == generatedRoom.GetComponent<RoomController>().hasEastDoor) && (usingWestDoor == generatedRoom.GetComponent<RoomController>().hasWestDoor))
            {
                //alligned perfectly, do nothing
            }
            else
            {
                //Debug.Log("Rotating Room");
                changeDoorBools();
                generatedRoom.transform.Rotate(0, 90, 0, Space.Self);
                rotateRoom(x, y, usingNorthDoor, usingSouthDoor, usingEastDoor, usingWestDoor);
            }
    }

    //based on 90 degree X axis rotation, changes generatedRoom bools based on future 1 rotation
    void changeDoorBools()
    {
        bool futureHasNorthDoor;
        bool futureHasSouthDoor;
        bool futureHasEastDoor;
        bool futureHasWestDoor;
        futureHasNorthDoor = generatedRoom.GetComponent<RoomController>().hasWestDoor;
        futureHasSouthDoor = generatedRoom.GetComponent<RoomController>().hasEastDoor;
        futureHasEastDoor = generatedRoom.GetComponent<RoomController>().hasNorthDoor;
        futureHasWestDoor = generatedRoom.GetComponent<RoomController>().hasSouthDoor;
        generatedRoom.GetComponent<RoomController>().hasNorthDoor = futureHasNorthDoor;
        generatedRoom.GetComponent<RoomController>().hasSouthDoor = futureHasSouthDoor;
        generatedRoom.GetComponent<RoomController>().hasEastDoor = futureHasEastDoor;
        generatedRoom.GetComponent<RoomController>().hasWestDoor = futureHasWestDoor;
    }

    //use with caution. Used to find empty spot for boss room. NO CHECKS IN PLACE TO STOP FOR LOOP
    void findRandomEmptyRoom()
    {
        for (int i = 0; i < 1; i++)
        {
            currentX = Random.Range(0, length);
            currentZ = Random.Range(0, width);
            if (referenceLevel[currentX, currentZ].GetComponent<RoomSpawnerController>().hasRoom == true)
            {
                i--;
            }
            else
            {
                break;
            }

        }
    }
}