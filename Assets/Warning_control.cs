using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Warning_control : MonoBehaviour
{
    public Text mytext;
    public GameObject GM  ;

    void Start()
    {

       
          mytext.text = ""; 


    }

    private void Update()
    {

        if (GM.GetComponent<GameManager>().totalscore <= 0) InvokeRepeating("showHide", 0.5f, 0.5f);
        else {

            mytext.text = "";
            CancelInvoke();
                
                
        };
    }

    void showHide()
    {

        if (mytext.text == "")
        { 

            mytext.text = "Out of Score,"+ GM.GetComponent<GameManager>().win_table.Count+""; 

        }
        else
        { 

            mytext.text = ""; //將 mytext內容改成空的

        }

    }
}
