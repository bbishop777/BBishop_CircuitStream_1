using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Haptics : MonoBehaviour
{
    public XRNode m_node;

    private InputDevice m_vrController;
    // Start is called before the first frame update
    void Start()
    {
        m_vrController = InputDevices.GetDeviceAtXRNode(m_node);
    }

   public void VibrateContoller(float amplitude, float time)    //how much does it vibrate (amplitude) and how long
    {
       // HapticCapabilities hapcap = new HapticCapabilities();   

        //m_vrController.TryGetHapticCapabilities(out hapcap); //Similer to Physics.Raycast...if successful will store it as hapcap

        m_vrController.SendHapticImpulse(0, amplitude, time);

        //if (hapcap.supportsImpulse)
        //{
        //    m_vrController.SendHapticImpulse(0, amplitude, time);
        //}

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Button")
        {
            VibrateContoller(0.25f, 0.5f);  //The strongest vibration amplitude is 1, and then the time is in seconds
        }
    }

}
