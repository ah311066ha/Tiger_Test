using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shiny : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("showHide", 0.5f, 0.5f);
    }

    void showHide()
    {

        if (this.gameObject.GetComponent<SpriteRenderer>().enabled == false)
        {

            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;

        }
        else
        {

            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        }

    }
}
