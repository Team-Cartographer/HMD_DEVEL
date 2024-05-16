using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using UnityEngine;

public class FlareRemoval : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera;
    float lastTime;
    int ctr = 0;

    void Start()
    {
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastTime < 5) { return; }

        
        lastTime = Time.time;

        Rect rect = new Rect(0, 0, mainCamera.pixelWidth, mainCamera.pixelHeight);
        RenderTexture renderTexture = new RenderTexture(mainCamera.pixelWidth, mainCamera.pixelHeight, 24);
        Texture2D screenShot = new Texture2D(mainCamera.pixelWidth, mainCamera.pixelHeight, TextureFormat.RGBA32, false);

        mainCamera.targetTexture = renderTexture;
        mainCamera.Render();

        RenderTexture.active = renderTexture;

        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();


        mainCamera.targetTexture = null;
        RenderTexture.active = null;

        byte[] arr = new byte[screenShot.height * screenShot.width];

        int ctr = 0;

        for (int i = 0; i < screenShot.height; i++)
        {
            for (int j = 0; j < screenShot.width; j++)
            {
                Color p = screenShot.GetPixel(i, j);

                arr[ctr] = (byte) Convert.ToInt32(0.2126 * p.r + 0.7152 * p.g + 0.0722 * p.b * 255);
                ctr++;
            }
        }

        Debug.Log(screenShot.height);
        Debug.Log(screenShot.width);
        ctr += 1;
    }
}
