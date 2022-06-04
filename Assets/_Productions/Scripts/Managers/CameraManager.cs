using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public ThirdPersonCam thirdPersonCam;
    public Camera mainCam;
    
    public Vector3 forward { get; set; }

    private void Update()
    {
        if (mainCam != null)
        {
            forward = mainCam.transform.TransformDirection(Vector3.forward);
        }
    }

    public void SetTarget(Transform target)
    {
        thirdPersonCam.player = target;
        thirdPersonCam.Initialize();
    }
}
