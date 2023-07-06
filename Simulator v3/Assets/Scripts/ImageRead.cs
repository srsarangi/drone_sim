using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageRead : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        string desktopDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
        Texture2D loadedImage = TextureOps.LoadImage(System.IO.Path.Combine(desktopDir, "Drone-Sim/Assets/screen_2550x3300_2022-09-29_02-25-04.png"));
        if (loadedImage != null)
        {
            Texture2D scaledImage = TextureOps.ScaleFill(loadedImage, 512, 512, Color.red);
            if (scaledImage != null)
                Debug.Log("Save result: " + TextureOps.SaveImage(scaledImage, System.IO.Path.Combine(desktopDir, "image_new.jpeg")));

            // Destroy procedural textures that are not needed anymore (otherwise, they'll continue consuming memory)
            DestroyImmediate(loadedImage);
            DestroyImmediate(scaledImage);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
