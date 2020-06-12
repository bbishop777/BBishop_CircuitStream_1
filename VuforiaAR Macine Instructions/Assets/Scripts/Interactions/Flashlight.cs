using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light m_light;   //Unity editor variable for light assignment

    void TriggerDown()      //We will broadcast from our SimHand to a particular object (the flashlight) and call this function..if the object
                            //has a script with this function, it will run this funciton
    {
        //if(m_light.enabled)   //These lines show a somewhat shorter way to write the code out than original code (see bottom)
        //{
        //   ! m_light.enabled;
        //}
        //else
        //{
        //    m_light.enabled = true;
        //}

        m_light.enabled = !m_light.enabled; //This is a simpler way to reduce the code. This just says take it's value and make it opposite
                                            //(if true, make it false...if false, make it true)
    }
}

//Original TriggerDown() function:
//if(m_light.enabled == true)
//{
//  m_light.enabled == false;
//}
//else if (m_light.enabled == false)
//{
//  m_light.enabled = true;
//}