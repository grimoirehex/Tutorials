using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Make Sure To Include This

public class AkaneStats : MonoBehaviour {
    
    public Slider healthbar;//Reference To Slider

    public static int health; //Health Of Player

	// Use this for initialization
	void Start ()
    {
        health = 100; //Max Health Of Player, Should Be Same As Max Slider Value        
	}
	
	// Update is called once per frame
	void Update ()
    {
        healthbar.value = health; //Update Slider's Value To Equal Player's Health
        
        ChangeHealth(); //Used If No Triggers Are Present
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("FireBall(Clone)")) { if(health > 0) health = health - 20; }//Stop Damaging Once Health Reaches 0
        if (other.gameObject.name.Equals("HolyBall(Clone)")) { if(health < 100) health = health + 20; }//Stops Healing Once Health Reaches 100
    }
    
    void ChangeHealth()
    {
        if (Input.GetKeyDown(KeyCode.Z)) { if (health > 0) health = health - 20; }
        if (Input.GetKeyDown(KeyCode.X)) { if (health < 100) health = health + 20; }
    }
}
