using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RuleBook : MonoBehaviour
{
    // The grand variable, that in the end, deterines if the sign is lying
    private bool signIsLying;

    // Delegate so we can store methods
    public delegate bool RuleMethod();

    // Which rules are in this game
    public List<int> rulesInThisGame;

    // Rulebook containing our rules
    public List<RuleMethod> rules = new List<RuleMethod>();

    // Add each rule that is defined at the bottom of this script. Use this as an example
    public RuleBook()
    {
        rules.Add(LyingIfRat);
    }

    // Randomly generates 2-4 rules to start
    public void StartRules()
    {
        int howMany = Mathf.RoundToInt(UnityEngine.Random.Range(2, 4));
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
        rulesInThisGame.ForEach((t) =>
        {
            rules[t]();
        });
    }

    /// <summary>
    /// If rat, sign is lying (always true, for pseudocode purposes)
    /// </summary>
    public bool LyingIfRat()
    {
        bool rat = true;
        signIsLying = rat;
        return rat;
    }

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
