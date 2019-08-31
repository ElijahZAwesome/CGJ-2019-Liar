using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MasterLogic : MonoBehaviour
{
    AudioMaster AM;

    // Handles the overall GameLogic of which door ends up being safe

    public static MasterLogic MLInstance;

    // The rulebook script attached to this object
    private RuleBook ruleBook;

    // The list of whether or not sign i was truthful
    private List<bool> truthfulSigns;

    // The possible positions of signs
    [SerializeField]
    private List<GameObject> signs;

    private int doorYouCameFrom = -1;

    // All rooms the player has traversed
    public List<RoomStats> allRooms;

    // Current ID of the rooms
    private int currentId;

    // The game manager that will generate a key
    private GameManager GM;

    // The trap event object that will handle traps
    private TrapEvent TE;

	private float UIwaitTime = 5;
	private bool TutorialActivated = true;
	// A room object containing all the info
	public struct RoomStats
    {
        // Rooms ID
        public int id;

        public int numRocks, numShrooms, numRats, numWebs, numGems, numStalags, numCracks;

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

    // When you press a number, this prevents the "door you came from" from being changeable while transitioning
    private bool canSelectRoom;

    // How to tell the player a new rule is in affect
    public Image newRuleHint;
    public Text hintText;

    // Start is called before the first frame update
    void Start()
    {
        MLInstance = this;

        AM = GameObject.Find("AudioManager").GetComponent<AudioMaster>();

        ruleBook = GetComponent<RuleBook>();
        GM = GetComponent<GameManager>();
        TE = FindObjectOfType<TrapEvent>();

        //foreach(GameObject sign in signs)
        //{
        //   DontDestroyOnLoad(sign);
        //}

        truthfulSigns = new List<bool> { };
        allRooms = new List<RoomStats> { };

        // Make a new room with some stats. This will have to read back info from the manager object with room generation
        ruleBook.AddNewRule();
        CreateNewRoom();
		newRuleHint.gameObject.SetActive(true);
		if (allRooms.Count == 0) hintText.text = "Check your rulebook. New rules appear every 5 tunnels. \n[W]";
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && canSelectRoom)
        {
            // Select left door
            //EnterDoor(0);
            doorYouCameFrom = 0;
            canSelectRoom = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && canSelectRoom)
        {
            // Select middle door
            //EnterDoor(1);
            doorYouCameFrom = 1;
            canSelectRoom = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && canSelectRoom)
        {
            // Select right door
            //EnterDoor(2);
            doorYouCameFrom = 2;
            canSelectRoom = false;
        }
		
		if (newRuleHint != null)
		{
			if (newRuleHint.gameObject.activeSelf)
			{
				UIwaitTime -= Time.deltaTime;
				if (UIwaitTime < 0)
				{
					UIwaitTime = 5;
					newRuleHint.gameObject.SetActive(false);
				}
			}
			else
			{
				if (TutorialActivated)
				{
					print("Gave Hint");
					newRuleHint.gameObject.SetActive(true);
					hintText.text = "Press 'A' or 'D' to check out your environment. \n[A][D]";
					TutorialActivated = false;
					UIwaitTime = 5;
				}
			}
		}
		
	}

    // Creates a new RoomStats object with the info, should be called at the start of each room
    private void CreateNewRoom()
    {
        // Call on the GameManager to get a seed
        string roomKey = GM.GenerateNewKey();

        // Pick a random door to place the sign above
        int doorWithSign = Random.Range(0, 3);
        PlaceSign(doorWithSign);
	
        // Pick random thing for sign to say
        bool signSaysSafe_;
        int randomMessage = Random.Range(0, 2);
        if (randomMessage == 1)
        {
            signSaysSafe_ = true;
            signs[doorWithSign].gameObject.transform.GetChild(0).gameObject.SetActive(false);
            signs[doorWithSign].gameObject.transform.GetChild(1).gameObject.SetActive(true);
            print("THE SIGN SAYS SAFE");
        }
        else
        {
            signSaysSafe_ = false;
            signs[doorWithSign].gameObject.transform.GetChild(1).gameObject.SetActive(false);
            signs[doorWithSign].gameObject.transform.GetChild(0).gameObject.SetActive(true);
            print("THE SIGN SAYS DEATH");
        }

        // Roll to place a trap
        bool trapped = false;
        int aTrap = Random.Range(1, 5);
        if (aTrap == 1 && allRooms.Count > 0) // 25%, first room cannot be trapped
        {
            trapped = true;
            TE.StartTrap();
        }
        
        // Create a new room with all of this info
        RoomStats newRoom = new RoomStats();

        // Key Order: Rocks, Shrooms, Rats, Webs (as of right now, this will grow later)

        // Get the info from the new room key
        string[] roomNums = roomKey.Split('v');
        int.TryParse(roomNums[0], out newRoom.numRocks);
        int.TryParse(roomNums[1], out newRoom.numShrooms);
        int.TryParse(roomNums[2], out newRoom.numRats);
        int.TryParse(roomNums[3], out newRoom.numWebs);
        int.TryParse(roomNums[4], out newRoom.numGems);
        int.TryParse(roomNums[5], out newRoom.numStalags);
        int.TryParse(roomNums[6], out newRoom.numCracks);

        // Set the ID
        newRoom.id = currentId;
        newRoom.roomTrapped = trapped; // is it trapped?
        newRoom.signLying = false; // default value of the sign lying
        newRoom.signLocation = doorWithSign; // which door is the sign above? (0, 1, 2)
        newRoom.entranceDoor = doorYouCameFrom;
        newRoom.signSaysSafe = signSaysSafe_; // does the sign say safe?

        // Set this room as the current one
        currentRoom = newRoom;

        // Apply all logic to find if the sign is truthful or not
        ruleBook.RunThroughRules();

        // Increment the ID
        currentId++;

        // Add the bool value of signLying to the list of bools
        truthfulSigns.Add(!currentRoom.signLying);

        print(currentRoom.safeDoors[0] + ", " + currentRoom.safeDoors[1] + ", " + currentRoom.safeDoors[2]);

        canSelectRoom = true;
    }

    // Place the sign sprite above the corresponding door
    private void PlaceSign(int doorNum)
    {
        print("Placing sign");
        // Turn off all signs, then turn on the correct sign
        foreach(GameObject sign in signs)
        {
            sign.SetActive(false);
        }
        signs[doorNum].SetActive(true);
    }

    // Called when you enter a door
    public void EnterDoor(int doorNum)
    {
        if (currentRoom.safeDoors[doorNum] == true)
        {
            print("YOU PICKED A SAFE DOOR");
			GM.UpgradeKey();
            allRooms.Add(currentRoom);
            if (allRooms.Count % 5 == 0)
            {
                ruleBook.AddNewRule();
                // show hint
                newRuleHint.gameObject.SetActive(true);
                if (allRooms.Count == 0) hintText.text = "Check your rulebook. New rules appear every 5 tunnels. \n[W]";
                else hintText.text = "The cave has gotten more complex.";
            }
            else
            {
                // hide hint
                newRuleHint.gameObject.SetActive(false);
            }
			GM.AddToTimer();
            GM.DestroyProps();
            CreateNewRoom();
		}
        else
        {
            print("YOU ARE DEAD, WRONG DOOR");
            // Kill everything
            AM.youLose();
			GM.isTiming = false;
            
            SceneManager.LoadScene("Death");
        }        
    }
}