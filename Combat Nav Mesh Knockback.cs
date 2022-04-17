void Attack(int attacker)
    {
        GameObject clone;

        switch (attacker)
        {
            case 1:
                print("Place Holder");
                break;
            case 2:
                print("Place Holder");
                break;
            case 3:                                                                //Slightly Higher than the ground                      //Slightly in front of the player
                clone = Instantiate(attackEffects[0], new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + transform.forward * 1.2f, transform.rotation) as GameObject;                
                break;            
            default:
                print("Place Holder");
                break;
        }



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public NavMeshAgent navAgent;
bool knockBack;
Vector3 direction;

 void Start()
    {       
        knockBack = false;
    }

void FixedUpdate()
    {
        if (knockBack) 
        {            
            navAgent.velocity = direction * 8;//Knocks the enemy back when appropriate 
        }       
    }

IEnumerator KnockBack()
    {       
        knockBack = true;
        navAgent.speed = 10;
        navAgent.angularSpeed = 0;//Keeps the enemy facing forwad rther than spinning
        navAgent.acceleration = 20;                    

        yield return new WaitForSeconds(0.2f); //Only knock the enemy back for a short time     

        //Reset to default values
        knockBack = false;
        navAgent.speed = 4;
        navAgent.angularSpeed = 180;
        navAgent.acceleration = 10;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        direction = other.transform.forward; //Always knocks enemy in the direction the main character is facing
        if (other.name.Equals("AkaneAttack1(Clone)")) { StartCoroutine(KnockBack()); Destroy(other.gameObject); }
    }
}