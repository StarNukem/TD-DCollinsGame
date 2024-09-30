using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera newCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (newCamera != null)
            {
                Camera[] allCameras = Camera.allCameras;
                foreach (Camera cam in allCameras)
                {
                    if (cam.enabled)
                    {
                        cam.enabled = false;
                    }
                }

                newCamera.enabled = true;
            }
        }
    }
}