using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRLocomotion : MonoBehaviour
{
    public float m_speed;
    public string m_horizontalAxis;
    public string m_verticalAxis;
    //public string m_buttonName;
    public Transform m_vrRig;

    private Vector2 m_joystick;
   

    // Update is called once per frame
    void Update()
    {
        m_joystick.x = Input.GetAxis(m_horizontalAxis) * m_speed * Time.deltaTime;
        m_joystick.y = Input.GetAxis(m_verticalAxis) * m_speed * Time.deltaTime;

        m_vrRig.position = m_vrRig.position + new Vector3(m_joystick.x, 0, m_joystick.y); //we take the y axis measurement from joystic &d make that a direction in the Z axis so we don't go up in 
                                                                                          //the air
        //if(Input.GetButtonDown("A"))
        //{

        //}
    }
}
