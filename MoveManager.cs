
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public GameObject selobject1;
    public GameObject selobject2;
    public GameObject addobject3;
    public GameObject addobject4;
    public GameObject mov;
    public bool inplace;
    public bool swaped = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(1)) 
        {
           // swaped = false;
           //MovePiece(); 
        }
        
    }
    void Swappiece()
    {
        selobject2.transform.gameObject.GetComponent<select>().OwnBall = selobject1.transform.gameObject.GetComponent<select>().OwnBall;
        selobject1.transform.gameObject.GetComponent<select>().OwnBall = null;
        selobject2.transform.gameObject.GetComponent<select>().selected = false;
        selobject1.transform.gameObject.GetComponent<select>().selected = false;
        selobject1 = null;
        selobject2 = null;
    }
    void MovePiece()
    {
        if (selobject1 != null && selobject2 != null)
        {
           
            
                for (int i = 0; i < 7; i++)
                {
                    if(swaped == false)
                    {
                        if (selobject1.transform.GetComponent<select>().neighbors[i] != null)
                        {
                            if (selobject1.transform.GetComponent<select>().neighbors[i] == selobject2)
                            {
                                selobject1.transform.gameObject.GetComponent<select>().OwnBall.transform.position = selobject2.transform.position;
                                ///Swappiece();
                                swaped = true;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {

                    }
                   
                }
            
            
            
                
            

        }
    }
}
