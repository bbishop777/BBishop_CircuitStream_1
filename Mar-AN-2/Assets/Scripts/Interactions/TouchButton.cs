using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;      //Here we are adding the library for Unity Events (like event listeners)

public class TouchButton : MonoBehaviour
{
    public UnityEvent m_buttonPressed;  
    public UnityEvent m_buttonReleased;

    public Transform m_upTransform;     //Refers to our Up and Down positions of our Trigger Zone
    public Transform m_downTransform;
    public Transform m_buttonMesh;

    private void OnTriggerEnter(Collider other)  //RigidBody required to activate this (our VR hand does not have that but our fireballs do)
    {
        m_buttonMesh.position = m_downTransform.position;  //Once you enter the Trigger Zone, it moves the Button Mesh to the Down position
        m_buttonPressed.Invoke();                          //The Unity Event here is not called upon to Invoke or execute (like a callback function?) 
    }

    private void OnTriggerExit(Collider other)  //RigidBody required to activate this (our VR hand does not have that but our fireballs do)
    {
        m_buttonMesh.position = m_upTransform.position; //Onced you exit the Trigger Zone the Button Mesh moves to the Up position. 
        m_buttonReleased.Invoke();
    }
}
