using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))] //If you attach this script to something without a renderer, this line will create one
                                     //for it.
public class Xrayable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.renderQueue = 3002;
        #region Comments on Render Queue
        //So here we are grabbing the component this script is on &
        //setting its Render Queue to 3002 (beyond 3000 which is 
        //where transparencies are rendered. If this script is
        //attached to something without a Renderer, it will crash
        //and throw an error, so we set the RequireComponent above
        #endregion
    }
}
