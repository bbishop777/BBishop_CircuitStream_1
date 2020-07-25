//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR;
//using UnityEngine.SpatialTracking;
//using UnityEngine.SceneManagement;                                  //To access multiple scenes in our project

//public class Gamemanager : MonoBehaviour                            //We are using this Gamemanager script for the UI vs the SceneChanger script because we
//                                                                    //this sets floor level at roomscale version for HMD's (see start function).
//{
//    void Start()
//    {
//        XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale);  //SetTrackingSpace is deprecated however the XRInteraction Toolkit is the alternative
//                                                                     //but is not yet mature and is similar to being in beta & doesn't work w/all headsets yet
//                                                                     //If making AR scene, would not need this line
//    }

//    public void LoadScene(int sceneNumber)                            //Since this function will be called by a button it must be public (also allows another
//                                                                      //script to call it
//    {
//        //SceneManager.LoadScene(1, LoadSceneMode.Single);            //We are hardcoding a value in here for scene # when we put 1 as the first paremeter. Next
//                                                                      //argument means to load a single scene and unload current scene. We can also use
//                                                                      //LoadSceneMode.Additive which allows multiple scenes to be loaded w/out unloading others
//        SceneManager.LoadScene(sceneNumber, LoadSceneMode.Single);    //Hardcoding works if only need one scene but instead, we use a variable to bring in the
//                                                                      //number of the scene we want since we have multiple scenes. So above. in function, we
//                                                                      //declare the parameter will be an integer and we will call it "sceneNumber"). So if this
//                                                                      //function was called from somewhere like this: "LoadScene(4);" it will pass 4 as the
//                                                                      //"sceneNumber".  When we create our UI button in Unity we will have an input field for the
//                                                                      //sceneNumber to go in.
//    }
//}
