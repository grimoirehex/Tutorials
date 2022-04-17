using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerControl : MonoBehaviour {	
	//Make sure you have a camera, it will determine the direction the character faces
	Transform cam;
	float speed = 10f;    //How fast the player can move
	float turnSpeed = 100;    //How fast the player can rotate
	Animator animator;//You may not need an animator, but if so declare it here 
	Rigidbody rigidBody;//Make sure you have a rigidbody
		 
	void Start()
	{		
		//Initialize appropriate components 
		animator = GetComponent&lt;Animator> ();
		rigidBody = GetComponent&lt;Rigidbody>();
		cam = Camera.main.transform;
	}
	//No need for update function right now, physics work better in Fixed Update
	void Update()
	{		
	}
	void FixedUpdate()
	{	        		
		//right is shorthand for (1,0,0) or the x value            forward is short for (0,0,1) or the z value 	
		Vector3 dir = (cam.right * Input.GetAxis("Horizontal")) + (cam.forward * Input.GetAxis("Vertical"));
		dir.y = 0;//Keeps character upright against slight fluctuations 
		if (Input.GetAxis("Horizontal") != 0 ||Input.GetAxis("Vertical") != 0) 
		{		
			                                       //rotate from this /........to this............../.........at this speed  
			rigidBody.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dir), turnSpeed * Time.deltaTime);
			rigidBody.velocity = transform.forward * speed;
			animator.SetInteger ("animation", 10);//Walk or run animation works well here
		} else
		{
			animator.SetInteger ("animation", 25);//Idle animation works well here
		}
	}	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraControl : MonoBehaviour {
	//Determines the limitations of vertical camera movement
	private const float Y_ANGLE_MIN = 15.0f;
	private const float Y_ANGLE_MAX = 25.0f;
	public Transform character; //What the camera is looking at..the main character
	private float distance = -5.0f; // Distance to stay from character, Make sure it is negative
	private float currentX = 0.0f; // Holds value of X mouse movement 
	private float currentY = 0.0f; // Holds value of Y mouse movement 
	 void start(){}
	 void Update()
	{
		if (Input.GetAxis ("Mouse X") != null || Input.GetAxis ("Mouse Y") != null)
		{
			currentX += Input.GetAxis("Mouse X");
			currentY += Input.GetAxis("Mouse Y");
		}	
		
		currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
	}
	 void LateUpdate()
	{			                                             //Rotation around character............/...Keeps distance from character           
		gameObject.transform.position = character.position + Quaternion.Euler(currentY, currentX, 0) * new Vector3 (0, 0, distance);
		gameObject.transform.LookAt(character.position);//Points camera at character
	}
}