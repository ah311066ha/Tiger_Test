using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Watcher : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GM;
    private bool game_start;
    private bool debug;

    void Start()
    {
        game_start = false;
        debug = false; 
    }

    // Update is called once per frame
    void Update()
    {

        if (GM.GetComponent<GameManager>().rolling_status == true)
        {


            game_start = true;
           // print(" not Check!\n ");

        }

        else  {



            //print("" + GameObject.Find("GameBoard").transform.GetChild(4).localPosition.y );
            
            //---------------------L3--------------------------------------
            if ( is_stop(GameObject.Find("GameBoard").transform.GetChild(6).localPosition.y ,6)
               && is_stop(GameObject.Find("GameBoard").transform.GetChild(13).localPosition.y,13)
               && is_stop(GameObject.Find("GameBoard").transform.GetChild(20).localPosition.y,20)
               && is_stop(GameObject.Find("GameBoard").transform.GetChild(27).localPosition.y,27)
               && is_stop(GameObject.Find("GameBoard").transform.GetChild(34).localPosition.y,34)

                //-------------------L2--------------------------------------
                && is_stop2(GameObject.Find("GameBoard").transform.GetChild(5).localPosition.y, 5)
                && is_stop2(GameObject.Find("GameBoard").transform.GetChild(12).localPosition.y, 12)
                && is_stop2(GameObject.Find("GameBoard").transform.GetChild(19).localPosition.y, 19)
                && is_stop2(GameObject.Find("GameBoard").transform.GetChild(26).localPosition.y, 26)
                && is_stop2(GameObject.Find("GameBoard").transform.GetChild(33).localPosition.y, 33)

                //-------------------L1-------------------------------
                && is_stop1(GameObject.Find("GameBoard").transform.GetChild(4).localPosition.y, 4)
                && is_stop1(GameObject.Find("GameBoard").transform.GetChild(11).localPosition.y, 11)
                && is_stop1(GameObject.Find("GameBoard").transform.GetChild(18).localPosition.y, 18)  // unormal bug ,not a big deal
                && is_stop1(GameObject.Find("GameBoard").transform.GetChild(25).localPosition.y, 25)
                && is_stop1(GameObject.Find("GameBoard").transform.GetChild(32).localPosition.y, 32) // unormal bug ,not a big deal

                //-------------------L0------------------------------
                && top_stop(GameObject.Find("GameBoard").transform.GetChild(3).localPosition.y, 3)
                && top_stop(GameObject.Find("GameBoard").transform.GetChild(10).localPosition.y, 10)
                && top_stop(GameObject.Find("GameBoard").transform.GetChild(17).localPosition.y, 17)
                && top_stop(GameObject.Find("GameBoard").transform.GetChild(24).localPosition.y, 24)
                && top_stop(GameObject.Find("GameBoard").transform.GetChild(31).localPosition.y, 31)
                )
            {
                if(game_start==true)GM.GetComponent<GameManager>().CheckForMatches();

                game_start = false;
                //print(" Check!\n ");
            }
            
                
        }

    }

    //L3
    bool is_stop( float a ,int b)
    {
        if (a <= -2.23 && a > -2.25) {

           //print("child:" + b);
            return true;
        }


        else {
           if(debug == true ) print("false child:" + b + " false num: " + a);
            return false;
                
                
        };

    }

    bool is_stop2(float a, int b)
    {
        if (a <= -0.23 && a > -0.25)
        {

            //print("child:" + b);
            return true;
        }
        else
        {
            if (debug == true) print("false child:" + b + " false num: " + a);
            return false;


        };

    }

    bool is_stop1(float a, int b)
    {
        if (a >= 1.75 && a < 1.77)
        {

            //print("child:" + b);
            return true;
        }

        else if (a >= 3.75 && a < 3.77)  // for unormal one
        {

            //print("child:" + b);
            return true;
        }


        else
        {
            if (debug == true) print("false child:" + b + " false num: " + a);
            return false;


        };

    }




    // L0
    bool top_stop( float a, int b)
    {
        if (a >= 3.75 && a < 3.77)
        {

            //print("child:" + b);
            return true;
        }
        else
        {
            if (debug == true) print("false child:" + b + " false num: " + a);
            return false;



        };

    }


}
