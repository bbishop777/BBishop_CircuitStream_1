using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;      //Here we are adding the library for Unity Events (like event listeners)

public class ConfigJointButton : MonoBehaviour
{
    public UnityEvent m_buttonPressed;
    public UnityEvent m_buttonReleased;

    public Transform m_onTransform;     //Refers to our Up and Down positions of our Trigger Zone
    public Transform m_offTransform;
    public Transform m_buttonMesh;

    private void OnCollisionEnter(Collision collision)
    {
        m_buttonMesh.position = m_onTransform.position;
        m_buttonPressed.Invoke();

    }

    private void OnCollisionExit(Collision collision)
    {
        m_buttonMesh.position = m_offTransform.position;
        m_buttonPressed.Invoke();
    }
}
