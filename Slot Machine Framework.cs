using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour {

    //This Variable Will Be Changed By The "Slots" Class To Control When The Reel Spins  
    public bool spin;

    //Speed That Reel Will Spin
    int speed;
 
    // Use this for initialization
    void Start()
    {
        spin = false;
        speed = 1500;
    }

    // Update is called once per frame
    void Update()
    {
        if (spin)
        {
            foreach (Transform image in transform)//This Targets All Children Objects Of The Main Parent Object
            {
                //Direction And Speed Of Movement
                image.transform.Translate(Vector3.down * Time.smoothDeltaTime * speed, Space.World);

                //Once The Image Moves Below A Certain Point, Reset Its Position To The Top 
                if (image.transform.position.y <= 0) { image.transform.position = new Vector3(image.transform.position.x, image.transform.position.y + 600, image.transform.position.z); }
            }
        }
    }

    //Once The Reel Finishes Spinning The Images Will Be Placed In A Random Position
    public void RandomPosition()
    {
        List<int> parts = new List<int>();

        //Add All Of The Values For The Original Y Postions  
        parts.Add(200);
        parts.Add(100);
        parts.Add(0);
        parts.Add(-100);
        parts.Add(-200);
        parts.Add(-300);


        foreach (Transform image in transform)
        {
           int rand = Random.Range(0, parts.Count);

            //The "transform.parent.GetComponent<RectTransform>().transform.position.y" Allows It To Adjust To The Canvas Y Position
            image.transform.position = new Vector3(image.transform.position.x, parts[rand] + transform.parent.GetComponent<RectTransform>().transform.position.y, image.transform.position.z);

           parts.RemoveAt(rand);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour {

    public Reel[] reel;
    bool startSpin;

	// Use this for initialization
	void Start ()
    {
        startSpin = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!startSpin)//Prevents Interference If The Reels Are Still Spinning
        {
            if (Input.GetKeyDown(KeyCode.K))//The Input That Starts The Slot Machine 
            {
                startSpin = true;
                StartCoroutine(Spinning());
            }
        }
	}

    IEnumerator Spinning()
    {
        foreach (Reel spinner in reel)
        {
            //Tells Each Reel To Start Spnning
            spinner.spin = true;
        }

        for(int i = 0; i < reel.Length; i++)
        {
            //Allow The Reels To Spin For A Random Amout Of Time Then Stop Them
            yield return new WaitForSeconds(Random.Range(1, 3));
            reel[i].spin = false;
            reel[i].RandomPosition();
        }

        //Allows The Machine To Be Started Again 
        startSpin = false;
    }
}