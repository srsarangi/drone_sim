using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSimpleMove : MonoBehaviour
{
    Rigidbody ourDrone2;
    void Awake(){

        ourDrone2 = GetComponent<Rigidbody>();
         droneSound = gameObject.transform.Find("drone_sound").GetComponent<AudioSource>();
    }
    public int numberOfRays = 12;
public float angle = 60;
public float targetVelocity = 2.0f;
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
    	MovementUpDown2();
    	MovementForward2();
    	
    	Swerwing();
    	Rotation();
        DroneSound();
    	ClampingSpeedValues();
    	ourDrone2.AddRelativeForce(Vector3.up * upForce);
  //   	ourDrone2.rotation = Quaternion.Euler(
		// new Vector3(tiltAmountForward2,currentYRotation2,tiltAmountSideways2)
  //   		);

        
    }

    public float upForce;
    void MovementUpDown2 (){
        if ((Mathf.Abs(Input.GetAxis("Vertical"))>0.2f||Mathf.Abs(Input.GetAxis("Horizontal"))>0.2f )){
            if (Input.GetKey(KeyCode.I)||Input.GetKey(KeyCode.K)){
                ourDrone2.velocity = ourDrone2.velocity;
            }
            
        
        if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.L)){
            ourDrone2.velocity = new Vector3(ourDrone2.velocity.x,Mathf.Lerp(ourDrone2.velocity.y,0,Time.deltaTime *5),ourDrone2.velocity.z);
            upForce= 281;
        }
        if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L))){
                ourDrone2.velocity = new Vector3(ourDrone2.velocity.x,Mathf.Lerp(ourDrone2.velocity.y,0,Time.deltaTime *5),ourDrone2.velocity.z);
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

    private float movementForwardSpeed2 = 50.0f;
    private float tiltAmountForward2 = 0;
    private float tiltVilocityForward2;
    void MovementForward2(){
    	if (Input.GetAxis("Vertical")!= 0){
    		ourDrone2.AddRelativeForce(Vector3.forward *Input.GetAxis("Vertical") * movementForwardSpeed2);
    		// tiltAmountForward2 = Mathf.SmoothDamp(tiltAmountForward2, 10 * Input.GetAxis("Vertical"), ref tiltVilocityForward2, 0.1f);



    	}
    }
   
    	private float sideMovementAmount2 =30.0f;
    	private float tiltAmountSideways2 = 0 ;
    	private float tiltAmountVelocity2;
    	private void Swerwing(){

    		if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f){
    			ourDrone2.AddRelativeForce(Vector3.right * Input.GetAxis("Horizontal") * sideMovementAmount2  );
    			// tiltAmountSideways2= Mathf.SmoothDamp(tiltAmountSideways2, -10* Input.GetAxis("Horizontal"), ref tiltAmountVelocity2, 0.1f);

    		}
    		// else {
    		
    		// 	// tiltAmountSideways2= Mathf.SmoothDamp(tiltAmountSideways2, 0, ref tiltAmountVelocity2, 0.1f);
    		// }
    	} 
    private Vector3 velocityToSmoothDampToZero;
    void ClampingSpeedValues(){
    	if (Mathf.Abs(Input.GetAxis("Vertical"))>0.2f && Mathf.Abs(Input.GetAxis("Horizontal"))>0.2f ){
    		ourDrone2.velocity = Vector3.ClampMagnitude(ourDrone2.velocity,Mathf.Lerp(ourDrone2.velocity.magnitude,10.0f,Time.deltaTime * 5f));

    	}
    	if (Mathf.Abs(Input.GetAxis("Vertical"))>0.2f && Mathf.Abs(Input.GetAxis("Horizontal"))<0.2f ){
    		ourDrone2.velocity = Vector3.ClampMagnitude(ourDrone2.velocity,Mathf.Lerp(ourDrone2.velocity.magnitude,10.0f,Time.deltaTime * 5f));
    	}
    	if (Mathf.Abs(Input.GetAxis("Vertical"))<0.2f && Mathf.Abs(Input.GetAxis("Horizontal"))>0.2f ){
    		ourDrone2.velocity = Vector3.ClampMagnitude(ourDrone2.velocity,Mathf.Lerp(ourDrone2.velocity.magnitude,5.0f,Time.deltaTime * 5f));
    		
    	}
    	if (Mathf.Abs(Input.GetAxis("Vertical"))<0.2f && Mathf.Abs(Input.GetAxis("Horizontal"))<0.2f ){
    		ourDrone2.velocity = Vector3.SmoothDamp(ourDrone2.velocity, Vector3.zero, ref velocityToSmoothDampToZero, 0.95f);
       	}
    }



    	private float wantedYRotation;
    	[HideInInspector]public float currentYRotation2;
    	private float rorateAmountByKeys= 2.5f;
    	private float rotationYVelocity;
    	void Rotation(){

    		if (Input.GetKey(KeyCode.J)){
    			wantedYRotation -= rorateAmountByKeys;
    		}
    		if (Input.GetKey(KeyCode.L)){
    			wantedYRotation += rorateAmountByKeys;
    		}
    		currentYRotation2= Mathf.SmoothDamp(currentYRotation2, wantedYRotation, ref rotationYVelocity, 0.25f);
    	}
        private AudioSource droneSound;
        void DroneSound(){
            droneSound.pitch = 1+ (ourDrone2.velocity.magnitude /100);
        }




    }
