using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	//Records corner of map which will be split off from and used to get the total size of the room so you can place things at (x,y)
	[SerializeField]
	private GameObject cornerOfMap;
	//Rocks V Stalactites V etc....
	private string key = "0v0v0v0";
	//Add more lists of the objects as needed to be spawned
	[SerializeField]
	private List<GameObject> rocks, mushrooms, rats, webs;

    private List<List<GameObject>> allProps;

	[SerializeField]
	private List<GameObject> spawnPoints;
    //This controlls the number of things the key will be able to generate. Decided by list of things like rocks and stalactites
    int maxNumberOfThings = 5, difficultyLevels = 1, minNumberOfThings = 0;

	// Start is called before the first frame update
	void Start()
	{
        allProps = new List<List<GameObject>>();

		if (instance == null)
		{
			instance = this;
		}
		//Debugging Purposes
		Debug.Log("GameStart");

        allProps.Add(rocks);
        allProps.Add(mushrooms);
        allProps.Add(rats);
        allProps.Add(webs);

		Debug.Log(key);
        //KeyUpdate();
        GenerateNewKey();
		Debug.Log(key);
	}
	//call this on the completion of a room to update the key
	void KeyUpdate()
	{
        int numberOfThings = Random.Range(minNumberOfThings, maxNumberOfThings + 1);
		List<int> keyPart = new List<int>();
		for (int i = 0; i < numberOfThings; i++)
		{
			//This adds the more to the part in the key that we are accessing
			keyPart.Add(int.Parse(key.Split('v')[i]) + difficultyLevels);
		}
		//This creates the new key
		key = keyPart[0].ToString() + 'v' + keyPart[1].ToString();
	}

    public string GenerateNewKey()
    {
        int numberOfThings = Random.Range(minNumberOfThings, maxNumberOfThings + 1);
        string newKey = "";
        foreach(List<GameObject> propList in allProps)
        {
            // If we still have objects to place, then place a random number of this object
            if (numberOfThings > 0)
            {
                int numberOfThisProp = Random.Range(0, numberOfThings + 1);
                newKey += numberOfThisProp.ToString() + "v";
                numberOfThings -= numberOfThisProp;
            }
        }
        return newKey;
    }

    public void UpgradeKey()
    {
        minNumberOfThings += 2;
        maxNumberOfThings += 2;
    }

	/**
	void GenTheRoom()
	{

	}
	**/
}
