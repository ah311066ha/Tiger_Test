using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudText : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = GetComponent<Text>();


        lowercenter();




    }
    public void lowercenter()
    {
        text.alignment = TextAnchor.LowerCenter;
        for (int i = 28; i > 14; i--)
        {
            text.fontSize = i;
        }
        Invoke("middlecenter", 0.2f);
    }
    public void middlecenter()
    {
        text.alignment = TextAnchor.MiddleCenter;
        for (int i = 14; i > 7; i--)
        {
            text.fontSize = i;
        }
        Invoke("uppercenter", 0.2f);
    }
    public void uppercenter()
    {
        text.alignment = TextAnchor.UpperCenter;
        for (int i = 7; i > 0; i--)
        {
            text.fontSize = i;
        }
        Destroy(gameObject, 0.2f);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
