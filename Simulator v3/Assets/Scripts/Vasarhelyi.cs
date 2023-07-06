using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vasarhelyi : MonoBehaviour
{

    GameObject[] followerDrones;
    GameObject leader;
    public float r;
    public float Crep;
    public float vfric;
    public float d0_fric;
    public float afric;
    public float pfric;
    public float Cfric;
    public float Vflock;

    // Start is called before the first frame update
    void Start()
    {
        leader = GameObject.FindGameObjectsWithTag("Leader")[0];
        followerDrones = GameObject.FindGameObjectsWithTag("Drone");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rep = get_rep();
        Debug.Log(rep.x + " x " + rep.z );
        Vector3 fric = get_fric();
        Debug.Log(fric.x + " y " + fric.z);
        Vector3 flock = get_flock();
        Debug.Log(flock.x + " z " + flock.z);
        this.GetComponent<Rigidbody>().velocity = (rep + fric + flock);
    }

    float cal_d(float dij, float a, float c)
    {
        if(dij < 0)
        {
            return 0;
        }
        if((dij * c)>0 && (dij * c) < (a / c))
        {
            return (dij * c);
        }
        return Mathf.Sqrt((2 * a * dij - (a * a) / (c * c)));
    }

    Vector3 get_rep()
    {
        Vector3 rep = Vector3.zero;
        foreach (GameObject g in followerDrones)
        {
            if (g != gameObject)
            {
                Vector3 pij = this.transform.position - g.transform.position;
                float dij = Vector3.Distance(g.transform.position, this.transform.position);
                rep += (Crep * (r - dij) * (pij / dij));
            }
        }
        return rep;
    }

    Vector3 get_fric()
    {
        Vector3 fric = Vector3.zero;
        foreach(GameObject g in followerDrones)
        {
            if (g != gameObject)
            {
                Vector3 vij = this.GetComponent<Rigidbody>().velocity - g.GetComponent<Rigidbody>().velocity;
                float vmod = Vector3.Magnitude(this.GetComponent<Rigidbody>().velocity - g.GetComponent<Rigidbody>().velocity);
                float dij = Vector3.Distance(g.transform.position, this.transform.position);
                float vij_fric = Mathf.Max(vfric, cal_d(dij - d0_fric, afric, pfric));
                fric += (vmod > vij_fric ?( Cfric * (vmod-vij_fric) * (vij/vmod)) : Vector3.zero);
            }
        }
        return fric;
    }

    Vector3 get_flock()
    {
        //float mag = Vector3.Magnitude(GetComponent<Rigidbody>().velocity)  <=0? 1 : Vector3.Magnitude(GetComponent<Rigidbody>().velocity);
        Vector3 flock = Vflock * (this.GetComponent<Rigidbody>().velocity) / Vector3.Magnitude(GetComponent<Rigidbody>().velocity);
        return flock;
    }
}
