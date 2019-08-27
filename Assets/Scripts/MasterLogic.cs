using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterLogic : MonoBehaviour
{
    // Handles the overall GameLogic of which door ends up being safe

    // The list of whether or not sign i was truthful
    private List<bool> truthfulSigns;

    // The possible positions of signs
    [SerializeField]
    private List<GameObject> signs;

    // All rooms the player has traversed
    private List<RoomStats> allRooms;

    // Current ID of the rooms
    private int currentId;

    // A room object containing all the info
    public struct RoomStats
    {
        // Rooms ID
        public int id;

        public int numberOfRocks, numberOfStalagmites;

        public bool hasRat, hasWater;

        // Was the room trapped (QTE)?
        public bool roomTrapped;

        // Is left, middle, right door safe
        public bool[] safeDoors;

        // The door the player entered from (1, 2, 3)
        public int entranceDoor;

        // Where is the sign (1, 2, 3)
        public int signLocation;

        // Does the sign say safe? if not, it says death
        public bool signSaysSafe;

        // Is the sign lying? Should match truthfulSigns[roomID]
        public bool signLying;
    }

    // The room the player is in
    private RoomStats currentRoom;

    // The sign object placed above the door
    [SerializeField]
    private GameObject signObject;

    // Start is called before the first frame update
    void Start()
    {
        CreateNewRoom();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Select left door
            EnterDoor(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Select middle door
            EnterDoor(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Select right door
            EnterDoor(3);
        }
    }

    // Creates a new RoomStats object with the info, should be called at the start of each room
    private void CreateNewRoom()
    {
        // Pick a random door to place the sign above
        int doorWithSign = Random.Range(1, 4);
        PlaceSign(doorWithSign);

        // Pick random thing for sign to say
        bool signSaysSafe_;
        int randomMessage = Random.Range(0, 2);
        if (randomMessage == 1)
        {
            signSaysSafe_ = true;
            print("THE SIGN SAYS SAFE");
        }
        else
        {
            signSaysSafe_ = false;
            print("THE SIGN SAYS DEATH");
        }

        // Place random props in the room

        // Roll to place a trap (TODO: Needs to be updated)
        bool trapped = false;

        // Is the sign lying?
        // THESE VALUES NEED TO BE CHANGED EVENTUALLY TO REFLECT LOGIC IN ANOTHER SCRIPT
        int randSignLie = Random.Range(0, 2);
        bool signLying;
        if (randSignLie == 0)
        {
            signLying = false;
            print("The sign is telling the truth");
        }
        else
        {
            signLying = true;
            print("The sign is lying");
        }

        // Which doors are safe?
        DetermineSafeDoors();

        // Create a new room with all of this info
        RoomStats newRoom = new RoomStats();
        newRoom.id = currentId;

        //newRoom.roomProps = 
        newRoom.roomTrapped = trapped;
        newRoom.signLocation = doorWithSign;
        newRoom.signSaysSafe = signSaysSafe_;
        //newRoom.entranceDoor = 
        newRoom.signLying = signLying;

        // Set this room as the current one
        currentRoom = newRoom;
    }

    // Sets the safe rooms in the current room. This will be the script that checks all logic
    private void DetermineSafeDoors()
    {
        // If sign is above middle door and reads "Safe" and it's lying, then all doors are set to safe, then the middle door is flipped to death

        bool doorSafe = currentRoom.signSaysSafe;
        // Set all doors to safe by default
        currentRoom.safeDoors = new bool[3] { true, true, true };

        if (currentRoom.signLying)
        {
            // Sign is lying, mark all doors to what it says
            currentRoom.safeDoors = new bool[3] { doorSafe, doorSafe, doorSafe };
            // Sign is lying, set the marked door to opposite of what it says
            currentRoom.safeDoors[currentRoom.signLocation - 1] = !doorSafe;
        }
        else
        {
            // Sign is telling the truth, mark all doors the opposite
            currentRoom.safeDoors = new bool[3] { !doorSafe, !doorSafe, !doorSafe };
            // Sign is telling the truth, set the marked door to what it reads
            currentRoom.safeDoors[currentRoom.signLocation - 1] = doorSafe;
        }
    }

    // Place the sign sprite above the corresponding door
    private void PlaceSign(int doorNum)
    {
        // Turn off all signs, then turn on the correct sign
        foreach(GameObject sign in signs)
        {
            sign.SetActive(false);
        }
        signs[doorNum - 1].SetActive(true);
    }

    private void EnterDoor(int doorNum)
    {
        if (currentRoom.safeDoors[doorNum-1] == true)
        {
            print("YOU PICKED A SAFE DOOR");
        }
        else
        {
            print("YOU ARE DEAD, WRONG DOOR");
        }
    }
}