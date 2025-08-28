using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using System.Linq;
using System;

public class ExampleCo : MonoBehaviour
{
    private Coroutine myCoroutine;
    private bool isApplicationQuitting = false;
    public FindMoves FMS;
    public GameObject FM;
    public GameData GDS;
    public GameObject GameData;
    public List<string> lvl0;
    public List<string> lvl1;
    public List<string> lvl2;
    public List<string> lvl3;
    public List<float> Evals;
    private GameObject[] boardstate3 = new GameObject[61];
    private GameObject[] boardstate2 = new GameObject[61];
    private GameObject[] boardstate1 = new GameObject[61];
    private GameObject[] boardstate0 = new GameObject[61];
    public string bestmove;
    public float alpha0 = -1000;
    public float beta0= 1000;
    public float alpha1 = -1000;
    public float beta1 = 1000;
    public float alpha2 = -1000;
    public float beta2 = 1000;
    public float alpha3 = -1000;
    public float beta3 = 1000;
    public float Max0Val;
    public int pushval;
    public int p;
    public int EVAL;
    public int EVAL2;
    public int killcheck;
    public int killboost;

    void Start()
    {
       
        FM = GameObject.Find("FindMoves");
        FMS = FM.GetComponent<FindMoves>();

        GameData = GameObject.Find("GameData");
        GDS = GameData.GetComponent<GameData>();
        alpha0 = -Mathf.Infinity;
 
        beta0 = Mathf.Infinity;
        alpha1 = -Mathf.Infinity;

        beta1 = Mathf.Infinity;
        alpha2 = -Mathf.Infinity;

        beta2 = Mathf.Infinity;
        alpha3 = -Mathf.Infinity;

        beta3 = Mathf.Infinity;
    }


    private IEnumerator MyCoroutine()
    {




        FMS.StaticEvaluation();
        killcheck = FMS.enemycount;
            Debug.Log("Coroutine is running...");


            Debug.Log("SaveBoardState(boardstate0)");
           // FMS.SaveBoardState(boardstate1);
            Debug.Log("ClearFreeMoves()");
            FMS.ClearFreeMoves();
            Debug.Log("SearchMyBalls();");
            FMS.SearchMyBalls();
            Debug.Log("ValidateMoves(lvl0)");
            FMS.ValidateMoves(lvl2);

            Debug.Log("about to call minimax");
        //MM(lvl0, 0, -1, -1, true);
        //Evals.Clear();
        //lvl0.Clear();
        //FMS.MakeMove(bestmove);
        //FMS.ChangeTurn();

        //MM(1, false);
            MM(2, true);
           // Evals.Clear();
            lvl2.Clear();
            lvl0.Clear();
            lvl1.Clear();
            //GDS.bmove = bestmove;
            FMS.MakeMove(bestmove);
            FMS.ChangeTurn();
            FMS.UpdateBoardVals();
            //pushval = FMS.pushed;
            //FMS.ChangeTurn();


        yield return new WaitForSeconds(1); // Wait for 1 second
        
        Debug.Log("Coroutine stopped safely.");
        /*
        while (!isApplicationQuitting)
        {


            Debug.Log("Coroutine is running...");
           



            yield return new WaitForSeconds(1); // Wait for 1 second
        }
        */
        Debug.Log("Coroutine stopped safely.");
    }
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            myCoroutine = StartCoroutine(MyCoroutine());
        }
        if(Input.GetKeyDown("b")) { lvl0.Clear(); }
        
        if (Input.GetKeyDown(KeyCode.Space)) // Press Space to stop the coroutine
        {
            Debug.Log("D");
            StopCoroutineSafely();
            
        }
       if(isApplicationQuitting)
        {
            StopCoroutineSafely();
        }

    }
    //void OnDestroy()
    //{
        // Ensure coroutine stops if the GameObject is destroyed
       // Debug.Log("OnDestroyCall in Coroutine");
       // StopCoroutineSafely();
   // }
  
    void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit in Coroutine");
        isApplicationQuitting = true; // Set a flag indicating application is quitting
        StopCoroutineSafely();
    }

    private void StopCoroutineSafely()
    {
        if (myCoroutine != null)
        {
            StopCoroutine(myCoroutine);
            Debug.Log("Coroutine stopped in cleanup method!");
            myCoroutine = null; // Clear the reference
        }
    }

    public float MM(int depth,bool maximizingPlayer)
    {
        // When you change to coruitne you need to reaseach how to end it and avoid any memory leaks. 
        // Make sure you have some way to manualy end the coruitne and some way for it to shut down after some task is complete.Finally add dubgu lines to the code ot monitor the status of the coroutine.
        /*
        if (depth == 0)// or game over
        {
            //ai move
            float maxEval = -1;
            string mov = "";
            foreach (string m in lvl0)
            {
                FMS.MakeMove(m);
                FMS.UpdateBoardVals();
                FMS.StaticEvaluation();

                if (maxEval < staticeval())
                {
                    maxEval = staticeval();
                    mov = m;
                }

                
                Evals.Add(staticeval());
                FMS.RestoreBoardState(boardstate0);

            }
            bestmove = mov;
            return maxEval;
            // return static evaluation of position
        }
        */
        if(depth == -1)
        {

        }
            
       

        if (maximizingPlayer)
        {
            // int maxEval = -1;
            if (depth == 3)
            {
                Debug.Log("Depth3");
                float maxEval = -Mathf.Infinity;
                int i = 0;
                foreach (string m in lvl3)
                {

                    FMS.MakeMove(m);
                    FMS.UpdateBoardVals();
                    FMS.ClearFreeMoves();
                    FMS.SearchMyBalls();
                    FMS.ValidateMoves(lvl2);


                    

                    float eval = MM( depth - 1, false);
                    //evaluations[i] = eval;
                    maxEval = Mathf.Max(maxEval, eval);
                    if(maxEval == eval)
                    {
                        bestmove = m;
                    }
                    alpha3 = Mathf.Max(alpha3, maxEval);
                   
                    FMS.RestoreBoardState(boardstate0);
                    if (beta3 <= alpha3)
                    {
                      //  break;
                    }
                    i++;
                }
                //
                return maxEval;
            }
            if (depth == 2)
            {
                Debug.Log("Depth2");
                //ai move
                alpha2 = -Mathf.Infinity;
                FMS.SaveBoardState(boardstate2);
                //FMS.ChangeTurn();
                float maxEval = -Mathf.Infinity;
                foreach (string m in lvl2)
                {
                    Debug.Log("Depth2For");
                    FMS.MakeMove(m);
                    FMS.UpdateBoardVals();
                    lvl1.Clear();
                    //FMS.StaticEvaluation();

                    ///FMS.MakeMove(m);
                    FMS.ChangeTurn();
                    FMS.ClearFreeMoves();
                    FMS.SearchMyBalls();
                    FMS.ValidateMoves(lvl1);



                    //float eval = MM(lvl1, depth - 1, false);
                    float eval = MM(depth - 1, false);
                    maxEval = Mathf.Max(maxEval, eval);
                    if (maxEval == eval)
                    {
                        bestmove = m;
                    }
                    alpha2 = Mathf.Max(alpha2, maxEval);
                    
                    FMS.RestoreBoardState(boardstate2);
                    FMS.UpdateBoardVals();

                }
                alpha3 = alpha2;
                return maxEval;
            }
            if (depth == 1)
            {
                Debug.Log("Depth1Max");
                //player move
                FMS.SaveBoardState(boardstate1);
                //FMS.ChangeTurn();
                float maxEval = -Mathf.Infinity;
                foreach (string m in lvl1)
                {
                    Debug.Log("Depth1ForMax");
                    FMS.MakeMove(m);
                    FMS.UpdateBoardVals();

                    FMS.ChangeTurn();
                    FMS.UpdateBoardVals();
                    lvl0.Clear();
                    FMS.ClearFreeMoves();
                    FMS.SearchMyBalls();
                    FMS.ValidateMoves(lvl0);

                    float eval = MM(depth - 1, false);// float eval = MM(lvl0, depth - 1, false);
                    maxEval = Mathf.Max(maxEval, eval);
                    if (maxEval == eval)
                    {
                      //  bestmove = m;
                    }
                    alpha1 = Mathf.Max(alpha1, eval);
                   
                    FMS.RestoreBoardState(boardstate1);
                    if (beta1 <= alpha1)
                    {
                        break;
                    }
                }
                alpha2 = Mathf.Max(alpha2, alpha1);
                return maxEval;
            }
            if (depth == 0)// or game over
            {

                Debug.Log("Depth0Max");
                //ai move
                float maxEval = -Mathf.Infinity;
                //string mov = "";
                alpha0 = -Mathf.Infinity;
                FMS.SaveBoardState(boardstate0);
                foreach (string m in lvl0)
                {
                    //pshd = 0;
                    Debug.Log("Depth0ForMax");
                    FMS.MakeMove(m);
                    FMS.UpdateBoardVals();
                    FMS.StaticEvaluation();
                    string[] data = m.Split(',');
                    int pshd = int.Parse(data[4]);
                    if (pshd == 2)
                    {
                        p = 70;
                    }
                    else if (pshd == 1) { p = 60; }
                    else
                    {
                        p = 0;
                    }
                   
                    
                    if (maxEval < staticeval() )//+p
                    {
                        maxEval = staticeval();//+p
                       // bestmove = m;
                    }
                    alpha0 = Mathf.Max(alpha0,maxEval);
                    /*
                    beta = Mathf.Min(beta, minEval);

                    FMS.RestoreBoardState(boardstate1);
                    if (beta <= alpha)
                    {
                       // break;
                    }
                    */
                    if (beta1 <= alpha0)
                    {
                        break;
                    }
                    Evals.Add(staticeval());
                    FMS.RestoreBoardState(boardstate0);
                   // FMS.UpdateBoardVals();

                }
                //bestmove = mov;
                FMS.ChangeTurn();
                FMS.UpdateBoardVals();
                Max0Val = maxEval;
                beta1 = alpha0;
                Debug.Log("alpha0: " + alpha0);
                return maxEval;
                // return static evaluation of position
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
            if (depth == 3)
            {
                Debug.Log("Depth0");
                float minEval = Mathf.Infinity;
                foreach (string m in lvl3)
                {
                    FMS.MakeMove(m);
                    FMS.ClearFreeMoves();
                    FMS.SearchMyBalls();
                    FMS.ValidateMoves(lvl2);

                    float eval = MM(depth - 1, true);
                    minEval = Mathf.Min(minEval, eval);
                    if (minEval == eval)
                    {
                        bestmove = m;
                    }
                    beta3 = Mathf.Min(beta3, minEval);
                    if (beta3 <= alpha3)
                    {
                       break;
                    }
                }
                return minEval;
            }
            if (depth == 2)
            {
                Debug.Log("Depth2");
                //ai move
                FMS.SaveBoardState(boardstate2);
                float minEval = Mathf.Infinity;
                foreach (string m in lvl2)
                {
                    FMS.MakeMove(m);
                    FMS.UpdateBoardVals();
                    lvl1.Clear();
                    FMS.ClearFreeMoves();
                    FMS.SearchMyBalls();
                    FMS.ValidateMoves(lvl1);

                    float eval = MM(depth - 1, true);
                    minEval = Mathf.Min(minEval, eval);
                    if (minEval == eval)
                    {
                        bestmove = m;
                    }
                    beta2 = Mathf.Min(beta2,eval);

                    if (beta2 <= alpha2)
                    {
                       break;
                    }
                }
                return minEval;
            }
            if (depth == 1)
            {
                //beta 1000 init
                //alpha -1000 init
                Debug.Log("Depth1Min");
                //player move
                //FMS.ChangeTurn();
                beta1 = Mathf.Infinity;
                FMS.SaveBoardState(boardstate1);
                float minEval = Mathf.Infinity;
                foreach (string m in lvl1)
                {
                    //beta1 = Mathf.Infinity;
                    Debug.Log("Depth1ForMin");

                    FMS.MakeMove(m);
                    FMS.UpdateBoardVals();
                    lvl0.Clear();
                    FMS.ChangeTurn();
                    FMS.ClearFreeMoves();
                    FMS.SearchMyBalls();
                    FMS.ValidateMoves(lvl0);

                    float eval = MM(depth - 1, true);
                    minEval = Mathf.Min(minEval, eval);
                    if (minEval == eval)
                    {
                        //bestmove = m;
                    }
                    beta1 = Mathf.Min(beta1,minEval);
                   // alpha1 = -Mathf.Infinity;
                    FMS.RestoreBoardState(boardstate1);
                    FMS.UpdateBoardVals();
                    if (beta1 <= alpha2)
                    {
                        break;
                    }
                }
                alpha2 = beta1;
                FMS.ChangeTurn();
                FMS.UpdateBoardVals();
                return minEval;
            }
            if (depth == 0)// or game over
            {
                Debug.Log("Depth0 Min");
                //ai move
                float minEval = Mathf.Infinity;
                //string mov = "";
                FMS.SaveBoardState(boardstate0);
                foreach (string m in lvl0)
                {
                    Debug.Log("Depth0ForMin");
                    FMS.MakeMove(m);
                    FMS.UpdateBoardVals();
                    FMS.StaticEvaluation();
                    string[] data = m.Split(',');
                    
                    int pshd = int.Parse(data[4]);
                    if(pshd == 2)
                    {
                        p = 2;
                        pshd = 200;
                    }
                    else if(pshd == 1) { pshd = 100; p = 1; }
                    if (minEval < staticeval())
                    {

                        minEval = staticeval() + pshd;
                       // mov = m;
                    }
                    /*
                    beta = Mathf.Min(beta, minEval);

                    FMS.RestoreBoardState(boardstate1);
                    if (beta <= alpha)
                    {
                       // break;
                    }
                    */
                    Evals.Add(staticeval());
                    FMS.RestoreBoardState(boardstate0);

                }
                //bestmove = mov;
                FMS.ChangeTurn();
                FMS.UpdateBoardVals();
                beta1 = Mathf.Max(beta1, beta0);
                return minEval;
                // return static evaluation of position
            }
            return -1.0f;
        }
    }

    // initial call


    public int staticeval()
    {
        if(FMS.enemycount < killcheck)
        {
            killboost = 100;
        }
        else
        {
            killboost = 0;
        }
        //player wants to min ai wants to max
        if (FMS.enemycount == 14)
        {
            EVAL = 0;
        }
        if (FMS.enemycount == 13)
        {
            EVAL = 80;
        }
        if (FMS.enemycount == 12)
        {
            EVAL = 90;
        }
        if (FMS.enemycount == 11)
        {
            EVAL = 100;
        }
        if (FMS.enemycount == 10)
        {
            EVAL = 110;
        }
        if (FMS.enemycount == 9)
        {
            EVAL = 120;
        }
        if (FMS.enemycount == 8)
        {
            EVAL = 130;
        }

        if(FMS.friendlycount == 14)
        {
            EVAL2 = 34;
        }
        if (FMS.friendlycount == 13)
        {
            EVAL2 = 32;

        }
        if (FMS.friendlycount == 12)
        {
            EVAL2 = 30;
        }
        if (FMS.friendlycount == 11)
        {
            EVAL2 = 25;
        }
        if (FMS.friendlycount == 10)
        {
            EVAL2 = 20;
        }
        if (FMS.friendlycount == 9)
        {
            EVAL2 = 17;
        }
        if (FMS.friendlycount == 8)
        {
            EVAL2 = 14;
        }


        if (killboost == 100)
        {
            return FMS.centercount  + killboost;
        }
        else
        {
                return (FMS.centercount) + killboost;
        }
       
            
        
            //return 0;

        
         //+ (FMS.edgecount * 4) + (20 * (FMS.friendlycount - FMS.enemycount));
        //return (FMS.edgecount * 2) + (20 * (FMS.friendlycount - FMS.enemycount));
    }

}