using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{  
    //The Laser's Origin Position
    Transform laserTransform;

    //The Line That Will Create The Visual Effect For The Laser
    LineRenderer lineRenderer;
   
    //The Ray Will Detect If The Laser Hits An Object
    private Ray ray;    
    //RaycastHit Has Information Of The Hit Object
    private RaycastHit hit;    
  
    void Awake()
    {
        //Set The Laser And Line Renderer
        laserTransform = this.GetComponent<Transform>();     
        lineRenderer = this.GetComponent<LineRenderer>();
    }
    void Update()
    {   
        //Create A Ray That Starts At The Laser's Origin And Moves Along The Laser
        ray = new Ray(laserTransform.position, laserTransform.forward);
        
        //Reset Number Of Positions Every Frame
        lineRenderer.positionCount = 1;
        //Set First Position Of The Line Renderer To Be The Laser's Position
        lineRenderer.SetPosition(0, laserTransform.position);
                           
        
       //Set The Maximum Times The Laser Can Be Reflected
        for (int i = 0; i < 100; i++)
        {
            //Check If The Ray Collided With Something
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f))
            {       
                if (hit.collider.tag.Equals("Mirror"))
                {
                    //Add A New Position To The Line Renderer
                    lineRenderer.positionCount += 1;

                    //Set The Line Renderer's New Position To Have An Origin Starting At The Location Hit By The Ray
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                    //Since A Mirror Was Hit, The Direction Of The Ray Will Be The Result Of The Angle Between The Vector And The Mirror That Was Hit
                    ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                }              
            }
            else
            {              
                //Add A New Position To The Line Renderer
                lineRenderer.positionCount += 1;

                //Set The Line Renderer's New Position To Have The Same Origin And Direction As The Ray
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * 100f);
                break;
            }
        }
    }
}