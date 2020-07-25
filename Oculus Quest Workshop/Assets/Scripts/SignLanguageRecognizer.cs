//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//[System.Serializable]
//public struct FingerSign
//{
//    public string name;
//    public List<Vector3> boneInfo;
//    public UnityEvent onSignRecognized;
//}

//public class SignLanguageRecognizer : MonoBehaviour
//{
//    public bool m_recording;
//    public float m_threshold = 0.5f;
//    public List<FingerSign> m_signs = new List<FingerSign>();

//    private OVRSkeleton m_skeleton;
//    private List<OVRBone> m_bonePoints = new List<OVRBone>();
//    private FingerSign m_lastSign;

//    // Start is called before the first frame update
//    void Start()
//    {
//        m_skeleton = GetComponent<OVRSkeleton>();
//        m_bonePoints = new List<OVRBone>(m_skeleton.Bones);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(Input.GetKeyDown(KeyCode.Space) && m_recording)
//        {
//            SaveSign();
//        }

//        FingerSign currentSign = Recognized();

//        bool hasRecognized = !currentSign.Equals(new FingerSign());

//        if(hasRecognized && !currentSign.Equals(m_lastSign))
//        {
//            m_lastSign
//        }
//    }

//    void SaveSign()
//    {
//        FingerSign g = new FingerSign();
//        g.name = "New Sign";
//        List<Vector3> info = new List<Vector3>();
//        foreach (var bone in m_bonePoints)
//        {
//            info.Add(m_skeleton.transform.InverseTransformDirection(bone.Transform.position));
//        }

//        g.boneInfo = info;
//        m_signs.Add(g);
//    }

//    FingerSign Recognized()
//    {
//        FingerSign currentSign = new Sign();
//        float currentMin = Mathf.Infinity;

//        foreach(var fingersign in m_signs)
//        {
//            float sumDistance = 0;
//            bool isDiscarded = false;

//            for(int i = 0; i < m_bonePoints.Count; i++)
//            {
//                Vector3 currentData = m_skeleton.transform.InverseTransformPoint(m_bonePoints[i].Transform.position);
//                float distance = Vector3.Distance(currentData, fingersign.boneInfo[i]); 
//                if(distance > m_threshold)
//                {
//                    isDiscarded = true;
//                    break;
//                }
//                sumDistance += distance;
//            }
//        }   if(isDiscarded && sumDistance < currentMin)
//        {
//            currentMin = sumDistance;
//            currentSign = 
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct FingerSign
{
    public string name;
    public List<Vector3> boneInfo;
    public UnityEvent onSignRecognized;
}

public class SignLanguageRecognizer : MonoBehaviour
{
    public bool m_recording;
    public float m_threshold = 0.5f;
    public List<FingerSign> m_signs = new List<FingerSign>();

    private OVRSkeleton m_skeleton;
    private List<OVRBone> m_bonePoints = new List<OVRBone>();
    private FingerSign m_lastSign;

    // Start is called before the first frame update
    void Start()
    {
        m_skeleton = GetComponent<OVRSkeleton>();
        m_bonePoints = new List<OVRBone>(m_skeleton.Bones);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_recording)
        {
            SaveSign();
        }

        FingerSign currentSign = Recognized();

        bool hasReognized = !currentSign.Equals(new FingerSign());

        if (hasReognized && !currentSign.Equals(m_lastSign))
        {
            m_lastSign = currentSign;
            currentSign.onSignRecognized.Invoke();
        }
    }

    void SaveSign()
    {
        FingerSign g = new FingerSign();
        g.name = "New Sign";
        List<Vector3> info = new List<Vector3>();

        foreach (var bone in m_bonePoints)
        {
            info.Add(m_skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }

        g.boneInfo = info;
        m_signs.Add(g);
    }

    FingerSign Recognized()
    {
        FingerSign currentSign = new FingerSign();
        float currentMin = Mathf.Infinity;

        foreach (var fingerSign in m_signs)
        {
            float sumDistance = 0;
            bool isDiscarded = false;

            for (int i = 0; i < m_bonePoints.Count; i++)
            {
                Vector3 currentData = m_skeleton.transform.InverseTransformPoint(m_bonePoints[i].Transform.position);
                float distance = Vector3.Distance(currentData, fingerSign.boneInfo[i]);
                if (distance > m_threshold)
                {
                    isDiscarded = true;
                    break;
                }
                sumDistance += distance;
            }

            if (isDiscarded && sumDistance < currentMin)
            {
                currentMin = sumDistance;
                currentSign = fingerSign;
            }
        }

        return currentSign;
    }
}

