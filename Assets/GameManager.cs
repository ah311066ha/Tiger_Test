using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public struct g_win_line
{
    //public List<int> winline;
    public string id;
    public int[] winline;

};
public class GameManager : MonoBehaviour
{


    [SerializeField]
    private int boardHeight, boardWidth, score;
    public int totalscore;
    public bool rolling_status;
    public int bonus;
    [SerializeField]
    private GameObject[] gamePieces;
    public GameObject shiny;

    public List<g_win_line> win_table; 

    public bool auto_mode;
    public int idle_time;
    public bool has_auto_spin;


    public GameObject texthud;
   

    private GameObject _board;
    private GameObject[,] _gameBoard;
    private Vector3 _offset = new Vector3(0, 0, -1);
    private List<GameObject> _matchLines;

    private Vector3 origin_position;
    AudioSource audioSource;
    public AudioClip Price_Sound;
    public AudioClip Roll_Sound;
    public AudioClip ADD_sound;
    public AudioClip stop_sound;
    public AudioClip bonus_sound;
    public AudioClip blip;
    public float speed_plus;
    private float origin_speed;

    TextAsset t;
    private bool Debug;
    // Start is called before the first frame update
    void Start()
    {
        
        win_table = new List<g_win_line>();
        auto_mode = false;
        idle_time = 0;
        has_auto_spin = false;

        _board = GameObject.Find("GameBoard");
        origin_speed = speed_plus;
        _gameBoard = new GameObject[boardHeight, boardWidth];
        _matchLines = new List<GameObject>();
        totalscore = 0;
        score = 0;
        rolling_status = false;
        audioSource = GetComponent<AudioSource>();
        bonus = 1;
        Debug = false ;
        
       

        for (int i = 0; i < boardHeight; i++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                GameObject gridPosition = _board.transform.Find(i + " " + j).gameObject;
                if (gridPosition.transform.childCount > 0)
                {
                    GameObject destroyPiece = gridPosition.transform.GetChild(0).gameObject;
                    Destroy(destroyPiece);
                }
                GameObject pieceType = gamePieces[Random.Range(0, gamePieces.Length)];
                GameObject thisPiece = Instantiate(pieceType, gridPosition.transform.position + _offset, Quaternion.identity);
                thisPiece.name = pieceType.name;
                thisPiece.transform.parent = gridPosition.transform;
                _gameBoard[i, j] = thisPiece;
            }
        }

        Read_Wintable();
    }

    public void Read_Wintable() {

        bool Read_Wintable_DEBUG =true;
        //StreamReader inp_stm = new StreamReader("Win_table");
       

        g_win_line tmp = new g_win_line();
        tmp.id = "";
        tmp.winline = new int[5] { -1, -1, -1, -1, -1 };


        //---------------------------------------------------

       
        t = Resources.Load("Win_table") as TextAsset;
        string s = t.text;


        List<string> sentences = new List<string>();            //句子的数组
        int num = 0;

        for (int i = 0; i < s.Length; i++)
        {
            string c = s.Substring(i, 1);     //提取字符串中第i个字符，
            if (c == "\n")                    //如果遇到换行符，跳过，
            {
                num += 1;
                continue;
            }
            if (sentences.Count <= num)         //创建新句子
            {
                sentences.Add(c);
            }
            else
            {
                sentences[sentences.Count - 1] += c;
            }
        }


        int a = 0;
        for (int i = 0; i < sentences.Count; i++)
        {
            //print("sentences[i]:"+sentences[i]);

            if (sentences[i].Contains("@") == false)
            {
                tmp.winline[a] = int.Parse(sentences[i]);
                // if (Read_Wintable_DEBUG==true) print("tmp.winline[a]:" + tmp.winline[a] );
            }



            else
            {
                a = -1;
                tmp.id = sentences[i];

               



                for (int j = 0; j < 5; j++)
                {




                    if ((tmp.winline[j] % 5) != j + 1)
                    {

                        if ((tmp.winline[j] % 5) == 0 && j == 4) ;

                        else { print("errorinput.id :" + tmp.id); }


                    }


                }

                win_table.Add(tmp);

                tmp.id = "";
                tmp.winline = new int[5] { -1, -1, -1, -1, -1 };

            }




            a++;

        }




        if (Read_Wintable_DEBUG == true) list_win_line();
        if (Read_Wintable_DEBUG == true) print("win_table_count :" + win_table.Count);

        // inp_stm.Close();




    }


  

    public void list_win_line() {

        print("========================list_win_line=======================");

        for(int i = 0; i < win_table.Count; i++)
        {
            print("win_line");
            for( int j = 0; j < 5; j++)
            {
                
                print(win_table[i].winline[j]);

            }

            print("@");
        }
        print("===========================================================");
    }



    public void Spin()
    {
        auto_mode = false;
        idle_time = 0;
        has_auto_spin = false;

        speed_plus =1.2f;
        audioSource.PlayOneShot(Roll_Sound);
        foreach (GameObject l in _matchLines)
        {
            GameObject.Destroy(l);
        }
        _matchLines.Clear();
        rolling_status = true;
        auto_mode = false;
        idle_time = 0;
    }



    public void Setbonus1() { 
        bonus = 1;

        audioSource.PlayOneShot(bonus_sound);
        auto_mode = false;
        idle_time = 0;
        has_auto_spin = false;
    }
    public void Setbonus2() {
        bonus = 2;
        audioSource.PlayOneShot(bonus_sound);
        auto_mode = false;
        idle_time = 0;
        has_auto_spin = false;

    }
    public void Setbonus3() { 
        bonus = 3;
        audioSource.PlayOneShot(bonus_sound);
        auto_mode = false;
        idle_time = 0;
        has_auto_spin = false;
    }


    public void ADD100() { 
         totalscore = totalscore + 100;
        audioSource.PlayOneShot(ADD_sound);
        auto_mode = false;
        idle_time = 0;
        has_auto_spin = false;


        GameObject go = Instantiate(texthud, new Vector3( 390, 40 , 0 ), Quaternion.identity, transform);
      //  go.GetComponent<Text>().text = "+ 100" ;
    }
    public void Pause()
    {
        auto_mode = false;
        idle_time = 0;
        has_auto_spin = false;
        if (bonus == 1 && totalscore >= 30) totalscore = totalscore - 30;
        else if (bonus == 2 && totalscore >= 60) totalscore = totalscore - 60;
        else if (bonus == 3 && totalscore >= 90) totalscore = totalscore - 90;

        else { 
            print("out of score!\n");
            audioSource.PlayOneShot(blip);
            return;
        }

       
        if (Debug == true) print("======================================");
        audioSource.PlayOneShot(stop_sound);
        //rolling_status = false;
        foreach (GameObject l in _matchLines)
        {
            GameObject.Destroy(l);
        }
        _matchLines.Clear();
        score = 0;
        totalscore = totalscore + score;
        for (int i = 0; i < boardHeight; i++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                GameObject gridPosition = _board.transform.Find(i + " " + j).gameObject;
                if (gridPosition.transform.childCount > 0)
                {
                    GameObject destroyPiece = gridPosition.transform.GetChild(0).gameObject;
                    Destroy(destroyPiece);
                }
                GameObject pieceType = gamePieces[Random.Range(0, gamePieces.Length)];
                GameObject thisPiece = Instantiate(pieceType, gridPosition.transform.position + _offset, Quaternion.identity);
                thisPiece.name = pieceType.name;
                thisPiece.transform.parent = gridPosition.transform;
                _gameBoard[i, j] = thisPiece;
            }
        }



        if (rolling_status == false) CheckForMatches();
        // Invoke("CheckForMatches", 1);
        rolling_status = false;
        //audioSource.PlayOneShot(stop_sound);
    }



    public void CheckForMatches()
    {
        print("@@@@@@@@@@@@@CheckForMatches@@@@@@@@@@@@@");
        speed_plus = origin_speed;
 
    





        for( int i = 0;i<win_table.Count; i++)
        {


            
            if( Check_One_Line(win_table[i])==true) audioSource.PlayOneShot(Price_Sound);



        }

    }


    public bool Check_One_Line(g_win_line tmp)
    {
        bool Check_One_Line_DEBUG = true;
    


        
        int x = (tmp.winline[0] - 1) / 5;
        int y = (tmp.winline[0] - 1) % 5;
        string record = _gameBoard[x, y].name;
        int length = 0;

        for (int i = 0; i < 5; i++)
        {
            if(tmp.winline[i] > 20)
            {
                print("error:winline[i] > 20, winline[i]:" + tmp.winline[i]);
                return false;

            }

            else if (tmp.winline[i] < -1)
            {
                print("error:winline[i] > -1, winline[i]:" + tmp.winline[i]);
                return false;

            }



            x = (tmp.winline[i] - 1) / 5;
             y = (tmp.winline[i] - 1) % 5;

            if (tmp.winline[i] != -1)
            {


                if (record == _gameBoard[x, y].name)
                {
                    length++;
                }

                else if (length < 3) return false;

               




            }



        }


        if (length < 3) return false;

        //  win, now draw it



        if (Check_One_Line_DEBUG == true) print(" ===========check one line===============");
       
        
        for (int i = 0; i < 5 ; i++)
        {
            
            x = (tmp.winline[i] - 1) / 5;
            y = (tmp.winline[i] - 1) % 5;
            if (Check_One_Line_DEBUG == true) print(" _gameBoard[x, y].name:" + _gameBoard[x, y].name + "x:" + x + "y:" + y);


            if ( i < 4 && tmp.winline[i] != -1 && tmp.winline[i+1] != -1 )
            {
                int a = (tmp.winline[i+1] - 1) / 5;
                int b = (tmp.winline[i+1] - 1) % 5;
               
                if(_gameBoard[x, y].name == record && _gameBoard[a, b].name == record) DrawLine(_gameBoard[x, y].transform.position, _gameBoard[a, b].transform.position);
                else DrawDottedLine(_gameBoard[x, y].transform.position, _gameBoard[a, b].transform.position);

            }

            //if (i > length) break;

        }

        // calculate score
        int score = 0;
        if (auto_mode == true) return true;
        if (length == 3)
        {
            if (record.Contains("1")) score = 10 * bonus;
            else if (record.Contains("2")) score = 20 * bonus;
            else if (record.Contains("3")) score = 30 * bonus;
            else if (record.Contains("4")) score = 40 * bonus;
            else if (record.Contains("5")) score = 50 * bonus;
            else if (record.Contains("6")) score = 60 * bonus;
            else if (record.Contains("7")) score = 70 * bonus;
            totalscore = totalscore + score;
            if (Check_One_Line_DEBUG == true) print("lenth is 3, win_line_id:"+tmp.id);
          //  audioSource.PlayOneShot(Price_Sound);
        }

        else if (length == 4)
        {
            if (record.Contains("1")) score = 30 * bonus;
            else if (record.Contains("2")) score = 60 * bonus;
            else if (record.Contains("3")) score = 90 * bonus;
            else if (record.Contains("4")) score = 120 * bonus;
            else if (record.Contains("5")) score = 150 * bonus;
            else if (record.Contains("6")) score = 180 * bonus;
            else if (record.Contains("7")) score = 210 * bonus;
            totalscore = totalscore + score;
            if (Check_One_Line_DEBUG == true) print("lenth is 4, win_line_id:" + tmp.id);
          //  audioSource.PlayOneShot(Price_Sound);
        }

        else if ( length == 5)
        {

            if (record.Contains("1")) score = 50 * bonus;
            else if (record.Contains("2")) score = 100 * bonus;
            else if (record.Contains("3")) score = 150 * bonus;
            else if (record.Contains("4")) score = 200 * bonus;
            else if (record.Contains("5")) score = 250 * bonus;
            else if (record.Contains("6")) score = 300 * bonus;
            else if (record.Contains("7")) score = 350 * bonus;
            totalscore = totalscore + score;
            if (Check_One_Line_DEBUG == true) print("lenth is 5, win_line_id:" + tmp.id);
         //   audioSource.PlayOneShot(Price_Sound);
        }

        if (Check_One_Line_DEBUG == true) print("======================");
        return true;
    } 


    private void DrawLine(Vector3 start, Vector3 end)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.startWidth = .05f;
        lr.endWidth = .05f;
        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lr.startColor = Color.yellow;
        lr.endColor = Color.yellow;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        _matchLines.Add(myLine);


        GameObject s1;
        GameObject s2;
        s1 = Instantiate(shiny, start, Quaternion.identity);
        s2 = Instantiate(shiny, end, Quaternion.identity);
        _matchLines.Add(s1);
        _matchLines.Add(s2);

    }

    private void DrawDottedLine(Vector3 start, Vector3 end)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.startWidth = .05f;
        lr.endWidth = .05f;
        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lr.startColor = Color.gray;
        lr.endColor = Color.gray;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        _matchLines.Add(myLine);
    }


    public void AutoSpin()
    {

        speed_plus = 1.2f;
        audioSource.PlayOneShot(Roll_Sound);
        foreach (GameObject l in _matchLines)
        {
            GameObject.Destroy(l);
        }
        _matchLines.Clear();
        rolling_status = true;
    }

    public void AutoPause()
    {


       
        if (Debug == true) print("======================================");
        audioSource.PlayOneShot(stop_sound);
        //rolling_status = false;
        foreach (GameObject l in _matchLines)
        {
            GameObject.Destroy(l);
        }
        _matchLines.Clear();
        score = 0;
        totalscore = totalscore + score;
        for (int i = 0; i < boardHeight; i++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                GameObject gridPosition = _board.transform.Find(i + " " + j).gameObject;
                if (gridPosition.transform.childCount > 0)
                {
                    GameObject destroyPiece = gridPosition.transform.GetChild(0).gameObject;
                    Destroy(destroyPiece);
                }
                GameObject pieceType = gamePieces[Random.Range(0, gamePieces.Length)];
                GameObject thisPiece = Instantiate(pieceType, gridPosition.transform.position + _offset, Quaternion.identity);
                thisPiece.name = pieceType.name;
                thisPiece.transform.parent = gridPosition.transform;
                _gameBoard[i, j] = thisPiece;
            }
        }



        if (rolling_status == false) CheckForMatches();
        // Invoke("CheckForMatches", 1);
        rolling_status = false;
        //audioSource.PlayOneShot(stop_sound);
    }

    private void Update()
    {
        var down = Input.GetKeyDown(KeyCode.Space);

        idle_time++;
       // print(idle_time + "auto_mode :" + auto_mode );

        if(idle_time > 10000)
        {
            auto_mode = true;
            idle_time = 0;
            totalscore = 0;
        }


        if (auto_mode == true)
        {
            if (has_auto_spin == false && idle_time > 1000) { 
                AutoSpin();
                idle_time = 0;
                has_auto_spin = true;
            }

            else if (has_auto_spin == true && idle_time > 1000)
            {
                AutoPause();
                idle_time = 0;
                has_auto_spin = false;

            }


        }



        if (down) {
            print("press space to check for matches"); 
            CheckForMatches(); 
        }
    }
}
