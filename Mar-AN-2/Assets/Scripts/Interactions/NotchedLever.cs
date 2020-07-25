using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotchedLever : MonoBehaviour
{
    public Transform m_startNotch; //First start position
    public Transform m_endNotch;  //Last notch position

    public float m_notchSpeed;  //How fast it moves between notches

    public List<Transform> m_notches = new List<Transform>();

    private Transform m_closestNotch;

    private void OnTriggerStay(Collider other) //Determine if we are touching an object..specifically if the lever is touching an object with the tag Player (our hand in the scene)
    {
        if (other.tag == "Player")
        {
            m_closestNotch = null;  //Since we know we are touching the lever with the Player we don't want Update to move toward another closest notch so we make it null

            Vector3 heading = m_endNotch.position - m_startNotch.position; //Gives our line/direction from start point to end point
            float magnitudeOfHeading = heading.magnitude;  //Tells us the distance of the entire line from lever's start and stop points
            heading.Normalize();    //This takes whatever the distance is and makes it one unit. So round it down to one meter or up to one meter (??). We do this to move our lever based on the 
                                    //the distance of our dot product multiplied by the unit vector(heading.normalize) and move it on whether it is greater or lesser than the unit vector(???).  
                                    //For example, if the length from start to finish was 2 meters and our dot product was 1 meter, when we multiplied the two (2x1) we would get 2meters so our 
                                    //lever would move to the end position. Instead, by normalizing heading, we would multiply 1x1 and get 1 mete rwhich is the end of our unit vector length.
                                    // If our dot product is 2, then we get 2 x 1(unit vector) and end up with 2 meters which is the end position.
                                    
            Vector3 startToHand = other.transform.position - m_startNotch.position;  //This gives us a line from the Start position to the touching Object's position

            float dotProduct = Vector3.Dot(startToHand, heading); //This Vector3.Dot calculates our dot product which gives us the distance between start position and the hand's position but
                                                                 //on the same plane as our lever. If we were to reverse the parameters (heading, starToHand) we would get the distance between the
                                                                 //Unit vector's position but on the hand's plane to the hand's position).

            dotProduct = Mathf.Clamp(dotProduct, 0, magnitudeOfHeading); //Mathf has many functions. Clamp will lock down the dotProduct at whatever the dot product's lenght is by putting it between
                                                                        //the start of the dot product and the entire length of the line (which was found in magnitudeOfHeading). Even if it comes out 
                                                                        //to be larger than the magnitudeOfHeading, clamp will reset it to the maximum allowed length of our track to the end point. 0 is 
                                                                        //the start of the line
            Vector3 spot = m_startNotch.position + heading * dotProduct; //Calculating where lever should end up. So example: Our hand is touching lever and has moved to .5 meters along the lever's
                                                                        //travel plane.  We would get this .5 through the dot product function which takes in the start postion to hand position distance
                                                                        //and the heading (entire length of line) normalized to 1 meter or 1 unit vector. No matter what the product of the dot product is
                                                                        //we use Clamp to keep it within the entire magnitude or lenght of the travel line. Then, here, we times .5 times our heading
                                                                        //(unit vector) of 1 getting .5 and then adding that to our start position to get the new position for the lever  to move to

            transform.position = spot;                              //We then move the lever to the new position
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_closestNotch = m_startNotch;

            foreach(var notch in m_notches)
            {
                if (Vector3.Distance(transform.position, notch.position) < Vector3.Distance(transform.position, m_closestNotch.position))
                {
                    m_closestNotch = notch;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_closestNotch)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_closestNotch.position, m_notchSpeed);
        }
    }
}
