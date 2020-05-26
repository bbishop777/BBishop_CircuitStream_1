using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script is to be used specifically for a VR controller using an axis based (trigger) interface.
public class VRHand : MonoBehaviour
{
    public Animator m_anim;
    public string m_gripName;               //This is part of flagging process (see below).
    public string m_triggerName;    //We will get this from our Axis 

    //Best practice is to separate public and private variables
    private bool m_triggerHeld;     //This will be a flag
    private bool m_gripHeld;                //This is are boolean for our flag (see below).
    private GameObject m_touchingObject;
    private GameObject m_heldObject;

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Rigidbody>())
        {
            m_touchingObject = other.gameObject; //other.gameObject is the object we are touching
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_touchingObject = null;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis(m_gripName) > 0.5f && !m_gripHeld)  //Due to working with an axis (variable degrees of input) we need to create a flag. A flag is a
                                                            //basically a boolean variable set to true or false based on conditions. If initial conditions are
                                                            //met, we turn it on.  Each input is checked again only to see if the conditions to set the flag
                                                            //to false are met, otherwise are flag remains true.  So here instead of getting a button keycode
                                                            //we will obtain a string variable (m_gripName). GetAxis requires a string...it also returns a
                                                            //float between 0 and 1 so we will require are flag to be over .5 and the bool m_gripHeld to be
                                                            //false (since the update is run every frame, we don't want the Grab() function to run every time.
        {
            m_gripHeld = true;                              //Since the conditions are met for our flag, we set it to true
            m_anim.SetBool("isGrabbing", true);             //The "isGrabbing" refers to the Animation boolean created in Unity
            if(m_touchingObject)
            {
                Grab();
            }
        }
        else if(Input.GetAxis(m_gripName) < 0.5f && m_gripHeld) //Here we are checking to see if conditions are met to set our flag to false.  Here, it must
                                                                //already be in a true situation and the axis must be less than .5
        {
            m_gripHeld = false;
            m_anim.SetBool("isGrabbing", false);
            if(m_heldObject)
            {
                Release();
            }
        }
        if (Input.GetAxis(m_triggerName) > 0.8f && !m_triggerHeld)
        {
            m_triggerHeld = true;   //Setting the flag
            if (m_heldObject)    //If holding an object
            {
                m_heldObject.SendMessage("TriggerDown");
            }
        }
        else if (Input.GetAxis(m_triggerName) < 0.8f && m_triggerHeld)
        {
            m_triggerHeld = false; //Removing the flag
            if (m_heldObject)
            {
                m_heldObject.SendMessage("TriggerUp");
            }
        }
    }

    void Grab()
    {
        m_heldObject = m_touchingObject;
        m_heldObject.GetComponent<Rigidbody>().isKinematic = true;
        m_heldObject.transform.SetParent(transform);
    }

    void Release()
    {
        m_heldObject.GetComponent<Rigidbody>().isKinematic = false;
        m_heldObject.transform.SetParent(null);
        m_heldObject = null;
    }
}
