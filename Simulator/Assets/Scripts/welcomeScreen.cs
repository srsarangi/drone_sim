using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class welcomeScreen : MonoBehaviour
{
   public void LoadManualFlight ()
   {
   	SceneManager.LoadScene("ManualSwarm");

   }
   public void LoadAutoSwarm()
   {
   	SceneManager.LoadScene("AutoSwarm");

   }
    public void LoadFollowTheLeader()
   {
   	SceneManager.LoadScene("FollowLeaderSwarm");

   }
     public void IndipendentPath()
   {
    SceneManager.LoadScene("navScene 2");

   }
    public void SinglePath()
   {
   	SceneManager.LoadScene("NavSceneCity");

   }
    public void CreditsScreen()
   {
   	SceneManager.LoadScene("CreditsScreen");

   }
   public void CircuitMovement()
   {
   	SceneManager.LoadScene("1.5_Circuit_In_Environment");

   }
    public void DroneCamera()
   {
   	SceneManager.LoadScene("DroneCamera");

   }
    public void Credits()
   {
   	SceneManager.LoadScene("CreditsScreen");

   }

}
