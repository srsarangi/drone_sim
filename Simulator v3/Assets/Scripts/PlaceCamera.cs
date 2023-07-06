using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;


public class DroneNew
{
    private Tuple<int, int> midPosition;
    public int depth;
    public DroneNew(Tuple<int, int> mP, int d)
    {
        midPosition = mP;
        depth = d;
    }
}

public class PlaceCamera : MonoBehaviour
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Test2DArray
    {
        public IntPtr p2D;
        public int nRows;
        public int nCols;
    };

    [DllImport("CVINTEGRATION", EntryPoint = "TextureToCVMat")]
    public static extern void TextureToCVMat(IntPtr texData, IntPtr texData1, int width, int height, ref Test2DArray astruct);

    private Transform cameraHeadTransform;
    private GameObject monoCamera;
    private GameObject leftEyeCamera;
    private GameObject rightEyeCamera;
    public float EyeSeparation = 0.08f;
    public float NearClipPlane = 0.01f;
    public float FarClipPlane = 100.0f;
    public float FieldOfView = 180.0f;
    private bool isStereo;
    private int resWidth = 512;
    private int resHeight = 512;
    public int maxRow = 512;
    private int maxCol = 512;
    static int threshold = 1;
    
    static int pixelcountThreshold = 50;
    private List<float> initialDepth;
    public Material mat;

    static List<DroneNew> neighbours;

    static bool isSafe(int[][] depthMap, int row, int col, int n, int l, bool[,] visited)
    {
        return (row >= 0 && row < n) &&
            (col >= 0 && col < l) &&
            (depthMap[row][col] < threshold &&
            !visited[row, col]);
    }

    

    static void DFS(int[][] depthMap, int row, int col, int n, int l, List<Tuple<int, int>> list, bool[,] visited)
    {
        // These arrays are used to get row and column
        // numbers of 4 neighbours of a given cell
        int[] rowNbr = { -1, 1, 0, 0 };
        int[] colNbr = { 0, 0, 1, -1 };

        // Mark this cell as visited
        visited[row, col] = true;
        var tuple = new Tuple<int, int>(row, col);
        list.Add(tuple);

        // Recur for all connected neighbours
        for (int k = 0; k < 4; ++k)
        {
            if (isSafe(depthMap, row + rowNbr[k],
                        col + colNbr[k], n, l, visited))
            {
                DFS(depthMap, row + rowNbr[k],
                    col + colNbr[k], n, l, list, visited);
            }
        }
    }

    static List<List<Tuple<int, int>>> connectedComponents(int[][] depthMap, int n, int l)
    {
        int connectedComp = 0;
        //int l = M[0].Length;
        bool[,] visited = new bool[512, 512];
        List<List<Tuple<int, int>>> components = new List<List<Tuple<int, int>>>();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < l; j++)
            {
                if (!visited[i, j] && depthMap[i][j] < threshold)
                {
                    var list = new List<Tuple<int, int>>();
                    DFS(depthMap, i, j, n, l, list, visited);
                    if (list.Count > pixelcountThreshold)
                    {
                        components.Add(list);
                        connectedComp++;
                    }
                }
            }
        }
        return components;
        //return connectedComp;
    }

    List<float> computeDepth()
    {
        List<float> depth = new List<float>();
        Debug.Log("camerapositio is " + cameraHeadTransform.transform.position);
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        leftEyeCamera.GetComponent<Camera>().targetTexture = rt;
        //GetComponent<Camera>().targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        leftEyeCamera.GetComponent<Camera>().usePhysicalProperties = true;
        leftEyeCamera.GetComponent<Camera>().Render();
        RenderTexture.active = rt;
        Graphics.Blit(rt, rt, mat);
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        leftEyeCamera.GetComponent<Camera>().targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToJPG();
        string filename = ScreenShotName(resWidth, resHeight, "ll");
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log("positio is " + leftEyeCamera.transform.position);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));

        //RIGHT
        rt = new RenderTexture(resWidth, resHeight, 24);
        rightEyeCamera.GetComponent<Camera>().targetTexture = rt;
        rightEyeCamera.GetComponent<Camera>().usePhysicalProperties = true;
        //GetComponent<Camera>().targetTexture = rt;
        Texture2D screenShot1 = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        rightEyeCamera.GetComponent<Camera>().Render();
        RenderTexture.active = rt;
        Graphics.Blit(rt, rt, mat);
        screenShot1.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        rightEyeCamera.GetComponent<Camera>().targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        bytes = screenShot1.EncodeToJPG();
        filename = ScreenShotName(resWidth, resHeight, "rr");
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log("positio is " + rightEyeCamera.transform.position);
        //Debug.Log(string.Format("Took screenshot to: {0}", filename));

        Test2DArray s = new Test2DArray();

        s = TextureToCVMat(screenShot, screenShot1, s);

        int[][] int2DArray = new int[s.nRows][];
        for (int i = 0; i < s.nRows; i++)
            int2DArray[i] = new int[s.nCols];


        IntPtr[] ptrRows = new IntPtr[s.nRows];
        Marshal.Copy(s.p2D, ptrRows, 0, s.nRows);

        for (int i = 0; i < s.nRows; i++)
            Marshal.Copy(ptrRows[i], int2DArray[i], 0, s.nCols);


        // Free unmanaged memory allocated for columns
        for (int r = 0; r < s.nRows; r++)
            Marshal.FreeHGlobal(ptrRows[r]);

        // Free unmanaged memory allocated for rows
        Marshal.FreeHGlobal(s.p2D);


        /*int n = 512;
        int l = 512;
        List < List < Tuple<int, int> >> components = connectedComponents(int2DArray, n, l);
        int k = components.Count;
        for (int i = 0; i < components.Count; i++)
        {
            float max_depth = 0;
            int drone_row = 0;
            int drone_col = 0;
            for (int j = 0; j < components[i].Count; j++)
            {
                int myRow = components[i][j].Item1;
                int myCol = components[i][j].Item2;
                drone_row += myRow;
                drone_col += myCol;
                if (int2DArray[myRow][myCol] > max_depth)
                {
                    max_depth = int2DArray[myRow][myCol];
                }
            }
            drone_row /= components[i].Count;
            drone_col /= components[i].Count;

            float d = leftEyeCamera.GetComponent<Camera>().focalLength;
            d = (d * 2.25f) / max_depth;
            depth.Add(d);
            Debug.Log(drone_row + " " + drone_col + " " + max_depth + " distace is " + d);
        }
        Debug.Log(" k is " + k);*/
        return depth;
    }

    private void Start()
    {
        if(mat == null)
        {
            mat = new Material(Shader.Find("Hidden/DepthShader"));
        }
        // Get the transform of the CameraHead gameobject
        cameraHeadTransform = this.gameObject.transform;

        // create stereo camera setup
        leftEyeCamera = new GameObject("leftEyeCamera");
        leftEyeCamera.transform.SetParent(cameraHeadTransform.transform);
        leftEyeCamera.transform.position = cameraHeadTransform.transform.position;
        leftEyeCamera.transform.position -= new Vector3(0.9f, 0, -1.25f);
        var cameraLE = leftEyeCamera.AddComponent<Camera>();
        cameraLE.rect = new Rect(0, 0, 1, 1);
        cameraLE.fieldOfView = FieldOfView;
        cameraLE.aspect *= 2;
        cameraLE.nearClipPlane = NearClipPlane;
        cameraLE.farClipPlane = FarClipPlane;
        leftEyeCamera.SetActive(false);

        rightEyeCamera = new GameObject("rightEyeCamera");
        rightEyeCamera.transform.SetParent(cameraHeadTransform.transform);
        rightEyeCamera.transform.position = cameraHeadTransform.transform.position;
        rightEyeCamera.transform.position += new Vector3(0.9f, 0, 1.25f);
        var cameraRE = rightEyeCamera.AddComponent<Camera>();
        cameraRE.rect = new Rect(0, 0, 1, 1);
        cameraRE.fieldOfView = FieldOfView;
        cameraRE.aspect *= 2;
        cameraRE.nearClipPlane = NearClipPlane;
        cameraRE.farClipPlane = FarClipPlane;
        //rightEyeCamera.SetActive(false);
        leftEyeCamera.SetActive(true);
        rightEyeCamera.SetActive(true);

        initialDepth = computeDepth();

    }



    // Start is called before the first frame update

    private bool startit=true;
    private int counter = 0;
    private float avgDisplacement = 0f;
    private Vector3 targetposition;
    // Update is called once per frame
/*    void Update()
    {
        

        if (startit)
        {
            targetposition = this.transform.position;
            initialDepth = computeDepth();
            startit = false;
        }
        else
        {
            
                Vector3 velocity = Vector3.zero;
                //this.transform.position = Vector3.SmoothDamp(this.transform.position, this.transform.position, ref velocity, 0.1f);

                //this.transform.position = targetposition;
                List<float> depth = computeDepth();

                avgDisplacement = 0f;
                for (int i = 0; i < depth.Count; i++)
                {
                    avgDisplacement += (depth[i] - initialDepth[i]);
                }
                avgDisplacement /= depth.Count;
                if (depth.Count == 0)
                {
                    avgDisplacement = 0;
                }
                Debug.Log("avg dusp " + avgDisplacement);
                //2. take average move drone in z axis by that.
                //targetposition = this.transform.position + new Vector3(0.0f, 0.0f, avgDisplacement);
                //this.transform.position = Vector3.SmoothDamp(this.transform.position, targetposition, ref velocity, 1.0f);
                //avgDisplacement = targetposition.z - this.transform.position.z;
                this.transform.position = this.transform.position + new Vector3(0.0f, 0.0f, 8*avgDisplacement);
                //this.transform.position = targetposition;
        }
    }
*/
    unsafe Test2DArray TextureToCVMat(Texture2D texData, Texture2D texData1, Test2DArray s)
    {
        Color32[] texDataColor = texData.GetPixels32();
        Color32[] texDataColor1 = texData1.GetPixels32();
        //Pin Memory
        fixed (Color32* p = texDataColor)
        {
            //TextureToCVMat((IntPtr)p, texData.width, texData.height);
            fixed (Color32* p1 = texDataColor1)
            {
                TextureToCVMat((IntPtr)p, (IntPtr)p1, texData.width, texData.height, ref s);
            }
        }
        return s;
    }

    public static string ScreenShotName(int width, int height, string s)
    {
        return string.Format("{0}/screenshot" + s + ".jpeg",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
}




