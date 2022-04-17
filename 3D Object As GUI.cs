using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI3D : MonoBehaviour
{
    public Camera cam;    
    public GameObject objectToShow;
    
   
    // Use this for initialization
    void Start()
    {        
        //Places Object 2 Units In Front Of The Camera
        objectToShow.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z + 2);
    }    

    // Update is called once per frame
    void Update()
    {
        //Use This To Have The Object Follow The Camera        
        //objectToShow.transform.position  = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z + 2);

        //Spins The Object For Demonstration Purposes
        objectToShow.transform.Rotate(Vector3.up * 2, Space.Self);
    }
}