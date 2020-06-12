using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;  //Since using ARFoundations we need the library
using UnityEngine.XR.ARSubsystems;  //To do an AR Raycast we need this library

public class ARTapToPlace : MonoBehaviour           //This script will instanitiate something into our world once we tap the screen on our phone
{
    public GameObject m_prefabObject; //refers to our prefab we will spawn or instantiate into our world...public allows us to drag and drop in Unity Editor

    static List<ARRaycastHit> s_hits = new List<ARRaycastHit>(); //list of touchpoints. For AR Raycasting we need a static list

    public ARRaycastManager m_arRaycastManager; //similar to raycast in VR but need this manager for AR

    //Touch points on the screen are Indexed (0,1,2,etc).  However, they only count if they are currently on the screen.  So if you put 3 fingers on the screen, one right after the
    //other, the count will be 0, then 1, then 2.  If you remove the second finger you placed on the screen the last placed finger now becomes index 1 (or the second touch point).  
    //Other information on the "touch" data type can be horizontal and vertical positions (if phone goes to landscape the horizontal and vertical will change in length), you can get the 
    //"phase" (did touch just begin, is it stationary, is it moving, did it just lift) also multi touch applications (ex: two touch points moving apart will increase their delta position
    //between them so we could use this to resize the screen accordingly

    private void Update()
    {
        if(Input.touchCount > 0) //0 means there is no finger touches on the screen currently..also some devices limit the count of touches it recognizes..so if touching the screen
        {
            Touch touch = Input.GetTouch(0); //Get a reference to the first touch to screen. Some phones restrict the number of simultaneous touch points on the screen counted

            if (m_arRaycastManager.Raycast(touch.position, s_hits, TrackableType.PlaneWithinPolygon)) //Similar to our VR Raycast..start at our touch.position (origin) and can only shoot
                                                                                                      //forward since screen is 2D (so no direction needed), the where we store info (s_hits)
                                                                                                      //just like out hit, (we are doing "s_" because it is a static variable but underscored but
                                                                                                      //because it is available throughout our script like a member variable. The last parameter
                                                                                                      //refers to our trackable type for our raycast which could be a face, or image, or feature
                                                                                                      //point, etc (we are looking for a plane with polygon or the floor that is mapped out)
                                                                                                      //So we are aksing if our AR raycast is successful getting a 
                                                                                                      //a touchposition/origin of touch on screen, list of hits, and the trackable type named.
                                                                                                      //the Planewithinpolygon is like our layer mask we are looking for
            {
                Pose hitPose = s_hits[0].pose;   //Instead of hit.point for VR, we get specific pose of a successful hit..a rotation and position, etc in 3D space
                Instantiate(m_prefabObject, hitPose.position, hitPose.rotation); //Instantiate object at this place (transform)..Rotation will usually always be x: 0 an z:0
            }

            //if (touch.phase == TouchPhase.Began)    //Example of checking the touch phase if touch just began..this is the only time we instantiate something
            //{
            //    if (m_arRaycastManager.Raycast(touch.position, s_hits, TrackableType.PlaneWithinPolygon)) //if our AR raycast is successful getting a 
            //                                                                                              //a touchposition (origin of touch on screen, list of hits, 
            //    {
            //        Pose hitPose = s_hits[0].pose;   //get specific pose..a rotation and position in 3D space
            //        Instantiate(m_prefabObject, hitPose.position, hitPose.rotation);
            //    }
            //}  
        }
    }
}
