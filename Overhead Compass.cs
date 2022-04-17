using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Don't Forget

public class Compass : MonoBehaviour {

    public RectTransform North;
    public RectTransform East;
    public RectTransform South;
    public RectTransform West;
    public Camera cam;

    float camAngle;
     
	// Use this for initialization
	void Start ()
    {	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //The Camera's Current Angle
        camAngle = cam.transform.rotation.eulerAngles.y;

        //Facing North 
        if (camAngle >= 315 && camAngle < 359) 
        {
            TurnOffOtherDirections("North");// Turns On North Indicator While Turning Others Off
            CalculateNorth1();//Determines Where The Indicator Will Show On Screen
        }
        if (camAngle >= 0 && camAngle < 45)
        {
            TurnOffOtherDirections("North");
            CalculateNorth2();
        }       
      
        //Facing East
        if(camAngle >= 45 && camAngle < 135)
        {
            TurnOffOtherDirections("East");
            CalculateEast();
        }       

        //Facing South
        if(camAngle >= 135 && camAngle < 225)
        {
            TurnOffOtherDirections("South");
            CalculateSouth();
        }    

        //Facing West
        if(camAngle >= 225 && camAngle < 315)
        {
            TurnOffOtherDirections("West");
            CalculateWest();
        }                    
    }

    //When Calculating The Postion Of The Indicator, The X Value Is Always Equal To The Main Direction, Minus The Camera's Current Angle
    //The North Direction Is Where The Circle Begins And Ends, So Two Methods Are Needed

    void CalculateNorth1()
    {
        North.localPosition = new Vector3((360 - camAngle) * 4, 300, North.localPosition.z);
    }

    void CalculateNorth2()
    {
        North.localPosition = new Vector3 ((0 - camAngle) * 4 , 300, North.localPosition.z);
    }

    void CalculateEast()
    {
        East.localPosition = new Vector3((90 - camAngle) * 4, 300, East.localPosition.z);
    }   

    void CalculateSouth()
    {
        South.localPosition = new Vector3((180 - camAngle) * 4, 300, South.localPosition.z );
    }
   
    void CalculateWest()
    {
        West.localPosition = new Vector3((270 - camAngle) * 4, 300, West.localPosition.z);
    }
    
    void TurnOffOtherDirections(string directionToKeepOn)
    {
        switch (directionToKeepOn)
        {
            case "North":
                North.GetComponent<Text>().text = "N";
                East.GetComponent<Text>().text = "";
                West.GetComponent<Text>().text = "";
                South.GetComponent<Text>().text = "";
                break;
            case "East":
                North.GetComponent<Text>().text = "";
                East.GetComponent<Text>().text = "E";
                West.GetComponent<Text>().text = "";
                South.GetComponent<Text>().text = "";
                break;
            case "West":
                North.GetComponent<Text>().text = "";
                East.GetComponent<Text>().text = "";
                West.GetComponent<Text>().text = "W";
                South.GetComponent<Text>().text = "";
                break;
            case "South":
                North.GetComponent<Text>().text = "";
                East.GetComponent<Text>().text = "";
                West.GetComponent<Text>().text = "";
                South.GetComponent<Text>().text = "S";
                break;

            default:
                break;
        }
    }
}