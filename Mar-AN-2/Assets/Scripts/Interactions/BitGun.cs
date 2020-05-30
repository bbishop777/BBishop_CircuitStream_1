using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BitGun : MonoBehaviour
{
    public GameObject m_prefabBit;      //will refer to our prefab bit
    public Transform m_spawn;
    public float m_shootForce;

    private Coroutine  m_shootCoroutine;
    void TriggerDown()
    {
        m_shootCoroutine = StartCoroutine(SpamShoot());
        
    }

    void TriggerUp()
    {
        StopCoroutine(m_shootCoroutine);
    }
    IEnumerator SpamShoot()
    {
        while(true)
        {
            GameObject bit = Instantiate(m_prefabBit, m_spawn.position, m_spawn.rotation);  //Will create it at this postion and rotation...we use a temp var 
                                                                          //here to refer to our instance of our prefab. Parameters are the prefab(blueprint),
                                                                          //the spawn position and rotation (from the empty gameobject created) 
            bit.GetComponent<Rigidbody>().AddForce(m_spawn.forward * m_shootForce); //we give the force direction but also a magnitude (add force only needs a direction)
                                                                                    //by multiplying it by a shootforce we increase it's power/speed.
            Destroy(bit, 5f);               //Destroy the bit after 5 seconds...this destroys something only in your scene versus DestroyImmediate
                                            //DestroyImmediate();  //This allows it destroy something in your entire Project like a file and usually used with [ExecuteInEditMode] which runs it outside
                                            //of Play mode and only in Unity Editor  but DestroyImmediate can run in Play mode too 
            yield return new WaitForSeconds(0.125f);
        }
        //for(int i = 0; i < 10; i++)
        //{

        //}
     
       
        //StartCoroutine(SpamShoot());   //you can have multiple corountines starting at the same time (multiple threads)
    }
}
