using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFlare : MonoBehaviour
{
    public GameObject flare;
    public Transform leftCave, midCave, rightCave;
    public int caveNum;

    AudioMaster toss;

    void Start()
    {
        toss = GameObject.Find("AudioManager").GetComponent<AudioMaster>();
    }

    void Update()
    {

    }

    private void OnMouseOver()
    {
        if(Inventory.inv.flareCount > 0)
        {
            if (Input.GetMouseButtonDown(1) && caveNum == 2)
            {
                GameObject newFlare = Instantiate(flare, rightCave.transform.position, Quaternion.identity);
                toss.flareToss();
                Inventory.inv.Throw();
                if (MasterLogic.MLInstance.currentRoom.safeDoors[caveNum] == true)
                {
                    newFlare.GetComponent<AnimFlare>().correct = true;
                }
            }
            else if (Input.GetMouseButtonDown(1) && caveNum == 1)
            {
                GameObject newFlare = Instantiate(flare, midCave.transform.position, Quaternion.identity);
                toss.flareToss();
                Inventory.inv.Throw();
                if (MasterLogic.MLInstance.currentRoom.safeDoors[caveNum] == true)
                {
                    newFlare.GetComponent<AnimFlare>().correct = true;
                }
            }
            else if (Input.GetMouseButtonDown(1) && caveNum == 0)
            {
                GameObject newFlare = Instantiate(flare, leftCave.transform.position, Quaternion.identity);
                toss.flareToss();
                Inventory.inv.Throw();
                if (MasterLogic.MLInstance.currentRoom.safeDoors[caveNum] == true)
                {
                    newFlare.GetComponent<AnimFlare>().correct = true;
                }
            }
        }
    }
}
