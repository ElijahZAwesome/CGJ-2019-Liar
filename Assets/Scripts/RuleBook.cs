using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RuleBook : MonoBehaviour
{
    // The grand variable, that in the end, deterines if the sign is lying
    private bool signIsLying, leftSafe, middleSafe, rightSafe;

    // If the value of the door is altered directly by a condition. If these are false, they will become opposite of the door with the sign
    private bool leftTouched, middleTouched, rightTouched;

    private MasterLogic ML;

    // Delegate so we can store methods
    public delegate bool RuleMethod();

    // Which rules are in this game
    public List<int> rulesInThisGame;

    // Rulebook containing our rules
    public List<RuleMethod> rules = new List<RuleMethod>() { };

    // Add each rule that is defined at the bottom of this script. Use this as an example
    public void AddRules()
    {
        rules.Clear();
        rules.Add(LyingIfRat);
        rules.Add(DripLeftDeath);
    }

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

    /// <summary>
    /// Calls each rule, which will modify signIsLying and also return whether or not the rule was true
    /// (currently I can't think of a use for this, but futureproofing never hurts)
    /// </summary>
    public void RunThroughRules()
    {
        ML = GetComponent<MasterLogic>();
        signIsLying = false;
        leftSafe = false; middleSafe = false; rightSafe = false;
        for (int i = 0; i < rules.Count; i++)
        {
            rules[i]();
        }

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

        print("before setting the room to these: " + leftSafe + ", " + middleSafe + ", " + rightSafe);
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

    /* SHORT DESCRIPTION OF EACH RULE (many more to come)
     * Rat in room = lying
     * Odd number of stalagmites = truth
     * Even number of stalagmites = lying
     * Dripping = left door not safe
     * Came from middle = right door not safe
     * Multiple of 3 rocks in the room = sign is truthful
     * 
     */

    // NOTE: Rules which directly change the state of the sign should occur early, so that it can be more complex with rules that mention specific doors

    public bool LyingIfRat()
    {
        if (ML.currentRoom.hasRat)
        {
            signIsLying = true;
            print("There is a rat, sign is lying");
        }
        return signIsLying;
    }

    public bool DripLeftDeath()
    {
        if (ML.currentRoom.hasWater)
        {
            print("There is dripping, left is deadly");
            leftSafe = false;
            leftTouched = true;
            return true;
        }
        return false;
    }

    public bool EnterMidRightDeath()
    {
        if (ML.currentRoom.entranceDoor == 2)
        {
            print("Came from the middle, right is deadly");
            rightSafe = false;
            return true;
        }
        print("Didn't enter middle");
        return false;
    }

    #endregion

    /* Example:
     * Rules in this game: 1, 3, 5, 6
     * Switch statement of rule 1 = run func LyingIfRat() which checks if there's a rat, and if so, sets signlying to true
     * Switch statement of rule 3 = run func DripRightNotSafe() which check if there's a drip, and if so, sets right room to not safe
     * This means that the sign is lying if and only if it is marking right as safe. Adjust the bool signIsLying accordingly
     * This script must be able to take the stats of the room in a function that decides if the sign is lying or not
     * It also contains all the functions for the rules, which is a lot of them
     * Functions should be short, as there are many
     */

}
