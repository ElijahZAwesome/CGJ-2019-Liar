using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public bool isDamaged = false;
	public int numFlares = 0;

    public string playerRules = "";

    public float timeRemaining;

	//Rocks V Stalactites V etc....
	private string key = "0v0v0v0v0";

	//Add more lists of the objects as needed to be spawned
	[SerializeField]
	private List<GameObject> rocks, mushrooms, rats, webs, gems;

    private List<List<GameObject>> allProps;

	[SerializeField]
	private List<GameObject> spawnPoints;

    private List<GameObject> availableSpawnPoints;

    

    //This controlls the number of things the key will be able to generate. Decided by list of things like rocks and stalactites
    int maxNumberOfThings = 5, difficultyLevels = 1, minNumberOfThings = 0;

	// Start is called before the first frame update
	void Start()
    {
        allProps = new List<List<GameObject>>() { };

        availableSpawnPoints = spawnPoints;

		if (instance == null)
		{
			instance = this;
		}
		//Debugging Purposes
		Debug.Log("GameStart");
		DontDestroyOnLoad(instance);
        allProps.Add(rocks);
        allProps.Add(mushrooms);
        allProps.Add(rats);
        allProps.Add(webs);
        allProps.Add(gems);

		//Debug.Log(key);
        //KeyUpdate();
        //GenerateNewKey();
		//Debug.Log(key);
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
        for (int i = 0; i < allProps.Count; i++)
        {
            // If we still have objects to place, then place a random number of this object
            if (numberOfThings > 0)
            {
                int numberOfThisProp;
                if (i == allProps.Count)
                {
                    // This is the last spot, dump the rest of the things here
                    numberOfThisProp = numberOfThings;
                }
                else
                {
                    numberOfThisProp = Random.Range(0, numberOfThings + 1);
                    newKey += numberOfThisProp.ToString() + "v";
                    numberOfThings -= numberOfThisProp;
                }
                // Place the item in the room
                print("Attempting to place with key: " + newKey);
                PlaceItems(allProps[i], numberOfThisProp);
            }
            else
            {
                newKey += 0.ToString() + "v";
            }
        }
        print("Built Key: " + newKey);
        return newKey;
    }

    public void UpgradeKey()
    {
        minNumberOfThings += 2;
        maxNumberOfThings += 2;
    }

    // Place items on the random spawnpoints
	void PlaceItems(List<GameObject> propList, int howMany)
	{
        if (howMany > 0)
        {
            // Place the item at the point
            for (int i = 0; i < howMany; i++)
            {
                if (availableSpawnPoints.Count > 0 && propList.Count > 0)
                {
                    // Pick a random available spawn point to place it at
                    int randomPoint = Random.Range(0, availableSpawnPoints.Count);

                    // Pick a random prefab from the proplist
                    int randomProp = Random.Range(0, propList.Count);

                    // Place the item at the point
                    print("Attempting to place prop number: " + randomProp + " at spawnPoint " + randomPoint);
                    GameObject prop = Instantiate(propList[randomProp], availableSpawnPoints[randomPoint].transform.position, Quaternion.identity);
                    prop.transform.SetParent(gameObject.transform);

                    // Remove this spawn point from the list
                    availableSpawnPoints.RemoveAt(randomPoint);
                }
            }
        }
	}

    public void DestroyProps()
    {
        List<GameObject> children = new List<GameObject>() { };
        foreach(Transform child in gameObject.transform)
        {
            children.Add(child.gameObject);
        }

        foreach(GameObject prop in children)
        {
            Destroy(prop);
        }
    }
}
