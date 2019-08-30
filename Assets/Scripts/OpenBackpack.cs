using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBackpack : MonoBehaviour
{
    public GameObject closedPack, flarePack, healthPack, emptyPack;

    private bool openedFlare = false, openedHealth = false, unopened = true, collected = false;

    AudioMaster play;

    void Start()
    {
        //Keeps the backpack closed at the start
        flarePack.SetActive(false);
        healthPack.SetActive(false);
        emptyPack.SetActive(false);
        closedPack.SetActive(true);

        play = GameObject.Find("AudioManager").GetComponent<AudioMaster>();
    }

    private void OnMouseDown()
    {
        if(unopened)
        {
            //Picks what is inside the backpack
            closedPack.SetActive(false);
            unopened = false;
            int item = Random.Range(1, 5); // 1 = flare, 2 = medkit, 3-4 = empty

            //Debug.Log(item);

            if (item == 1)
            {
                play.openPack();
                flarePack.SetActive(true);
                openedFlare = true;
            }
            else if (item == 2)
            {
                play.openPack();
                healthPack.SetActive(true);
                openedHealth = true;
            }
            else
            {
                play.openPack();
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
                play.pickupFlare();
                emptyPack.SetActive(true);
            }
            else if (openedHealth)
            {
                //Heal up missing health
                collected = true;
                healthPack.SetActive(false);
                play.pickupHealth();
                emptyPack.SetActive(true);
                GameManager.instance.isDamaged = false;
            }
        }
    }
}
