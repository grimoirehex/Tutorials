using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class enemyInView : MonoBehaviour {
    Camera cam;//Camera Used To Detect Enemies On Screen       
    bool addOnlyOnce;//This Boolean Is Used To Only Allow The Enemy To Be Added To The List Once 
	
	void Start ()
    {
        cam = Camera.main;        
        addOnlyOnce = true;               
    }	
	
	void Update () {
        //First Create A Vector3 With Dimensions Based On The Camera's Viewport
        Vector3 enemyPosition = cam.WorldToViewportPoint(gameObject.transform.position);
        //If The X And Y Values Are Between 0 And 1, The Enemy Is On Screen
        bool onScreen = enemyPosition.z > 0 &amp;&amp; enemyPosition.x > 0 &amp;&amp; enemyPosition.x &lt; 1 &amp;&amp; enemyPosition.y > 0 &amp;&amp; enemyPosition.y &lt; 1;
        //If The Enemy Is On Screen Add It To The List Of Nearby Enemies Only Once
        if (onScreen &amp;&amp; addOnlyOnce)
        {
            addOnlyOnce = false;            
            targetController.nearByEnemies.Add(this);
        }      
    }      
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Be Sure To Include This
public class targetController : MonoBehaviour {
    Camera cam; //Main Camera
    enemyInView target; //Current Focused Enemy In List
    Image image;//Image Of Crosshair
    bool lockedOn;//Keeps Track Of Lock On Status     
    //Tracks Which Enemy In List Is Current Target
    int lockedEnemy;
    //List of nearby enemies
    public static List&lt;enemyInView> nearByEnemies = new List&lt;enemyInView>();
    void Start () {
        cam = Camera.main;
        image = gameObject.GetComponent&lt;Image>();
        lockedOn = false;
        lockedEnemy = 0;
    }	
	
	void Update () {        
        //Press Space Key To Lock On
        if (Input.GetKeyDown(KeyCode.Space) &amp;&amp; !lockedOn)
        {
            if (nearByEnemies.Count >= 1)
            {
                lockedOn = true;
                image.enabled = true;
                //Lock On To First Enemy In List By Default
                lockedEnemy = 0;
                target = nearByEnemies[lockedEnemy];
            }
        }
        //Turn Off Lock On When Space Is Pressed Or No More Enemies Are In The List 
        else if ((Input.GetKeyDown(KeyCode.Space) &amp;&amp; lockedOn) || nearByEnemies.Count == 0)
        {
            lockedOn = false;
            image.enabled = false;
            lockedEnemy = 0;
            target = null;
        }
        //Press X To Switch Targets
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (lockedEnemy == nearByEnemies.Count - 1)
            {
                //If End Of List Has Been Reached, Start Over
                lockedEnemy = 0;
                target = nearByEnemies[lockedEnemy];
            }
            else
            {
                //Move To Next Enemy In List
                lockedEnemy++;
                target = nearByEnemies[lockedEnemy];
            }            
        }       
        if (lockedOn)
        {
            target = nearByEnemies[lockedEnemy];
            //Determine Crosshair Location Based On The Current Target 
            gameObject.transform.position = cam.WorldToScreenPoint(target.transform.position);    
            
            //Rotate Crosshair
            gameObject.transform.Rotate(new Vector3(0, 0, -1));
        }              
    }
}