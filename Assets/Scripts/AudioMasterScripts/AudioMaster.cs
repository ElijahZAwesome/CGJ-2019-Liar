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
    public AudioSource death;
    public AudioSource heart;
    public AudioSource fuse;
    public AudioSource boom;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
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

    public void youLose()
    {
        death.Play();
    }

    public void lowHealth()
    {
        heart.Play();
    }

    public void fuseSound()
    {
        fuse.Play();
    }

    public void bigBoom()
    {
        boom.Play();
    }

    public void stopHeart()
    {
        heart.Stop();
    }

    public void stopFuse()
    {
        fuse.Stop();
    }
}
