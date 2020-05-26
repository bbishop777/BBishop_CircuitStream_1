using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BitGun : MonoBehaviour
{
    public GameObject m_prefabBit;      //will refer to our prefab bit
    public Transform m_spawn;
    public float m_shootForce;
    
    void TriggerDown()
    {
        GameObject bit = Instantiate(m_prefabBit, m_spawn.position, m_spawn.rotation);  //Will create it at this postion and rotation
        bit.GetComponent<Rigidbody>().AddForce(m_spawn.forward * m_shootForce); //we give the force direction but also a magnitude
        Destroy(bit, 5f);               //Destroy the bit after 5 seconds...this destroys something only in your scene
        //DestroyImmediate();  //This allows something to destroy something in your entire Project and goes along with
    }
}
