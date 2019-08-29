using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBackpack : MonoBehaviour
{
    public GameObject closedPack, flarePack, healthPack, emptyPack;

    private bool openedFlare = false, openedHealth = false, unopened = true, collected = false;

    void Start()
    {
        //Keeps the backpack closed at the start
        flarePack.SetActive(false);
        healthPack.SetActive(false);
        emptyPack.SetActive(false);
        closedPack.SetActive(true);
    }

    private void OnMouseDown()
    {
        if(unopened)
        {
            //Picks what is inside the backpack
            closedPack.SetActive(false);
            unopened = false;
            int item = Random.Range(1, 4);

            //Debug.Log(item);

            if (item == 1)
            {
                flarePack.SetActive(true);
                openedFlare = true;
            }
            else if (item == 2)
            {
                healthPack.SetActive(true);
                openedHealth = true;
            }
            else
            {
                emptyPack.SetActive(true);
            }
        }

        //Makes sure you cant collect it more than once
        else if(!collected)
        {
            if (openedFlare)
            {
				Inventory.inv.AddFlare();
                //Add a flare
                collected = true;
                flarePack.SetActive(false);
                emptyPack.SetActive(true);
            }
            else if (openedHealth)
            {
                //Heal up missing health
                collected = true;
                healthPack.SetActive(false);
                emptyPack.SetActive(true);
            }
        }
    }
}
