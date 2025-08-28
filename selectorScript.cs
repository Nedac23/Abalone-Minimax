using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class selectorScript : MonoBehaviour
{
    public float speed;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // transform.position = new Vector3(Input.mousePosition.x/Screen.width, transform.position.y , Input.mousePosition.y/Screen.width);
        // transform.position = Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, mousePos.y, cam.nearClipPlane));
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // Set this to the distance from the camera

    // Convert the mouse position to world coordinates
   // https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/#:~:text=Converting%20the%20position%20of%20the%20mouse%20on%20the,the%20Scene.%20Like%20this%3A%20Vector3%20worldPosition%20%3D%20Camera.main.ScreenToWorldPoint%28Input.mousePosition%29%3B
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //

        // Update the position of the object
        transform.position = new Vector3(worldPosition.x, worldPosition.y , worldPosition.z);
    }
}
