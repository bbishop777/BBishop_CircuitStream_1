using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimTeleport : MonoBehaviour  //We will use raycasting to teleport to new postion.  Raycast needs 3 input (Origin point, direction, and where to
                                          //hold this information.  Example, if our hand is pointing at a hill, hand is the origin, where are line of raycast
                                          //strikes the hill and the angle gives the direction and angle, then we store this in a "hit.point".
{
    private LineRenderer m_line;           //This references our Line renderer component on our Simhand
    private bool m_teleportValid;         //This will store whether we have found a location to teleport to or not
    private RaycastHit m_hit;

    private void Start()
    {
        m_line = GetComponent<LineRenderer>();  //Grabs our Line Renderer Component (a reference to it)
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.T))         //So if T key is held down we will check and perform code below.
        {
            //RaycastHit hit;                 //hit is a local variable created in this enclosure.  We don't need the "m_" as it lives and dies in this enclosure
                                            //We declare it here to store the 3 parameter for the raycast. hit is a raycast "bucket" to hold info.  However,
                                            //since we can't access it for the teleport, we will delcalre it above with an "m_"

            if(Physics.Raycast(transform.position, transform.forward, out m_hit)) //Parameters are origin, direction, and where line hits. This is asking if
                                                                                //a raycast was performed using these variables, will it succeed? If yes, this
                                                                                //receives a true and code enclosed executes.  So the
                                                                                //"Physics.Raycast(transform.postion, transform.forward. out hit)" performs a
                                                                                //raycast on its own and returns a value. If present, this evaluates to true
            {
                m_line.enabled = true;                      //So if sucessful, we turn on our line (make visible)
                m_line.SetPosition(0, transform.position);  //We set the origin position of line to hand position (setting 1st position---Index 0)
                m_line.SetPosition(1, m_hit.point);           //We set the end point of line to the hit point (setting 2nd position---Index 1)
                m_teleportValid = true;                     //Since T key raycast found something to locate, we allow our teleport to be valid or truew
            }
            else                                    //This else is if the raycast is not successful but we have held down the T key (we aim at sky,at nothing)
            {
                m_line.enabled = false;             //Since T was held down but raycast unsuccessful, we disable our line (invisible)
                m_teleportValid = false;            //Raycast did not find anything to teleport to, so it is not valid or false

            }
        }
        else if(Input.GetKeyUp(KeyCode.T)) //This is asking if we are not holding the T key down at all, we perform code below. So we could be pointing but let
                                           //go of T key, so line should disappear
        {
            m_line.enabled = false;             //Keep line invisible
            if(m_teleportValid)             //So we let go of the T key, if the previous raycast was successful, our teleport would be changed to true
            {
                transform.position = m_hit.point;  //Since it is true, we can now teleport object this script is on to the hit point found.  For VR experience
                                                   //we would get a transform the entire VR rig.  We will do this next time.
            }
        }
    }
}
