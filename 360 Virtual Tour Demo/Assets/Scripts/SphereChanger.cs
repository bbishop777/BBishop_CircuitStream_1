using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereChanger : MonoBehaviour {

    //Keeps track of what sphere camera has been moved to
    public GameObject sphereNow;

    //To keep track of camera
    private GameObject theCamera;

    //Reference to the WhichSphere Script which is on the camera
    private WhichSphere sphereTrackingScript;

    //This object should be called 'Fader' and placed over the camera
    GameObject m_Fader;

    //This ensures that we don't mash to change spheres
    bool changing = false;


    void Awake()
    {
        theCamera = GameObject.Find("Main Camera");
        //Assign the WhichSphere script by grabbing it from the camer (which it is on)
        sphereTrackingScript = theCamera.GetComponent<WhichSphere>();
        //Find the fader object
        m_Fader = GameObject.Find("Fader");

        //Check if we found something
        if (m_Fader == null)
            Debug.LogWarning("No Fader object found on camera.");

    }


    public void ChangeSphere(Transform nextSphere)
    {

        //Start the fading process
        StartCoroutine(FadeCamera(nextSphere));
        sphereNow = nextSphere.gameObject;
        sphereTrackingScript.updateSphere(sphereNow);

    }

    IEnumerator FadeCamera(Transform nextSphere)
    {

        //Ensure we have a fader object
        if (m_Fader != null)
        {
            //Fade the Quad object in and wait 0.75 seconds
            changing = true;
            StartCoroutine(FadeIn(0.75f, m_Fader.GetComponent<Renderer>().material));
            yield return new WaitForSeconds(0.75f);

            //Change the camera position
            Camera.main.transform.parent.position = nextSphere.position;

            //Fade the Quad object out 
            Material mat = m_Fader.GetComponent<Renderer>().material;
            //Debug.Log("What is alpha? " + mat.color.a);
            if (mat.color.a >= 1.0f)
            {
                changing = false;
                StartCoroutine(FadeOut(0.75f, m_Fader.GetComponent<Renderer>().material));
                yield return new WaitForSeconds(0.75f);
            }
            
        }
        else
        {
            //No fader, so just swap the camera position
            Camera.main.transform.parent.position = nextSphere.position;
        }


    }


    IEnumerator FadeOut(float time, Material mat)
    {
        //While we are still visible, remove some of the alpha colour
        while (mat.color.a > 0.0f && !changing)
        {
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a - (Time.deltaTime / time));
            yield return null;
        }
    }


    IEnumerator FadeIn(float time, Material mat)
    {
        //While we aren't fully visible, add some of the alpha colour
        while (mat.color.a < 1.0f && changing)
        {
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a + (Time.deltaTime / time));
            yield return null;
        }
    }


}
