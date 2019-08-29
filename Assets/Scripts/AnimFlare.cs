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
        if (correct)
        {
            Instantiate(caveLight, gameObject.transform.position, Quaternion.identity);
        }
    }
}
