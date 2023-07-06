using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HiResScreenShots : MonoBehaviour
{
    public int resWidth = 512;
    public int resHeight = 512;

    

    private bool takeHiResShot = false;
    public int i = 0;
    public static string ScreenShotName( string i)
    {
        return "screenshot" + i + ".jpeg";
    }

    public void TakeHiResShot()
    {
        takeHiResShot = true;
    }

    void LateUpdate()
    {
        takeHiResShot |= Input.GetKeyDown("k");
        if (takeHiResShot)
        {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
            GetComponent<Camera>().targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight);
            GetComponent<Camera>().Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            GetComponent<Camera>().targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToJPG();
            string filename = ScreenShotName((this.transform.parent.name + this.name));
            i++;
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
            takeHiResShot = true;
            Debug.Log(screenShot.GetPixel(10, 4));



        }
    }
}
