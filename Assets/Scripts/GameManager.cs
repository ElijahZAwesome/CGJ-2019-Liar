using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public bool isDamaged = false;
	public int numFlares = 2;

    public string playerRules = "";

    public float timeRemaining;

	//Rocks V Stalactites V etc....
	private string key = "0v0v0v0v0";

    [SerializeField]
    private GameObject damaged;

    //Add more lists of the objects as needed to be spawned
    [SerializeField]
	private List<GameObject> rocks, mushrooms, rats, webs, gems, stalagmites, cracks;

    private List<List<GameObject>> allProps;
    /*
	[SerializeField]
	private List<GameObject> spawnPoints;
    */
	[SerializeField]
	private List<List<Vector2>> transformPoints;

    // Spawn point remaining in this room
    private List<GameObject> availableSpawnPoints = new List<GameObject>();

    // Backpack Spawn
    private List<Vector2> backpackTransformPoints = new List<Vector2>();

    [SerializeField]
    private List<GameObject> backPackSpawnPoints;
    [SerializeField]
    private GameObject backPackObject;

    #region PROP SPAWNING

    // Rock Spawn
    private List<Vector2> rockTransformPoints = new List<Vector2>();
    [SerializeField]
    private List<GameObject> rockSpawn;

    // Mushroom Spawn
    private List<Vector2> shroomTransformPoints = new List<Vector2>();
    [SerializeField]
    private List<GameObject> shroomSpawn;

    // Rat Spawn
    private List<Vector2> ratTransformPoints = new List<Vector2>();
    [SerializeField]
    private List<GameObject> ratSpawn;

    // Web Spawn
    private List<Vector2> webTransformPoints = new List<Vector2>();
    [SerializeField]
    private List<GameObject> webSpawn;

    // Gem Spawn
    private List<Vector2> gemTransformPoints = new List<Vector2>();
    [SerializeField]
    private List<GameObject> gemSpawn;

    // Stalagmite Spawn
    private List<Vector2> stalagmiteTransformPoints = new List<Vector2>();
    [SerializeField]
    private List<GameObject> stalagmiteSpawn;

    // Crack Spawn
    private List<Vector2> crackTransformPoints = new List<Vector2>();
    [SerializeField]
    private List<GameObject> crackSpawn;



    #endregion

    // The player is currently in a trap QTE
    public bool currentlyTrapped;

    //This controlls the number of things the key will be able to generate. Decided by list of things like rocks and stalactites
    int maxNumberOfThings = 2, difficultyLevels = 1, minNumberOfThings = 0;
	private float timer = 30;
	private float timeAdd = 5;
	public bool isTiming = true;
	[SerializeField]
	TextMeshProUGUI timerText;
	// Start is called before the first frame update
	void Start()
    {
        isDamaged = false;
        currentlyTrapped = false;
		damaged = GameObject.Find("Damage");
        damaged.SetActive(false);
        allProps = new List<List<GameObject>>() { };
        transformPoints = new List<List<Vector2>>() { };
        /*
		for(int i = 0; i < spawnPoints.Count;i++)
		{
			availableSpawnPoints.Add(spawnPoints[i]);
		}
        */
		foreach (GameObject r in rockSpawn)
		{
            rockTransformPoints.Add(r.transform.position);
		}
        transformPoints.Add(rockTransformPoints);

        foreach (GameObject s in shroomSpawn)
        {
            shroomTransformPoints.Add(s.transform.position);
        }
        transformPoints.Add(shroomTransformPoints);

        foreach (GameObject r in ratSpawn)
        {
            ratTransformPoints.Add(r.transform.position);
        }
        transformPoints.Add(ratTransformPoints);

        foreach (GameObject w in webSpawn)
        {
            webTransformPoints.Add(w.transform.position);
        }
        transformPoints.Add(webTransformPoints);

        foreach (GameObject g in gemSpawn)
        {
            gemTransformPoints.Add(g.transform.position);
        }
        transformPoints.Add(gemTransformPoints);

        foreach (GameObject s in stalagmiteSpawn)
        {
            stalagmiteTransformPoints.Add(s.transform.position);
        }
        transformPoints.Add(stalagmiteTransformPoints);

        foreach (GameObject c in crackSpawn)
        {
            crackTransformPoints.Add(c.transform.position);
        }
        transformPoints.Add(crackTransformPoints);
		
        foreach (GameObject b in backPackSpawnPoints)
        {
            backpackTransformPoints.Add(b.transform.position);
        }

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
        allProps.Add(stalagmites);
        allProps.Add(cracks);
	}

    private void Update()
    {
		if (isTiming && !currentlyTrapped)
		{
			StartTimer();
		}
		if (damaged == null)
		{
			damaged = GameObject.Find("Dam" +
				"age");
		}
		if(damaged != null)
		 damaged.SetActive(isDamaged);
    }
	void StartTimer()
	{
		timer -= Time.deltaTime;
		timerText.text = Mathf.Round(timer).ToString();
		if(timer <= 0)
		{
			isTiming = false;
			SceneManager.LoadScene("Death");
		}
	}
	public void AddToTimer()
	{
		timer += timeAdd;
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
        // How many props will be placed in this room (excludes backpacks)
        print("Rolling between: " + minNumberOfThings + " and " + maxNumberOfThings);
		int numberOfThings = Random.Range(minNumberOfThings, maxNumberOfThings + 1);
        print("This many props: " + numberOfThings);
		string newKey = "";

        // For each type of prop
		for (int i = 0; i < allProps.Count; i++)
		{
			// If we still have objects to place, then place a random number of this object
			if (numberOfThings > 0)
			{
				int numberOfThisProp;
				if (i == allProps.Count - 1)
				{
                    print("Last i value");
					// This is the last prop, dump the rest of the things here
					numberOfThisProp = numberOfThings;
                    print("Dumping: " + numberOfThings + " things");
                    if (numberOfThings > transformPoints[i].Count)
                    {
                        numberOfThisProp = transformPoints[i].Count;
                    }
				}
				else
				{
                    // Place random number of this prop, up to a max of number of spawns for this proptype
                    print("Placing a random number of things between 0 and " + transformPoints[i].Count);
					numberOfThisProp = Random.Range(0, transformPoints[i].Count);
                    print("Placing " + numberOfThisProp + " things");
                    // Ensure we still can place that many props
                    if (numberOfThisProp > numberOfThings)
                    {
                        print(numberOfThisProp + " is too high, place " + numberOfThings + " instead");
                        numberOfThisProp = numberOfThings;
                    }
                    // Add this info to the room key
					newKey += numberOfThisProp.ToString() + "v";
                    // Subtract from the total number of items remaining
					numberOfThings -= numberOfThisProp;
				}
				// Place the item in the room
				print("Attempting to place with key: " + newKey);
                // Pick the correct list of prefabs from all props, then places that many at random points made for that prop type
                PlaceProps(allProps[i], numberOfThisProp, transformPoints[i]);
			}
			else
			{
				newKey += 0.ToString() + "v";
			}
		}
		print("Built Key: " + newKey);

        // Will an item spawn in this room?
        CheckSpawnItems();

        return newKey;
    }

    // Method for placing backpacks
    public void CheckSpawnItems()
    {
		List<Vector2> tempBackPackSpawn = new List<Vector2>();
		for (int i = 0; i < backpackTransformPoints.Count; i++)
		{
			tempBackPackSpawn.Add(backpackTransformPoints[i]);
		}
        int rollItem = Random.Range(1, 11);
        int itemsToSpawn = 0;
        // 1-2 spawns one backpack (20%)
        if (rollItem < 3)
        {
            itemsToSpawn = 1;
        }
        // 10 spawns 2 backpacks (10%)
        else if (rollItem == 10)
        {
            itemsToSpawn = 2;
        }

        if (itemsToSpawn > 0)
        {
            // Pick a random spawn point and place a pack there
            for (int i = 0; i < itemsToSpawn; i++)
            {
                int randBackPackSpawn = Random.Range(0, tempBackPackSpawn.Count);
                GameObject pack = Instantiate(backPackObject, tempBackPackSpawn[randBackPackSpawn], Quaternion.identity);
                pack.transform.SetParent(gameObject.transform);
                tempBackPackSpawn.RemoveAt(randBackPackSpawn);
            }
        }
    }

    public void UpgradeKey()
    {
        int totalSpawnPointCount = 0;

        foreach (List<Vector2> points in transformPoints)
        {
            totalSpawnPointCount += points.Count;
        }

        minNumberOfThings += 1;
        maxNumberOfThings += 1;
        if (maxNumberOfThings >= transformPoints.Count)
        {
            maxNumberOfThings = transformPoints.Count;
        }
        if (minNumberOfThings >= maxNumberOfThings)
        {
            minNumberOfThings = maxNumberOfThings;
        }
        print("Min: " + minNumberOfThings + " Max: " + maxNumberOfThings);
        print("Number of things in spawnPoints is: " + transformPoints.Count);
    }

    // Place props on the random spawnpoints, these are props in the room logic
	void PlaceProps(List<GameObject> propList, int howMany, List<Vector2> location)
	{
        List<Vector2> temp = new List<Vector2>() { };
        foreach (Vector2 item in location)
        {
            temp.Add(item);
        }
        if (howMany > 0)
        {
            // Place the item at the point
            for (int i = 0; i < howMany; i++)
            {
                if (temp.Count > 0 && propList.Count > 0)
                {
                    // Pick a random available spawn point to place it at
                    int randomPoint = Random.Range(0, temp.Count);

                    // Pick a random prefab from the proplist
                    int randomProp = Random.Range(0, propList.Count);

                    // Place the item at the point
                    print("Attempting to place prop number: " + randomProp + " at spawnPoint " + randomPoint);
                    GameObject prop = Instantiate(propList[randomProp], temp[randomPoint], Quaternion.identity);
                    prop.transform.SetParent(gameObject.transform);

                    // Remove this spawn point from the list
                    temp.RemoveAt(randomPoint);
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
