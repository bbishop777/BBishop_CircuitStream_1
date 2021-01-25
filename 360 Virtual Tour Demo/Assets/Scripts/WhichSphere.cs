using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class WhichSphere : MonoBehaviour
{
    public Camera theCamera;

    public GameObject myVideoPlane;

    public VideoPlayer myPlayer;

    private string videoURL = "";

    private GameObject currentSphere;

    private SmoothMouseLook camScript;

    private Transform focalPoint;

    public float m_turnSpeed = 4f;  //To help smooth out the speed of the turn when an agent is detecting a target to follow
    public float m_agroDistance = 10f; //Aggravation distance...a radius around agent that detects or aggravates agent that target is within radius

    private bool isTurning = false;
   // public Transform m_target;  //Our target or player...we just need the transform position so no need to reference entire gameObject

   


    void Awake()
    {
        myVideoPlane.SetActive(false);
        myPlayer.enabled = false;
        camScript = theCamera.GetComponent<SmoothMouseLook>();
    }

    public void updateSphere (GameObject newSphere)
    {
      
        currentSphere = newSphere;
        if (currentSphere.tag == "POI")
        {
            if(currentSphere.name == "Sphere 2")
            {
                videoURL = "https://storage.googleapis.com/rcuh-test-bf133.appspot.com/SloRollIntro.webm";
                myPlayer.url = videoURL;
                myVideoPlane.SetActive(true);
                myPlayer.enabled = true;

            }
            else if (currentSphere.name == "Sphere 7")
            {
                videoURL = "https://storage.googleapis.com/rcuh-test-bf133.appspot.com/SloRollFittingArea.webm";
                myPlayer.url = videoURL;
                myVideoPlane.SetActive(true);
                myPlayer.enabled = true;
            }
           
            //StartCoroutine(TurnCamera());
            //if (Vector3.Distance(focalPoint.position, theCamera.transform.position) < m_agroDistance) { }
            //focalPoint = currentSphere.transform.Find("FocalPoint");
            
            
        }
        else
        {
            myVideoPlane.SetActive(false);
            myPlayer.enabled = false;
            camScript.enabled = true;
        }
    }

    IEnumerator TurnCamera()
    {
        yield return new WaitForSeconds(1.0f);
        camScript.enabled = false;
        theCamera.transform.rotation = Quaternion.LookRotation(new Vector3(1f, 0f, 0f));
        isTurning = true;
        Debug.Log("did this happen? What is camera transform? " + theCamera.transform.rotation);
        
    }
    private void Update()
    {
        if (isTurning)
        {
            //    //Vector3 dir = focalPoint.position - theCamera.transform.position;
            //    //dir.Normalize();
            //    //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, dir.z));
            //    //theCamera.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * m_turnSpeed);
            Quaternion fullturn = Quaternion.LookRotation(new Vector3(-1f, 0f, 0f));
            theCamera.transform.rotation = Quaternion.Slerp(theCamera.transform.rotation, fullturn, Time.deltaTime * m_turnSpeed);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("What is the tag? " + other.tag);
    //    if (other.tag == "POI")
    //    {
    //        Debug.Log("are you here?");
    //        Transform focalPoint = other.transform.Find("FocalPoint");
    //        camScript.enabled = false;
    //        theCamera.transform.rotation = focalPoint.rotation;
    //    }
    //    else
    //    {
    //        camScript.enabled = true;
    //    }
    //}
}
