using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFlare : MonoBehaviour
{
    public GameObject flare;
    public Transform leftCave, midCave, rightCave;
    public int caveNum;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if(Inventory.inv.flareCount > 0)
        {
            if (Input.GetMouseButtonDown(1) && caveNum == 3)
            {
                Instantiate(flare, rightCave.transform.position, Quaternion.identity);
                Inventory.inv.Throw();
            }
            else if (Input.GetMouseButtonDown(1) && caveNum == 2)
            {
                Instantiate(flare, midCave.transform.position, Quaternion.identity);
                Inventory.inv.Throw();
            }
            else if (Input.GetMouseButtonDown(1) && caveNum == 1)
            {
                Instantiate(flare, leftCave.transform.position, Quaternion.identity);
                Inventory.inv.Throw();
            }
        }
    }
}
