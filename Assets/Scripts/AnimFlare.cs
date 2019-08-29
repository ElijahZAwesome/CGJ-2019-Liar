using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFlare : MonoBehaviour
{
    public bool flare;
    public bool correct = false;
    public GameObject caveLight;
    public GameObject x;
    public GameObject o;

    void Start()
    {
        
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
                Instantiate(o, transform.position, Quaternion.identity);
            }
            else
            {
                // put X on door
                Instantiate(x, transform.position, Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }

    void addLight()
    {
        Instantiate(caveLight, gameObject.transform.position, Quaternion.identity);
    }
}
