using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public ThirdPersonCam thirdPersonCam;

    public void SetTarget(Transform target)
    {
        thirdPersonCam.player = target;
        thirdPersonCam.Initialize();
    }
}
