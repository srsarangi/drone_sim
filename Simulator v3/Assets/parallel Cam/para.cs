using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;
using System.IO;
using System;

public class para : MonoBehaviour
{
    public Shader uberReplacementShader;

    public ComputeShader myComputeShader;

    private ComputeBuffer depthBuffer;
    private int kernelHandle;
    private float[] depths= new float[256 * 256];

    public enum ReplacementMode
    {
        ObjectId = 0,
        CatergoryId = 1,
        DepthCompressed = 2,
        DepthMultichannel = 3,
        Normals = 4
    };

    // Start is called before the first frame update
  /*  void Start()
    {
        if (!uberReplacementShader)
            uberReplacementShader = Shader.Find("Hidden/UberReplacement");
        Debug.Log("start");
        // Create a compute buffer to hold the depth values
        depthBuffer = new ComputeBuffer(Screen.width * Screen.height, sizeof(float));

        // Find the kernel handle for the compute shader
        kernelHandle = myComputeShader.FindKernel("MyKernel");

        RenderTexture rt = new RenderTexture(256, 256, 24);
        this.GetComponent<Camera>().targetTexture = rt;
        Texture2D screenShot = new Texture2D(256, 256, TextureFormat.RGB24, false);
        this.GetComponent<Camera>().usePhysicalProperties = true;
        this.GetComponent<Camera>().RemoveAllCommandBuffers();
        var cb = new CommandBuffer();
        cb.SetGlobalFloat("_OutputMode", (int)ReplacementMode.DepthCompressed); // @TODO: CommandBuffer is missing SetGlobalInt() method
        this.GetComponent<Camera>().AddCommandBuffer(CameraEvent.BeforeForwardOpaque, cb);
        this.GetComponent<Camera>().AddCommandBuffer(CameraEvent.BeforeFinalPass, cb);
        this.GetComponent<Camera>().SetReplacementShader(uberReplacementShader, "");
        this.GetComponent<Camera>().backgroundColor = Color.white;
        this.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        this.GetComponent<Camera>().allowHDR = false;
        this.GetComponent<Camera>().allowMSAA = false;
        this.GetComponent<Camera>().Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        this.GetComponent<Camera>().targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        depthBuffer.SetData(depths);
        // Read the depth buffer from the camera and store it in the compute buffer
        myComputeShader.SetInt("width", 256);
        myComputeShader.SetInt("height", 256);
        myComputeShader.SetTexture(kernelHandle, "depthTexture", screenShot);
        myComputeShader.SetBuffer(kernelHandle, "depthBuffer", depthBuffer);
        myComputeShader.Dispatch(kernelHandle, Screen.width / 8, Screen.height / 8, 1);

        // Read the depth values from the compute buffer and store them in an array

        depthBuffer.GetData(depths);
        byte[] bytes = screenShot.EncodeToJPG();
        string filename = ScreenShotName(256, 256, this.gameObject.name, 25);
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(depths.Length);
        StreamWriter writetext = new StreamWriter("writeParalel"+this.gameObject.name+".txt");
        for (int i = 0; i < 256; i++)
        {
            string line = "";
            for (int j = 0; j < 256; j++)
            {
                line += (depths[j + i * 256].ToString() + " ");
            }
            writetext.WriteLine(line);           
        }
        Debug.Log(depths.Length);
    }*/

    private string ScreenShotName(int width, int height, string s, int i)
    {
        Debug.Log("screenshot" + s + i + ".jpeg");
        return string.Format("{0}/screenshot" + s + i + ".jpeg",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    private void OnDestroy()
    {
        // Release the resources
        depthBuffer.Release();
    }

    // Update is called once per frame
    void Update()
    {
        


        // Do something with the depth values
        /*for (int i = 0; i < depths.Length; i++)
        {
            float depth = depths[i].x;
            // Do something with the depth value
        }*/
    }
}
