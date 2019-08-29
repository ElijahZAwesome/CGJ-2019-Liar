using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    public AudioSource flare;
    public AudioSource backPack;

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
}
