using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
	public static Inventory inv;
	[SerializeField]
	private TextMeshProUGUI numText;
	[SerializeField]
	private GameObject flare;
	private int flareCount = 0;
    // Start is called before the first frame update
    void Start()
    {
		inv = this;
		numText.text = 0.ToString(); //GameManager.instance.numFlares.ToString();
	
    }
	void Throw()
	{
		DeleteFlare();
	}
    // Update is called once per frame
	public void AddFlare()
	{
		flareCount++;
		GameManager.instance.numFlares = flareCount;
		numText.text = GameManager.instance.numFlares.ToString();
	}
	public void DeleteFlare()
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
