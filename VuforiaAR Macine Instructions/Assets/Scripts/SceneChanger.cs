using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;      //This library allows us to call SceneManager & deal with the scenes in a project

public class SceneChanger : MonoBehaviour
{

    public void LoadScene(int sceneNumber)                          //For a function to be called by a button (as in this example) the function must be public
                                                                    //here my argument is an integer and we will call it "sceneNumber"
    {
        SceneManager.LoadScene(sceneNumber, LoadSceneMode.Single);
    }
}
