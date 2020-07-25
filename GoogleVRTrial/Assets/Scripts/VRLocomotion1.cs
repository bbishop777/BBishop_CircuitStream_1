using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRLocomotion1 : MonoBehaviour
{
    public string m_horizontal;     //Since using Axis this will be our Horizontal button axis name
    public string m_vertical;       //This will be our Vertical button axis name
    public string m_run;

    public Transform m_vRRig;       //our VR Rig
    //public Transform m_director;    //Will control our forward direction (could be dir head is facing or controller is pointing or camera,player)
    public Transform m_vRHead;      //This is to Raycast down from our head to determine proper rig height
    public LayerMask m_groundLayer; //Our layermask will make sure when Raycasting with our head we ignore things like our hands and other objects


    // Update is called once per frame
    void Update()           //Going to handle locomotion here
    {
        Vector2 touchPosition;                  //This is what part of the touchpad/thumbstick we are touching (Vector2 stores 2 values ".x, .y")
        touchPosition.x = Input.GetAxis(m_horizontal); //This value will be how much you are touching horizontal (no touch = 0) and determines
                                                      //the horizontal speed from touchpad
        touchPosition.y = Input.GetAxis(m_vertical);  //This value will be how much you are touching vertical (no touch = 0)and determines the 
                                                      //vertical speed from our touchpad
       //touchPosition.Normalize();   //if you don't want player to control speed you can normalize magnitude of the 2 vectors(will get no speed
                                                      //or full speed no matter where touching)

        Vector3 playerRight = touchPosition.x * m_vRHead.right;     //determine how much moving to sideways by muliplying how much are director
                                                                      //(controller) is pointing to the right (if pointing to left then x negative
                                                                     //so left movement) by our horizontal speed
       // Vector3 playerRight = new Vector3(touchPosition.x, 0, 0);
       Vector3 playerForward = touchPosition.y * m_vRHead.forward; //determine how much moving forward by multiplying degree of director 
                                                                   //controller facing forward by our vertical speed

       // Debug.Log("What is vertical giving me? " + Input.GetAxis(m_vertical) + " And also what is horizontal? " + Input.GetAxis(m_horizontal));

        playerRight.y = 0;    //since "playerRight" is a Vector3 (giving x, y, & z coordinates)if controller was tilted front to end, it would 
                             //produce sideways movement with a possible upwards or downwards angle as well because y axis would be tilted.
                            //To prevent this the y must be ignored
        playerForward.y = 0; //As above, the same is true if controller was held side to side..it would cause player to fly up into the air or
                            //downwards because y axis here would by tilted. To prevent this we must ignore the y vector, so in both,
                            //we make it equal 0. If we wanted to allow flight..we would not include these 2 lines as well as adjusting the 
                            //groundheight function
       
       
        if (Input.GetButton(m_run))
        {
            Debug.Log("Got run button down! " + m_run);
            m_vRRig.position += playerRight * Time.deltaTime * 10f;   //We take the rig positon & add to it the vector for sideways movement
            m_vRRig.position += playerForward * Time.deltaTime * 10f; //We take the rig positon & add to it the vector for forward/backward  movement

            m_vRRig.position = new Vector3(m_vRRig.position.x, GetGroundHeight(), m_vRRig.position.z);
        }

        else
        {
            m_vRRig.position += playerRight * Time.deltaTime;   //We take the rig positon & add to it the vector for sideways movement
            m_vRRig.position += playerForward * Time.deltaTime; //We take the rig positon & add to it the vector for forward/backward  movement

            m_vRRig.position = new Vector3(m_vRRig.position.x, GetGroundHeight(), m_vRRig.position.z); //here we use the xposition as is and zposition
                                                                                                       //to get y value we call the function. Because   
                                                                                                       //even with zeroing out the y for player, we can
                                                                                                       //still float above or cut thru ground plane (
                                                                                                       //if it rises or falls).  So need this to 
                                                                                                       //determine proper height
        }
    }

    float GetGroundHeight() //We want to raycast from head to ground to get value to keep head at proper height. we make function a float
                            //to return a value for our y value or height. Need a "return" if not using void
    {
        RaycastHit hit;
        if (Physics.Raycast(m_vRHead.position, Vector3.down, out hit, 100, m_groundLayer)) //origin is headposition, direction is down, hit is 
                                                                                           //destination, 100 is max distance for raycast,
                                                                                           //but we want to just go to ground not any other layer
                                                                                           //so destination is ground layer
        {
            return hit.point.y;            //return Y height of head to ground if raycast is successful..later instructor said this refers to location of 
                                        //vertex or face of object hit on ground so not height..not sure I understand how we are using this as y coordinate
        }
        return m_vRRig.position.y;      //If raycast to hit ground is not successful, we leave the y alone
    }
}
