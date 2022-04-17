using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkaneAttack : MonoBehaviour {

    Animator animator; 
    Rigidbody rigidBody;

    GameObject target; // Enemy To Attack

    bool faceTarget;
    bool moveToTarget;
    bool attackTarget;
    bool moveAway;
    bool canAttack;

    Vector3 locationToMoveTo; // Where To Go After Attacking

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();

        canAttack = true;

        faceTarget = false;
        moveToTarget = false;
        attackTarget = false;
        moveAway = false;
               
        locationToMoveTo = new Vector3(0, 0, 0);

        StartCoroutine(Attack());
    }
	
	// Update is called once per frame
	void Update ()
    {        
        if (target == null) //If No Target, Find One
        {
            target = GameObject.FindGameObjectWithTag("Enemy");
        }
       
        if (faceTarget) //Face Target Before Attacking
        {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, (target.transform.position - transform.position), 100, 0.0F));
        }

        if (attackTarget) //Trigger Attack Animation
        {
            animator.SetInteger("animation", 31);
            attackTarget = false;
        }

        if (moveAway) //After Attacking, Rotate To Face The "Location To Move To"
        {            
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, (locationToMoveTo - transform.position), 100, 0.0F));
        }
	}

    void FixedUpdate()
    {
        //Move Towards Target, If The Distance Between The Character And Target Is More Than 2
        if (moveToTarget && Vector3.Distance(target.transform.position,gameObject.transform.position) > 2)
        {
            animator.SetInteger("animation", 26); //Run Animation 
            rigidBody.velocity = transform.forward * 11f; //Move Character Forward
        }
        else if(moveToTarget && Vector3.Distance(target.transform.position, gameObject.transform.position) < 2 && canAttack)
        {            
            //Once The Target Is Reached Start Attack
            moveToTarget = false;
            canAttack = false;
            StartCoroutine(Attack2());
        }

        //Move Away From Target
        if(moveAway && Vector3.Distance(locationToMoveTo, gameObject.transform.position) > 1)
        {
            animator.SetInteger("animation", 26); //Run Animation
            rigidBody.velocity = transform.forward * 11f; //Move Character Forward
        }
        else if(moveAway && Vector3.Distance(locationToMoveTo, gameObject.transform.position) <= 1)
        {
            moveAway = false;
            animator.SetInteger("animation", 4); //Idle Animation
            StartCoroutine(Attack()); //Start Attack Again
        }
    }

    IEnumerator Attack()
    {
        //Confirm Attack Is Possible And Face Target
        canAttack = true;
        faceTarget = true;

        yield return new WaitForSeconds(0.5f);

        //Facing The Target Is Complete, Just Move Towards It
        faceTarget = false;
        moveToTarget = true;       
    }

    IEnumerator Attack2()
    {        
        attackTarget = true;

        yield return new WaitForSeconds(0.5f);
        animator.SetInteger("animation", 4); //Idle Animation       

        //Now That The Attack Is Over, Choose A Location To Move To
        int rand = Random.Range(0,4);

        if(rand == 0) { locationToMoveTo = new Vector3(target.transform.position.x - 3, 0, target.transform.position.z + 3); }
        if (rand == 1) { locationToMoveTo = new Vector3(target.transform.position.x - 3, 0, target.transform.position.z - 3); }
        if (rand == 2) { locationToMoveTo = new Vector3(target.transform.position.x + 3, 0, target.transform.position.z + 3); }
        if (rand == 3) { locationToMoveTo = new Vector3(target.transform.position.x + 3, 0, target.transform.position.z - 3); }
        
        moveAway = true;
    }
}