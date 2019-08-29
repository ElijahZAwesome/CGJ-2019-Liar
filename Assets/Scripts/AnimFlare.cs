using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFlare : MonoBehaviour
{
    public bool correct = false;
    public GameObject caveLight;

    void Start()
    {
        caveLight = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        
    }

    void stopAnim()
    {
        Destroy(gameObject);
    }

    void addLight()
    {
        if (correct)
        {
            caveLight.transform.parent = null;
            caveLight.SetActive(true);
        }
    }
}
