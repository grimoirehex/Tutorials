using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {	

	//Make Sure You Have A Camera, It Will Determine The Direction The Character Faces
	Transform cam;

    public float speed;    //How Fast The Player Can Move
    public float turnSpeed;    //How Fast The Player Can Rotate
    public float jumpForce;//Amount Of Force To Use To Jump

    Animator animator;//You May Not Need An Animator, But If So Declare It Here 
	Rigidbody rigidBody;//Make Sure You Have A Rigidbody
           
    CapsuleCollider myCollider;//The Main Collider Of The Player
    public LayerMask groundLayers;//The Layer That The Collider Will Check If It Is In Contact With In Order To Jump    

    void Start()
	{		
		//Initialize Appropriate Components 
		animator = GetComponent<Animator> ();
		rigidBody = GetComponent<Rigidbody>();
        myCollider = GetComponent<CapsuleCollider>();

        cam = Camera.main.transform;      
    }    

	void FixedUpdate()
	{        
        //Get Input Values
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        //Direction Character Faces Is Based On The Camera
        Vector3 dir = (cam.right * moveHorizontal) + (cam.forward * moveVertical);
		dir.y = 0;//Keeps Character Upright Against Slight Fluctuations              
               
        //Rotate From The Current Rotation To The New Rotation At A Certain Speed  
        rigidBody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);        
        
        if (IsGrounded())
        {
            //If On The Ground And Starting A Jump
            if ( Input.GetKeyDown(KeyCode.Space))
            {
                rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);                
                animator.SetInteger("animation", 26);//Jump Animation Works Well Here
            }
            //If On The Ground Simply Walking Or Running
            else if (moveHorizontal != 0 || moveVertical != 0)
            {
                rigidBody.AddForce(rigidBody.transform.forward * speed);
                animator.SetInteger("animation", 26);//Walk Or Run Animation Works Well Here            
            }
            //If On The Ground With No Movement Inputs
            else if ( moveVertical == 0 && moveHorizontal == 0)
            {              
                animator.SetInteger("animation", 2);//Idle Or Jump Animation Works Well Here
            }
        }
        else
        {
            //If In The Air And Moving
            if (moveHorizontal != 0 || moveVertical != 0)
            {
                rigidBody.AddForce(rigidBody.transform.forward * speed);
                animator.SetInteger("animation", 26);//Walk Or Run Animation Works Well Here
            }
        }
    }

    private bool IsGrounded()
    {        
        //Check If Collider Is Touching The Ground
        return Physics.CheckCapsule(myCollider.bounds.center, new Vector3(myCollider.bounds.center.x, myCollider.bounds.min.y, myCollider.bounds.center.z), groundLayers);
    }  
}