using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkaneSwordHit : MonoBehaviour {

     GameObject Akane; // Main Character
     Animator animator;//Animator Attached To Main Character 

    private void Awake()
    {
         Akane = GameObject.FindGameObjectWithTag("Akane"); //Assigning Game Object      
    }

    // Use this for initialization
    void Start ()
    {
        animator = Akane.GetComponent<Animator>();//Assigning Animator
    }
	
	// Update is called once per frame
	void Update ()
    {		
	}
   

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Equals("Monster 1"))//Checks For Type Of Enemy Hit
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("AtkB 6"))//If Character IS In Attacking Animation
            {
                other.gameObject.SendMessage("ApplyDamage");//Send Message To Enemy 
            }
        }   
    }
    void OnCollisionExit(Collision other)
    {
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMessage : MonoBehaviour {

    private Animator anim; //Reference For Attached Animator
    public GameObject hitEffect; //GameObject To Instantiate
   

    // Use this for initialization
    void Start ()
    {        
        anim = GetComponent<Animator>(); //Assign Component To Animator Variable       
    }

    void ApplyDamage()
    {
        anim.SetTrigger("hit");//Trigger Animation        

        //If Instantiating An Object / Effect Do So Here
        GameObject clone;
        clone = Instantiate(hitEffect, new Vector3( transform.position.x, transform.position.y + 2, transform.position.z), transform.rotation) as GameObject;         
    }

    // Update is called once per frame
    void Update () {        
    }
}