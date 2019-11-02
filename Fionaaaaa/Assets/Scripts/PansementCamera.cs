using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PansementCamera : MonoBehaviour
{
    public Camera outlineCamera;
    UTimer timerPansementCamera;

    // Start is called before the first frame update
    void Start()
    {
        timerPansementCamera = UTimer.Initialize(0.1f, this, pansementCamera);
        timerPansementCamera.start();
    }

    private void pansementCamera()
    {
        outlineCamera.enabled = true;
    }
}
