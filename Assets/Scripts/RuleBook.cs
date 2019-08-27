using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleBook : MonoBehaviour
{
    // The grand variable, that in the end, deterines if the sign is lying
    private bool signIsLying;

    // Which rules are in this game
    public List<int> rulesInThisGame;

    /* Example:
     * Rules in this game: 1, 3, 5, 6
     * Switch statement of rule 1 = run func LyingIfRat() which checks if there's a rat, and if so, sets signlying to true
     * Switch statement of rule 3 = run func DripRightNotSafe() which check if there's a drip, and if so, sets right room to not safe
     * This means that the sign is lying if and only if it is marking right as safe. Adjust the bool signIsLying accordingly
     * This script must be able to take the stats of the room in a function that decides if the sign is lying or not
     * It also contains all the functions for the rules, which is a lot of them
     * Functions should be short, as there are many
     */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
