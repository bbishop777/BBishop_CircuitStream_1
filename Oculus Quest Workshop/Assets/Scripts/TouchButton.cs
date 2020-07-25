using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TouchButton : MonoBehaviour
{
    public UnityEvent m_buttonPressed;
    public UnityEvent m_buttonReleased;

    public Transform m_downTransform;
    public Transform m_upTransform;
    public Transform m_buttonMesh;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_buttonPressed.Invoke();
            m_buttonMesh.position = m_downTransform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_buttonReleased.Invoke();
            m_buttonMesh.position = m_upTransform.position;
        }
    }
}
