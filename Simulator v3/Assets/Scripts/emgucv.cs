using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class emgucv : MonoBehaviour
{
    // Start is called before the first frame update
    public string serverURL = "http://localhost:80/abc";
    void Start()
    {
        /*StartCoroutine(Upload());*/
    }

    /*IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        //Debug.Log(1);
        string desktopDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
        
        Texture2D img1 = TextureOps.LoadImage(System.IO.Path.Combine(desktopDir, "Drone-Sim/Assets/imgL.jpeg"));
        Texture2D img2 = TextureOps.LoadImage(System.IO.Path.Combine(desktopDir, "Drone-Sim/Assets/imgR.jpeg"));
        //Texture2D img = new Texture2D(loadimage.width, loadimage.height, TextureFormat.BGRA32, false);
        //img.SetPixels(loadimage.GetPixels());
        byte[] bytes1 = img1.EncodeToPNG();
        byte[] bytes2 = img2.EncodeToPNG();


        form.AddBinaryData("a", bytes1);
        form.AddBinaryData("b", bytes2);

        //Debug.Log(2);

        UnityWebRequest www = UnityWebRequest.Post(serverURL, form);
        //Debug.Log(3);
        yield return www.SendWebRequest();
        //Debug.Log(4);

        if (www.result != UnityWebRequest.Result.Success)
        {
           // Debug.Log(40);
            Debug.Log(www.error);
            //Debug.Log(41);
        }
        else
        {
            // Debug.Log(42);
            byte[] final = www.downloadHandler.data;
            //System.IO.File.WriteAllBytes("abc.png", final);
            Debug.Log(final.Length );
            //Texture2D getimage = new Texture2D(img.width,img.height,TextureFormat.BGRA32,false);
            //img.LoadRawTextureData(final);
            //TextureOps.SaveImage(img, System.IO.Path.Combine(desktopDir, "saved_imggg.png"));
            //Texture2D getimg = new Texture2D(1, 1);
            //getimg.LoadImage(final);
            Debug.Log("success");
        }
        //Debug.Log(5);
    }*/
}
