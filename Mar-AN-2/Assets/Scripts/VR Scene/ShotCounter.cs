using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Need to be able update UI


public class ShotCounter : MonoBehaviour    //This script will get info from our Shoot script to count the number of shots fired & update "Shots Fired:" UI
{
    [SerializeField]                //don't need m_countDisplay to be accessed by other scripts but need to see in Inspector so we SerializeField (let's us see
    private Text m_countDisplay;    //in Unity Inspector)

    [HideInInspector]           //m_count must be accessed by other scripts but not needed to be seen in Inspector so we HideInInspector
    public int m_count;

    public void UpdateDisplay()
    {
        m_countDisplay.text = "Shots Fired: " + m_count; //In inspector, the Text component has a subcomponent called text..why we use ".text".  Also here we
                                                        //give it a string to replace text and add to it the m_count.  This is called a static cast as it allows
                                                        //integer: m_count to change it to a string to add to our text.
        //m_count.ToString();                           //If you wanted to use an integer value as a string value we can convert it this way (as long as it can be
                                                        //done sensibly.  For example a true float (1.333) can't be converted to a integer
    }
}
