using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARFTapToMove : MonoBehaviour //Alternative script to the script "ARFTapToPlace" the first was to tap to create clones this script will take subsequent taps to move prefab
{
    public GameObject m_prefabObject; //refers to our prefab we will spawn into our world

    private Transform m_placedTransform; //we need a reference to the existing prefab's transform

    static List<ARRaycastHit> s_hits = new List<ARRaycastHit>(); //list of touchpoints

    public ARRaycastManager m_arRaycastManager; //similar to raycast in VR ..if you didn't want to drag this in could do it in the Awake function like so:
    //private void Awake()
    //{
    //    m_arRaycastManager = GetComponent<ARRaycastManager>();
    //}
    private void Update()
    {
        if (Input.touchCount > 0) //0 means there is no finger touches on the screen currently..also some devices limit the count of touches it recognizes
        {
            Touch touch = Input.GetTouch(0); //Get a reference to the first touch to screen

            if (m_arRaycastManager.Raycast(touch.position, s_hits, TrackableType.PlaneWithinPolygon)) //if our AR raycast is successful getting a 
                                                                                                      //a touchposition (origin of touch on screen, list of hits, 
            {
                Pose hitPose = s_hits[0].pose;   //get specific pose..a rotation and position in 3D space
                if(!m_placedTransform) //if m_placedTransform is null or doesn't exist then we instantiate below
                {
                    m_placedTransform = Instantiate(m_prefabObject, hitPose.position, hitPose.rotation).transform; //We can instantiate and store our prefab in one line..also by adding
                                                                                                                   //.transform to the end of our instantiate, we can return just the transform 
                                                                                                                   //of the gameObject we spawned in (which we need to do since the var holder
                                                                                                                   //is of the data type transform

                }
                else  //So if the prefab already exists we just want to move it
                {
                    m_placedTransform.position = hitPose.position; 
                }
            }
        }
    }
}
