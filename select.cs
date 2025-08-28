
using UnityEngine;

public class select : MonoBehaviour
{
    public bool free;
    public bool On;
    public bool selected;
    private Renderer ren;
    public GameObject mouse;
    public float threshold;
    public GameObject OwnBall;
    public MoveManager moveManager;
    public GameObject[] neighbors = new GameObject[6];
    public GameData gameData;
    public GameObject GameData;
    public int BoardPosNum;
    public int Checkspotrtn;
    public bool Enemyball;
    public bool blank;
    public bool Myball;
    public bool kill;
    private string ballname;
    public GameObject BadObject;
    // Start is called before the first frame update
    void Start()
    {
        GameData = GameObject.Find("GameData");
        BadObject = GameObject.Find("BadObject");
        gameData = GameData.GetComponent<GameData>(); 
        moveManager = GameObject.Find("MoveManager").GetComponent<MoveManager>();
        free = true;
        ren = GetComponent<Renderer>();
       
    }
    //Need to add a way to know if it is occupied by an eme
    // Update is called once per frame
    void Update()
    {
       if(gameData.turn == 0)
       {
           // ballname = OwnBall.gameObject.name;
             if (this.gameObject.name[0] == 'K')
            {
                Enemyball = false;
                Myball = false;
                blank = false;
                kill = true;
                gameData.board[BoardPosNum] = 3;
            }
            else if (OwnBall == BadObject)
            {
                blank = true;
                Myball = false;
                Enemyball = false;
                kill = false;
                gameData.board[BoardPosNum] = 0;
            }
            else
            {
                ballname = OwnBall.gameObject.name;
                //Debug.Log(ballname[0]);
                if (ballname[0] == 'B')
                {
                    kill = false;
                    Myball = true;
                    blank = false;
                    Enemyball = false;
                    gameData.board[BoardPosNum] = 1;
                }
               
                else
                {
                    kill = false;
                    Enemyball = true;
                    Myball = false;
                    blank = false;
                    gameData.board[BoardPosNum] = 2;
                }
            }
       }
       else if(gameData.turn == 1) 
       {
            if (this.gameObject.name[0] == 'K')
            {
                kill = true;
                Enemyball = false;
                Myball = false;
                blank = false;
                gameData.board[BoardPosNum] = 3;
            }
            else if (OwnBall == BadObject)
            {

                kill = false;
                blank = true;
                Myball = false;
                Enemyball = false;
                gameData.board[BoardPosNum] = 0;
            }
            else
            {
                ballname = OwnBall.gameObject.name;
                //Debug.Log(ballname[0]);
                if (ballname[0] == 'B')
                {
                    kill = false;
                    Myball = false;
                    blank = false;
                    Enemyball = true;
                    gameData.board[BoardPosNum] = 2;
                }
                else
                {
                    kill = false;
                    Enemyball = false;
                    Myball = true;
                    blank = false;
                    gameData.board[BoardPosNum] = 1;
                }
            }
        }
        


       
        //Updates the GameData
       
       

        Checkspotrtn = Checkspot();
        /*

        if (Vector3.Distance(transform.position, mouse.transform.position) < threshold)
        {

            On = true;
            ren.material.color = Color.yellow;
            if (Input.GetMouseButtonDown(0))
            {
                //deslecting code
                if (selected == true)
                {
                    if (OwnBall != null)
                    {
                        moveManager.selobject1 = null;
                    }
                    else
                    {
                        moveManager.selobject2 = null;
                    }

                    selected = false;
                }
                else
                {
                    //selects boardspot
                    selected = true;
                }


            }
        }
        else
        {
            ren.material.color = Color.red;
            On = false;
        }


        if (selected == true)
        {

            //sets it to selobject 1 or 2
            if (OwnBall != null)
            {
                if (moveManager.selobject1 == null)
                {
                    moveManager.selobject1 = this.gameObject;

                }
                else if (moveManager.selobject1 != null)
                {
                    moveManager.addobject3 = this.gameObject;
                }
                else if (moveManager.addobject3 != null)
                {
                    moveManager.addobject4 = this.gameObject;
                }

            }
           
            else
            {
                if (moveManager.selobject2 == null)
                {
                    moveManager.selobject2 = this.gameObject;
                }

            }
            if (moveManager.selobject1 == this.gameObject ||moveManager.selobject2 == this.gameObject || moveManager.addobject3 == this.gameObject || moveManager.addobject4 == this.gameObject) 
            {
                ren.material.color = Color.green;
            }
            


        }
        
        free = true;
       */
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == 6)
        {
            selected = true;
            //ren.material.color = Color.green;
        }
        else
        {
            selected = false;
           // ren.material.color = Color.red;
        }
    }
    public int Checkspot()
    {

        if (Vector3.Distance(transform.position, mouse.transform.position) < threshold)
        {
            if (Input.GetMouseButtonDown(0))
            {
               // Debug.Log("IN Range");

                if ((gameData.board[BoardPosNum] == 0 || gameData.board[BoardPosNum] == 2) && gameData.empty == BadObject)
                {
                   
                    
                        gameData.empty = this.gameObject;
                    
                    
                    
                }
                else if (gameData.board[BoardPosNum] == 1 && gameData.full[0] == BadObject)
                {
                    gameData.full[0] = this.gameObject;
                }
                
                else if (gameData.board[BoardPosNum] == 1 && gameData.full[1] == BadObject)
                {
                    gameData.full[1] = this.gameObject;
                }
               
                else if (gameData.board[BoardPosNum] == 1 && gameData.full[2] == BadObject)
                {
                    gameData.full[2] = this.gameObject;
                }   
                else
                {
                    return 1;
                }
            }

            CheckRepeats();
        }
        return 0;
    }
    public int CheckRepeats()
    {
        
        GameObject rep2 = gameData.full[0];
        for (int i = 1; i < 3; i++)
        {
            if (gameData.full[i] == rep2)
            {
                gameData.full[i] = BadObject;
            }

        }
        return 0;
    }

   
}
