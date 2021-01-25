using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoLockView : MonoBehaviour
{
    public Camera myCamera;

    public Transform focalPoint; 

    private SmoothMouseLook camScript;

    void Awake()
    {
        camScript = myCamera.GetComponent<SmoothMouseLook>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = myCamera.WorldToScreenPoint(focalPoint.position);
        if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
        {
            camScript.enabled = false;
            myCamera.transform.rotation = new Quaternion(0, 90, 0, 0);
        }
        else
        {
            camScript.enabled = true;
        }

    }
}
