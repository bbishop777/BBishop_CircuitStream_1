using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script is to be used specifically for a VR controller using an axis based (trigger) interface.
public class VRHand1 : MonoBehaviour
{
    public Animator m_anim;

    public string m_gripName;               //Grip name
    //public string m_triggerName;    //We will get this from our Axis 
    public string m_menuButtonName;

    private Vector3 m_handVelocity;
    private Vector3 m_oldPosition;

    private Vector3 m_handAngularVelocity;
    private Vector3 m_oldEulerAngles;

    //Best practice is to separate public and private variables
   // private bool m_triggerHeld;     //This will be a flag to see it Button GetDown or just Get. This is part of flagging process (see below).
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
        Vector3 tp = transform.position;
        m_handVelocity = tp - m_oldPosition;
        m_oldPosition = tp;

        Vector3 te = transform.eulerAngles;
        m_handAngularVelocity = te - m_oldEulerAngles;
        m_oldEulerAngles = te;

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
            Debug.Log("What is grip name? " + m_gripName);
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
                m_heldObject.SendMessage("GrabReleased");
                Release(); 
            }
        }
        //if (Input.GetAxis(m_triggerName) > 0.8f && !m_triggerHeld) //Checking to see this is first time we see trigger is held so set flag to true
        //{
        //    m_triggerHeld = true;   //Setting the flag to true
        //    if (m_heldObject)    //If holding an object exists
        //    {
        //        m_heldObject.SendMessage("TriggerDown"); //We then send a message to the object held to activate the script and function
        //    }
        //}
        //else if (Input.GetAxis(m_triggerName) < 0.8f && m_triggerHeld) //checking to see if we are starting to let go off the trigger and flag was set that we were
        //{
        //    m_triggerHeld = false; //Removing the flag
        //    if (m_heldObject)
        //    {
        //        m_heldObject.SendMessage("TriggerUp"); //Sending message to object to run function on script called TriggerUp
        //    }
        //}
        //if(Input.GetButtonDown(m_menuButtonName))
        //{
        //    if(m_heldObject)
        //    {
        //        m_heldObject.SendMessage("MenuDown");
        //    }
        //}
    }

    void Grab()
    {
        m_heldObject = m_touchingObject;
        // m_heldObject.GetComponent<Rigidbody>().isKinematic = true;
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.connectedBody = m_heldObject.GetComponent<Rigidbody>();
        fx.breakForce = 5000;
        fx.breakTorque = 5000;
        m_heldObject.transform.SetParent(transform);
    }

    void Release()
    {
        // m_heldObject.GetComponent<Rigidbody>().isKinematic = false;
        Destroy(GetComponent<FixedJoint>());

        m_heldObject.transform.SetParent(null);

        Rigidbody rb = m_heldObject.GetComponent<Rigidbody>();
        rb.velocity = m_handVelocity * 60 / rb.mass;
        rb.angularVelocity = m_handVelocity * 60 / rb.mass;

        m_heldObject = null;
    }

    private void OnJointBreak(float breakForce)
    {
        m_heldObject.SendMessage("GrabReleased");
        m_heldObject.transform.SetParent(null);
        m_heldObject = null;
    }
}
