using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltBrush : MonoBehaviour
{
    public GameObject m_prefabTrail;    //We will spawn this so need a GameObject...this is reference to our prefab spawn
    public Transform m_trailSpawn;      //Since we just need the position of the child object that is the trail, we just create an empty object but from that
                                        //all we need is the Transform variable..so we can create a transform and drag the empty parent game object into it in
                                        //the Unity editor

    private GameObject m_currentTrail;  //Doesn't need to be visible to Unity Editor and just keeps track of current trail we have started
    private List<GameObject> m_drawnTrails = new List<GameObject>();
   
    void TriggerDown()  //While button is down
    {
        m_currentTrail = Instantiate(m_prefabTrail, m_trailSpawn); //This causes the prefabTrail to be created and makes it a child of the trailSpawn
                                                                   //transform so takes the transform which has the position, rotation so we don't need to 
                                                                   //type each of these out. As trailspawn moves so does curret trail because it is made a 
                                                                   //child here
    }

    void TriggerUp()        //When button is released
    {
        m_currentTrail.transform.SetParent(null);  //we just disconnect the current trail from being the child of m_trailSpawn so brush no longer creates 
                                                   //this trail but trail is not destroyed
        m_drawnTrails.Add(m_currentTrail);         //Once we let up button, we add this trail to drawntrails list
    }

    void GrabReleased()
    {
        m_currentTrail.transform.SetParent(null);
        m_drawnTrails.Add(m_currentTrail);
    }
    void MenuDown() //going to be menu down on Vive controller or X or A on Touch controllers.
    {
        if (m_drawnTrails.Count > 0)
        {
            GameObject lineToBeDeleted = m_drawnTrails[m_drawnTrails.Count - 1];
            m_drawnTrails.Remove(lineToBeDeleted);
            Destroy(lineToBeDeleted);
        }
    }
    private void OnCollisionEnter(Collision collision)//will execute if 2 objects both w/colliders and rigidbodies (ONtrigger only needs on object to have
                                                     //a rigidbody...this is on exam.
    {
        if(collision.collider.tag == "Paint") //Checking the tag of the object we collide with
        {
            m_prefabTrail.GetComponent<TrailRenderer>().material = collision.collider.GetComponent<MeshRenderer>().material; //collision is the impact
                                                                                                //colider is the object collided with.  We could also use
                                                                                                //sharedMaterial but this changes it for all objects sharing
                                                                                                //this material. We get the material of the object we collide 
                                                                                                //with and get its materail and make our trail have the same
            if (gameObject.name == "CanBody")
            {
                gameObject.GetComponent<MeshRenderer>().material = collision.collider.GetComponent<MeshRenderer>().material;
            }
        }
 
    }
}
