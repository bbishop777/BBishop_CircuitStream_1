using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerAI : MonoBehaviour
{
    public float m_turnSpeed = 4;
    public float m_agroDistance = 10; //Aggravation distance

    public Transform m_target;  //Our target or player...we just need the position so no need to reference entire gameObject

    private NavMeshAgent m_agent;


    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_target = PlayerSingleton.s_instance.m_player;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(m_target.position, transform.position) < m_agroDistance) //Distance checks the distance between 2 Vector3 points, if greater than agroDistance then move to target
        {
            m_agent.SetDestination(m_target.position);
            Vector3 dir = m_target.position - transform.position; //Figures out which direction to point toward
            dir.Normalize();
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, dir.z));  //Definition of our look location (ignoring the vertical)
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * m_turnSpeed);
        }
    }

    private void OnDrawGizmosSelected()     //When you have a particular object selected it will draw a gizmo
    {
        Gizmos.color = Color.red;
        //Gizmos.color = new Color(0, 0, 0, 1); //Could do this to make custom color (this is black)
        Gizmos.DrawWireSphere(transform.position, m_agroDistance); //This takes a position and a radius
    }
}
