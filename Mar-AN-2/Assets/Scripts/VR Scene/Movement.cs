//The first 3 lines below are libraries.  Libraries bring in methods and operations to use within C# because it is not
//robust on its own.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;  //This library brings in stuff like transform, etc

public class Movement : MonoBehaviour   //public means it is accessible outside this file scripts...other scripts can reach
                                        //  it as well as Unity Inspector. "private" restricts access to this script only. 
                                        //"Movement" must be used here because it is the name of the script.
{
    public float m_speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * m_speed * Time.deltaTime);
        
    }
}
