using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BonusSelect : MonoBehaviour
{
    public Text mytext;
    public GameObject GM;   // Start is called before the first frame update
    void Start()
    {
        mytext.text = "↑";
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("showHide", 0.5f, 0.5f);


        if (GM.GetComponent<GameManager>().bonus == 1) transform.localPosition = new Vector3( -274.6f , -202 ,0 );
        else if (GM.GetComponent<GameManager>().bonus == 2) transform.localPosition = new Vector3(-185.6f, -202, 0);
        else if (GM.GetComponent<GameManager>().bonus == 3) transform.localPosition = new Vector3(-100.6f, -202, 0);






    }


    void showHide()
    {

        if (mytext.text == "")
        {

            mytext.text = "↑";

        }
        else
        {

            mytext.text = ""; //將 mytext內容改成空的

        }

    }

}
