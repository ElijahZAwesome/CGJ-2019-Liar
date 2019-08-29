using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI numText;
	[SerializeField]
	private GameObject flare;
	private int flareCount = 0;
    // Start is called before the first frame update
    void Start()
    {

		numText.text = GameManager.instance.numFlares.ToString();
	
    }
	void Throw()
	{

	}
    // Update is called once per frame
	void AddFlare()
	{
		flareCount++;
		GameManager.instance.numFlares = flareCount;
		numText.text = GameManager.instance.numFlares.ToString();
	}
	void DeleteFlare()
	{
		flareCount--;
		if(flareCount < 0)
		{
			flareCount = 0;
		}
		GameManager.instance.numFlares = flareCount;
		numText.text = GameManager.instance.numFlares.ToString();
	}
	void Update()
    {
		if (Input.GetKeyDown(KeyCode.L)){
			AddFlare();
		}
    }
}
