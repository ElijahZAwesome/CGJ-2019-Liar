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
	private string key = "0v0";
	//Add more lists of the objects as needed to be spawned
	private List<GameObject> rocks;
	private List<GameObject> stalactites;
	//This controlls the number of things the key will be able to generate. Decided by list of things like rocks and stalactites
	int numberOfThings = 2, difficultyLevels = 5;
	// Start is called before the first frame update
	void Start()
	{
		if (instance == null)
		{
			instance = this;
		}
	}
	//call this on the completion of a room to update the key
	void KeyUpdate()
	{

		List<int> keyPart = new List<int>();
		for (int i = 0; i < numberOfThings; i++)
		{

			keyPart.Add(int.Parse(key.Split('v')[i]) + difficultyLevels);
		}
		//Add to key based on where the object you want to influene is in the list based on what you want the difficulty to be and how the scale will rise.
		key = keyPart[0].ToString() + 'v' + keyPart[1].ToString();

	}
	/**
	void GenTheRoom()
	{

	}
	**/
}
