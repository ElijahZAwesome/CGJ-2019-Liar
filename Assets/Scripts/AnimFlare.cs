﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFlare : MonoBehaviour
{
    public bool flare;
    public bool correct = false;
    public GameObject caveLight;
    public GameObject x;
    public GameObject o;

    private GameManager GM;

    void Start()
    {
        GM = GameObject.Find("LogicHandler").GetComponent<GameManager>();
    }

    void Update()
    {
        
    }

    void stopAnim()
    {
        if (flare)
        {
            if (correct)
            {
                // put check on door
                GameObject mark = Instantiate(o, transform.position, Quaternion.identity);
                mark.transform.SetParent(GM.transform);
            }
            else
            {
                // put X on door
                GameObject mark = Instantiate(x, transform.position, Quaternion.identity);
                mark.transform.SetParent(GM.transform);
            }
        }
        Destroy(gameObject);
    }

    void addLight()
    {
        Instantiate(caveLight, gameObject.transform.position, Quaternion.identity);
    }
}
