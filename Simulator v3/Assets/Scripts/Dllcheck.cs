using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class Dllcheck : MonoBehaviour
{

    // Import and expose native c++ functions
    /*[DllImport("OPENCVINTEGRATION", EntryPoint = "displayNumber")]
    public static extern int displayNumber();
    [DllImport("OPENCVINTEGRATION", EntryPoint = "getRandom")]
    public static extern int getRandom();
    [DllImport("OPENCVINTEGRATION", EntryPoint = "displaySum")]
    public static extern int displaySum();*/

    [DllImport("OPENCVINTEGRATION", EntryPoint = "TextureToCVMat")]
    public static extern void TextureToCVMat(IntPtr texData, int width, int height);

    [DllImport("OPENCVINTEGRATION", EntryPoint = "GetRawImageBytes")]
    private static extern void GetRawImageBytes(IntPtr data, int width, int height);

    private string desktopDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);

    private Texture2D tex;
    private Texture2D img;
    private Color32[] pixel32;

    private GCHandle pixelHandle;
    private IntPtr pixelPtr;

    unsafe void TextureToCVMat(Texture2D texData, Texture2D texData1)
    {
        Color32[] texDataColor = texData.GetPixels32();
        Color32[] texDataColor1 = texData1.GetPixels32();
        //Pin Memory
        fixed (Color32* p = texDataColor)
        {
            //TextureToCVMat((IntPtr)p, texData.width, texData.height);
            fixed (Color32* p1 = texDataColor1)
            {
                TextureToCVMat((IntPtr)p ,texData.width, texData.height);
            }
        }
    }
    // Start is called before the first frame update

    

    void InitTexture()
    {
        tex = new Texture2D(img.width, img.height, TextureFormat.BGRA32, false);
        pixel32 = tex.GetPixels32();
        //Pin pixel32 array
        pixelHandle = GCHandle.Alloc(pixel32, GCHandleType.Pinned);
        //Get the pinned address
        pixelPtr = pixelHandle.AddrOfPinnedObject();
    }

    void MatToTexture2D()
    {
        //Convert Mat to Texture2D
        GetRawImageBytes(pixelPtr, tex.width, tex.height);
        //Update the Texture2D with array updated in C++
        tex.SetPixels32(pixel32);
        tex.Apply();
        TextureOps.SaveImage(tex, System.IO.Path.Combine(desktopDir, "comeback.jpeg"));
    }

    void OnApplicationQuit()
    {
        //Free handle
        pixelHandle.Free();
    }

    void Start()
    {
        

        img = TextureOps.LoadImage(System.IO.Path.Combine(desktopDir, "Drone-Sim/Assets/screenshot.jpeg"));
        TextureToCVMat(img,img);
        InitTexture();
        MatToTexture2D();
        //Debug.Log(getRandom());
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
