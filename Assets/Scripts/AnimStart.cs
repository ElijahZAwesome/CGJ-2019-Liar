using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStart : MonoBehaviour
{
    public bool multipleAnim;
    public float randTime;
    float pickTime;
    bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        pickTime = Random.Range(0, randTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!multipleAnim)
        {
            if (!start)
            {
                pickTime -= Time.deltaTime;
                if (pickTime < 0)
                {
                    gameObject.GetComponent<Animator>().SetBool("Go", true);
                    start = true;
                }
            }
        }
        else
        {
            pickTime -= Time.deltaTime;
            if (!start)
            {
                if (pickTime < 0)
                {
                    gameObject.GetComponent<Animator>().SetBool("Go", true);
                    start = true;
                }
            }
            else
            {
                // make your animation do more things here
            }
        }
    }
}
