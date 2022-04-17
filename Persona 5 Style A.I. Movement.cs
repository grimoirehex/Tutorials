using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FayeAiMove : MonoBehaviour {

    Animator animator;//You may not need an animator, but if so declare it here 
    Rigidbody rigidBody;//Make sure you have a rigidbody
    Rigidbody mainCharRigidBody;//Needed To Determine Main Character's Velocity

   GameObject mainCharacter;//Available Movement Is Based On Main Character's Position    

    Vector3 locationToMoveTo;//Holds Location For AI To Move To

    bool noPointSelected;//Checks If A Location Has Been Chosen To Move Towards
    bool pointReached;//Checks If Destination Has Been Reached
    bool tooFar;//Checks If AI Character Is Too Far Away From Main Character   

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        mainCharacter = CameraControl.character;
        mainCharRigidBody = mainCharacter.GetComponent<Rigidbody>();

        noPointSelected = true;
        pointReached = true;
        tooFar = false;        
    }

    // Update is called once per frame
    void Update()
    {
        mainCharacter = CameraControl.character;

        //Check If AI Is Within 3.5 Units Of Main Character
        if (Vector3.Distance(mainCharacter.transform.position, transform.position) >= 3.5f)
        {
            pointReached = false;
            tooFar = true;
        }
    }

    void FixedUpdate()
    {
        //If Too Far From Main Character Without A Point To Move Towards To Catch Up
        if (tooFar && noPointSelected)
        {
            SelectLocationNearMainCharacter();//Select Point Near Main Character To Move Towards
            noPointSelected = false;
        }

        //If The Point Hasn't Been Reached, Continue To Move Towards It
        if (tooFar && !pointReached)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, (locationToMoveTo - transform.position), 100, 0.0F)); //Rotates AI To Look At Point
            animator.SetInteger("animation", 10);           
            
            rigidBody.velocity = transform.forward * 11f;//Moves AI Forward              
        }

        //Reset Values Once AI Is Close Enough To Location
        if (Vector3.Distance(transform.position, locationToMoveTo) < 0.5)
        {
            pointReached = true;
            tooFar = false;
            noPointSelected = true;

            //Stop Playing RunAnimation Is Main Character Is No Longer Moving
            if (mainCharRigidBody.velocity == new Vector3(0, 0, 0))
            {
                animator.SetInteger("animation", 3);//Idle Animation
            }
        }
    }

    void SelectLocationNearMainCharacter()
    {
        int random = Random.Range(0, 4); // Choose Random Location Near Main Character

        //Moves AI To Upper Left Position Of Main Character
        if (random == 0) { locationToMoveTo = new Vector3(mainCharacter.transform.position.x - 2, 0, mainCharacter.transform.position.z + 2); }

        //Moves AI To Upper Right Position Of Main Character
        if (random == 1) { locationToMoveTo = new Vector3(mainCharacter.transform.position.x + 2, 0, mainCharacter.transform.position.z + 2); }

        //Moves AI To Lower Right Position Of Main Character
        if (random == 2) { locationToMoveTo = new Vector3(mainCharacter.transform.position.x + 2, 0, mainCharacter.transform.position.z - 2); }

        //Moves AI To Lower Left Position Of Main Character
        if (random == 3) { locationToMoveTo = new Vector3(mainCharacter.transform.position.x - 2, 0, mainCharacter.transform.position.z - 2); }
    }   
}