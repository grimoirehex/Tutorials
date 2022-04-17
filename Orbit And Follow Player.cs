using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

	GameObject character;
	Vector3 characterPosition;

	float rotationSpeed;
	float distance;

	void Start () 
	{
		character = GameObject.Find ("Akane"); //Find Character To Rotate	

		rotationSpeed = 0.0f;
		distance = -0.8f; //Make Sure This Is Negative, Values Less Than -0.5 Seem To Work Best 
	}	

	void Update () {

		//Adjusting The Y Axis To Be Slightly Higher 
		characterPosition = new Vector3 (character.transform.position.x, character.transform.position.y + 1, character.transform.position.z);

		rotationSpeed = rotationSpeed +  200 * Time.deltaTime; // How Fast To Orbit Player
					
		                                                   //Rotation Around Character............/...Keeps Distance From Character
		gameObject.transform.position = characterPosition + Quaternion.Euler(0, rotationSpeed, 0) * new Vector3(0, 0, distance);

		}		
	}