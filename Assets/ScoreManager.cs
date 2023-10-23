using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update


    public Text Total_Score;
    public GameObject GM;
    void Start()
    {
        Total_Score.text = "Score : " + GM.GetComponent<GameManager>().totalscore + "\nbonus: " + GM.GetComponent<GameManager>().bonus;
    }







    // Update is called once per frame
    void Update()
    {
       
        
        
        Total_Score.text = "Score : " + GM.GetComponent<GameManager>().totalscore + "\nbonus: " + GM.GetComponent<GameManager>().bonus; ;
    }
}
