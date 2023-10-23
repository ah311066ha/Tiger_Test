using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling : MonoBehaviour
{

    private Vector3 origin_position;
    public GameObject GM;
    private bool has_rolling = false;
    private bool need_fall;
    public int offset;
    private float speed_plus;


    public float line_speed ;

    void Start()
    {
       
        origin_position = transform.position;
        need_fall = false;
      
    }

    // Update is called once per frame
    void Update()
    {

        speed_plus = GM.GetComponent<GameManager>().speed_plus;
        if (GM.GetComponent<GameManager>().rolling_status == true)
        {
            
          
            transform.Translate(0, speed_plus * line_speed *- 1, 0);
            has_rolling = true;
        }

        else
        {


            if (has_rolling == true)
            {

                has_rolling = false;
                need_fall = true;

               
            }



            if(need_fall == false)transform.position = Vector3.MoveTowards(transform.position, origin_position, speed_plus*5);
            else transform.Translate(0, speed_plus * line_speed* - 1 , 0);

            // set to the  point 
        }





        if (transform.position.y <= -4.24)
        {
           



            if (need_fall == true)
            {
                //transform.position = new Vector3(origin_position.x, 7.76f, origin_position.z);
                transform.position = new Vector3(origin_position.x, origin_position.y + offset, origin_position.z);
                need_fall = false;

            }
            //else transform.position = origin_position;

            else transform.position = new Vector3( origin_position.x , origin_position.y + offset , origin_position.z );
        }
    }
}
