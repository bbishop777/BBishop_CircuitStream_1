﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimGrab : MonoBehaviour
{
    public Animator m_anim;  //Reference to our animator which are parameter is "isGrabbing" created in Animation window.

    private GameObject m_touchingObject;
    private GameObject m_heldObject;

    private void OnTriggerStay(Collider other) //When we are overtop of a trigger for a moment and not over another at same time
    {
        //if(other.GetComponent<Rigidbody>())   //if touched object has a rigidbody then we store it in variable below.Here we look for the Rigidbody but
                                                //OnTrigger functions run every frame & to find this is processor expensive (looking thru component list of)
        if(other.tag == "Interactable")         //object touched).However it is more efficient to search for a tag than use GetComponent. We call
                                                //this tag "Interactable" and we must document that any object tagged with this tag must have a RigidBody and
                                                //any object tagged this way can be picked up.  You could also add || or && to check for additonal tags or
                                                //layers or even name so ex: "if(other.tag == "Interactble" || other.name == "Brad" && other.layer == "One""
        {
            m_touchingObject = other.gameObject;      //We store this touched object in the variable here
        }
    }

    private void OnTriggerExit(Collider other) //When it is detected that our collider is no longer over an object
    {
        //if(m_touchingObject == other.gameObject) //If our object is stored here is our "other" object as found in OnTrigger
        //Stay then we free the variable container because we are exiting the area

        if (other.tag == "Interactable")    //However it is more efficient to search for a tag than use GetComponent since this runs every frame
        {
            m_touchingObject = null;        //We free the variable and make it equal to null
        }
    }

    //Now witn OnTriggerStay and OnTriggerExit only one object needs a rigidbody (here our hand doesn't but the boxes do)
    //We can do OnCollisionEnter (both objects need rigidbodies) Collisions are surface level (moment of impact) where
    //OnTrigger allows object to pass thru. Also, the object this script is on needs a trigger activated. We have this on
    //the hand. You can have multiple colliders on one object...some as colliders and some as not. Also OnTrigger will fire as
    //long as one of the objects has a collider with a trigger on it. So either put a rigidbody on the object w/the script 
    //on it or a rigidbody on the other object. OnCollision commands do not need this trigger but both must have 
    //rigidbodies and normal colliders on all objects involved (at least one each).
    //As an equivalent to the OnTriggerStay we could do:

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.collider.GetComponent<Rigidbody>())
    //    {
    //        m_touchingObject = other.gameObject;
    //    }
    //}
    //You can also call "collision.contactCount.GetComponent<RigidBody>()" to get a list of all contact points touched.  OR
    //"collision.contacts" (gives you an array so you could name the first point such as: "collision.contacts[0]"

    // Update is called once per frame
    //Avoid using GetComponent and GameObject.find in the update loop as it will significantly effect the performance 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))  //If right click mouse...
        {
            m_anim.SetBool("isGrabbing", true);
            if(m_touchingObject)  //Check to see if touching object (variable is not null so has object in it).  We formerly
                                  //wrote this as "if(m_touchingObject != null)" but the way it is now is shorter. Same for
                                  //line 42
            {
                Grab();
            }
        }
        if(Input.GetKeyUp(KeyCode.Mouse1))  //If release click mouse...
        {
            m_anim.SetBool("isGrabbing", false);
            if (m_heldObject)   //Check to see if holding object (if variable is not null but has object in it)
            {
                Release();
            }
        }
    }

    void Grab()
    {
        if(m_touchingObject.GetComponent<Rigidbody>().mass > 10)
        #region
        //Here we are checking if object's mas is greater than
        //10 kg (22 lbs) then we return or just break out of this
        //function and return to the next line from where this
        //function was called
        #endregion
        {
            return;
        }
        //if this mass is less than 10 or the above is false, then go to the next part below:
        m_heldObject = m_touchingObject; //Reference to the touched object..store it in m_heldObject
        m_heldObject.GetComponent<Rigidbody>().isKinematic = true; //So when we grab object we change rigidbody to
                                                                   //kinematic so no longer effected by forces
        m_heldObject.transform.SetParent(transform); //We then set grabbed object as child to hand so moves with the hand
    }

    void Release()
    {
        m_heldObject.transform.SetParent(null); //No we break it free from the parent-child relationship
        m_heldObject.GetComponent<Rigidbody>().isKinematic = false;//no object is no longer kinematic so it can be
                                                                   //effected by other forces (if we didn't do this, when
                                                                   //released, it would just float..now we fall down
        m_heldObject = null;   //Here we free the variable container
    }

}
