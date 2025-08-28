using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

public class FindMoves : MonoBehaviour
{
    public int turn;
    public GameObject BadObject;
    public GameData gameDatasc;
    public GameObject Gamedata;
    private GameObject p;
    private GameObject T;
    public GameObject[] free = new GameObject[61];
    public string ballname;
    public int pushed;
    //private GameObject[] boardstate = new GameObject[61];

    public List<string> tree;
    private bool add;
    int ecount = 0;
    public int enemycount;
    public int centercount;
    public int friendlycount;
    public int rvl;
    public int ct;
    public int edgecount;
    public int[] broadcnt;
    private int t1;
    private int b1;
    private int t2;
    private int b2;
    private int broad;
    public int m;
    public int l;
    public int r;
    public int c;
    public int d;
    // Start is called before the first frame update
    void Start()
    {
        Gamedata = GameObject.Find("GameData");
        gameDatasc = Gamedata.GetComponent<GameData>();
        BadObject = GameObject.Find("BadObject");
    }

    // Update is called once per frame
    void Update()
    {
        //  if(Input.GetKeyDown("a"))
        // {
        // Debug.Log("Button Hit");
        // SearchforEmptySpacesE();
        //  SearchMyBalls();
        //  ValidateMoves();


        //  }

        if(Input.GetKeyDown("l"))
        {
            //SaveBoardState(boardstate0);
            
            ClearFreeMoves();
           
            SearchMyBalls();
            
            ValidateMoves(tree);
        }
       
        if (Input.GetKeyDown("b"))
        {
            ClearFreeMoves();
            tree.Clear();
        }
        if (Input.GetKeyDown("c"))
        {
            SearchforOccupiedSpaces();
        }
        if (Input.GetKeyDown("c"))
        {
            SearchforOccupiedSpaces();
        }
        if (Input.GetKey("e"))
        {
            Application.Quit();
        }
    }
    //The following funtions search for empty and enemy spaces that neigbor myball spaces. 
    //Need to be able to use the values collected in free to validate moves.
   
    public bool ValidateMoves(List<string> moves)
    {
        ecount = 0;   
        for (int i = 0; i < 61; i++)
        {
            ecount = 0;
            if (free[i] != null)
            {
                for (int j = 0; j < 6; j++)
                {
                    ecount = 0;
                    if (free[i].GetComponent<select>().neighbors[j].transform.GetComponent<select>().Enemyball == true)
                    {
                        ecount = 1;
                        //one enmey ball
                        if (free[i].GetComponent<select>().neighbors[j].transform.GetComponent<select>().neighbors[j].transform.GetComponent<select>().Enemyball == true)
                        {
                            Debug.Log("Ecount 2");
                            ecount = 2;
                            //two enemy ball
                            if (free[i].GetComponent<select>().neighbors[j].transform.GetComponent<select>().neighbors[j].transform.GetComponent<select>().neighbors[j].transform.GetComponent<select>().Enemyball == true)
                            {
                                //three enemyball
                                ecount = 3;
                            }
                            else if (free[i].GetComponent<select>().neighbors[j].transform.GetComponent<select>().neighbors[j].transform.GetComponent<select>().neighbors[j].transform.GetComponent<select>().Myball == true)
                            {
                                //three enemyball
                                ecount = 4;
                            }

                        }
                        else if(free[i].GetComponent<select>().neighbors[j].transform.GetComponent<select>().neighbors[j].transform.GetComponent<select>().Myball == true)
                        {
                            ecount = 4;
                        }
                    }
                    pushed = ecount;
                    Debug.Log("PushedValue: " + pushed);


                    if (free[i].GetComponent<select>().neighbors[j].transform.GetComponent<select>().blank == true || free[i].GetComponent<select>().neighbors[j].transform.GetComponent<select>().Enemyball == true)
                    {
                        //broadside movement
                        /*
                        if (free[i].GetComponent<select>().neighbors[j].transform.GetComponent<select>().blank == true)
                        {
                            
                            for(int k = 0; k < 6; k++)
                            {
                                
                                if(free[i].GetComponent<select>().neighbors[k].transform.GetComponent<select>().Myball == true)
                                {
                                    if (k == 0)
                                    {
                                        m = 1;
                                        l = 4;
                                        r = 3;
                                        c = 5;
                                        d = 2;
                                    }
                                    if (k == 1)
                                    {
                                        m = 0;
                                        l = 3;
                                        r = 3;
                                        c = 5;
                                        d = 2;
                                    }
                                    if (k == 2)
                                    {
                                        m = 0;
                                        l = 0;
                                        r = 5;
                                    }
                                    if (k == 3)
                                    {
                                        m = 0;
                                        l = 0;
                                        r = 0;
                                    }
                                    if (k == 4)
                                    {
                                        m = 0;
                                        l = 0;
                                        r = 0;
                                    }
                                    if (k == 5)
                                    {
                                        m = 0;
                                        l = 0;
                                        r = 0;
                                    }


                                    if (free[i].GetComponent<select>().neighbors[k].transform.GetComponent<select>().neighbors[m].transform.GetComponent<select>().Myball == true)
                                    {
                                            
                                            
                                        if (free[i].GetComponent<select>().neighbors[k].transform.GetComponent<select>().neighbors[m].transform.GetComponent<select>().neighbors[r].transform.GetComponent<select>().blank == true)
                                        {
                                            //top is good
                                            t1 = 1;
                                        }
                                    }
                                    if (free[i].GetComponent<select>().neighbors[k].transform.GetComponent<select>().neighbors[l].transform.GetComponent<select>().Myball == true)
                                    {

                                        if (free[i].GetComponent<select>().neighbors[k].transform.GetComponent<select>().neighbors[l].transform.GetComponent<select>().neighbors[r].transform.GetComponent<select>().blank == true)
                                        {
                                            //bottom is good
                                            b1 = 1;
                                        }
                                    }

                                    if (free[i].GetComponent<select>().neighbors[k].transform.GetComponent<select>().neighbors[c].transform.GetComponent<select>().Myball == true)
                                    {


                                        if (free[i].GetComponent<select>().neighbors[k].transform.GetComponent<select>().neighbors[c].transform.GetComponent<select>().neighbors[r].transform.GetComponent<select>().blank == true)
                                        {
                                            //top is good
                                            t2 = 1;
                                        }
                                    }
                                    if (free[i].GetComponent<select>().neighbors[k].transform.GetComponent<select>().neighbors[d].transform.GetComponent<select>().Myball == true)
                                    {

                                        if (free[i].GetComponent<select>().neighbors[k].transform.GetComponent<select>().neighbors[d].transform.GetComponent<select>().neighbors[r].transform.GetComponent<select>().blank == true)
                                        {
                                            //bottom is good
                                            b2 = 1;
                                        }
                                    }

                                    if (t2 == 1 && b2 == 1)
                                    {
                                        broad = 2;
                                        int[] numbersb = { i, k, c, d, 0, broad };
                                        string resultb = string.Join(", ", numbersb); // "1, 2, 3, 4, 5"
                                        moves.Add(resultb);
                                    }
                                    else if (t2 == 1)
                                    {
                                        broad = 1;
                                        int[] numbersb = { i, k, c, -1, 0, broad };
                                        string resultb = string.Join(", ", numbersb); // "1, 2, 3, 4, 5"
                                        moves.Add(resultb);
                                    }
                                    else if (b2 == 1)
                                    {
                                        broad = 1;
                                        int[] numbersb = { i, k, -1, d, 0, broad };
                                        string resultb = string.Join(", ", numbersb); // "1, 2, 3, 4, 5"
                                        moves.Add(resultb);
                                    }
                                    if (t1 == 1 && b1 == 1)
                                    {
                                        broad = 2;
                                        int[] numbersb = { i, k, m,l, 0, broad};
                                        string resultb = string.Join(", ", numbersb); // "1, 2, 3, 4, 5"
                                        moves.Add(resultb);
                                    }
                                    else if(t1 == 1)
                                    {
                                        broad = 1;
                                        int[] numbersb = { i, k, m, -1, 0, broad };
                                        string resultb = string.Join(", ", numbersb); // "1, 2, 3, 4, 5"
                                        moves.Add(resultb);
                                    }
                                    else if(b1 == 1)
                                    {
                                        broad = 1;
                                        int[] numbersb = { i, k, -1, l, 0, broad };
                                        string resultb = string.Join(", ", numbersb); // "1, 2, 3, 4, 5"
                                        moves.Add(resultb);
                                    }
                                    
                                }
                                


                                
                            }
                           
                        }
                        */
                        //end of broadside
                        //if(free[i].GetComponent<select>().neighbors[j].transform.GetComponent<select>().blank == true)
                        //{
                        //  if (ecount == 0)
                        //  {
                        //    int[] numbers = { i, j, -1, 1, ecount };
                        //  string result = string.Join(", ", numbers); // "1, 2, 3, 4, 5"
                        //moves.Add(result);
                        //}
                        //}
                        if (j == 0)
                        {
                            rvl = BuildStrings2(i, 3, 0, ecount);
                            if(rvl == 1)
                            {
                                int[] numbers = { i, j, -1, 1, ecount };
                                string result = string.Join(", ", numbers); // "1, 2, 3, 4, 5"
                                moves.Add(result);
                            }
                            if (rvl == 2)
                            {
                                int[] numbers2 = { i, j, 3, 2, ecount };
                                string result2 = string.Join(", ", numbers2); // "1, 2, 3, 4, 5"
                                moves.Add(result2);
                            }
                            else if(rvl == 3)
                            {
                                int[] numbers3 = { i, j, 3, 3, ecount };
                                string result3 = string.Join(", ", numbers3); // "1, 2, 3, 4, 5"
                               moves.Add(result3);
                            }
                        }
                        if (j == 1)
                        {
                            rvl = BuildStrings2(i, 4, 1, ecount);
                            if (rvl == 1)
                            {
                                int[] numbers = { i, j, -1, 1, ecount };
                                string result = string.Join(", ", numbers); // "1, 2, 3, 4, 5"
                                moves.Add(result);
                            }
                            if (rvl == 2)
                            {
                                int[] numbers2 = { i, j, 4, 2, ecount };
                                string result2 = string.Join(", ", numbers2); // "1, 2, 3, 4, 5"
                                moves.Add(result2);
                            }
                            else if (rvl == 3)
                            {
                                int[] numbers3 = { i, j, 4, 3, ecount };
                                string result3 = string.Join(", ", numbers3); // "1, 2, 3, 4, 5"
                               moves.Add(result3);
                            }
                        }
                        if (j == 2)
                        {
                            rvl = BuildStrings2(i, 5, 2, ecount);
                            if (rvl == 1)
                            {
                                int[] numbers = { i, j, -1, 1, ecount };
                                string result = string.Join(", ", numbers); // "1, 2, 3, 4, 5"
                                moves.Add(result);
                            }
                            if (rvl == 2)
                            {
                                int[] numbers2 = { i, j, 5, 2, ecount };
                                string result2 = string.Join(", ", numbers2); // "1, 2, 3, 4, 5"
                                moves.Add(result2);
                            }
                            else if (rvl == 3)
                            {
                                int[] numbers3 = { i, j, 5, 3,ecount };
                                string result3 = string.Join(", ", numbers3); // "1, 2, 3, 4, 5"
                               moves.Add(result3);
                            }
                        }
                        if (j == 3)
                        {
                            rvl = BuildStrings2(i, 0, 3, ecount);
                            if (rvl == 1)
                            {
                                int[] numbers = { i, j, -1, 1, ecount };
                                string result = string.Join(", ", numbers); // "1, 2, 3, 4, 5"
                                moves.Add(result);
                            }
                            if (rvl == 2)
                            {
                                int[] numbers2 = { i, j, 0, 2 , ecount };
                                string result2 = string.Join(", ", numbers2); // "1, 2, 3, 4, 5"
                                moves.Add(result2);
                            }
                            else if (rvl == 3)
                            {
                                int[] numbers3 = { i, j, 0, 3 , ecount };
                                string result3 = string.Join(", ", numbers3); // "1, 2, 3, 4, 5"
                                moves.Add(result3);
                            }
                        }
                        if (j == 4)
                        {
                            rvl = BuildStrings2(i, 1, 4, ecount);
                            if (rvl == 1)
                            {
                                int[] numbers = { i, j, -1, 1, ecount };
                                string result = string.Join(", ", numbers); // "1, 2, 3, 4, 5"
                                moves.Add(result);
                            }
                            if (rvl == 2)
                            {
                                int[] numbers2 = { i, j, 1, 2 , ecount };
                                string result2 = string.Join(", ", numbers2); // "1, 2, 3, 4, 5"
                                moves.Add(result2);
                            }
                            else if (rvl == 3)
                            {
                                int[] numbers3 = { i, j, 1, 3, ecount };
                                string result3 = string.Join(", ", numbers3); // "1, 2, 3, 4, 5"
                                moves.Add(result3);
                            }
                        }
                        if (j == 5)
                        {
                            rvl = BuildStrings2(i, 2, 5, ecount);
                            if (rvl == 1)
                            {
                                int[] numbers = { i, j, -1, 1, ecount };
                                string result = string.Join(", ", numbers); // "1, 2, 3, 4, 5"
                                moves.Add(result);
                            }
                            if (rvl == 2)
                            {
                                int[] numbers2 = { i, j, 2, 2 , ecount };
                                string result2 = string.Join(", ", numbers2); // "1, 2, 3, 4, 5"
                                moves.Add(result2);
                            }
                            else if (rvl == 3)
                            {
                                int[] numbers3 = { i, j, 2, 3 , ecount };
                                string result3 = string.Join(", ", numbers3); // "1, 2, 3, 4, 5"
                                moves.Add(result3);
                            }
                        }
                    }
                   



                }
            }
        }
        
        return false;
    }
   
    public int BuildStrings2(int index, int n, int j, int ecount)
    {
        if (free[index].GetComponent<select>().neighbors[n].transform.GetComponent<select>().Myball == true)
        {

            //Have 2 balls 
           
            
            //checks for 3
            //this line is bad
            if (free[index].GetComponent<select>().neighbors[n].transform.GetComponent<select>().neighbors[n].transform.GetComponent<select>().Myball == true)
            {
                //Debug.Log("Have 3 par options");
                //have 3
                if (ecount <= 2)
                {
                    Debug.Log("Have 3 par options");
                    return 3;
                }
                    
            }
            //Debug.Log("Have 2 par options");
            if (ecount <= 1)
            {

                return 2;
            }
            return 0;

        }
        else
        {
            if(ecount < 1)
            {
                return 1;
            }
        }
        return 0;
       
    }
    public void SearchMyBalls()
    {
        for (int i = 0; i < 61; i++)
        {
            
            if (gameDatasc.board[i] == 1)
            {
                
                
                p = GameObject.Find(i.ToString());
                //  Debug.Log(p);
                add = false;
                for (int j = 0; j < 6; j++)
                {
                    if (p.GetComponent<select>().neighbors[j].transform.GetComponent<select>().blank == true)
                    {
                        // Debug.Log(j);
                        add = true;


                    }
                    if(p.GetComponent<select>().neighbors[j].transform.GetComponent<select>().Enemyball == true)
                    {
                        add = true;
                    }
                }
                if (add == true)
                {
                    Debug.Log("Adding: " + p.gameObject.name);
                    free[i] = p;
                }
                else
                {
                    free[i] = null;
                }
            }
            if (gameDatasc.board[i] == 2)
            {
               
            }


        }
    }
    public void SearchforEmptySpacesE()
    {
       
        for (int i = 0; i < 61; i++) 
        {
            if (gameDatasc.board[i] == 0)
            {
               
                p = GameObject.Find(i.ToString());
              //  Debug.Log(p);
                add = false;
                for(int j = 0; j < 6; j++)
                {
                    if(p.GetComponent<select>().neighbors[j].transform.GetComponent<select>().Myball == true)
                    {
                       // Debug.Log(j);
                       add = true;

                       
                    }
                }
                if(add == true)
                {
                    Debug.Log("Adding: " + p.gameObject.name);
                    free[i] = p;    
                }
                else
                {
                    free[i] = null;
                }
            }
            

        }
       // Debug.Log(free[47]);
    }
    public void SearchforOccupiedSpaces()
    {
        for (int i = 0; i < 61; i++)
        {
            if (gameDatasc.board[i] == 2)
            {

                p = GameObject.Find(i.ToString());
                //Debug.Log(p);
                add = false;
                for (int j = 0; j < 6; j++)
                {
                    if (p.GetComponent<select>().neighbors[j].transform.GetComponent<select>().Myball == true)
                    {
                        //may be easier to just collect data and report it back to GameData to check.
                      // if(j == 0)
                        //{
                            //3
                           // if(p.GetComponent<select>().neighbors[3].transform.GetComponent<select>().Myball == true)
                            //{

                           // }
                        //}


                        add = true;


                    }
                }
                if (add == true)
                {
                    //efree[i] = p;
                }
            }
            else
            {

            }
        }

    }
    public void ClearFreeMoves()
    {
        for (int i = 0; i < 61; i++)
        {
            free[i] = null;

        }

    }
    
    public void ClearBoardState(GameObject[] boardstate)
    {
        for (int i = 0; i < 61; i++)
        {
            p = GameObject.Find(i.ToString());
            if (p.GetComponent<select>().OwnBall != BadObject)
            {

                boardstate[i] = BadObject;

            }
            else
            {
                boardstate[i] = BadObject;
            }

        }
    }
    public void SaveBoardState(GameObject[] boardstate)
    {
        for (int i = 0; i < 61; i++)
        {
                p = GameObject.Find(i.ToString());
                if (p.GetComponent<select>().OwnBall != BadObject)
                {

                    boardstate[i] = p.GetComponent<select>().OwnBall;

                }
                else
                {
                    boardstate[i] = BadObject;
                }
     
        }   
    }
    public void ChangeTurn()
    {
        if(gameDatasc.turn == 0)
        {
            gameDatasc.turn = 1;
        }
        else if (gameDatasc.turn == 1)
        {
            gameDatasc.turn = 0;
        }
        for (int i = 0; i < 61; i++)
        {
            if (gameDatasc.board[i] == 1)
            {
                gameDatasc.board[i] = 2;
                p = GameObject.Find(i.ToString());
                //Debug.Log(p);
                if (p.GetComponent<select>().OwnBall != BadObject)
                {

                    p.GetComponent<select>().Enemyball = true;
                    p.GetComponent<select>().Myball = false;

                }
            }
            else if(gameDatasc.board[i] == 2)
            {
                gameDatasc.board[i] = 1;
                p = GameObject.Find(i.ToString());
                //Debug.Log(p);
                if (p.GetComponent<select>().OwnBall != BadObject)
                {

                    p.GetComponent<select>().Enemyball = false;
                    p.GetComponent<select>().Myball = true;

                }
            }

        }
    }

    public void MakeMove(string move)
    {
        // int[] numbers3 = { i, j, 3 };
        //string result3 = string.Join(", ", numbers3); // "1, 2, 3, 4, 5"
        //tree.Add(result3);

        string[] data = move.Split(',');
        int ennum = int.Parse(data[1]);
        int nnum = int.Parse(data[2]);

        if (int.Parse(data[3]) == 1)
        {
            gameDatasc.full[0] = GameObject.Find(data[0]);
            gameDatasc.empty = gameDatasc.full[0].GetComponent<select>().neighbors[ennum];
            gameDatasc.full[1] = BadObject;
            gameDatasc.full[2] = BadObject;
            gameDatasc.CallMove();
            //gameDatasc.retval = gameDatasc.Checkneighbors1();
            // Debug.Log("Checking 1 neigbor");
            //gameDatasc.ShiftToEmpty(gameDatasc.retval);
            // Debug.Log("Output: " + Checkneighbors1());
        }
        else if (int.Parse(data[3]) == 2)
        {
            gameDatasc.full[0] = GameObject.Find(data[0]);
            gameDatasc.empty = gameDatasc.full[0].GetComponent<select>().neighbors[ennum];
            gameDatasc.full[1] = gameDatasc.full[0].GetComponent<select>().neighbors[nnum];
            gameDatasc.full[2] = BadObject;
            gameDatasc.CallMove();

            //gameDatasc.retval = gameDatasc.Checkneighbors1();
           // gameDatasc.retval = gameDatasc.Checkneighbors2();
           
            //Debug.Log("Checking 2 neigbor");
           // gameDatasc.ShiftToEmpty(gameDatasc.retval);
            //Debug.Log("Output: " + Checkneighbors2());

        }
        else if (int.Parse(data[3]) == 3)
        {
            gameDatasc.full[0] = GameObject.Find(data[0]);
            gameDatasc.empty = gameDatasc.full[0].GetComponent<select>().neighbors[ennum];
            gameDatasc.full[1] = gameDatasc.full[0].GetComponent<select>().neighbors[nnum];
            gameDatasc.full[2] = gameDatasc.full[0].GetComponent<select>().neighbors[nnum].GetComponent<select>().neighbors[nnum];
            gameDatasc.CallMove();
            //gameDatasc.retval = gameDatasc.Checkneighbors1();
           // gameDatasc.retval = gameDatasc.Checkneighborsfull();
            // Debug.Log("Checking 3 neigbor");
            //Debug.Log("Value to Chcek: " +  retval);
            //gameDatasc.ShiftToEmpty(gameDatasc.retval);
            // Debug.Log("Output: " + Checkneighborsfull());
        }
        //pushed = int.Parse(data[4]);
        //Debug.Log("PushedValue: " + pushed);
       //gameDatasc.full[0] = BadObject;
        //gameDatasc.full[1] = BadObject;
       // gameDatasc.full[2] = BadObject;
      // gameDatasc.empty = BadObject;
    }
    public void UpdateBoardVals()
    {
        for (int i = 0; i < 61; i++)
        {
            p = GameObject.Find(i.ToString());
            if (turn == 0)
            {
               
                // ballname = OwnBall.gameObject.name;
                if (p.gameObject.name[0] == 'K')
                {
                    p.GetComponent<select>().Enemyball = false;
                    p.GetComponent<select>().Myball = false;
                    p.GetComponent<select>().blank = false;
                    p.GetComponent<select>().gameData.board[i] = 3;
                }
                else if (p.GetComponent<select>().OwnBall == BadObject)
                {
                    p.GetComponent<select>().blank = true;
                    p.GetComponent<select>().Myball = false;
                    p.GetComponent<select>().Enemyball = false;
                    p.GetComponent<select>().gameData.board[i] = 0;
                }
                else
                {
                    ballname = p.GetComponent<select>().OwnBall.gameObject.name;
                    //Debug.Log(ballname[0]);
                    if (ballname[0] == 'B')
                    {
                        p.GetComponent<select>().Myball = true;
                        p.GetComponent<select>().blank = false;
                        p.GetComponent<select>().Enemyball = false;
                        p.GetComponent<select>().gameData.board[i] = 1;
                    }

                    else
                    {
                        p.GetComponent<select>().Enemyball = true;
                        p.GetComponent<select>().Myball = false;
                        p.GetComponent<select>().blank = false;
                        p.GetComponent<select>().gameData.board[i] = 2;
                    }
                }
            }
            else if (turn == 1)
            {
                if (p.gameObject.name[0] == 'K')
                {
                    p.GetComponent<select>().Enemyball = false;
                    p.GetComponent<select>().Myball = false;
                    p.GetComponent<select>().blank = false;
                    p.GetComponent<select>().gameData.board[i] = 3;
                }
                else if (p.GetComponent<select>().OwnBall == BadObject)
                {
                    p.GetComponent<select>().blank = true;
                    p.GetComponent<select>().Myball = false;
                    p.GetComponent<select>().Enemyball = false;
                    p.GetComponent<select>().gameData.board[i] = 0;
                }
                else
                {
                    ballname = p.GetComponent<select>().OwnBall.gameObject.name;
                    //Debug.Log(ballname[0]);
                    if (ballname[0] == 'B')
                    {
                        p.GetComponent<select>().Myball = false;
                        p.GetComponent<select>().blank = false;
                        p.GetComponent<select>().Enemyball = true;
                        p.GetComponent<select>().gameData.board[i] = 2;
                    }
                    else
                    {
                        p.GetComponent<select>().Enemyball = false;
                        p.GetComponent<select>().Myball = true;
                        p.GetComponent<select>().blank = false;
                        p.GetComponent<select>().gameData.board[i] = 1;
                    }
                }
            }

        }
    }
    public void StaticEvaluation()
    {
        friendlycount = 0;
        enemycount = 0;
        centercount = 0;
        edgecount = 0;
        for (int i = 0; i < 61; i++)
        {
            if (gameDatasc.board[i] == 1)
            {
                friendlycount++;
                if (i > 5 && i < 10)
                {
                    centercount = centercount + 1;//1
                }
                if (i == 12 || i == 19 || i == 27 || i == 36 || i == 48 || i == 51 || i == 52 || i == 33 || i == 54 || i == 53 || i == 41 || i == 44 || i == 24 || i == 16)
                {
                    centercount = centercount + 1;//1
                }
                if (i > 12 && i <= 15)
                {
                    centercount = centercount + 2;//2
                }
                if (i == 20 || i == 23 || i == 32 || i == 40 || i == 47 || i == 46 || i == 45 || i == 37 || i == 28)
                {
                    centercount = centercount + 2;//2
                }
                if (i == 21 || i == 22 || i == 31 || i == 39 || i == 38 || i == 29)
                {
                    centercount = centercount + 3;//3
                }
                if (i == 30)
                {
                    centercount = centercount + 4;//center position
                }
                if (i > 44 && i <= 47)
                {
                    centercount = centercount + 2;//2
                }
                if (i >= 0 && i <= 4)
                {
                    centercount = centercount - 2;//0 -5
                }
                if (i == 5 || i == 11 || i == 18 || i == 26 || i == 35 || i == 43 || i == 50 || i == 56 || i == 57 || i == 58 || i == 59 || i == 60 || i == 55 || i == 49 || i == 42 || i == 34 || i == 25 || i == 17 || i == 10)
                {
                    centercount = centercount   -2;//0 - 5
                }
             
            }
            else if (gameDatasc.board[i] == 2)
            {
                enemycount++;
                if (i >= 6 && i <= 9)
                {
                    edgecount = edgecount + 3;
                }
                if (i == 12 || i == 19 || i == 27 || i == 36 || i == 48 || i == 51 || i == 52 || i == 33 || i == 54 || i == 53 || i == 41 || i == 44 || i == 24 || i == 16)
                {
                    edgecount = edgecount + 3;
                }
                if (i > 12 && i <= 15)
                {
                    edgecount = edgecount + 2;
                }
                if (i == 20 || i == 23 || i == 32 || i == 40 || i == 47 || i == 46 || i == 45 || i == 37 || i == 28)
                {
                    edgecount = edgecount + 2;
                }
                if (i == 21 || i == 22 || i == 31 || i == 39 || i == 38 || i == 29)
                {
                    edgecount = edgecount + 1;
                }
                if (i == 30)
                {
                    edgecount = edgecount + 0;//center position
                }
                if (i > 44 && i <= 47)
                {
                    edgecount = edgecount + 2;
                }
                if (i >= 0 && i <= 4)
                {
                    edgecount = edgecount + 4;
                }
                if (i == 5 || i == 11 || i == 18 || i == 26 || i == 35 || i == 43 || i == 50 || i == 56 || i == 57 || i == 58 || i == 59 || i == 60 || i == 55 || i == 49 || i == 42 || i == 34 || i == 25 || i == 17 || i == 10)
                {
                    edgecount = edgecount + 4;
                }
            }
           
        }

    }
    /*   if (i >= 6 && i <= 9)
                {
                    edgecount = edgecount + 3;
                }
                if (i == 12 || i == 19 || i == 27 || i == 36 || i == 48 || i == 51 || i == 52 || i == 33 || i == 54 || i == 53 || i == 41 || i == 44 || i == 24 || i == 16)
                {
                    edgecount = edgecount + 3;
                }
     * */
    public void RestoreBoardState(GameObject[] boardstate)
    {
        for (int i = 0; i < 61; i++)
        {
            p = GameObject.Find(i.ToString());

            T = GameObject.Find(boardstate[i].name);
            T.transform.position = p.transform.position;
            p.GetComponent<select>().OwnBall = boardstate[i];
        }

          
        
    }
}
