using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;   //Refer to Functions as Actions
using System.Linq; //ToArray() helps us convert our dictionary key values to an array (likes words to actions)
using UnityEngine.Windows.Speech; //Engine from Windows to use keyword recognizer

public class VoiceRecognizer : MonoBehaviour
{
    private Dictionary<string, Action> m_keywordActions = new Dictionary<string, Action>(); //Declaring a private dictionary, takes 2 types of datatypes
                                                                                            //could be any kind..initalize it as a new empty dictionary
    private KeywordRecognizer m_keywordRecognizer;  //var for data type KeyworkRecognizer

    // Start is called before the first frame update
    void Start()
    {
        m_keywordActions.Add("Create cube", SpawnCube);
        m_keywordActions.Add("Ra Za Na Ba Do A", SpawnCube); //Here we can use phoenetic spellings (this is from Jaba the Hut in Star Wars)

        m_keywordRecognizer = new KeywordRecognizer(m_keywordActions.Keys.ToArray()); //Takes our keyword actions and makes them an array
        m_keywordRecognizer.OnPhraseRecognized += OnKeywordRecognized;
        m_keywordRecognizer.Start();
    }

    void OnKeywordRecognized(PhraseRecognizedEventArgs args)
    {
        m_keywordActions[args.text].Invoke();   //args.text gives us the string value of our dictionary
    }

    public Transform m_spawnTransform;

    public GameObject m_prefabCube;
    void SpawnCube()
    {
        Instantiate(m_prefabCube, m_spawnTransform.position, m_spawnTransform.rotation);

    }
}
