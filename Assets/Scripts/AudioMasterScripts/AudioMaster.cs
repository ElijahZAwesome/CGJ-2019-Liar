using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    public AudioSource flare;
    public AudioSource backPack;
    public AudioSource flarePickup;
    public AudioSource healthPickup;
    public AudioSource button;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void flareToss()
    {
        flare.Play();
    }

    public void openPack()
    {
        backPack.Play();
    }

    public void pickupFlare()
    {
        flarePickup.Play();
    }

    public void pickupHealth()
    {
        healthPickup.Play();
    }

    public void buttonClick()
    {
        button.Play();
    }
}
