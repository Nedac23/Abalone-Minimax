using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSpotScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("s") || Input.GetKeyDown("a"))
        {
            FixKillSpot();
        }
        
    }
    public void FixKillSpot()
    {
        if (this.gameObject.transform.GetComponent<select>().OwnBall != null)
        {
            this.gameObject.transform.GetComponent<select>().OwnBall = null;
        }
    }
}
