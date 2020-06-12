using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    private Rigidbody m_Rb;

    private Vector3 RandomVector(float min, float max)
    {
        var x = Random.Range(min, max);
        var y = Random.Range(min, max);
        var z = Random.Range(min, max);
        return new Vector3(x, 0, z);
    }

    void Start()
    {
        m_Rb = GetComponent<Rigidbody>();
        InvokeRepeating("changeDirection", 2.0f, 3);
    }

    void Update()
    {

    }

    private void changeDirection()
    {
        m_Rb.velocity = Random.insideUnitSphere * 5;
        m_Rb.transform.rotation = Random.rotation;

    }
}