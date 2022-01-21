using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasResizer : MonoBehaviour

{
    public GameObject[] canvas;
    public GameObject[] MaineCanvas;

    private void Awake()
    {

        //canvas = GameObject.FindGameObjectsWithTag("canvasResize");
        MaineCanvas = GameObject.FindGameObjectsWithTag("MainCanvas");

        bool deviceIsIpad = UnityEngine.iOS.Device.generation.ToString().Contains("iPad");

        if (deviceIsIpad)
        {

            for (int i = canvas.Length - 1; i >= 0; i--)
            {
                canvas[i].GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
            }
            MaineCanvas[0].GetComponent<CanvasScaler>().matchWidthOrHeight = 0;

        }
    }


}
