using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addvelocity : MonoBehaviour
{

    public Vector3 vel = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        vel = new Vector3(0, 0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody>().velocity = vel;
    }
}
