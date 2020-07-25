using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimGrab2 : MonoBehaviour
{
    public Animator m_anim;  //Reference to our animator which are parameter is "isGrabbing" created in Animation window.

    private GameObject m_touchingObject;
    private GameObject m_heldObject;

    private float m_rotation = 0;
    private int m_counter;
   

    private Vector3 m_handVelocity;
    private Vector3 m_oldPosition;

    private Vector3 m_handAngularVelocity;
    private Vector3 m_oldEulerAngles;



    private void OnTriggerStay(Collider other) //When we are overtop of a trigger for a moment and not over another at same time
    {    
        if (other.tag == "Interactable")         
        {
            m_touchingObject = other.gameObject;      //We store this touched object in the variable here
        }
    }

    private void OnTriggerExit(Collider other) //When it is detected that our collider is no longer over an object
    {     
        if (other.tag == "Interactable")   
        {
            m_touchingObject = null;        //We free the variable and make it equal to null
        }
    }

    void Update()
    {
        m_counter++;
        if (m_counter == 5)
        {
            Vector3 tp = transform.position;
            m_handVelocity = tp - m_oldPosition;
            m_oldPosition = tp;

            Vector3 te = transform.eulerAngles;
            m_handAngularVelocity = te - m_oldEulerAngles;
            m_oldEulerAngles = te;

            m_counter = 0;
        }
        

        if (Input.GetKeyDown(KeyCode.Mouse1))  //If right click mouse...
        {
            m_anim.SetBool("isGrabbing", true);
            if (m_touchingObject)                      
            {
                Grab();
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))  //If release right mouse button...
        {
            m_anim.SetBool("isGrabbing", false);
            if (m_heldObject)   //Check to see if holding object (if variable is not null but has object in it)
            {
                m_heldObject.BroadcastMessage("GrabReleased");
                Release();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) //if holding left mouse button down...
        {
            if (m_heldObject)                                
            {
                m_heldObject.BroadcastMessage("TriggerDown");                                                      
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))  //
        {
            if (m_heldObject)
            {
                m_heldObject.BroadcastMessage("TriggerUp");
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            if (m_heldObject)
            {
                m_heldObject.BroadcastMessage("MenuDown");
            }
        }
    }

    void Grab()
    {
        if (m_touchingObject.GetComponent<Rigidbody>().mass > 10)
        {
            return;
        }
        m_heldObject = m_touchingObject;                                                                  
        m_heldObject.transform.SetParent(transform);
        //m_heldObject.transform.localEulerAngles = new Vector3(m_rotation, m_heldObject.transform.localEulerAngles.y, m_rotation);
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.connectedBody = m_heldObject.GetComponent<Rigidbody>();
        fx.breakForce = 5000;
        fx.breakTorque = 5000;
    }

    void Release()
    {
        m_heldObject.transform.SetParent(null); //Now we break it free from the parent-child relations
        // m_heldObject.GetComponent<Rigidbody>().isKinematic = false;
        Destroy(GetComponent<FixedJoint>());
        Rigidbody rb = m_heldObject.GetComponent<Rigidbody>();
        rb.velocity = m_handVelocity * 150 / rb.mass;
        rb.angularVelocity = m_handVelocity * 150 / rb.mass;
        m_heldObject = null;   //Here we free the variable container
    }

    private void OnJointBreak(float breakForce) //We are still holding the grip (Grab) when this happens.  
    {
        m_heldObject.SendMessage("GrabReleased");
        m_heldObject.transform.SetParent(null);
        m_heldObject = null;
    }

}
