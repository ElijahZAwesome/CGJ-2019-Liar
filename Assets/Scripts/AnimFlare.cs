using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFlare : MonoBehaviour
{
    public bool correct = false;
    public GameObject caveLight;

    void Start()
    {
        
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
        Instantiate(caveLight, gameObject.transform.position, Quaternion.identity);

        if (correct)
        {
            // put check on door
        }
        else
        {
            // put X on door
        }
    }
}
