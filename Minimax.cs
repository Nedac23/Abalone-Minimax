using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class Minimax : MonoBehaviour
{
    public FindMoves FMS;
    public GameObject FM;
    public List<string> lvl0;
    public List<string> lvl1;
    public List<string> lvl2;
    public List<string> lvl3;
    private GameObject[] boardstate3 = new GameObject[61];
    private GameObject[] boardstate2 = new GameObject[61];
    private GameObject[] boardstate1 = new GameObject[61];
    private GameObject[] boardstate0 = new GameObject[61];
    public float[] evaluations = new float[61];



    // Start is called before the first frame update
    void Start()
    {
        FM = GameObject.Find("FindMoves");
       FMS = FM.GetComponent<FindMoves>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {

            // SearchforEmptySpacesE();
           // FMS.SaveBoardState(boardstate3);
           // FMS.ClearFreeMoves();
           // FMS.SearchMyBalls();
            //FMS.ValidateMoves(lvl3);
           // MM(lvl3,3,-1,-1,true );


        }

    }
    public float MM(List<string> moves, int depth, float alpha, float beta, bool maximizingPlayer)
    {
        // When you change to coruitne you need to reaseach how to end it and avoid any memory leaks. 
       // Make sure you have some way to manualy end the coruitne and some way for it to shut down after some task is complete.Finally add dubgu lines to the code ot monitor the status of the coroutine.

        if (depth == 0)// or game over
        {
            float maxEval = -Mathf.Infinity;
            foreach (string m in lvl0)
            {
                maxEval = Mathf.Min(maxEval, staticeval());
            }
            return maxEval;
                // return static evaluation of position
        }


        if (maximizingPlayer)
        {
           // int maxEval = -1;
            if (depth == 3)
            {

                float maxEval = -Mathf.Infinity;
                int i = 0;
                foreach (string m in lvl3)
                {

                    FMS.MakeMove(m);
                    FMS.ClearFreeMoves();
                    FMS.SearchMyBalls();
                    FMS.ValidateMoves(lvl2);

                    float eval = MM(lvl2, depth - 1, alpha, beta, false);
                    evaluations[i] = eval;
                    maxEval = Mathf.Min(maxEval, eval);
                    alpha = Mathf.Min(alpha, eval);
                    if (beta <= alpha)
                    {
                        break;
                    }
                    i++;
                }
                //
                return maxEval;
            }
            if (depth == 2)
            {
                FMS.SaveBoardState(boardstate2);
                FMS.ChangeTurn();
                float maxEval = -Mathf.Infinity;
                foreach (string m in lvl2)
                {
                   
                    FMS.MakeMove(m);
                    FMS.ClearFreeMoves();
                    FMS.SearchMyBalls();
                    FMS.ValidateMoves(lvl1);

                    float eval = MM(lvl1, depth - 1, alpha, beta, false);
                    maxEval = Mathf.Min(maxEval, eval);
                    alpha = Mathf.Min(alpha, eval);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return maxEval;
            }
            if (depth == 1)
            {
                FMS.SaveBoardState(boardstate1);
                FMS.ChangeTurn();
                float maxEval = -Mathf.Infinity;
                foreach (string m in lvl1)
                {
                  
                    FMS.MakeMove(m);
                    FMS.ClearFreeMoves();
                    FMS.SearchMyBalls();
                    FMS.ValidateMoves(lvl0);

                    float eval = MM(lvl0, depth - 1, alpha, beta, false);
                    maxEval = Mathf.Min(maxEval, eval);
                    alpha = Mathf.Min(alpha, eval);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return maxEval;
            }
            //for each child of position

            //int eval = MM(move, depth - 1, alpha, beta, false);

            //maxEval = max(maxEval, eval)

            //alpha = max(alpha, eval)

            //if beta <= alpha

            //  break

            return -1.0f;
        }

        else
        {
            // minEval = +infinity
            if(depth == 3)
            {

                float minEval = Mathf.Infinity;
                foreach (string m in lvl3)
                {
                    FMS.MakeMove(m);
                    FMS.ClearFreeMoves();
                    FMS.SearchMyBalls();
                    FMS.ValidateMoves(lvl2);
                    
                    float eval = MM(lvl2, depth - 1, alpha, beta, true);
                    minEval = Mathf.Min(minEval, eval);
                    beta = Mathf.Min(beta, eval);
                    if(beta <= alpha)
                    {
                        break;
                    }
                }
                return minEval;
            }
            if (depth == 2)
            {
                FMS.SaveBoardState(boardstate2);
                float minEval = Mathf.Infinity;
                foreach (string m in lvl2)
                {
                    FMS.MakeMove(m);
                    FMS.ClearFreeMoves();
                    FMS.SearchMyBalls();
                    FMS.ValidateMoves(lvl1);
                    
                    float eval = MM(lvl1, depth - 1, alpha, beta, true);
                    minEval = Mathf.Min(minEval, eval);
                    beta = Mathf.Min(beta, eval);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return minEval;
            }
            if (depth == 1)
            {
                FMS.SaveBoardState(boardstate1);
                float minEval = Mathf.Infinity;
                foreach (string m in lvl1)
                {
                    FMS.MakeMove(m);
                    FMS.ClearFreeMoves();
                    FMS.SearchMyBalls();
                    FMS.ValidateMoves(lvl0);
                   
                    float eval = MM(lvl0, depth - 1, alpha, beta, true);
                    minEval = Mathf.Min(minEval, eval);
                    beta = Mathf.Min(beta, eval);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return minEval;
            }
            return -1.0f;
        }
    }

        // initial call

  
    public int staticeval()
    {
       return FMS.enemycount;
    }
}
