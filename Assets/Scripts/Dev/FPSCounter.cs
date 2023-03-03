using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public static float fps;
    public TMP_Text text;
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    void OnGUI()
    {
        fps = 1.0f / Time.deltaTime;
        text.text = "FPS: " + (int)fps;
    }
}
