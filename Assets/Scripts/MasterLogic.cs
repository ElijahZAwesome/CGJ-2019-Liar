using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterLogic : MonoBehaviour
{
    // Handles the overall GameLogic of which door ends up being safe

    private RuleBook ruleBook;

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
    public RoomStats currentRoom;

    // The sign object placed above the door
    [SerializeField]
    private GameObject signObject;

    // Start is called before the first frame update
    void Start()
    {
        ruleBook = GetComponent<RuleBook>();
        CreateNewRoom();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Select left door
            EnterDoor(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Select middle door
            EnterDoor(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Select right door
            EnterDoor(2);
        }
    }

    // Creates a new RoomStats object with the info, should be called at the start of each room
    private void CreateNewRoom()
    {
        // Pick a random door to place the sign above
        int doorWithSign = Random.Range(0, 3);
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

        // Roll to place a trap (TODO: Needs to be updated)
        bool trapped = false;

        // Create a new room with all of this info
        RoomStats newRoom = new RoomStats();
        newRoom.id = currentId;

        //newRoom.roomProps = 
        newRoom.hasRat = true;
        newRoom.hasWater = true;
        newRoom.roomTrapped = trapped;
        newRoom.signLocation = doorWithSign;
        newRoom.signSaysSafe = signSaysSafe_;
        //newRoom.entranceDoor = 

        // Set this room as the current one
        currentRoom = newRoom;
        ruleBook.RunThroughRules();
    }

    // Place the sign sprite above the corresponding door
    private void PlaceSign(int doorNum)
    {
        // Turn off all signs, then turn on the correct sign
        foreach(GameObject sign in signs)
        {
            sign.SetActive(false);
        }
        signs[doorNum].SetActive(true);
    }

    private void EnterDoor(int doorNum)
    {
        if (currentRoom.safeDoors[doorNum] == true)
        {
            print("YOU PICKED A SAFE DOOR");
        }
        else
        {
            print("YOU ARE DEAD, WRONG DOOR");
        }
    }
}