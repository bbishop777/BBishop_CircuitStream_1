using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SpatialTracking;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
       // XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale);

        if(Application.platform == RuntimePlatform.Android)
        {
            OVRManager.fixedFoveatedRenderingLevel = OVRManager.FixedFoveatedRenderingLevel.Low;
        }
    }

    
}
