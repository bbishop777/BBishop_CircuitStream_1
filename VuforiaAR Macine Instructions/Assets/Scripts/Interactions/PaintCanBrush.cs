using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PaintCanBrush : MonoBehaviour
{
    public GameObject m_prefabTrail1;  
    public GameObject m_prefabTrail2;
    public GameObject m_prefabTrail3;
    public GameObject m_CanBody1;
    public GameObject m_CanBody2;
    public GameObject m_CanBody3;

    public Transform m_trailSpawn1;      
    public Transform m_trailSpawn2;
    public Transform m_trailSpawn3;

    private GameObject m_currentTrail1;
    private GameObject m_currentTrail2;
    private GameObject m_currentTrail3;

    private Coroutine m_materialChangeRoutine;
    private Material m_canBody1Color;
    private Material m_canBody2Color;
    private Material m_canBody3Color;

    private bool m_isPaintCanBeingDipped = false;
    private bool m_can1Loaded = false;
    private bool m_can2Loaded = false;
    private bool m_can3Loaded = false;
    private bool m_delay = false;

    private List<GameObject> m_drawnTrails = new List<GameObject>();

    private void Start()
    {
        m_CanBody2.transform.localScale = new Vector3(0, 0, 0);
        m_CanBody3.transform.localScale = new Vector3(0, 0, 0);

    }   
    void TriggerDown()  //While button is down
    {
        if (m_can1Loaded)
        {
            m_currentTrail1 = Instantiate(m_prefabTrail1, m_trailSpawn1);
        }
        else if (!m_can1Loaded)
        {
            m_currentTrail1 = null;
        }
        if (m_can2Loaded)
        {
            m_currentTrail2 = Instantiate(m_prefabTrail2, m_trailSpawn2);
        }
        else if (!m_can2Loaded)
        {
            m_currentTrail2 = null;
        }
        if (m_can3Loaded)
        {
            m_currentTrail3 = Instantiate(m_prefabTrail3, m_trailSpawn3);
        }
        else if (!m_can3Loaded)
        {
            m_currentTrail3 = null;
        }

    }

    void TriggerUp()        //When button is released
    {
        if (m_currentTrail1)
        {
            m_currentTrail1.transform.SetParent(null);
        }
        if (m_currentTrail2)
        {
            m_currentTrail2.transform.SetParent(null);
        }
        if (m_currentTrail3)
        {
            m_currentTrail3.transform.SetParent(null);
        }
        m_drawnTrails.Add(m_currentTrail1);
        m_drawnTrails.Add(m_currentTrail2);
        m_drawnTrails.Add(m_currentTrail3);
    }

    void GrabReleased()
    {
        if (m_currentTrail1)
        {
            m_currentTrail1.transform.SetParent(null);
        }
        if (m_currentTrail2)
        {
            m_currentTrail2.transform.SetParent(null);
        }
        if (m_currentTrail3)
        {
            m_currentTrail3.transform.SetParent(null);
        }
        m_drawnTrails.Add(m_currentTrail1);
        m_drawnTrails.Add(m_currentTrail2);
        m_drawnTrails.Add(m_currentTrail3);
    }
    void MenuDown() //going to be menu down on Vive controller or X or A on Touch controllers.
    {
        if (m_drawnTrails.Count > 0)
        {
            GameObject lineToBeDeleted = m_drawnTrails[m_drawnTrails.Count - 1];
            m_drawnTrails.Remove(lineToBeDeleted);
            Destroy(lineToBeDeleted);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (m_delay)
        {
            return;
        }
        else
        {
            m_CanBody3.GetComponent<MeshRenderer>().material = m_CanBody2.GetComponent<MeshRenderer>().material;
            m_CanBody2.GetComponent<MeshRenderer>().material = m_CanBody1.GetComponent<MeshRenderer>().material;
            m_CanBody1.GetComponent<MeshRenderer>().material = collision.collider.GetComponent<MeshRenderer>().material;
            CheckAndSet();
            if (m_can1Loaded)
            {
                m_prefabTrail1.GetComponent<TrailRenderer>().material = m_CanBody1.GetComponent<MeshRenderer>().material;
            }
            if (m_can2Loaded)
            {
                m_prefabTrail2.GetComponent<TrailRenderer>().material = m_CanBody2.GetComponent<MeshRenderer>().material;
            }
            if (m_can3Loaded)
            {
                m_prefabTrail3.GetComponent<TrailRenderer>().material = m_CanBody3.GetComponent<MeshRenderer>().material;
            }
            m_materialChangeRoutine = StartCoroutine(WaitingTime());
        }

    }

    private void CheckAndSet()
    {
        m_canBody1Color = m_CanBody1.GetComponent<MeshRenderer>().material;
        m_canBody2Color = m_CanBody2.GetComponent<MeshRenderer>().material;
        m_canBody3Color = m_CanBody3.GetComponent<MeshRenderer>().material;

        if (!m_canBody1Color.name.Contains("Chrome"))
        {
            m_can1Loaded = true;
        }
        else if (m_canBody1Color.name.Contains("Chrome"))
        {
            m_can1Loaded = false;
        }
        if (!m_canBody2Color.name.Contains("Chrome"))
        {
            m_can2Loaded = true;
            m_CanBody2.transform.localScale = new Vector3(0.3598993f, 13.92177f, 0.1528888f);         
        }
        else if (m_canBody2Color.name.Contains("Chrome"))
        {
            m_can2Loaded = false;
            m_CanBody2.transform.localScale = new Vector3(0, 0, 0); 
        }
        if (!m_canBody3Color.name.Contains("Chrome"))
        {
            m_can3Loaded = true;
            m_CanBody3.transform.localScale = new Vector3(0.3598993f, 13.92177f, 0.1528888f);
        }
        else if (m_canBody1Color.name.Contains("Chrome"))
        {
            m_can3Loaded = false;
            m_CanBody3.transform.localScale = new Vector3(0, 0, 0);
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        //m_isPaintCanBeingDipped = false;
       // StopCoroutine(m_materialChangeRoutine);
    }

    IEnumerator WaitingTime()
    {
        m_delay = true;
        yield return new WaitForSeconds(5f);
        m_delay = false;
    }
}
