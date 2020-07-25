using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   //AI Library

public class FollowerAI : MonoBehaviour
{
    public float m_turnSpeed = 4f;  //To help smooth out the speed of the turn when an agent is detecting a target to follow
    public float m_agroDistance = 10f; //Aggravation distance...a radius around agent that detects or aggravates agent that target is within radius

    public Transform m_target;  //Our target or player...we just need the transform position so no need to reference entire gameObject

    private NavMeshAgent m_agent;  //This is our AI agent follower.   //This script will be attached to the AI follower so don't need to make this public


    void Start()  
    {
        m_agent = GetComponent<NavMeshAgent>();     //Reference to our AI Follower's NavMeshAgent
        m_target = PlayerSingleton.s_instance.m_player; //A singleton kind of runs against the principles of good C# coding methodology but is handy in certain situations.  Here we are 
                                                        //calling upon the script defining this class (PlayerSingleton) and setting our m_target transform var to the transform var of s_instance
                                                        //.m_player declared there. Again, the s_instance is a reference to itself or the Class script which contains the defined var of m_player
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(m_target.position, transform.position) < m_agroDistance) //Distance checks the distance between two Vector3 points, if less than agroDistance then move to target
        {
            m_agent.SetDestination(m_target.position);  //Every frame destination will be updated to be the position of the player
            Vector3 dir = m_target.position - transform.position; //Figures out which direction to point toward. So take 2 points A and B.  If you do B-A you get a line pointing from A ---> B
                                                                 //If you do A-B you get a line pointing from B -----> A. So we get a direction. The magnitude could be figured by doing the same
                                                                 //subtraction but adding ".magnitude" to our position.  But we just want the direction for our agent to look towards
           // m_agent.speed  This is a way to customize our agent and there are other things we can adjust too (like doing OnTriggerEnter and slowing his speed while colliding with ladder)
            dir.Normalize(); //Here we are taking the entire line and just making it a unit vector just giving us a direction (we know our x, y, z rotation values ?)
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, dir.z));  //Definition of our look rotation is(ignoring the vertical because he is bipedal, we don't want him
                                                                                           //him to tilt upwards or downwards keeping him on same plane).
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * m_turnSpeed); //We set our agent's rotation to our new look rotation. A Slerp function
                                                                                                                   //allows us to intrapolate between 2 rotations. We start with our origin 
                                                                                                                   //rotation, the end goal rotation we want, and the time to get there so we
                                                                                                                   //take Time.deltaTime * our turn speed.  We could just set the transform
                                                                                                                   //rotation to the lookRotation but this would cause the agent just to snap
                                                                                                                   //to that rotation. The method used here helps him turn smoothly. The 
                                                                                                                   //Quaternion helps calculate the 4th W value top prevent Gimbal locking
        }
    }

    private void OnDrawGizmosSelected()     //When you have a particular object selected it will draw a gizmo...This is just an in Editor funciton (you can use OnDrawGizmos which will
                                            //draw it all the time.
    {
        Gizmos.color = Color.red;
        //Gizmos.color = new Color(0, 0, 0, 1); //Could do this to make custom color (this is black)
        Gizmos.DrawWireSphere(transform.position, m_agroDistance); //This takes a origin position and a radius to draw the sphere around.  
    }
}
