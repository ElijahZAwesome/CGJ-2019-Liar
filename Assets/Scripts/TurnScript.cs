using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnScript : MonoBehaviour
{
	[SerializeField]
	GameObject player;

	int left = -90;
	int right = 90;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if(player.transform.rotation.eulerAngles.y != left)
			{
				float currentDir = player.transform.rotation.eulerAngles.y;
				currentDir += left;
			}
		}
		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			if (player.transform.rotation.eulerAngles.y != right)
			{
				float currentDir = player.transform.rotation.eulerAngles.y;
				currentDir += right;
			}
		}
	}
}
