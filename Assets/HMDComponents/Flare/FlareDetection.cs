using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using TMPro;
using Unity.Barracuda;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlareDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera;
    public NNModel modelAsset;
    public Canvas textCanvas;

    public bool isEnabled;

    float lastTime;

    private Model m_RuntimeModel;
    private IWorker worker; 


    void Start()
    {
        m_RuntimeModel = ModelLoader.Load(modelAsset);

        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.CSharp, m_RuntimeModel);

        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastTime < 3 || !isEnabled) { return; }

        
        lastTime = Time.time;

        Rect rect = new Rect(0, 0, mainCamera.pixelWidth, mainCamera.pixelHeight);
        RenderTexture renderTexture = new RenderTexture(mainCamera.pixelWidth, mainCamera.pixelHeight, 24);

        Texture2D screenShot = new Texture2D(mainCamera.pixelWidth, mainCamera.pixelHeight, TextureFormat.RGB24, false);

        mainCamera.targetTexture = renderTexture;
        mainCamera.Render();

        RenderTexture.active = renderTexture;

        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();


        mainCamera.targetTexture = null;
        RenderTexture.active = null;

        Tensor tensor = new Tensor(screenShot);
        worker.Execute(tensor);

        Tensor output = worker.PeekOutput();

        float flare = softmax(output[0], output[1]);
        float nonflare = softmax(output[1], output[0]);
        
        if (flare >= 0.6)
        {
            textCanvas.enabled = true;
        }
        else
        {
            textCanvas.enabled = false;
        }
    }
    float softmax(float x, float y)
    {
        float ex = Mathf.Exp(x);
        return ex / (ex + Mathf.Exp(y));
    }

    public void Enable()
    {
        isEnabled = true;
    }

    public void Disable() 
    { 
        isEnabled = false; 
    }
}
