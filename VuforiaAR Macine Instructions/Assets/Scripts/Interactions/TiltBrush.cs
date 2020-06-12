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
    private List<GameObject> m_drawnTrails = new List<GameObject>(); //Whenever declare a list start with an empty list by using the keyword "new" if 
                                                                    //don't can cause problems.Each time we do m_drawnTrails.Add(arg) we will create
                                                                    //a new bucket or slot in our list. Array is similar to a list but some functions require an
                                                                    //array (.toArray() or GetComponentsInChildren needs an array). In general lists are easier
                                                                    //on memory allocation and are better unless you have an array that won't change in size. We 
                                                                    //must use indices to find what we store. Dictionaries are similar but we can name our buckets
                                                                    //or indices similar to an object in Javascript.
   
    void TriggerDown()  //While button is down
    {
        m_currentTrail = Instantiate(m_prefabTrail, m_trailSpawn); //This causes the prefabTrail to be created and makes it a child of the trailSpawn
                                                                   //transform so takes the transform which has the position, rotation so we don't need to 
                                                                   //type each of these out. As trailspawn moves so does curret trail because it is made a 
                                                                   //child here
    }

    void TriggerUp()        //When button is released..we want to save our current trails to our drawn trails list
    {
        m_currentTrail.transform.SetParent(null);  //we just disconnect the current trail from being the child of m_trailSpawn so brush no longer creates 
                                                   //this trail but trail is not destroyed
        m_drawnTrails.Add(m_currentTrail);         //Once we let up button, we add this trail to drawntrails list
    }

    void GrabReleased() //This will happen if we are painting (triggerdown) and release the brush so the last drawn trail is still added
    {
        m_currentTrail.transform.SetParent(null); //so we uncuuple trail from the empty game object at end of brush
        m_drawnTrails.Add(m_currentTrail); //We store it in list
    }
    void MenuDown() //going to be menu down on Vive controller or X or A on Touch controllers or middle mouse button or delete button
    {
        if (m_drawnTrails.Count > 0)    //if our list size is greater than 0 (so must have at least 1 trail to delete for this to activate)
        {
            GameObject lineToBeDeleted = m_drawnTrails[m_drawnTrails.Count - 1]; //we make a gameobject and put the last drawn trail here
            m_drawnTrails.Remove(lineToBeDeleted); //We need to remove the line first before destroying it.If we just destroy it, it leaves an empty
                                                   //bucket or index that will still show in count but will be null. I believe we do this by finding
                                                   //our lineToBeDeleted in list by matching it and then we remove it and then destroy it below.
            Destroy(lineToBeDeleted);
        }
    }
    private void OnCollisionEnter(Collision collision)//will execute if 2 objects both w/colliders and rigidbodies (ONtrigger only needs on object to have
                                                     //a rigidbody...this is on exam.
    {
        if(collision.collider.tag == "Paint") //Checking the tag of the object we collide with
        {
            m_prefabTrail.GetComponent<TrailRenderer>().material = collision.collider.GetComponent<MeshRenderer>().material;
                                                                                    //collision is the 
                                                                                   //impact & colider is the object collided with.
                                                                                   //  We could also use "sharedMaterial" but this
                                                                                   //changes it for all objects sharing this
                                                                                   //this material. We get the material of the 
                                                                                   //object we collide with and get its materail 
                                                                                   //and make our trail have the same
            if (gameObject.name == "CanBody")
            {
                gameObject.GetComponent<MeshRenderer>().material = collision.collider.GetComponent<MeshRenderer>().material;
            }
        }
 
    }
}
