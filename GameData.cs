
using UnityEngine;


public class GameData : MonoBehaviour
{
    public int[] board = new int[61];
    public GameObject[] full = new GameObject [3];
    public GameObject empty;
    public int retval;
    public int r2;
    public GameObject placeholder1;
    public GameObject BadObject;
    public int turn = 0;
    public FindMoves fms;
    public GameObject FindMoves;
    public int shiftstat;
    public ExampleCo minmax;
    public GameObject PracticeCoroutine;
    public string bmove;

    // Start is called before the first frame update
    void Start()
    {
        PracticeCoroutine = GameObject.Find("PracticeCoroutine");
        minmax = PracticeCoroutine.GetComponent<ExampleCo>();
        FindMoves = GameObject.Find("FindMoves");
        fms = FindMoves.GetComponent<FindMoves>();
        BadObject = GameObject.Find("BadObject");
        full[0] = BadObject;
        full[1] = BadObject;
        full[2] = BadObject;
        empty = BadObject;
        shiftstat = 2;
        bmove = "";
        //fms = FindMoves.GetComponent<FindMoves>();
    }

    // Update is called once per frame
    void Update()
    {
        //Need to add checks for if empty is an enemy and how many enemies are behind it.
        if(Input.GetKeyDown("t"))
        {
           // Debug.Log(turn);
            if(turn == 0) { turn = 1; }
            else if(turn == 1) {  turn = 0; }
        }
        if(Input.GetKeyDown("r"))
            {
            //bmove = " after";
            //fms.MakeMove(bmove);
           // fms.MakeMove(bestmove);
            CallMove();
            fms.UpdateBoardVals();
        }
        if (Input.GetKeyDown("s"))
        {
           
            if (full[1] == BadObject)
            {
                retval = Checkneighbors1();
               // Debug.Log("Checking 1 neigbor");
                ShiftToEmpty(retval);
                if(retval != -1)
                {
                    fms.ChangeTurn();
                }
               // fms.ChangeTurn();
                //if (turn == 0) { turn = 1; }
                //else if (turn == 1) { turn = 0; }
                full[0] = BadObject;
                full[1] = BadObject;
                full[2] = BadObject;
                empty = BadObject;

                // Debug.Log("Output: " + Checkneighbors1());
            }
            else if (full[2] == BadObject)
            {
                retval = Checkneighbors2();
                Debug.Log(retval);
                //Debug.Log("Checking 2 neigbor");
                ShiftToEmpty(retval);
                if (retval != -1)
                {
                    fms.ChangeTurn();
                }
                //if (turn == 0) { turn = 1; }
                //else if (turn == 1) { turn = 0; }
                full[0] = BadObject;
                full[1] = BadObject;
                full[2] = BadObject;
                empty = BadObject;
                //Debug.Log("Output: " + Checkneighbors2());

            }
            else
            {
                retval = Checkneighborsfull();
               // Debug.Log("Checking 3 neigbor");
                //Debug.Log("Value to Chcek: " +  retval);
                ShiftToEmpty(retval);
                if (retval != -1)
                {
                    fms.ChangeTurn();
                }
                //if (turn == 0) { turn = 1; }
                //else if (turn == 1) { turn = 0; }
                full[0] = BadObject;
                full[1] = BadObject;
                full[2] = BadObject;
                empty = BadObject;
                // Debug.Log("Output: " + Checkneighborsfull());
            }

            
            /*
            if(Checkneighborsfull() > -1)
            {
                ShiftToEmpty();
            }
            */
        }
        if(Input.GetKey("o"))
        {
            full[0] = BadObject;
            full[1] = BadObject;
            full[2] = BadObject;
            empty = BadObject;
        }
        
        
            
           
        
    }
    public bool FairMove()
    {
        




        return true;
    }
    public void CallMove()
    {
        if (full[1] == BadObject)
        {
            Debug.Log("Making 1 moves");
            retval = Checkneighbors1();
            // Debug.Log("Checking 1 neigbor");
            ShiftToEmpty(retval);
            //fms.ChangeTurn();
            if (retval != -1)
            {
               // fms.ChangeTurn();
            }
            
            full[0] = BadObject;
            full[1] = BadObject;
            full[2] = BadObject;
            empty = BadObject;

            // Debug.Log("Output: " + Checkneighbors1());
        }
        else if (full[2] == BadObject)
        {
            Debug.Log("Making 2 moves");
            retval = Checkneighbors2();
            Debug.Log(retval);
            //Debug.Log("Checking 2 neigbor");
            ShiftToEmpty(retval);
            //fms.ChangeTurn();
            if (retval != -1)
            {
               // fms.ChangeTurn();
            }
            full[0] = BadObject;
            full[1] = BadObject;
            full[2] = BadObject;
            empty = BadObject;
            //Debug.Log("Output: " + Checkneighbors2());

        }
        else
        {
            Debug.Log("Making 3 moves");
            retval = Checkneighborsfull();
            // Debug.Log("Checking 3 neigbor");
            //Debug.Log("Value to Chcek: " +  retval);
            ShiftToEmpty(retval);
            // fms.ChangeTurn();
            if (retval != -1)
            {
               // fms.ChangeTurn();
            }
            full[0] = BadObject;
            full[1] = BadObject;
            full[2] = BadObject;
            empty = BadObject;
            // Debug.Log("Output: " + Checkneighborsfull());
        }

    }
    /*
    public int Broadside(int opt)
    {
       
        if(opt == 2) 
        {
            for (int i = 0; i < 6; i++)
            {
                if (full[0].transform.GetComponent<select>().neighbors[i] == empty)
                {
                    if (full[1].transform.GetComponent<select>().neighbors[i].GetComponent<select>().blank == true)
                    {
                        return 300;
                    }
                }
            }
            return -1;
        }
        if(opt == 3)
        {
            for (int i = 0; i < 6; i++)
            {
                if (full[0].transform.GetComponent<select>().neighbors[i] == empty)
                {
                    if (full[1].transform.GetComponent<select>().neighbors[i].GetComponent<select>().blank == true)
                    {
                        if(full[2].transform.GetComponent<select>().neighbors[i].GetComponent<select>().blank == true)
                        {
                            return 301;
                        }
                    }
                }
            }
            return -1;
        }
        return -1;
    }
    */
    public int Checkneighbors1()
    {
        // Only one selected with an empty selected
        if (full[1] == BadObject)
        {
            for (int i = 0; i < 6; i++)
            {
                if (full[0].transform.GetComponent<select>().neighbors[i] == empty)
                {
                    if(empty.transform.GetComponent<select>().Enemyball == true || empty.transform.GetComponent<select>().Myball == true )
                    {
                        return -1;
                    }
                    return 0;
                }

            }
            return -1;

        }
        return -1;
    }
    public int Checkneighbors2()
    {
        //Only two are selected
        //keep working on
        int m = 100;
        int z = 100;
        for (int i = 0; i < 6; i++)
        {
            if (full[0].transform.GetComponent<select>().neighbors[i] == full[1])
            {
                m = i;
            }
            if (full[0].transform.GetComponent<select>().neighbors[i] == empty)
            {
                z = i;
            }
        }
       // Debug.Log("M : " + m);
        //Debug.Log("Z : " + z);

        if (m != 100 && z != 100)
        {
          //  Debug.Log("In Wrong Zone");
            if (m == 0 && z == 3)
            {
                //checks to make sure that the enemy balls do not out number or equal same as trying to move
                if(empty.GetComponent<select>().Enemyball == true)
                {
                    r2 = CheckEnemyCount2(3, 90);

                    //return r2;
                    if (r2 != 1)
                    {
                        return -2;
                    }
                    else
                    {
                        return 37;
                    }

                }
                else
                {
                    return 37;
                }
                
                

            }
            else if (m == 3 && z == 0)
            {
                if (empty.GetComponent<select>().Enemyball == true)
                {
                    r2 = CheckEnemyCount2(0, 91);

                    if (r2 != 1)
                    {
                        return -3;
                    }
                    else
                    {
                        return 38;
                    }

                }
                else
                {
                    return 38;
                }
                

            }
            else if (m == 1 && z == 4)
            {
               
                if (empty.GetComponent<select>().Enemyball == true)
                {
                    r2 = CheckEnemyCount2(4, 92);

                    if (r2 != 1)
                    {
                        return -1;
                    }
                    else
                    {
                        return 39;
                    }

                }
                else
                {
                    return 39;
                }

            }
            else if (m == 4 && z == 1)
            {
                if (empty.GetComponent<select>().Enemyball == true)
                {
                    r2 = CheckEnemyCount2(1, 93);
                    if (r2 != 1)
                    {
                        return -1;
                    }
                    else
                    {
                        return 40;
                    }

                }
                else
                {
                    return 40;
                }
            }
            else if (m == 2 && z == 5)
            {
                if (empty.GetComponent<select>().Enemyball == true)
                {
                    r2 = CheckEnemyCount2(5, 94);
                    if (r2 != 1)
                    {
                        return -1;
                    }
                    else
                    {
                        return 41;
                    }

                }
                else
                {
                    return 41;
                }
            }
            else if (m == 5 && z == 2)
            {
                if (empty.GetComponent<select>().Enemyball == true)
                {
                    r2 = CheckEnemyCount2(2, 95);
                    if (r2 != 1)
                    {
                        return -1;
                    }
                    else
                    {
                        return 42;
                    }

                }
                else
                {
                    return 42;
                }
            }
        }
       
        if(m != 100 && z == 100)
        {
            if (m == 0)
            {
                if (full[1].transform.GetComponent<select>().neighbors[0] == empty)
                {
                    if (empty.GetComponent<select>().Enemyball == true)
                    {
                        r2 = CheckEnemyCount2(0, 100);
                        if (r2 != 1)
                        {
                            return -4;
                        }
                        else
                        {
                            return 43;
                        }

                    }
                    else
                    {
                        return 43;
                    }

                }
                else
                {
                    return -5;
                }

            }
            else if (m == 3)
            {
                if (full[1].transform.GetComponent<select>().neighbors[3] == empty)
                {
                    if (empty.GetComponent<select>().Enemyball == true)
                    {
                        r2 = CheckEnemyCount2(3, 101);
                        if (r2 != 1)
                        {
                            return -6;
                        }
                        else
                        {
                            return 44;
                        }

                    }
                    else
                    {
                        return 44;
                    }
                }
                else
                {
                    return -7;
                }

            }
            else if (m == 1)
            {

                if (full[1].transform.GetComponent<select>().neighbors[1] == empty)
                {
                    if (empty.GetComponent<select>().Enemyball == true)
                    {
                        r2 = CheckEnemyCount2(1, 102);
                        if (r2 != 1)
                        {
                            return -1;
                        }
                        else
                        {
                            return 45;
                        }

                    }
                    else
                    {
                        return 45;
                    }
                }
                else
                {
                    return -1;
                }
            }
            else if (m == 4)
            {
                if (full[1].transform.GetComponent<select>().neighbors[4] == empty)
                {
                    if (empty.GetComponent<select>().Enemyball == true)
                    {
                        r2 = CheckEnemyCount2(4, 103);
                        if(r2 != 1)
                        {
                            return -1;
                        }
                        else
                        {
                            return 46;
                        }
                       

                    }
                    else
                    {
                        return 46;
                    }
                }
                else
                {
                    return -1;
                }

            }
            else if (m == 2)
            {
                if (full[1].transform.GetComponent<select>().neighbors[2] == empty)
                {
                    if (empty.GetComponent<select>().Enemyball == true)
                    {
                        r2 = CheckEnemyCount2(2, 104);
                        if (r2 != 1)
                        {
                            return -1;
                        }
                        else
                        {
                            return 47;
                        }

                    }
                    else
                    {
                        return 47;
                    }
                }
                else
                {
                    return -1;
                }

            }
            else if (m == 5)
            {
                if (full[1].transform.GetComponent<select>().neighbors[5] == empty)
                {
                    if (empty.GetComponent<select>().Enemyball == true)
                    {
                        r2 = CheckEnemyCount2(5, 105);
                        if (r2 != 1)
                        {
                            return -1;
                        }
                        else
                        {
                            return 48;
                        }

                    }
                    else
                    {
                        return 48;
                    }
                }
                else
                {
                    return -1;
                }

            }
        }

      //  Debug.Log("In Wrong Zone");



        return -1;
    }
    public int Checkneighborsfull()
    {
        //Good up to here for checking chain of empties
        //All three are selected
        int n = 100;
        int k = 100;
        for (int i = 0; i < 6; i++)
        {
            if (full[0].transform.GetComponent<select>().neighbors[i] == full[1])
            {
                n = i;
            }
            if (full[0].transform.GetComponent<select>().neighbors[i] == full[2])
            {
                k = i;
            }

        }
       // Debug.Log(n);
       // Debug.Log(k);
        //2 hits
        if (n != 100 && k != 100)
        {
            
            if (n == 0 && k == 3)
            {
                if (full[1].transform.GetComponent<select>().neighbors[0] == empty)
                {

                    if (empty.GetComponent<select>().Enemyball == true)
                    {
                        r2 = CheckEnemyCount(0, 106, 107);

                        if (r2 != 1)
                        {
                            return -1;
                        }
                        else
                        {
                            return 1;
                        }

                    }
                    else
                    {
                        return 1;
                    }

                    
                }
                else if (full[2].transform.GetComponent<select>().neighbors[3] == empty)
                {

                    if (empty.GetComponent<select>().Enemyball == true)
                    {
                        r2 = CheckEnemyCount(3, 108, 109);

                        if (r2 != 1)
                        {
                            return -1;
                        }
                        else
                        {
                            return 2;
                        }

                    }
                    else
                    {
                        return 2;
                    }


                   
                }
                else
                {
                    return -1;
                }

            }
            else if (n == 3 && k == 0)
            {
                if (full[1].transform.GetComponent<select>().neighbors[3] == empty)
                {

                    if (empty.GetComponent<select>().Enemyball == true)
                    {
                        r2 = CheckEnemyCount(3, 110, 111);

                        if (r2 != 1)
                        {
                            return -1;
                        }
                        else
                        {
                            return 3;
                        }

                    }
                    else
                    {
                        return 3;
                    }
                   
                }
                else if (full[2].transform.GetComponent<select>().neighbors[0] == empty)
                {
                    if (empty.GetComponent<select>().Enemyball == true)
                    {
                        r2 = CheckEnemyCount(0, 112, 113);

                        if (r2 != 1)
                        {
                            return -1;
                        }
                        else
                        {
                            return 4;
                        }

                    }
                    else
                    {
                        return 4;
                    }
                    
                }
                else
                {
                    return -1;
                }
                
            }
            else if(n == 1 && k == 4)
            {
                if (full[1].transform.GetComponent<select>().neighbors[1] == empty)
                {
                    if (empty.GetComponent<select>().Enemyball == true)
                    {
                        r2 = CheckEnemyCount(1, 106, 107);

                        if (r2 != 1)
                        {
                            return -1;
                        }
                        else
                        {
                            return 5;
                        }

                    }
                    else
                    {
                        return 5;
                    }
                   
                }
                else if (full[2].transform.GetComponent<select>().neighbors[4] == empty)
                {
                    if (empty.GetComponent<select>().Enemyball == true)
                    {
                       CheckEnemyCount(4, 106, 107);

                        if (r2 != 1)
                        {
                            return -1;
                        }
                        else
                        {
                            return 6;
                        }

                    }
                    else
                    {
                        return 6;
                    }
                    
                }
                else
                {
                    return -1;
                }
               
            }
            else if (n == 4 && k == 1)
            {
                if (full[1].transform.GetComponent<select>().neighbors[4] == empty)
                {
                    if (empty.GetComponent<select>().Enemyball == true)
                    {
                       CheckEnemyCount(4, 106, 107);

                        if (r2 != 1)
                        {
                            return -1;
                        }
                        else
                        {
                            return 7;
                        }

                    }
                    else
                    {
                        return 7;
                    }
                   
                }
                else if (full[2].transform.GetComponent<select>().neighbors[1] == empty)
                {
                    if (empty.GetComponent<select>().Enemyball == true)
                    {
                        r2 = CheckEnemyCount(1, 106, 107);

                        if (r2 != 1)
                        {
                            return -1;
                        }
                        else
                        {
                            return 8;
                        }

                    }
                    else
                    {
                        return 8;
                    }
                    
                }
                else
                {
                    return -1;
                }
            }
            else if (n == 2 && k == 5)
            {
                if (full[1].transform.GetComponent<select>().neighbors[2] == empty)
                {
                    if (empty.GetComponent<select>().Enemyball == true)
                    {
                        r2 = CheckEnemyCount(2, 106, 107);

                        if (r2 != 1)
                        {
                            return -1;
                        }
                        else
                        {
                            return 9;
                        }

                    }
                    else
                    {
                        return 9;
                    }
                    
                }
                else if (full[2].transform.GetComponent<select>().neighbors[5] == empty)
                {
                    if (empty.GetComponent<select>().Enemyball == true)
                    {
                        r2 = CheckEnemyCount(5, 106, 107);

                        if (r2 != 1)
                        {
                            return -1;
                        }
                        else
                        {
                            return 10;
                        }

                    }
                    else
                    {
                        return 10;
                    }
                   
                }
                else
                {
                    return -1;
                }
            }
            else if (n == 5 && k == 2)
            {
                if (full[1].transform.GetComponent<select>().neighbors[5] == empty)
                {
                    if (empty.GetComponent<select>().Enemyball == true)
                    {
                        r2 = CheckEnemyCount(5, 106, 107);

                        if (r2 != 1)
                        {
                            return -1;
                        }
                        else
                        {
                            return 11;
                        }

                    }
                    else
                    {
                        return 11;
                    }
                   
                }
                else if (full[2].transform.GetComponent<select>().neighbors[2] == empty)
                {
                    if (empty.GetComponent<select>().Enemyball == true)
                    {
                        r2 = CheckEnemyCount(2, 106, 107);

                        if (r2 != 1)
                        {
                            return -1;
                        }
                        else
                        {
                            return 12;
                        }

                    }
                    else
                    {
                        return 12;
                    }
                   
                }
                else
                {
                    return -1;
                }
            }
        }
        //k hit
        if( k != 100 && n == 100)
        {
            if (k == 0)
            {
                if (full[2].transform.GetComponent<select>().neighbors[0] == full[1])
                {
                    if (full[1].transform.GetComponent<select>().neighbors[0] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(0, 106, 107);


                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 13;
                            }



                        }
                        else
                        {
                            return 13;
                        }
                        
                    }
                    else if (full[0].transform.GetComponent<select>().neighbors[3] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(3, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 14;
                            }

                        }
                        else
                        {
                            return 14;
                        }
                        
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            if (k == 1)
            {
                if (full[1].transform.GetComponent<select>().neighbors[1] == full[1])
                {
                    if (full[2].transform.GetComponent<select>().neighbors[1] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(1, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 15;
                            }

                        }
                        else
                        {
                            return 15;
                        }
                       
                    }
                    else if (full[0].transform.GetComponent<select>().neighbors[4] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(4, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 16;
                            }

                        }
                        else
                        {
                            return 16;
                        }
                        
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            if (k == 2)
            {
                if (full[2].transform.GetComponent<select>().neighbors[2] == full[1])
                {
                    if (full[1].transform.GetComponent<select>().neighbors[2] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(2, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 17;
                            }

                        }
                        else
                        {
                            return 17;
                        }
                       
                    }
                    else if (full[0].transform.GetComponent<select>().neighbors[5] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(5, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 18;
                            }

                        }
                        else
                        {
                            return 18;
                        }
                        
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            if (k == 3)
            {
                if (full[2].transform.GetComponent<select>().neighbors[3] == full[1])
                {
                    if (full[1].transform.GetComponent<select>().neighbors[3] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(3, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 19;
                            }

                        }
                        else
                        {
                            return 19;
                        }
                      
                    }
                    else if (full[0].transform.GetComponent<select>().neighbors[0] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(0, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 20;
                            }

                        }
                        else
                        {
                            return 20;
                        }
                      
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            if (k == 4)
            {
                if (full[2].transform.GetComponent<select>().neighbors[4] == full[1])
                {
                    if (full[1].transform.GetComponent<select>().neighbors[4] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(4, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 21;
                            }

                        }
                        else
                        {
                            return 21;
                        }
                        
                    }
                    else if (full[0].transform.GetComponent<select>().neighbors[1] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(1, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 22;
                            }

                        }
                        else
                        {
                            return 22;
                        }
                       
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            if (k == 5)
            {
                if (full[2].transform.GetComponent<select>().neighbors[5] == full[1])
                {
                    if (full[1].transform.GetComponent<select>().neighbors[5] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(5, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 23;
                            }

                        }
                        else
                        {
                            return 23;
                        }
                       
                    }
                    else if (full[0].transform.GetComponent<select>().neighbors[2] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(2, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 24;
                            }

                        }
                        else
                        {
                            return 24;
                        }
                        
                    }
                    else
                    {
                        return -1;
                    }
                }
            }

        }
        //n hit
        if( n  != 100 && k == 100)
        {
            if (n == 0)
            {
               if( full[1].transform.GetComponent<select>().neighbors[0] == full[2])
                {
                    if (full[2].transform.GetComponent<select>().neighbors[0] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(0, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 25;
                            }

                        }
                        else
                        {
                            return 25;
                        }
                       
                    }
                    else if (full[0].transform.GetComponent<select>().neighbors[3] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(3, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 26;
                            }

                        }
                        else
                        {
                            return 26;
                        }
                       
                    }
                    else
                    {
                        return -1;
                    }
                }

            }
            if (n == 1)
            {
                if (full[1].transform.GetComponent<select>().neighbors[1] == full[2])
                {
                    if (full[2].transform.GetComponent<select>().neighbors[1] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(1, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 27;
                            }

                        }
                        else
                        {
                            return 27;
                        }
                       
                    }
                    else if (full[0].transform.GetComponent<select>().neighbors[4] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(4, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 28;
                            }

                        }
                        else
                        {
                            return 28;
                        }
                       
                    }
                    else
                    {
                        return -1;
                    }
                }

            }
            if (n == 2)
            {
                if (full[1].transform.GetComponent<select>().neighbors[2] == full[2])
                {
                    if (full[2].transform.GetComponent<select>().neighbors[2] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(2, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 29;
                            }

                        }
                        else
                        {
                            return 29;
                        }
                       
                    }
                    else if (full[0].transform.GetComponent<select>().neighbors[5] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(5, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 30;
                            }

                        }
                        else
                        {
                            return 30;
                        }
                        
                    }
                    else
                    {
                        return -1;
                    }
                }

            }
            if (n== 3)
            {
                if (full[1].transform.GetComponent<select>().neighbors[3] == full[2])
                {
                    if (full[2].transform.GetComponent<select>().neighbors[3] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(3, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 31;
                            }

                        }
                        else
                        {
                            return 31;
                        }
                       
                    }
                    else if (full[0].transform.GetComponent<select>().neighbors[0] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(0, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 32;
                            }

                        }
                        else
                        {
                            return 32;
                        }
                        
                    }
                    else
                    {
                        return -1;
                    }
                }

            }
            if (n == 4)
            {
                if (full[1].transform.GetComponent<select>().neighbors[4] == full[2])
                {
                    if (full[2].transform.GetComponent<select>().neighbors[4] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(4, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 33;
                            }

                        }
                        else
                        {
                            return 33;
                        }
                        
                    }
                    else if (full[0].transform.GetComponent<select>().neighbors[1] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(1, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 34;
                            }

                        }
                        else
                        {
                            return 34;
                        }
                       
                    }
                    else
                    {
                        return -1;
                    }
                }

            }
            if (n == 5)
            {
                if (full[1].transform.GetComponent<select>().neighbors[5] == full[2])
                {
                    if (full[2].transform.GetComponent<select>().neighbors[5] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(5, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 35;
                            }

                        }
                        else
                        {
                            return 35;
                        }
                        
                    }
                    else if (full[0].transform.GetComponent<select>().neighbors[2] == empty)
                    {
                        if (empty.GetComponent<select>().Enemyball == true)
                        {
                            r2 = CheckEnemyCount(2, 106, 107);

                            if (r2 != 1)
                            {
                                return -1;
                            }
                            else
                            {
                                return 36;
                            }

                        }
                        else
                        {
                            return 36;
                        }
                        
                    }
                    else
                    {
                        return -1;
                    }
                }

            }
        }

        return -1;


    }
    public int ShiftToEmpty(int retval)
    {  
        if(retval == 0)
        {
            full[0].transform.GetComponent<select>().OwnBall.transform.position =empty.transform.position;
            empty.transform.GetComponent<select>().OwnBall = full[0].transform.GetComponent<select>().OwnBall;
            full[0].transform.GetComponent<select>().OwnBall = BadObject;

        }
        if(retval == 1)
        {
            Swap201e();
        }
        if(retval == 2)
        {
            Swap102e();
        }
        if( retval == 3)
        {
            Swap201e();
        }
        if(retval == 4)
        {
            Swap102e();
        }
        if(retval == 5)
        {
            Swap201e();
        }
        if(retval == 6)
        {
            Swap102e();
        }
        if(retval == 7)
        {
            Swap201e();
        }
        if(retval == 8)
        {
            Swap102e();
        }
        if(retval == 9)
        {
            Swap201e();
        }
        if(retval == 10)
        {
            Swap102e();
        }
        if(retval == 11)
        {
            Swap201e();
        }
        if(retval == 12)
        {
            Swap102e();
        }
        if(retval == 13)
        {
            Swap021e();
        }
        if(retval == 14)
        {
            Swap120e();
        }
        if(retval == 15)
        {
            Swap021e();
        }
        if (retval == 16)
        {
            Swap120e();
        }
        if(retval == 17)
        {
            Swap021e();
        }
        if(retval == 18)
        {
            Swap120e();
        }
        if(retval == 19)
        {
            Swap021e();
        }
        if(retval == 20)
        {
            Swap120e();
        }
        if(retval == 21)
        {
            Swap021e();
        }
        if(retval == 22)
        {
            Swap120e();
        }
        if(retval == 23)
        {
            Swap021e();
        }
        if(retval == 24)
        {
            Swap120e();
        }
        if(retval == 25)
        {
            Swap012e();
        }
        if(retval == 26)
        {
            Swap210e();
        }
        if(retval == 27)
        {
            Swap012e();
        }
        if(retval == 28)
        {
            Swap210e();
        }
        if (retval == 29)
        {
            Swap012e();
        }
        if(retval == 30)
        {
            Swap210e();
        }
        if(retval == 31)
        {
            Swap012e();
        }
        if(retval == 32)
        {
            Swap210e();
        }
        if(retval == 33)
        {
            Swap012e();
        }
        if( retval == 34)
        {
            Swap210e();
        }
        if (retval == 35)
        {
            Swap012e();
        }
        if (retval == 36)
        {
            Swap210e();
        }
        if (retval == 37)
        {
            Swap10e();
        }
        if (retval == 38)
        {
            Swap10e();
        }
        if (retval == 39)
        {
            Swap10e();
        }
        if (retval == 40)
        {
            Swap10e();
        }
        if (retval == 41)
        {
            Swap10e();
        }
        if (retval == 42)
        {
            Swap10e();
        }
        if (retval == 43)
        {
            Swap01e();
        }
        if (retval == 44)
        {
            Swap01e();
        }
        if (retval == 45)
        {
            Swap01e();
        }
        if (retval == 46)
        {
            Swap01e();
        }
        if (retval == 47)
        {
            Swap01e();
        }
        if (retval == 48)
        {
            Swap01e();
        }
        
        //Need to have a special case for each instance of the above return value.
        //based off htis instance you will know which balls to move where.
        return 0;
    }
    public  void Swap201e()
    {
        full[1].transform.GetComponent<select>().OwnBall.transform.position = empty.transform.position;
        empty.transform.GetComponent<select>().OwnBall = full[1].transform.GetComponent<select>().OwnBall;
        full[1].transform.GetComponent<select>().OwnBall = BadObject;

        full[0].transform.GetComponent<select>().OwnBall.transform.position = full[1].transform.position;
        full[1].transform.GetComponent<select>().OwnBall = full[0].transform.GetComponent<select>().OwnBall;
        full[0].transform.GetComponent<select>().OwnBall = BadObject;

        full[2].transform.GetComponent<select>().OwnBall.transform.position = full[0].transform.position;
        full[0].transform.GetComponent<select>().OwnBall = full[2].transform.GetComponent<select>().OwnBall;
        full[2].transform.GetComponent<select>().OwnBall = BadObject;
    }
    public void Swap102e()
    {
        full[2].transform.GetComponent<select>().OwnBall.transform.position = empty.transform.position;
        empty.transform.GetComponent<select>().OwnBall = full[2].transform.GetComponent<select>().OwnBall;
        full[2].transform.GetComponent<select>().OwnBall = BadObject;

        full[0].transform.GetComponent<select>().OwnBall.transform.position = full[2].transform.position;
        full[2].transform.GetComponent<select>().OwnBall = full[0].transform.GetComponent<select>().OwnBall;
        full[0].transform.GetComponent<select>().OwnBall = BadObject;

        full[1].transform.GetComponent<select>().OwnBall.transform.position = full[0].transform.position;
        full[0].transform.GetComponent<select>().OwnBall = full[1].transform.GetComponent<select>().OwnBall;
        full[1].transform.GetComponent<select>().OwnBall = BadObject;
    }
    public void Swap021e()
    {
        full[1].transform.GetComponent<select>().OwnBall.transform.position = empty.transform.position;
        empty.transform.GetComponent<select>().OwnBall = full[1].transform.GetComponent<select>().OwnBall;
        full[1].transform.GetComponent<select>().OwnBall = BadObject;

        full[2].transform.GetComponent<select>().OwnBall.transform.position = full[1].transform.position;
        full[1].transform.GetComponent<select>().OwnBall = full[2].transform.GetComponent<select>().OwnBall;
        full[2].transform.GetComponent<select>().OwnBall = BadObject;

        full[0].transform.GetComponent<select>().OwnBall.transform.position = full[2].transform.position;
        full[2].transform.GetComponent<select>().OwnBall = full[0].transform.GetComponent<select>().OwnBall;
        full[0].transform.GetComponent<select>().OwnBall = BadObject;
    }
    public void Swap120e()
    {
        full[0].transform.GetComponent<select>().OwnBall.transform.position = empty.transform.position;
        empty.transform.GetComponent<select>().OwnBall = full[0].transform.GetComponent<select>().OwnBall;
        full[0].transform.GetComponent<select>().OwnBall = BadObject;

        full[2].transform.GetComponent<select>().OwnBall.transform.position = full[0].transform.position;
        full[0].transform.GetComponent<select>().OwnBall = full[2].transform.GetComponent<select>().OwnBall;
        full[2].transform.GetComponent<select>().OwnBall = BadObject;

        full[1].transform.GetComponent<select>().OwnBall.transform.position = full[2].transform.position;
        full[2].transform.GetComponent<select>().OwnBall = full[1].transform.GetComponent<select>().OwnBall;
        full[1].transform.GetComponent<select>().OwnBall = BadObject;
    }
    public void Swap012e()
    {
        full[2].transform.GetComponent<select>().OwnBall.transform.position = empty.transform.position;
        empty.transform.GetComponent<select>().OwnBall = full[2].transform.GetComponent<select>().OwnBall;
        full[2].transform.GetComponent<select>().OwnBall = BadObject;

        full[1].transform.GetComponent<select>().OwnBall.transform.position = full[2].transform.position;
        full[2].transform.GetComponent<select>().OwnBall = full[1].transform.GetComponent<select>().OwnBall;
        full[1].transform.GetComponent<select>().OwnBall = BadObject;

        full[0].transform.GetComponent<select>().OwnBall.transform.position = full[1].transform.position;
        full[1].transform.GetComponent<select>().OwnBall = full[0].transform.GetComponent<select>().OwnBall;
        full[0].transform.GetComponent<select>().OwnBall = BadObject;
    }
    public void Swap210e()
    {
        full[0].transform.GetComponent<select>().OwnBall.transform.position = empty.transform.position;
        empty.transform.GetComponent<select>().OwnBall = full[0].transform.GetComponent<select>().OwnBall;
        full[0].transform.GetComponent<select>().OwnBall = BadObject;

        full[1].transform.GetComponent<select>().OwnBall.transform.position = full[0].transform.position;
        full[0].transform.GetComponent<select>().OwnBall = full[1].transform.GetComponent<select>().OwnBall;
        full[1].transform.GetComponent<select>().OwnBall = BadObject;

        full[2].transform.GetComponent<select>().OwnBall.transform.position = full[1].transform.position;
        full[1].transform.GetComponent<select>().OwnBall = full[2].transform.GetComponent<select>().OwnBall;
        full[2].transform.GetComponent<select>().OwnBall = BadObject;
    }
    public void Swap10e()
    {
        full[0].transform.GetComponent<select>().OwnBall.transform.position = empty.transform.position;
        empty.transform.GetComponent<select>().OwnBall = full[0].transform.GetComponent<select>().OwnBall;
        full[0].transform.GetComponent<select>().OwnBall = BadObject;

        full[1].transform.GetComponent<select>().OwnBall.transform.position = full[0].transform.position;
        full[0].transform.GetComponent<select>().OwnBall = full[1].transform.GetComponent<select>().OwnBall;
        full[1].transform.GetComponent<select>().OwnBall = BadObject;

    }
    public void Swap01e()
    {
        full[1].transform.GetComponent<select>().OwnBall.transform.position = empty.transform.position;
        empty.transform.GetComponent<select>().OwnBall = full[1].transform.GetComponent<select>().OwnBall;
        full[1].transform.GetComponent<select>().OwnBall = BadObject;

        full[0].transform.GetComponent<select>().OwnBall.transform.position = full[1].transform.position;
        full[1].transform.GetComponent<select>().OwnBall = full[0].transform.GetComponent<select>().OwnBall;
        full[0].transform.GetComponent<select>().OwnBall = BadObject;

    }
    
    
    public int CheckEnemyCount(int index, int rval, int rval1)
    {

        Debug.Log("Checking Enemy Count");
        //while i am checking for enemy im not checking that its empty?
        //Need to check to see if it is a ownball too

        // Empty  1  2
        //if 1 is myball
        if(empty.transform.GetComponent<select>().neighbors[index].transform.GetComponent<select>().Myball == true)
        {
            return -1;
        }
        else if(empty.transform.GetComponent<select>().neighbors[index].transform.GetComponent<select>().blank == true)
        {
            ShiftEnemyBalls1(index);
            Debug.Log("Blank Move");
            return 1;
        }
        else if (empty.transform.GetComponent<select>().neighbors[index].transform.GetComponent<select>().kill == true)
        {
            ShiftEnemyBalls1(index);
            Debug.Log("Kill Move");
            return 1;
        }
        else if(empty.transform.GetComponent<select>().neighbors[index].transform.GetComponent<select>().Enemyball == true)
        {
            if(empty.transform.GetComponent<select>().neighbors[index].transform.GetComponent<select>().neighbors[index].transform.GetComponent<select>().Myball == true)
            {
                return -14;
            }
            else if(empty.transform.GetComponent<select>().neighbors[index].transform.GetComponent<select>().neighbors[index].transform.GetComponent<select>().blank == true)
            {
                ShiftEnemyBalls2(index);
                Debug.Log("Enemy Move So double move");
                return 1;
            }
            else if (empty.transform.GetComponent<select>().neighbors[index].transform.GetComponent<select>().neighbors[index].transform.GetComponent<select>().kill == true)
            {
                ShiftEnemyBalls2(index);
                Debug.Log("Enemy Move So double move");
                return 1;
            }
            else if(empty.transform.GetComponent<select>().neighbors[index].transform.GetComponent<select>().neighbors[index].transform.GetComponent<select>().Enemyball == true)
            {
                return -13;
            }
        }
        
        return -12;
    
    }

    //I belive this is good for 2
    public int CheckEnemyCount2(int index, int rval)
    {
        Debug.Log("Checking Enemy Count2");
        if (empty.transform.GetComponent<select>().neighbors[index].transform.GetComponent<select>().Myball == true)
        {
            return -1;
        }
        else if (empty.transform.GetComponent<select>().neighbors[index].transform.GetComponent<select>().blank == true)
        {
            
            ShiftEnemyBalls1(index);
            return 1;
        }
        else if (empty.transform.GetComponent<select>().neighbors[index].transform.GetComponent<select>().kill == true)
        {
           
            ShiftEnemyBalls1(index);
            return 1;
        }
        else if (empty.transform.GetComponent<select>().neighbors[index].transform.GetComponent<select>().Enemyball == true)
        {
            return -1;
        }

        return -10;

    }
    public void ShiftEnemyBalls1(int index)
    {

        // Debug.Log(empty.GetComponent<select>().neighbors[index].gameObject.transform.position);

        Debug.Log("ShiftEnemyBalls1");

     
        if (empty.transform.GetComponent<select>().OwnBall != BadObject)
        {
            placeholder1 = empty.GetComponent<select>().neighbors[index];
            empty.transform.GetComponent<select>().OwnBall.transform.position = placeholder1.transform.position;
            empty.transform.GetComponent<select>().neighbors[index].GetComponent<select>().OwnBall = empty.transform.GetComponent<select>().OwnBall;
            empty.transform.GetComponent<select>().OwnBall = BadObject;
            shiftstat = 1;
        }
        else
        {
            shiftstat = 0;
        }
       

    }
    public void ShiftEnemyBalls2(int index)
    {
        Debug.Log("ShiftEnemyBalls2");
        empty.transform.GetComponent<select>().neighbors[index].GetComponent<select>().OwnBall.transform.position = empty.transform.GetComponent<select>().neighbors[index].transform.GetComponent<select>().neighbors[index].transform.position;
        empty.transform.GetComponent<select>().neighbors[index].transform.GetComponent<select>().neighbors[index].GetComponent<select>().OwnBall = empty.transform.GetComponent<select>().neighbors[index].GetComponent<select>().OwnBall;
        empty.transform.GetComponent<select>().neighbors[index].GetComponent<select>().OwnBall = BadObject;

        if(empty.transform.GetComponent<select>().OwnBall != BadObject) 
        {
            empty.transform.GetComponent<select>().OwnBall.transform.position = empty.transform.GetComponent<select>().neighbors[index].transform.position;
            empty.transform.GetComponent<select>().neighbors[index].GetComponent<select>().OwnBall = empty.transform.GetComponent<select>().OwnBall;
            empty.transform.GetComponent<select>().OwnBall = BadObject;
        }
        
    }

}
