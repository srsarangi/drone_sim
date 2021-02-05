using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneWaypointMovement : MonoBehaviour
{
	public Transform path;
	private List<Transform> nodes;
	private int currentNode1 = 0;
	public float maxSteerAngle= 40f;
	// Adjust the speed for the application.
    public float speed = 1.0f;

    // The target (cylinder) position.
    private Transform target;
    // Start is called before the first frame update
    [Header("Sensors")]
    public float sensorLength =14f;
    
    public Vector3 frontSensorPosition = new Vector3(0f,0f,0f);
    public float frontSideSensorPosition = 1f;
    public float frontSensorAngle = 25f;
    public float sideSensorPosition =0.2f;
    private bool avoiding = false;

    void Start()
    {
        Transform [] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for (int i = 0 ; i< pathTransforms.Length; i++)
        {
        	if (pathTransforms[i] != path.transform)
        	{
        		nodes.Add(pathTransforms[i]);
        	}
        }
    }
private int k =0;
    // Update is called once per frame
    void FixedUpdate()
    {
    	
        // ApplySteer();
        MoveToNextNode();
        MoveInCircuit();
        Sensors();
    }    
    // dont use now used in cars 
	// void ApplySteer()
	// {

 //    	Vector3 reelativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
 //    	float new maxSteer = (reelativeVector.x/ reelativeVector.magnitude)*maxSteerAngle;
 //    }
    
    
 	void MoveToNextNode()
 	{
 		 float step =  speed * Time.deltaTime;
 		
 			transform.position = Vector3.MoveTowards(transform.position, nodes[currentNode1].position, step);

 		
 			Vector3 relativePos = nodes[currentNode1].transform.position - transform.position;
		   	Quaternion rotation = Quaternion.LookRotation(relativePos);
		   	// transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
		   	transform.rotation = rotation;
 			
 		}	
 	void MoveInCircuit()
 	{
 		if (Vector3.Distance(transform.position, nodes[currentNode1].position)<0.05f)
 		{
 			if (currentNode1 == nodes.Count -1)
 			{
 				currentNode1 = 0;
 			}
 			else
 			{
 				currentNode1 ++;
 			}
 		}


 	}

 	void Sensors()
 	{
 		RaycastHit hit;
 		Vector3 sensorStartPosition = transform.position;
 		 sensorStartPosition += transform.forward * frontSensorPosition.z;
 		sensorStartPosition += transform.up * frontSensorPosition.y;
 		float avoidMultiplier = 0;
 		avoiding = false;

 		// front sensor
 		if (Physics.Raycast(sensorStartPosition,transform.forward,out hit , sensorLength))
 		{
 			 // Vector3 currentPosition = transform.position;
 			// Debug.Log(transform.position);
 			if (!hit.collider.CompareTag("Terrain"))
 			{
 				Debug.DrawLine(sensorStartPosition, hit.point,Color.red);
 				avoiding = true;
 				

 				// transform.position = Vector3.MoveTowards(transform.position, nodes[currentNode1 --].position, 0.0f);
 				// Debug.Log(transform.position);
 				// Debug.Log(currentNode1);
 				
 				

 				
 				// if (currentNode1 ==0){
 				// 	currentNode1= nodes.Count ;
 				// 	Debug.Log(transform.position);
 				// Debug.Log(currentNode1);
 				// }
 				// else
 				// {
 				// 	currentNode1 --;
 				// 	Debug.Log(transform.position);
 				// Debug.Log(nodes[currentNode1 ].position);
 				// }
 				
 				
 				
 			} 
 		}

 		// Debug.Log(transform.position);
 		// right sensor
 		sensorStartPosition += transform.right * frontSideSensorPosition;
 		if (Physics.Raycast(sensorStartPosition,transform.forward,out hit , sensorLength))
 		{
 			if (!hit.collider.CompareTag("Terrain"))
 			{
 				Debug.DrawLine(sensorStartPosition,hit.point);
 				avoiding = true;
 				avoidMultiplier -= 1f;
 			} 
 		}
 		// right angle sensor 
 		if (Physics.Raycast(sensorStartPosition,Quaternion.AngleAxis(frontSensorAngle,transform.up)* transform.forward,out hit , sensorLength))
 		{
 			if (!hit.collider.CompareTag("Terrain"))
 			{
 				Debug.DrawLine(sensorStartPosition,hit.point);
 				avoiding = true;
 				avoidMultiplier -= 0.5f;
 			} 
 		}

 		// left sensor
 		sensorStartPosition -= transform.right * frontSideSensorPosition *2;
 		if (Physics.Raycast(sensorStartPosition,transform.forward,out hit , sensorLength))
 		{
 			if (!hit.collider.CompareTag("Terrain"))
 			{
 				Debug.DrawLine(sensorStartPosition,hit.point);
 				avoiding = true;
 				avoidMultiplier += 1f;
 			} 
 		}
 		// left angle sensor 
 		if (Physics.Raycast(sensorStartPosition,Quaternion.AngleAxis(-frontSensorAngle,transform.up)* transform.forward,out hit , sensorLength))
 		{
 			if (!hit.collider.CompareTag("Terrain"))
 			{
 				Debug.DrawLine(sensorStartPosition,hit.point);
 				avoiding = true;
 				avoidMultiplier += 0.5f;
 			}

 		}
 		if (avoiding)
 		// { 
 		// 	   leftSteer = maxSteerAngle * avoidMultiplier;

 		// }
 		{
		   Vector3 relativePos = nodes[currentNode1].transform.position - transform.position;
		   Quaternion rotation = Quaternion.LookRotation(relativePos);
		   transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
		  }
		  // Enemy translate in forward direction.
		  // transform.Translate(Vector3.forward * Time.deltaTime * speed);
		  // //Checking for any Obstacle in front.
		  // // Two rays left and right to the object to detect the obstacle.
		  // Transform leftRay = transform;
		  // Transform rightRay = transform;
	}

private void OnCollisionEnter(Collision collision)
    {
	//Display collision on console
        Debug.Log("Collision");
    }



}
