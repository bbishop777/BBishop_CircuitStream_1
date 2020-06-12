using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimGrab : MonoBehaviour
{
    public Animator m_anim;  //Reference to our animator which are parameter is "isGrabbing" created in Animation window.

    private GameObject m_touchingObject;
    private GameObject m_heldObject;

    private Vector3 m_handVelocity;
    private Vector3 m_oldPosition;

    private Vector3 m_handAngularVelocity;
    private Vector3 m_oldEulerAngles;

    private void OnTriggerStay(Collider other) //When we are overtop of a trigger for a moment and not over another at same time
    {
        //if(other.GetComponent<Rigidbody>())   //if touched object has a rigidbody then we store it in variable below.Here we look for the Rigidbody but
        //OnTrigger functions run every frame & to find this is processor expensive (looking thru component list of)
        if (other.tag == "Interactable")         //object touched).However it is more efficient to search for a tag than use GetComponent. We call
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

        // if (other.tag == "Interactable")    //However it is more efficient to search for a tag than use GetComponent since this runs every frame
        if (m_touchingObject == other.gameObject)
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
        Vector3 tp = transform.position; //we can cache our transform or (as here) cache transform.position to optimize (a transform alone has 
                                         //3 - Vector3 numbers(position, scale, rotation)
        m_handVelocity = tp - m_oldPosition; //Unity's rigidbody velocity is meter per second (about 1 yard per second)...we are calculating between
                                             //each frame so will be 1 / 60th or less depending on frame rate so very miniscule
        m_oldPosition = tp;

        Vector3 te = transform.eulerAngles;   //Look this up but it is easier to use eulerAngles than quanterion
        m_handAngularVelocity = te - m_oldEulerAngles;
        m_oldEulerAngles = te;
        Debug.Log("Tell me position now " + tp + " tell me old position " + m_oldPosition + " tell me hand velocity " + m_handVelocity + " tell me hand angular velocity " + m_handAngularVelocity);


        if (Input.GetKeyDown(KeyCode.Mouse1))  //If right click mouse...
        {
            m_anim.SetBool("isGrabbing", true);
            if (m_touchingObject)  //Check to see if touching object (variable is not null so has object in it).  We formerly
                                   //wrote this as "if(m_touchingObject != null)" but the way it is now is shorter. Same for
                                   //line 42
            {
                Grab();
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))  //If release right mouse button...
        {
            m_anim.SetBool("isGrabbing", false);
            if (m_heldObject)   //Check to see if holding object (if variable is not null but has object in it)
            {
                m_heldObject.SendMessage("GrabReleased"); //if a sendmessage is not received, it will show as an error in Unity
                                                          //console but is not a true error just a notice
                Release();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) //if holding left mouse button down...
        {
            if (m_heldObject)    //If holding an object, we will send a message to that object...if it has a script with the function called,
                                 //it will run
            {
                m_heldObject.SendMessage("TriggerDown");    //Similar to BroadCast Message. so will send message to object we are holding and call
                                                            //TriggerDown which is a function on the script on this object (flashlight)...Broadcast
                                                            //will send it to the object and all of the children.  SendMessage only communicates
                                                            //to the object. If the function in the script on the object we are sending a message to takes 
                                                            //parameters (like: TriggerDown(int x) {}) then we can send an argument like:
                                                            //"m_heldObject.SendMessage("TriggerDown", 2):
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))  //if let up left mouse button...
        {
            if (m_heldObject)
            {
                m_heldObject.SendMessage("TriggerUp");
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse2)) //if holding down middle mouse button or A or X touch controllers...
        {
            if (m_heldObject)
            {
                m_heldObject.SendMessage("MenuDown");
            }
        }
    }

    void Grab()
    {
        if (m_touchingObject.GetComponent<Rigidbody>().mass > 10)
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
                                         // m_heldObject.GetComponent<Rigidbody>().isKinematic = true; //So when we grab object we change rigidbody to
                                         //kinematic so no longer effected by forces
        m_heldObject.transform.SetParent(transform); //We then set grabbed object as child to hand so moves with the hand

        FixedJoint fx = gameObject.AddComponent<FixedJoint>(); //GetComponent can be used without nameing calling the gameobject the script
                                                               //is attached to..not so with AddComponent..must call gameobject first
        fx.connectedBody = m_heldObject.GetComponent<Rigidbody>(); //Connected body should be rigidbody of heldobject
        fx.breakForce = 5000;   //Breaks away if hit or twisted over 5000 but this will destroy the fixedjoint
        fx.breakTorque = 5000;
    }

    void Release()
    {
        m_heldObject.transform.SetParent(null); //Now we break it free from the parent-child relations first
                                                //  m_heldObject.GetComponent<Rigidbody>().isKinematic = false;//no object is no longer kinematic so it can be
                                                //effected by other forces (if we didn't do this, when
                                                //released, it would just float..now we fall down
        Destroy(GetComponent<FixedJoint>()); //Then We must destroy fixedjoint before we can throw an object (as noted above in Grab function
                                             // GetComponent can be used without calling the gameobject this script is on

        Rigidbody rb = m_heldObject.GetComponent<Rigidbody>(); //Optimize this since we will have to use it several times
        //Debug.Log("what is heldObject name? " + m_heldObject.name + "Hand velocity "  + m_handVelocity);
        rb.velocity = m_handVelocity * 60 / rb.mass;  //we can't make the velocity equal to the handVelocity alone because it is so small and is based
                                                      // on tiny teleportations and we because of that we can't just make the rb.velocity equal to our
                                                      //hand's rigidbody.velocity because 1) it does not have one but more importantly 2)it is being
                                                      //teleported so it does not have a true velocity(hence why we do the calculations above in 
                                                      // Update to mimic this). So we multiply this very small calculated velocity and times it by 60
                                                      //(a fudge factor) and then we divide it by the mass of the object to limit the throw based on
                                                      //the mass of the object.One game idea is the character builds strength as game continues so
                                                      //the fudge factor would increase
        rb.angularVelocity = m_handVelocity * 60 / rb.mass; //Doing the same as with handVelocity
        Debug.Log("What is bitgun's velocity and angular velocity? " + rb.velocity + " " + rb.angularVelocity);
        m_heldObject = null;   //Here we free the variable container
    }

    private void OnJointBreak(float breakForce) //We are still holding the grip (Grab) when this happens but the fixedjoint was broken and destroyed 
    {
        m_heldObject.SendMessage("GrabReleased");
        m_heldObject.transform.SetParent(null); //So we are unparenting the held object when it is smashed out of our hand
        m_heldObject = null;  //need to free the m_heldObject var so when we release the grab button (or release right mouse button..see above around line 84...the
                              //Release function is not called by the keyup just animation is set to false
    }

}
