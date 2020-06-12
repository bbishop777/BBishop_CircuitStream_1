using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI; //To be able to access a text element

public class ARInstructions : MonoBehaviour
{
    public Text m_btnText;  //Reference to our Button Text
    public GameObject m_mainMenu;   //Reference to our main menu canvas
    public List<GameObject> m_steps = new List<GameObject>(); //List of all our instructions/steps...want to initialize Lists right away

    private int m_currentStep; //Integer that keeps track of our current stepS

    public void NextStep()
    {
        Debug.Log("I have been asked to go to the next step " + m_steps.Count);
        if (m_currentStep < m_steps.Count - 1)   //We want to only change to the next step as long as there are more steps present than the one we are on
        {
            Debug.Log("I made it to next step!!");
            m_steps[m_currentStep].SetActive(false);  //Reference our current step and turn it off
            m_currentStep++;                        //Increment the current step
            m_steps[m_currentStep].SetActive(true); //Now that we've increased the current step plus 1, we reference it and turn it on

            if (m_currentStep == m_steps.Count -1) //if at last step then change our bottom button text to "Return" to go back to main menu
            {
                m_btnText.text = "Return";
            }
        }
        else                   //If completed last step we come here and turn last nav button off, set step count back to 0 and set first step button back on
        {
            m_steps[m_currentStep].SetActive(false);
            m_currentStep = 0;
            m_steps[m_currentStep].SetActive(true);

            m_btnText.text = "Next";        //We also wil change the bottom button text to "Next" and turn on the main menu again and turn off our secondary menu which has these steps
            m_mainMenu.SetActive(true);
            gameObject.SetActive(false);    //This object holding the script will be the instructional menu.  On the main menu a button press on the "Next" button will call this script
        }
    }
}
