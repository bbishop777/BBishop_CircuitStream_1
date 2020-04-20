using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject m_prefabFireball;
    public float m_shootForce;
    public float m_transform;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse0 is left mouse button, Mouse1 is right mouse button, Mouse3 is middle Mouse button
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            #region Comments on code below
            //To spawn something we create an instant of something or to instantiate it.  So if we left click it spawns a
            //fireball. We can use this function 4 different way..number 3 shows us where it will spawn and what direction 
            //it will point. The transform (rotation and position) of the game object we put this script on will be used 
            //our fireball. Once spawned we need to get a reference to the rigidbody (something physics can act on)
            //of the instance of this prefab. This instance is the fireball made from the prefab. The prefab will have a
            //rigidbody so when an instance is created, it will to and we will use this to apply forces to the fireball.
            //We will add force to it and multiply this force by our m_shootForce. 1 newton of force is very small and, by
            //default, a rigidbody will weigh 1 kilograms or 2.2 pounds so we either need to decrease the mass by a lot or
            //m_shootForce should be high.  Destroy destroys object named immediately unless the second parameter is added 
            //which is in seconds...so it will be destroyed after 5 seconds.
            #endregion
            GameObject fireball = Instantiate(m_prefabFireball, transform.position, transform.rotation);
            fireball.GetComponent<Rigidbody>().AddForce(transform.forward * m_shootForce);
            Destroy(fireball, 5);
        }
    }
}

//The .GetComponent is a very processor expensive code.  It should not be in an Update unless it as we have it (housed in
//an "if.." statement).  It would be better to put it in a Start function if you only need it once. It is a search which
//data intensive.  "transform" is also a type of search (to find the position of the gameobject)...it is better to put in
//a variable in the start function to cache it and then it only searches it once.