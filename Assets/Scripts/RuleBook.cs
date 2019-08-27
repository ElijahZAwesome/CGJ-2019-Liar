using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RuleBook : MonoBehaviour
{
    // The grand variable, that in the end, deterines if the sign is lying
    private bool signIsLying, leftSafe, middleSafe, rightSafe;

    private MasterLogic ML;

    // Delegate so we can store methods
    public delegate bool RuleMethod();

    // Which rules are in this game
    public List<int> rulesInThisGame;

    // Rulebook containing our rules
    public List<RuleMethod> rules = new List<RuleMethod>();

    private void Start()
    {
        ML = GetComponent<MasterLogic>();
        AddRules();
    }

    // Add each rule that is defined at the bottom of this script. Use this as an example
    public void AddRules()
    {
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
    /// (currently i can't think of a use for this, but futureproofing never hurts)
    /// </summary>
    public void RunThroughRules()
    {
        signIsLying = false;
        leftSafe = true; middleSafe = true; rightSafe = true;
        foreach (RuleMethod ruleFunc in rules)
        {
            ruleFunc();
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

    // Sets the safe rooms in the current room. This will be the script that checks all logic
    private void DetermineSafeDoors()
    {
        int doorWithSign = ML.currentRoom.signLocation;

        // Set all door states to whatever the rules hard code them to be
        bool[] doorStates = new bool[3] { leftSafe, middleSafe, rightSafe };

        ML.currentRoom.safeDoors = doorStates;

        // Set the door with a sign to be whatever it says
        if (ML.currentRoom.signLying)
        {
            ML.currentRoom.safeDoors[doorWithSign] = !ML.currentRoom.signSaysSafe;
        }
        else
        {
            ML.currentRoom.safeDoors[doorWithSign] = ML.currentRoom.signSaysSafe;
        }

        // If the signed door's state conflicts with a hard rule
        if (ML.currentRoom.safeDoors[doorWithSign] != doorStates[doorWithSign])
        {
            // Flip the state of the sign 
            signIsLying = !signIsLying;

            ML.currentRoom.signLying = signIsLying;
        }

        // If all doors are not safe
        if (!leftSafe && !middleSafe && !rightSafe)
        {
            print("NONE OF THE DOORS ARE SAFE");
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
