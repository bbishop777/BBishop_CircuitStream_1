using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        
    }

    // Update is called once per frame but LateUpdate is called after everything else is completed in the frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        
    }
}
