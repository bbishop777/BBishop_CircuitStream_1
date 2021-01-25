using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using System;
using System.Linq;
using UnityEngine.Windows.Speech;
using Vuforia;

public class RobbieMove : MonoBehaviour
{
    public NavMeshAgent m_agentRobbieNavMesh;  //Robot's NavMeshAgent Component
    public float m_travelSpeed;

    public GameObject m_bitGun; //The gun
    public GameObject m_player; //The Simhand or player
    public GameObject m_spawnTargetPrefab; //Prefab of target marker to be spawned

    private Dictionary<string, Action> m_keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer m_keywordRecognizer;

    public bool m_isSeekingGun = false;
    public bool m_hasGun = false;
    public bool m_needToDropGun = false;
    public bool m_isChosenLocationValid = false; //Was Raycast to deliver gun to valid?
    public bool m_isPointDesignated = false;     //Was a point chosen to deliver gun?

    private RaycastHit m_shot;          //Valid Raycast to point to player
    private RaycastHit m_hitPoint;      //Valid Raycast to point where gun will be delivered
    private LineRenderer m_lineToPoint; //Line Renderer to point to place to deliver gun
    private GameObject m_currentSpawnTargetObject; //Current target object marking where gun is to be delivered

    private float m_allowedDistance = .5f;
    private float m_playerDistance;

    private List<GameObject> m_designatedSpawns = new List<GameObject>(); //List storing target marker objects

    // private Vector3 personalSpace;
    // public GameObject m_robbie;
    private void Awake()
    {
        m_agentRobbieNavMesh = GetComponent<NavMeshAgent>();
        m_agentRobbieNavMesh.enabled = false;

    }

    private void Start()
    {
        m_lineToPoint = m_player.GetComponent<LineRenderer>();
        m_keywordActions.Add("Robbie, get my gun", GetGun);
        m_keywordActions.Add("Robbie, drop my gun", DropGun);
        m_keywordActions.Add("Robbie, take my gun to the target area", TransportGun);
        m_keywordRecognizer = new KeywordRecognizer(m_keywordActions.Keys.ToArray());
        m_keywordRecognizer.OnPhraseRecognized += OnKeywordRecognized;
        m_keywordRecognizer.Start();
    }
    void OnKeywordRecognized(PhraseRecognizedEventArgs args)
    {
        m_keywordActions[args.text].Invoke();
    }

    void GetGun()
    {
        m_isSeekingGun = true;
        transform.LookAt(m_bitGun.transform);
    }

    void DropGun()
    {
        if (m_hasGun)
        {
            m_needToDropGun = true;
        }

    }

    void TransportGun()
    {
        if (m_hasGun && m_isChosenLocationValid)
        {

            if (m_designatedSpawns.Count > 0)
            {
                transform.LookAt(m_currentSpawnTargetObject.transform);
                m_isPointDesignated = true;
            }
        }
    }

    void DropGunOnTarget()
    {
        if (m_isChosenLocationValid && m_isPointDesignated && m_hasGun)
        {
            m_bitGun.GetComponent<Rigidbody>().isKinematic = false;
            m_bitGun.transform.SetParent(null);
            m_bitGun.transform.position = new Vector3(transform.position.x, 2.048043f, transform.position.z + .0125f);
            m_hasGun = false;
            m_needToDropGun = false;
            m_isPointDesignated = false;
            m_isChosenLocationValid = false;
            m_agentRobbieNavMesh.enabled = true;
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.P))  //Requesting a raycast to find a place to deliver gun      
        {
            if (Physics.Raycast(m_player.transform.position, m_player.transform.forward, out m_hitPoint))  //If raycast is successful..turn on line..declare local valid
            {
                m_lineToPoint.enabled = true;
                m_lineToPoint.SetPosition(0, m_player.transform.position);
                m_lineToPoint.SetPosition(1, m_hitPoint.point);
                m_isChosenLocationValid = true;
            }
            else                                    //If Raycast fails..keep line off and declare local invalid
            {
                m_lineToPoint.enabled = false;
                m_isChosenLocationValid = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.P)) //Once key is let up after finding a place
        {
            m_lineToPoint.enabled = false;       //turn off line
            if (m_isChosenLocationValid && !m_isPointDesignated)  //If local valid and we have not yet put a target there..spawn target, put at end of hit.point & add it to list 
            {
                m_currentSpawnTargetObject = Instantiate(m_spawnTargetPrefab);
                m_currentSpawnTargetObject.transform.position = new Vector3(m_hitPoint.point.x, m_hitPoint.point.y, m_hitPoint.point.z);
                m_designatedSpawns.Add(m_currentSpawnTargetObject);
                if (m_designatedSpawns.Count >= 2)   //If we have more than one in list..delete the first one..keeping only one target active at a time
                {
                    GameObject spawnToBeDeleted = m_designatedSpawns[0];
                    m_designatedSpawns.Remove(spawnToBeDeleted);
                    Destroy(spawnToBeDeleted);
                }
            }
        }
        if (m_isChosenLocationValid && m_isPointDesignated && m_hasGun)  //If the local is valid and we have placed a target and Robot has gun
        {
            if (m_agentRobbieNavMesh.enabled == true) //If Robot's navmeshagent is on, turn it off and start toward target
            {
                m_agentRobbieNavMesh.enabled = false;
                transform.position = Vector3.MoveTowards(transform.position, m_currentSpawnTargetObject.transform.position, m_travelSpeed);
            }
            else if (m_agentRobbieNavMesh.enabled == false) //If navmeshagent is already off just head toward target
            {
                transform.position = Vector3.MoveTowards(transform.position, m_currentSpawnTargetObject.transform.position, m_travelSpeed);
            }
            //if (transform.position == m_currentSpawnTargetObject.transform.position) //Once Robot reaches target, drop gun, Negate:needToDropGun, HasGun,isPointDesignated, Valid Locale
            //{


            //}

        }
        if (m_isSeekingGun && !m_hasGun) //Will only hit if Robbie is seeking gun and doesn't have gun
        {
            if (m_agentRobbieNavMesh.enabled == true)
            {
                m_agentRobbieNavMesh.enabled = false;
                transform.position = Vector3.MoveTowards(transform.position, m_bitGun.transform.position, m_travelSpeed);
            }
            else if (m_agentRobbieNavMesh.enabled == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, m_bitGun.transform.position, m_travelSpeed);
            }
            if (transform.position == m_bitGun.transform.position) //If arrive at gun set seeking gun to false, has gun as true, enable NavMeshAgent so he returns to player
            {
                m_bitGun.GetComponent<Rigidbody>().isKinematic = true;
                m_bitGun.transform.SetParent(transform);
                m_isSeekingGun = false;
                m_hasGun = true;
                m_agentRobbieNavMesh.enabled = true;
            }
        }
        else if (!m_isSeekingGun || m_hasGun && !m_isPointDesignated)
        {
            transform.LookAt(m_player.transform);
            if (m_needToDropGun)
            {
                m_bitGun.GetComponent<Rigidbody>().isKinematic = false;
                m_bitGun.transform.SetParent(null);
                m_bitGun.transform.position = new Vector3(transform.position.x, 2.048043f, transform.position.z + .0125f);
                m_hasGun = false;
                m_needToDropGun = false;
            }
            else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out m_shot))
            {
                m_playerDistance = m_shot.distance;
                if (m_playerDistance >= m_allowedDistance)
                {
                    m_agentRobbieNavMesh.enabled = true;
                    // m_travelSpeed = 0.02f;
                    // m_robbie.GetComponent<Animation>().Play("RobotIdle");
                    // transform.position = Vector3.MoveTowards(transform.position, m_player.transform.position, m_travelSpeed);
                    m_agentRobbieNavMesh.SetDestination(m_player.transform.localPosition);
                }
                else if (m_playerDistance < m_allowedDistance && m_agentRobbieNavMesh.enabled == true)
                {
                    //m_followSpeed = 0;
                    //m_robbie.GetComponent<Animation>().Play("anim_Idle_Loop_S");
                    m_agentRobbieNavMesh.enabled = false;
                }
            }
        }

        // agentRobbie.Move(target.transform.localPosition - personalSpace);


    }
}
