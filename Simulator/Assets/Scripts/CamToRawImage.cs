using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamToRawImage : MonoBehaviour
{
    public GameObject[] cameras;
    public RenderTexture guiCameraRenderTexture;
 
    // Start is called before the first frame update
    void Start()
    {
       
    }
 
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            var cam0 = cameras[0].GetComponent<Camera>();
            cam0.targetTexture = guiCameraRenderTexture;
           
            cameras[1].SetActive(true);
        }
    }
}
