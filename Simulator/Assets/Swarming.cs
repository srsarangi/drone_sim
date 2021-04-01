﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarming : MonoBehaviour
{
     private Transform myDrone;
   void Awake(){
   	myDrone= GameObject.FindGameObjectWithTag("RedDrone").transform;
   }
   private Vector3 velocityCameraFollow;
   public Vector3 behindPosition = new Vector3 (3,2,-6);
   public float angle;

   void FixedUpdate(){

   	transform.position = Vector3.SmoothDamp(transform.position, myDrone.transform.TransformPoint(behindPosition)+ Vector3.up * Input.GetAxis("Vertical"), ref velocityCameraFollow, 0.1f);
   	// transform.rotation = Quaternion.Euler(new Vector3 (angle, myDrone.GetComponent<DroneMovementScript>().correntYRotation, 0));
   }
}
