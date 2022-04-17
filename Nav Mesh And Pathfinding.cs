using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//Be Sure To Include This

public class Monster1Nav : MonoBehaviour {

    NavMeshAgent navAgent; //Will Detect The Nav Mesh And Move Along It   
    GameObject currentTarget;// Main Character Will Be The Target

    bool inBattle;//Triggers Nav Agent To Move

    // Use this for initialization
    void Start () {

        inBattle = false;

        currentTarget = GameObject.FindGameObjectWithTag("Akane"); //Find And Assign Target
        navAgent = gameObject.GetComponent<NavMeshAgent>();//Get The Nav Mesh Agent That Has Been Added To The Enemy
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Triggers Nav Agent If Player Gets Too Close To Monster
        if (Vector3.Distance(currentTarget.transform.position, transform.gameObject.transform.position) < 5)
        {
            inBattle = true;            
        }

        //Now That The Player Is In A Battle, Set The Destination Of The Nav Mesh To The Players Position
        if (inBattle)
        {
            navAgent.destination = currentTarget.transform.position;
        }        
    }
}