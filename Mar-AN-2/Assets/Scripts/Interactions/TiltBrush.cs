using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltBrush : MonoBehaviour
{
    public GameObject m_prefabTrail;    //We will spawn this so need a GameObject
    public Transform m_trailSpawn;      //Since we just need the position of the child object, we just create a Transform variable

    private GameObject m_currentTrail;  //Doesn't need to be visible to Unity Editor and just keeps track of trail
   
    void TriggerDown()
    {
        m_currentTrail = Instantiate(m_prefabTrail, m_trailSpawn); //This causes the prefabTrail to be created and made a child of the trailSpawn
                                                                   //position
    }

    void TriggerUp()
    {
        m_currentTrail.transform.SetParent(null);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Paint")
        {
            m_prefabTrail.GetComponent<TrailRenderer>().material = collision.collider.GetComponent<MeshRenderer>().material;
        }
 
    }
}
