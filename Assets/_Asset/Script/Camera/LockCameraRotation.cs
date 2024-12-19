using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LockCameraRotation : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    private void LateUpdate()
    {
        if (virtualCamera != null)
        {
            virtualCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
