using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkaneControl : MonoBehaviour {

    public GameObject teleportPortal;//Used If You Want To Instantiate An Object When Telporting

    //Make sure you have a camera, it will determine the direction the character faces
    Transform cam;
    
    float speed;    //How fast the player can move
    float turnSpeed ;    //How fast the player can rotate
  
    bool inBattle;//Allows Attacking Animations

    Animator animator;//You may not need an animator, but if so declare it here    
    Rigidbody rigidBody;//Make sure you have a rigidbody     

    void Start()
    {      
        //Initialize appropriate components 
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
       
        cam = Camera.main.transform;

        speed = 10f;
        turnSpeed = 100;

        inBattle = false;       
    }

    //No need for update function right now, physics work better in Fixed Update
    void Update()
    {
        //Checks Target Controller For nearby Enemies To Determine If In Battle
        if (targetController.nearByEnemies.Count > 0) { inBattle = true; } else { inBattle = false; }        
    }

    void FixedUpdate()
    {     
        if (inBattle)
        {
            battleMovement();
            CameraControl.inBattle = true;
        }
        else if(!inBattle)
        {
            normalMovement();
            CameraControl.inBattle = false;
        }

        if (Input.GetMouseButtonDown(2)) { animator.SetInteger("animation", 7); StartCoroutine(Teleport()); }
    }
    void battleMovement()
    {
        //right is shorthand for (1,0,0) or the x value            forward is short for (0,0,1) or the z value 	
        Vector3 dir = (cam.right * Input.GetAxis("Horizontal")) + (cam.forward * Input.GetAxis("Vertical"));
        dir.y = 0;//Keeps character upright against slight fluctuations               

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run 26"))
        {
            speed = 10;
        }
        else
        {
            speed = 0;
        }                                 
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {           
                //rotate from this /........to this............../.........at this speed  
                rigidBody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);                
                rigidBody.velocity = transform.forward * speed;
                animator.SetInteger("animation", 26);//Walk or run animation works well here             
            }
            else
            {
                animator.SetInteger("animation", 4);//Idle animation works well here
                speed = 10;
            }

       if (Input.GetMouseButton(0)) { animator.SetInteger("animation", 6); }
       if (Input.GetMouseButtonDown(1)) { animator.SetInteger("animation", 5); }                    
    }

    void normalMovement()
    {
        //right is shorthand for (1,0,0) or the x value            forward is short for (0,0,1) or the z value 	
        Vector3 dir = (cam.right * Input.GetAxis("Horizontal")) + (cam.forward * Input.GetAxis("Vertical"));

        dir.y = 0;//Keeps character upright against slight fluctuations 

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {            
            //rotate from this /........to this............../.........at this speed  
            rigidBody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
            rigidBody.velocity = transform.forward * speed;
            animator.SetInteger("animation", 26);//Walk or run animation works well here
        }
        else
        {            
            animator.SetInteger("animation", 2);//Idle animation works well here
        }       
    }

    IEnumerator Teleport()
    {       
        speed = 0;//Stops Movement During Teleport        

        //Produces Particle Effects
        GameObject teleport;       
        teleport = Instantiate(teleportPortal, transform.position, transform.rotation) as GameObject;

        yield return new WaitForSeconds(0.25f);//Wait A While So The Teleport Isn't Too Sudden       
        
        transform.position = transform.position + transform.forward * 10;//Teleports Character Forward

        //Produces Another Particle Effect At New Location
        teleport = Instantiate(teleportPortal, transform.position, transform.rotation) as GameObject;
        
        speed = 10;//Allows Movement After Teleport       
    }

    void OnCollisionEnter(Collision other)
    {
    }
    void OnCollisionExit(Collision other)
    {
    }
}