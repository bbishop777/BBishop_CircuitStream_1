using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerFollower : MonoBehaviour
{
    [SerializeField]
    private Transform m_trackedObject;

    private Rigidbody m_controllerRB;

    // Start is called before the first frame update
    void Start()
    {
        m_controllerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaPosition = m_trackedObject.position - transform.position;
        m_controllerRB.MovePosition(transform.position + deltaPosition);
        m_controllerRB.rotation = m_trackedObject.rotation;
    }
}
