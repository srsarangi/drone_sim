using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovementScript : MonoBehaviour
{
    Rigidbody ourDrone;
    void Awake(){

        ourDrone = GetComponent<Rigidbody>();
         droneSound = gameObject.transform.Find("drone_sound").GetComponent<AudioSource>();
    }
    public int numberOfRays = 12;
public float angle = 60;
public float targetVelocity = 10.0f;
public float rayRange = 14;
    // void Update()
    // {             
        

    //     var deltaPosition = Vector3.zero;
    //  for (int i = 0 ; i < numberOfRays;++i)
    //  {
    //      var rotation = this.transform.rotation;
    //      var rotationMod = Quaternion.AngleAxis((i/((float)numberOfRays) * angle *2 - angle), this.transform.up);
    //      var direction = rotation * rotationMod * Vector3.forward;
            
    //      var ray = new Ray( this.transform.position, direction);
    //      RaycastHit hitInfo;
    //      if (Physics.Raycast(ray,out hitInfo,rayRange))
    //      {
    //          deltaPosition -= (1.0f/numberOfRays)* targetVelocity* direction;
    //      }
    //      else 
    //      {
    //          deltaPosition += (1.0f/numberOfRays)* targetVelocity* direction;
    //      }
            
    //  }
    // this.transform.position += deltaPosition * Time.deltaTime;
    // }
    // void OnDrawGizmos()
    // {

    //     var deltaPosition = Vector3.zero;
    //     for (int i = 0 ; i < numberOfRays;++i)
    //     {
    //         var rotation = this .transform.rotation;
    //         var rotationMod = Quaternion.AngleAxis((i/((float)numberOfRays) * angle *2 - angle), this.transform.up);
    //         var direction = rotation * rotationMod * Vector3.forward;
    //         Gizmos.DrawRay(this.transform.position, direction);
            
    //     }
        
    // }


    // Update is called once per frame
    void FixedUpdate()   {
    	MovementUpDown();
    	MovementForward();
    	
    	Swerwing();
    	Rotation();
        DroneSound();
    	ClampingSpeedValues();
    	ourDrone.AddRelativeForce(Vector3.up * upForce);
    	ourDrone.rotation = Quaternion.Euler(
		new Vector3(tiltAmountForward,currentYRotation,tiltAmountSideways)
    		);

        
    }

    public float upForce;
    void MovementUpDown (){
        if ((Mathf.Abs(Input.GetAxis("Vertical"))>0.2f||Mathf.Abs(Input.GetAxis("Horizontal"))>0.2f )){
            if (Input.GetKey(KeyCode.I)||Input.GetKey(KeyCode.K)){
                ourDrone.velocity = ourDrone.velocity;
            }
            
        
        if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.L)){
            ourDrone.velocity = new Vector3(ourDrone.velocity.x,Mathf.Lerp(ourDrone.velocity.y,0,Time.deltaTime *5),ourDrone.velocity.z);
            upForce= 281;
        }
        if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L))){
                ourDrone.velocity = new Vector3(ourDrone.velocity.x,Mathf.Lerp(ourDrone.velocity.y,0,Time.deltaTime *5),ourDrone.velocity.z);
                upForce= 110;
            }
        if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L)){
            upForce= 410;
        }
    }
    if ((Mathf.Abs(Input.GetAxis("Vertical"))<0.2f||Mathf.Abs(Input.GetAxis("Horizontal"))>0.2f )){
            upForce = 135;
    }
    	if (Input.GetKey(KeyCode.I)){
    		upForce = 450;
            if (Mathf.Abs(Input.GetAxis("Horizontal"))>0.2f){
                upForce= 500;
            }
    	}
    	else if (Input.GetKey(KeyCode.K)){
    		upForce = -200;
    	}
    	else if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && (Mathf.Abs(Input.GetAxis("Vertical"))<0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) <0.2f )){
    		upForce = 98.1f;
    		// Debug.Log("anit gravity not working");

    	}
    }

    private float movementForwardSpeed = 500.0f;
    private float tiltAmountForward = 0;
    private float tiltVilocityForward;
    void MovementForward(){
    	if (Input.GetAxis("Vertical")!= 0){
    		ourDrone.AddRelativeForce(Vector3.forward *Input.GetAxis("Vertical") * movementForwardSpeed);
    		tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward, 10 * Input.GetAxis("Vertical"), ref tiltVilocityForward, 0.1f);



    	}
    }
   
    	private float sideMovementAmount =300.0f;
    	private float tiltAmountSideways ;
    	private float tiltAmountVelocity;
    	private void Swerwing(){

    		if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f){
    			ourDrone.AddRelativeForce(Vector3.right * Input.GetAxis("Horizontal") * sideMovementAmount  );
    			tiltAmountSideways= Mathf.SmoothDamp(tiltAmountSideways, -10* Input.GetAxis("Horizontal"), ref tiltAmountVelocity, 0.1f);

    		}
    		else {
    			tiltAmountSideways= Mathf.SmoothDamp(tiltAmountSideways, 0, ref tiltAmountVelocity, 0.1f);
    		}
    	} 
    private Vector3 velocityToSmoothDampToZero;
    void ClampingSpeedValues(){
    	if (Mathf.Abs(Input.GetAxis("Vertical"))>0.2f && Mathf.Abs(Input.GetAxis("Horizontal"))>0.2f ){
    		ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity,Mathf.Lerp(ourDrone.velocity.magnitude,10.0f,Time.deltaTime * 5f));

    	}
    	if (Mathf.Abs(Input.GetAxis("Vertical"))>0.2f && Mathf.Abs(Input.GetAxis("Horizontal"))<0.2f ){
    		ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity,Mathf.Lerp(ourDrone.velocity.magnitude,10.0f,Time.deltaTime * 5f));
    	}
    	if (Mathf.Abs(Input.GetAxis("Vertical"))<0.2f && Mathf.Abs(Input.GetAxis("Horizontal"))>0.2f ){
    		ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity,Mathf.Lerp(ourDrone.velocity.magnitude,5.0f,Time.deltaTime * 5f));
    		
    	}
    	if (Mathf.Abs(Input.GetAxis("Vertical"))<0.2f && Mathf.Abs(Input.GetAxis("Horizontal"))<0.2f ){
    		ourDrone.velocity = Vector3.SmoothDamp(ourDrone.velocity, Vector3.zero, ref velocityToSmoothDampToZero, 0.95f);
       	}
    }



    	private float wantedYRotation;
    	[HideInInspector]public float currentYRotation;
    	private float rorateAmountByKeys= 2.5f;
    	private float rotationYVelocity;
    	void Rotation(){

    		if (Input.GetKey(KeyCode.J)){
    			wantedYRotation -= rorateAmountByKeys;
    		}
    		if (Input.GetKey(KeyCode.L)){
    			wantedYRotation += rorateAmountByKeys;
    		}
    		currentYRotation= Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);
    	}
        private AudioSource droneSound;
        void DroneSound(){
            droneSound.pitch = 1+ (ourDrone.velocity.magnitude /100);
        }




    }


   