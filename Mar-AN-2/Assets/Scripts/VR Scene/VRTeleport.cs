using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]    //Requires LineRenderere on gameobject and will create it if not present...also a note to developers
public class VRTeleport : MonoBehaviour     //This code below will be similar to SimTeleport except we will be teleporting the entire VR Rig
                                            //The rig will have a center but your hand or head maybe offset from the center of your rig or
                                            //offset from center so when you teleport the rig the center point will be transported to the teleport
                                            //spot and the player will be offset.  We must resolve this
{
   // [Header("The Player")]
    public Transform m_vRRig;           //Refers to our Player Rig
    public string m_buttonName;         //We will be using our menu button or A button (Occulus vs Vive)

    private LineRenderer m_line;        //This will be our teleport line..visualizing our raycast
    private RaycastHit m_hit;
    private bool m_teleportValid;       //Keeps track whether our chosen teleport location is valid...starts off as false

    private void Start()
    {
        m_line = GetComponent<LineRenderer>();
        m_line.enabled = false;         //to make sure laser line is off when we start
    }

    private void Update()
    {
        if(Input.GetButton(m_buttonName))   //Instead of getkeydown we use GetButton and the button is whichever one we map out
        {
            if(Physics.Raycast(transform.position, transform.forward, out m_hit)) //if raycast is successful from our controller
            {
                m_teleportValid = true;                     //valid teleport location
                m_line.enabled = true;                      //enable line
                m_line.SetPosition(0, transform.position);  //0 positon should be location of our hand/controller..origin
                m_line.SetPosition(1, m_hit.point);         //1 postion should be location of our hit point
            }
            else                             //if Raycast is not successful
            {
                m_teleportValid = false;    //teleport is not valid
                m_line.enabled = false;     //line not made visible
            }
        }
        else if(Input.GetButtonUp(m_buttonName))  //if we release the button.  We could use just else but that would fire anytime button released
        {
            m_line.enabled = false;
            if(m_teleportValid)         //So if we released the button but had a valid teleport postion...teleport the rig. So we don't teleport
                                        //until releasing the button.
            {
                m_vRRig.position = m_hit.point;
            }
        }
    }
}
