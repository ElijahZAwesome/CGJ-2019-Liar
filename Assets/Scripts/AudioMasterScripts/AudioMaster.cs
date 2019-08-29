using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    public AudioSource flare;

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
}
