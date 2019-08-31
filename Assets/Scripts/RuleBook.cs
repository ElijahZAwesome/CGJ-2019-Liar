using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RuleBook : MonoBehaviour
{
    // The grand variable, that in the end, deterines if the sign is lying
    private bool signIsLying, leftSafe, middleSafe, rightSafe;

    // If the value of the door is altered directly by a condition. If these are false, they will become opposite of the door with the sign
    private bool leftTouched, middleTouched, rightTouched;

    private MasterLogic ML;
    private GameManager GM;

    // To prevent spamming
    private float ruleCooldown = 1f;

    // Delegate so we can store methods
    public delegate bool RuleMethod();

    // Which rules are in this game
    public List<RuleMethod> rulesInThisGame = new List<RuleMethod>() { };

    // Rulebook containing our rules
    public List<RuleMethod> rules = new List<RuleMethod>() { };


    [SerializeField]
    private GameObject playerRuleSheet;

    // Add each rule that is defined at the bottom of this script. Use this as an example
    public void AddRules()
    {
        rules.Clear();
        rules.Add(LyingIfRat);
        rules.Add(TwoGemLeftDeath);
        rules.Add(EnterMidRightDeath);
        rules.Add(EvenRocksLying);
        rules.Add(MultipleThreeRocksTruthful);
        rules.Add(MoreShroomsRatsMiddleDeath);
    }

    /*
    // Randomly generates 2-4 rules to start
    public void StartRules()
    {
        //int howMany = Mathf.RoundToInt(UnityEngine.Random.Range(2, 4));
        int howMany = UnityEngine.Random.Range(2, 5);
        for (int i = 0; i < howMany; i++)
        {
            rulesInThisGame.Add(Mathf.RoundToInt(UnityEngine.Random.Range(0, rules.Count)));
        }

    }
    */

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) && GM.currentlyTrapped == false)
        {
            /*
            bool isOn = playerRuleSheet.activeSelf;
            // Toggle the rule sheet image
            playerRuleSheet.SetActive(!isOn);
            */
            if (playerRuleSheet == null)
            {
                playerRuleSheet = GameObject.Find("Rule Sheet");

            }
            else
            { 
                playerRuleSheet.SetActive(true);
            }
        }
        else
        {
			if(playerRuleSheet == null)
				playerRuleSheet = GameObject.Find("RuleSheet");
			if (playerRuleSheet != null)
				playerRuleSheet.SetActive(false);
        }
    }

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
        GM = GetComponent<GameManager>();
        ML = GetComponent<MasterLogic>();
        AddRules();
        //playerRuleSheet.SetActive(false);
    }

    public void AddNewRule()
    {
        print("Adding a rule");
        if (rules.Count > 0)
        {
            int randomRule = UnityEngine.Random.Range(0, rules.Count);
            rulesInThisGame.Add(rules[randomRule]);
            print("Adding rule at: " + randomRule);
            rules.RemoveAt(randomRule);
        }
    }

    /// <summary>
    /// Calls each rule, which will modify signIsLying and also return whether or not the rule was true
    /// (currently I can't think of a use for this, but futureproofing never hurts)
    /// </summary>
    public void RunThroughRules()
    {
        GM.playerRules = "";
        signIsLying = false;
        leftSafe = false; middleSafe = false; rightSafe = false;
        leftTouched = false; middleTouched = false; rightTouched = false;

        for (int i = 0; i < rulesInThisGame.Count; i++)
        {
            print("Running a rule");
            rulesInThisGame[i]();
            GM.playerRules += '\n';
            GM.playerRules += '\n';
        }

        // Add the text to the player rule sheet
        playerRuleSheet.GetComponentInChildren<Text>().text = GM.playerRules;

        // Finalize the state of all rooms
        FinalizeRoomStats();
    }

    /// <summary>
    /// Taking all variables, sends back the room info
    /// </summary>
    public void FinalizeRoomStats()
    {
        ML.currentRoom.signLying = signIsLying;
        DetermineSafeDoors();
    }

    // Sets the safe rooms in the current room
    private void DetermineSafeDoors()
    {
        int doorWithSign = ML.currentRoom.signLocation;

        ML.currentRoom.safeDoors = new bool[3] { false, false, false };

        // If a door isn't affected directly, it is set to the opposite of what the sign says
        bool untouchedDoor;
        if (signIsLying)
        {
            untouchedDoor = ML.currentRoom.signSaysSafe;
        }
        else
        {
            untouchedDoor = !ML.currentRoom.signSaysSafe;
        }

        print("untouched doors should be: " + untouchedDoor);

        // Hard code the doors that weren't affected by logic to be the opposite of what the sign said
        if (!leftTouched && doorWithSign != 0) { leftSafe = untouchedDoor; print("left door not touched"); }
        if (!middleTouched && doorWithSign != 1) { middleSafe = untouchedDoor; print("middle door not touched"); }
        if (!rightTouched && doorWithSign != 2) { rightSafe = untouchedDoor; print("right door not touched"); }

        bool[] touchedStates = new bool[3] { leftTouched, middleTouched, rightTouched };
        // If the sign is lying and the door with the sign isn't hard coded
        if (ML.currentRoom.signLying && touchedStates[doorWithSign] == false)
        {
            // Set the marked door to be the opposite of what the sign says
            SetDoor(doorWithSign, !ML.currentRoom.signSaysSafe);
            print("sign is lying, door " + doorWithSign + " is set to " + !ML.currentRoom.signSaysSafe);
        }
        // If the sign is not lying and the door with the sign isn't hard coded
        else if (!ML.currentRoom.signLying && touchedStates[doorWithSign] == false)
        {
            // Set the marked door to be what the sign says
            SetDoor(doorWithSign, ML.currentRoom.signSaysSafe);
            print("sign is truthful, door " + doorWithSign + " is set to " + ML.currentRoom.signSaysSafe);
        }
        // The marked door was changed by a hard coded rule (example, sign is above left door saying safe but there's dripping meaning left door is deadly)
        else if (touchedStates[doorWithSign] == true && CheckDoorOpposite(doorWithSign) == true)
        {
            // Note: this does not apply if the hard code is the same as the sign, for example, if a truthful sign says "death" above a tunnel that is hard coded to be death, nothign happens
            print("The marked door has been hard forced to opposite, flip everything");
            // Flip other two doors and the sign state
            signIsLying = !signIsLying;
            FlipOtherDoors(doorWithSign);
        }

        print("setting the room to these: " + leftSafe + ", " + middleSafe + ", " + rightSafe);
        ML.currentRoom.safeDoors[0] = leftSafe;
        ML.currentRoom.safeDoors[1] = middleSafe;
        ML.currentRoom.safeDoors[2] = rightSafe;
    }

    /// <summary>
    /// Shows if the marked door's state (safe/death) is opposite of what the sign says it is
    /// </summary>
    /// <param name="doorNum">The door number (0, 1, 2)</param>
    /// <returns>If the doors are opposite, used to check if a full flip is needed</returns>
    private bool CheckDoorOpposite(int doorNum)
    {
        bool signState;
        if (ML.currentRoom.signLying)
            signState = !ML.currentRoom.signSaysSafe;
        else
            signState = ML.currentRoom.signSaysSafe;

        switch (doorNum)
        {
            case 0:
                if (leftSafe != signState)
                    return true;
                break;
            case 1:
                if (middleSafe != signState)
                    return true;
                break;
            case 2:
                if (rightSafe != signState)
                    return true;
                break;
        }
        return false;
    }

    private void SetDoor(int doorNum, bool doorState)
    {
        switch (doorNum)
        {
            case 0:
                leftSafe = doorState;
                break;
            case 1:
                middleSafe = doorState;
                break;
            case 2:
                rightSafe = doorState;
                break;
        }
    }

    private void FlipOtherDoors(int doorNum)
    {
        switch (doorNum)
        {
            case 0:
                middleSafe = !middleSafe;
                rightSafe = !rightSafe;
                break;
            case 1:
                leftSafe = !leftSafe;
                rightSafe = !rightSafe;
                break;
            case 2:
                leftSafe = !leftSafe;
                middleSafe = !middleSafe;
                break;
        }
    }

    #region ALL RULES

    // NOTE: Rules which directly change the state of the sign should occur early, so that it can be more complex with rules that mention specific doors

    // If there is at least one rat, the sign is lying
    public bool LyingIfRat()
    {
        GM.playerRules += "If rats are present, the Sign is Lying";
        if (ML.currentRoom.numRats > 0)
        {
            signIsLying = true;
            print("There is a rat, sign is lying");
            return true;
        }
        return false;
    }

    // If there are more than 2 gems in the room, the left door is deadly
    public bool TwoGemLeftDeath()
    {
        GM.playerRules += "If there are more than 2 gems, the Left Door is not safe";
        if (ML.currentRoom.numGems > 2)
        {
            print("There are more than 2 gems, left is deadly");
            leftSafe = false;
            leftTouched = true;
            return true;
        }
        return false;
    }

    // If you just entered from the middle, the right door is deadly
    public bool EnterMidRightDeath()
    {
        GM.playerRules += "If you just came from the Middle Door, the Right Door is not safe";
        if (ML.currentRoom.entranceDoor == 1)
        {
            print("Came from the middle, right is deadly");
            rightSafe = false;
            rightTouched = true;
            return true;
        }
        return false;
    }

    // If there is an even number of rocks, the sign is lying
    public bool EvenRocksLying()
    {
        GM.playerRules += "If there is an even number of rocks in the room, the Sign is Lying";
        int rocks = ML.currentRoom.numRocks;
        if (rocks > 0 && rocks % 2 == 0)
        {
            print("Even number of rocks, sign is lying");
            signIsLying = true;
            return true;
        }
        return false;
    }

    // If there are a multiple of three rocks in the room, then the sign is truthful
    public bool MultipleThreeRocksTruthful()
    {
        GM.playerRules += "If there are 3 or 6 rocks in the room and the Sign is Lying, the Sign is no longer Lying";
        int rocks = ML.currentRoom.numRocks;
        if ((rocks == 3 || rocks == 6) && signIsLying == true)
        {
            print("3 or 6 rocks, sign is now truthful");
            signIsLying = false;
            return true;
        }
        return false;
    }

    // If there are both mushrooms and rats, but more mushrooms, the middle is not safe
    private bool MoreShroomsRatsMiddleDeath()
    {
        GM.playerRules += "If there's at least 1 rat in the room, but more mushrooms, the Middle Door is not Safe";
        int rats = ML.currentRoom.numRats;
        int shrooms = ML.currentRoom.numShrooms;
        if (rats > 0 && shrooms > rats)
        {
            print("More shrooms than rats, middle is not safe");
            middleSafe = false;
            middleTouched = true;
            return true;
        }
        return false;
    }
    #endregion
}
