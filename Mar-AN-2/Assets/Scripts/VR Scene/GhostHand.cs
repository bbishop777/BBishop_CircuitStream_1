using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHand: MonoBehaviour
{
    public Transform m_hand;  //Reference to our hand...used to measure difference between visible hand and ghost hand

    private Color m_ghostColor; //The origginal alpha value of our ghost hand
    public Renderer m_ghostHandRend; //reference to our renderer of Ghost hand so we can change it and get it's color

    private bool m_isTracking = true;

    

    // Start is called before the first frame update
    void Start()
    {
        m_ghostColor = m_ghostHandRend.material.color; //assigning our ghost hand color to our variable
    }

    // Update is called once per frame
    void Update()
    {
        m_ghostColor.a = Vector3.Distance(transform.position, m_hand.position); //Calculating and assigning an alpha value to are ghost hand material
        m_ghostHandRend.material.color = m_ghostColor;
       
        if(m_isTracking)
        {
            m_hand.position = transform.position;
            m_hand.rotation = transform.rotation;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.isStatic)
        {
            m_isTracking = false; //no longer move the physical hand with ghost hand
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.isStatic)
        {
            m_isTracking = true;
        }
    }
}
