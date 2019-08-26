using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterLogic : MonoBehaviour
{
    // Handles the overall GameLogic of which door ends up being safe

    // The list of whether or not sign i was truthful
    private List<bool> truthfulSigns;

    // All rooms the player has traversed
    private List<RoomStats> allRooms;

    // Current ID of the rooms
    private int currentId;

    // A room object containing all the info
    public struct RoomStats
    {
        // Rooms ID
        public int id;

        // The objects in the room
        public List<GameObject> roomProps;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // select left door
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // select middle door
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // select right door
        }
    }

    // Selects which door you pressed, either 1, 2, or 3
    void SelectDoor(int doorNum)
    {
        // Check if the door selected was correct
    }

    // Creates a new RoomStats object with the info, should be called at the start of each room
    private void CreateNewRoom()
    {
        // Pick a random door to place the sign above
        int doorWithSign = Random.Range(1, 4);

        // Pick random thing for sign to say
        bool signSaysSafe_;
        int randomMessage = Random.Range(0, 2);
        if (randomMessage == 1)
        {
            signSaysSafe_ = true;
        }
        else
        {
            signSaysSafe_ = false;
        }

        // Place random props in the room

        // Roll to place a trap (TODO: Needs to be updated)
        bool trapped = false;

        // Is the sign lying?

        // Which doors are safe?

        // Create a new room with all of this info
        RoomStats newRoom = new RoomStats();
        newRoom.id = currentId;

        //newRoom.roomProps = 
        newRoom.roomTrapped = trapped;
        newRoom.signLocation = doorWithSign;
        newRoom.signSaysSafe = signSaysSafe_;
        //newRoom.safeDoors = 
        //newRoom.entranceDoor = 
        //newRoom.signLying = 
    }

    // Place the sign sprite above the corresponding door
    private void PlaceSign(int doorNum)
    {

    }
}