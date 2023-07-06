using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NewReynoldsRule_Variable : MonoBehaviour
{
    GameObject[] allDrones;
    GameObject[] leaderDrones;
    GameObject[] followerDrones;
    GameObject leader;
    public float multiplier = 0.5f;
    public Vector3 migrationPoint = Vector3.zero;
    public float r1 = 1f;
    public float r2 = 1.5f;
    public float r3 = 1f;
    public float r4 = 1f;
    public float r5 = 1f;
    public float avoid_speed = 3.0f;
    private int c = 0;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        leaderDrones = GameObject.FindGameObjectsWithTag("Leader");
        followerDrones = GameObject.FindGameObjectsWithTag("Drone");
        allDrones = new GameObject[leaderDrones.Length + followerDrones.Length];
        leaderDrones.CopyTo(allDrones, 0);
        followerDrones.CopyTo(allDrones, leaderDrones.Length);
        leader = leaderDrones[0];
    }

    void FixedUpdate()
    {
        //Rule 1 : Seperation
        Vector3 seperation = Vector3.zero;
        foreach (GameObject g in allDrones)
        {
            if (g != gameObject)
            {
                Vector3 direction = this.transform.position - g.transform.position;
                direction.Normalize();
                float distance = Vector3.Distance(g.transform.position, this.transform.position);
                direction = direction / distance;
                seperation += direction;
            }
        }

        //Rule 2 : Alignment
        Vector3 alignment = Vector3.zero;
        int neighbours = 0;
        foreach (GameObject g in allDrones)
        {
            if (g != gameObject)
            {
                Vector3 vel = g.GetComponent<Rigidbody>().velocity;
                neighbours += 1;
                alignment += vel;
            }
        }
        alignment /= neighbours;

        //Rule 3 : Cohesion
        Vector3 cohesion = Vector3.zero;
        foreach (GameObject g in allDrones)
        {
            if (g != gameObject)
            {
                Vector3 vel = g.transform.position;
                cohesion += vel;
            }
        }
        cohesion /= neighbours;

        //Rule 4 : Migration
        Vector3 migration = Vector3.zero;
        migration = leader.transform.position - this.transform.position;

        counter += 1;
        if(counter == 10)
        {
            //Agar abhi ka hit point aur purane hit point me zyada difference nahi hai to stop it where it is
            counter = 0;
        }
        float[] detection = Sensors();
        float val = detection[0];
        float obsext_at_left = detection[1];
        float obsext_at_right = detection[2];
        float minHitDistance = detection[3];
        float front = val % 10;
        val = val / 10;
        float right = val % 10;
        val = val / 10;
        float left = val % 10;
        val = val / 10;
        float bottom = val % 10;
        val = val / 10;
        float back = val % 10;

        Vector3 obstacleAvoidance = Vector3.zero;
        if (front.Equals((float)1))
        {
            if (obsext_at_left.Equals((float)1) && obsext_at_right.Equals((float)1))
            {
                //if the obstacle extends on both sides go right
                //obstacleAvoidance.x = this.transform.position.x;
                obstacleAvoidance.x += 1;

            }
            else if (obsext_at_left.Equals((float)1))
            {
                //obstacleAvoidance.x = this.transform.position.x;
                obstacleAvoidance.x += 1;
            }
            else if (obsext_at_right.Equals((float)1))
            {
                //obstacleAvoidance.x = this.transform.position.x;
                obstacleAvoidance.x -= 1;
            }
            else
            {
                //if the obstacle doesn't extend on both the sides go right
                //obstacleAvoidance.x = this.transform.position.x;
                obstacleAvoidance.x += 1;
            }
        }
        if (back.Equals((float)1))
        {
            if (obsext_at_left.Equals((float)1) && obsext_at_right.Equals((float)1))
            {
                //if the obstacle extends on both sides go right
                //obstacleAvoidance.x = this.transform.position.x;
                obstacleAvoidance.x += 1;

            }
            else if (obsext_at_left.Equals((float)1))
            {
                //obstacleAvoidance.x = this.transform.position.x;
                obstacleAvoidance.x += 1;
            }
            else if (obsext_at_right.Equals((float)1))
            {
                //obstacleAvoidance.x = this.transform.position.x;
                obstacleAvoidance.x -= 1;
            }
            else
            {
                //if the obstacle doesn't extend on both the sides go right
                //obstacleAvoidance.x = this.transform.position.x;
                obstacleAvoidance.x += 1;
            }
        }
        if (right.Equals((float)1))
        {
            //obstacleAvoidance.x = this.transform.position.x;
            obstacleAvoidance.x -= 2;
        }
        if (left.Equals((float)1))
        {
            //obstacleAvoidance.x = this.transform.position.x;
            obstacleAvoidance.x += 2;
        }
        /*if (bottom.Equals((float)1))
        {
            //obstacleAvoidance.y = this.transform.position.y;
            obstacleAvoidance.y += 1;
        }*/

        //Debug.Log(obstacleAvoidance.x + " " + obstacleAvoidance.y + " " + obstacleAvoidance.z);
        //Final velocity
        Vector3 droneVel = Vector3.zero;
        if (obstacleAvoidance == Vector3.zero)
        {
            if (minHitDistance != 0)
            {
                droneVel = r1 * seperation + r2 * alignment + r3 * cohesion + r4 * migration + r5 * (obstacleAvoidance);
            }
            else
            {
                droneVel = r1 * seperation + r2 * alignment + r3 * cohesion + r4 * migration + r5 * obstacleAvoidance;
            }

        }
        else //Obstacle hai to avoid
        {
            if (minHitDistance != 0)
            {
                //droneVel = r5 * ((obstacleAvoidance * sensorLength) / minHitDistance );
                droneVel = r1 * seperation + r2 * alignment + r3 * cohesion + r4 * migration + r5 * ((obstacleAvoidance * sensorLength) / minHitDistance);
            }
            else
            {
                //droneVel = r5 * obstacleAvoidance;
                droneVel = r1 * seperation + r2 * alignment + r3 * cohesion + r4 * migration + r5 * obstacleAvoidance;
            }
        }
        //droneVel = r1 * seperation + r2 * alignment + r3 * cohesion + r4 * migration;
        
        this.GetComponent<Rigidbody>().velocity = multiplier * droneVel;

    }

    [Header("Sensors")]
    public float sensorLength = 3f;
    public float frontSensorPosition = 1f;
    public float frontSideSensorPosition = 1f;
    public float frontSensorAngle = 30;
    public float sideSensorPosition = 1f;

    public Vector3 bottomSensorPosition = new Vector3(0f, -1.0f, 0f);

    private bool avoiding = false;

    float[] Sensors()
    {
        float[] result = new float[4];
        RaycastHit hit;
        Vector3 sensorStartPosition;
        int res = 0;
        int obsext_at_left = 0;
        int obsext_at_right = 0;
        int sensor_val;
        float hitDistances = int.MaxValue;


        ///FRONT SENSORS
        sensorStartPosition = transform.position;
        //Front mid sensor
        //sensorStartPosition.z += frontSensorPosition;
        sensorStartPosition += Vector3.forward * frontSensorPosition;
        if (Physics.Raycast(sensorStartPosition, Vector3.forward, out hit, sensorLength))
        {
            if (!(hit.collider.CompareTag("Drone") || hit.collider.CompareTag("Leader")))
            {
                res = 1;
                Debug.DrawLine(sensorStartPosition, hit.point, Color.red);
                hitDistances = hitDistances > hit.distance ? hit.distance : hitDistances;
                
            }
        }
        //Front right sensor
        //sensorStartPosition.x += (sideSensorPosition + 0.3f);
        sensorStartPosition += Vector3.right * (sideSensorPosition + 0.3f);
        if (Physics.Raycast(sensorStartPosition, Vector3.forward, out hit, sensorLength))
        {
            if (!(hit.collider.CompareTag("Drone") || hit.collider.CompareTag("Leader")))
            {
                res = 1;
                Debug.DrawLine(sensorStartPosition, hit.point, Color.red);
                hitDistances = hitDistances > hit.distance ? hit.distance : hitDistances;
            }
        }
        //Front right angle sensor
        if (Physics.Raycast(sensorStartPosition, Quaternion.AngleAxis(frontSensorAngle, Vector3.up) * Vector3.forward, out hit, sensorLength))
        {
            if (!(hit.collider.CompareTag("Drone") || hit.collider.CompareTag("Leader")))
            {
                obsext_at_right = 1;
                Debug.DrawLine(sensorStartPosition, hit.point, Color.red);
                hitDistances = hitDistances > hit.distance ? hit.distance : hitDistances;
            }
        }
        //Front left sensor
        //sensorStartPosition.x -= (2 * (sideSensorPosition + 0.3f));
        sensorStartPosition -= Vector3.right * (2 * (sideSensorPosition + 0.3f));
        if (Physics.Raycast(sensorStartPosition, Vector3.forward, out hit, sensorLength))
        {
            if (!(hit.collider.CompareTag("Drone") || hit.collider.CompareTag("Leader")))
            {
                res = 1;
                Debug.DrawLine(sensorStartPosition, hit.point, Color.red);
                hitDistances = hitDistances > hit.distance ? hit.distance : hitDistances;
            }
        }
        //Front left angle sensor
        if (Physics.Raycast(sensorStartPosition, Quaternion.AngleAxis(-frontSensorAngle, Vector3.up) * Vector3.forward, out hit, sensorLength))
        {
            if (!(hit.collider.CompareTag("Drone") || hit.collider.CompareTag("Leader")))
            {
                obsext_at_left = 1;
                Debug.DrawLine(sensorStartPosition, hit.point, Color.red);
                hitDistances = hitDistances > hit.distance ? hit.distance : hitDistances;
            }
        }
        sensor_val = res;
        res = 0;

        ///LEFT SENSORS
        sensorStartPosition = transform.position;
        //Left mid sensor
        //sensorStartPosition.x -= frontSensorPosition;
        sensorStartPosition -= Vector3.right * frontSensorPosition;
        if (Physics.Raycast(sensorStartPosition, Vector3.left, out hit, sensorLength))
        {
            if (!(hit.collider.CompareTag("Drone") || hit.collider.CompareTag("Leader")))
            {
                res = 100;
                Debug.DrawLine(sensorStartPosition, hit.point, Color.red);
                hitDistances = hitDistances > hit.distance ? hit.distance : hitDistances;
            }
        }

        // left front sensor
        //sensorStartPosition.z += (sideSensorPosition + 0.3f);
        sensorStartPosition += Vector3.forward * (sideSensorPosition + 0.3f);
        if (Physics.Raycast(sensorStartPosition, Vector3.left, out hit, sensorLength))
        {
            if (!(hit.collider.CompareTag("Drone") || hit.collider.CompareTag("Leader")))
            {
                res = 100;
                Debug.DrawLine(sensorStartPosition, hit.point, Color.red);
                hitDistances = hitDistances > hit.distance ? hit.distance : hitDistances;
            }
        }

        //left rear sensor
        //sensorStartPosition.z -= (2 * (sideSensorPosition + 0.3f));
        sensorStartPosition -= Vector3.forward * (2 * (sideSensorPosition + 0.3f));
        if (Physics.Raycast(sensorStartPosition, Vector3.left, out hit, sensorLength))
        {
            if (!(hit.collider.CompareTag("Drone") || hit.collider.CompareTag("Leader")))
            {
                res = 100;
                Debug.DrawLine(sensorStartPosition, hit.point, Color.red);
                hitDistances = hitDistances > hit.distance ? hit.distance : hitDistances;
            }
        }
        sensor_val += res;
        res = 0;

        ///RIGHT SENSORS
        sensorStartPosition = transform.position;
        //right mid sensor
        //sensorStartPosition.x += frontSensorPosition;
        sensorStartPosition += Vector3.right * frontSensorPosition;
        if (Physics.Raycast(sensorStartPosition, Vector3.right, out hit, sensorLength))
        {
            if (!(hit.collider.CompareTag("Drone") || hit.collider.CompareTag("Leader")))
            {
                res = 10;
                Debug.DrawLine(sensorStartPosition, hit.point, Color.red);
                hitDistances = hitDistances > hit.distance ? hit.distance : hitDistances;
            }
        }

        // right Front sensor
        //sensorStartPosition.z += (sideSensorPosition + 0.3f);
        sensorStartPosition += Vector3.forward * (sideSensorPosition + 0.3f);
        if (Physics.Raycast(sensorStartPosition, Vector3.right, out hit, sensorLength))
        {
            if (!(hit.collider.CompareTag("Drone") || hit.collider.CompareTag("Leader")))
            {
                res = 10;
                Debug.DrawLine(sensorStartPosition, hit.point, Color.red);
                hitDistances = hitDistances > hit.distance ? hit.distance : hitDistances;
                Debug.Log(hit.point);
                
            }
        }

        //right rear sensor
        //sensorStartPosition.z -= (2 * (sideSensorPosition + 0.3f));
        sensorStartPosition -= Vector3.forward * (2 * (sideSensorPosition + 0.3f));
        if (Physics.Raycast(sensorStartPosition, Vector3.right, out hit, sensorLength))
        {
            if (!(hit.collider.CompareTag("Drone") || hit.collider.CompareTag("Leader")))
            {
                res = 10;
                Debug.DrawLine(sensorStartPosition, hit.point, Color.red);
                hitDistances = hitDistances > hit.distance ? hit.distance : hitDistances;
            }
        }
        sensor_val += res;
        res = 0;

        ///BACK Sensor
        //int obsext_at_back = 0;
        sensorStartPosition = transform.position;
        //back mid sensor
        //sensorStartPosition.z += frontSensorPosition;
        sensorStartPosition += Vector3.back * frontSensorPosition;
        if (Physics.Raycast(sensorStartPosition, Vector3.back, out hit, sensorLength))
        {
            if (!(hit.collider.CompareTag("Drone") || hit.collider.CompareTag("Leader")))
            {
                res = 10000;
                Debug.DrawLine(sensorStartPosition, hit.point, Color.red);
                hitDistances = hitDistances > hit.distance ? hit.distance : hitDistances;
            }
        }

        //back right sensor
        //sensorStartPosition.x += (sideSensorPosition + 0.3f);
        sensorStartPosition += Vector3.right * (sideSensorPosition + 0.3f);
        if (Physics.Raycast(sensorStartPosition, Vector3.back, out hit, sensorLength))
        {
            if (!(hit.collider.CompareTag("Drone") || hit.collider.CompareTag("Leader")))
            {
                res = 10000;
                Debug.DrawLine(sensorStartPosition, hit.point, Color.red);
                hitDistances = hitDistances > hit.distance ? hit.distance : hitDistances;
            }
        }

        //back right angle sensor
        if (Physics.Raycast(sensorStartPosition, Quaternion.AngleAxis(-frontSensorAngle, Vector3.up) * Vector3.back, out hit, sensorLength))
        {
            if (!(hit.collider.CompareTag("Drone") || hit.collider.CompareTag("Leader")))
            {
                obsext_at_right = 1;
                Debug.DrawLine(sensorStartPosition, hit.point, Color.red);
                hitDistances = hitDistances > hit.distance ? hit.distance : hitDistances;
            }
        }

        //back left sensor
        //sensorStartPosition.x -= (2 * (sideSensorPosition + 0.3f));
        sensorStartPosition -= Vector3.right * (2 * (sideSensorPosition + 0.3f));
        if (Physics.Raycast(sensorStartPosition, Vector3.back, out hit, sensorLength))
        {
            if (!(hit.collider.CompareTag("Drone") || hit.collider.CompareTag("Leader")))
            {
                res = 10000;
                Debug.DrawLine(sensorStartPosition, hit.point, Color.red);
                hitDistances = hitDistances > hit.distance ? hit.distance : hitDistances;
            }
        }

        //back left angle sensor
        if (Physics.Raycast(sensorStartPosition, Quaternion.AngleAxis(frontSensorAngle, Vector3.up) * Vector3.back, out hit, sensorLength))
        {
            if (!(hit.collider.CompareTag("Drone") || hit.collider.CompareTag("Leader")))
            {
                obsext_at_left = 1;
                Debug.DrawLine(sensorStartPosition, hit.point, Color.red);
                hitDistances = hitDistances > hit.distance ? hit.distance : hitDistances;
            }
        }

        sensor_val += res;
        result[0] = sensor_val;
        result[1] = obsext_at_left;
        result[2] = obsext_at_right;
        result[3] = hitDistances;

        return result;

    }
}
