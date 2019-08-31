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
	public int flareCount = 2;

    private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("LogicHandler").GetComponent<GameManager>();
		inv = this;
		numText.text = GM.numFlares.ToString();
		flareCount = GM.numFlares;
	}
	public void Throw()
	{
		DeleteFlare();
	}
    // Update is called once per frame
	public void AddFlare()
	{
		flareCount++;
        GM.numFlares = flareCount;
		numText.text = GM.numFlares.ToString();
	}
	public void DeleteFlare()
	{
		flareCount--;
		if(flareCount < 0)
		{
			flareCount = 0;
		}
        GM.numFlares = flareCount;
		numText.text = GM.numFlares.ToString();
	}
	void Update()
    {
        /* 
		if (Input.GetKeyDown(KeyCode.L)){
			AddFlare();
		}
        */
    }
}
