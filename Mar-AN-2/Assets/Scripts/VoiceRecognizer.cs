//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//using System;   //Refer to  or convert Functions as Actions (does much more too)
//using System.Linq; //allows us to use ToArray() which helps us convert our dictionary key values to an array (likes words to actions)
//using UnityEngine.Windows.Speech; //Engine from Windows to use keyword recognizer

//public class VoiceRecognizer : MonoBehaviour
//{
//    private Dictionary<string, Action> m_keywordActions = new Dictionary<string, Action>(); //Declaring a private dictionary, takes 2 types of datatypes
//                                                                                            //could be any kind. We have keys and values. Our values will be references to
//                                                                                            //actions and our keys will be strings in this case. When dictionary recognizes
//                                                                                            //string it will look up actions then we can invoke it.
//                                                                                            //We initalize it as a new empty dictionary. even if public it won't be seen in
//                                                                                            //Unity editor. Our dicitonary will take a string as key and Action as value
//                                                                                            //Dictionaries are like object and don't need to be used just w/Speech Recognition
//    private KeywordRecognizer m_keywordRecognizer;  //var for data type KeyworkRecognizer..listens to speech

//    void Start()
//    {
//        m_keywordActions.Add("Create cube", SpawnCube); //Add to our dictionary a key and value (a word and it's definition)
//        m_keywordActions.Add("Ra Za Na Ba Do A", SpawnCube); //Here we can use phoenetic spellings (this is from Jaba the Hut in Star Wars) to add another key/value
//                                                             //and can use a different key for the same action

//        m_keywordRecognizer = new KeywordRecognizer(m_keywordActions.Keys.ToArray()); //Takes our keyword actions and makes them an array.Start by creating a new one and we
//                                                                                      //take just the keys from our dictionary and make them an array and add it to this
//        m_keywordRecognizer.OnPhraseRecognized += OnKeywordRecognized;  //OnPhraseRecognized is a method or event. When keywordrecognizer hears one of the keys entered this
//                                                                        //event fires and we add (and call) OnKeywordRecognized function as a string value(?). This is an 
//                                                                        //event listener (like the list events in Unity editor for button presses). So once it recognizes event,
//                                                                        //it runs OnKeyRecognized function which takes our recognized phrases and invokes their associated actions
//        m_keywordRecognizer.Start(); //We can stop and start the listener whenever we like. Ex: if talking to AI but move away..after certain distance it stop listening
//    }

//    void OnKeywordRecognized(PhraseRecognizedEventArgs args) //in this created function we add PhraseRecognizedEventArgs which gives us info on our recognized phrases called
//                                                            //args.. our parameter
//    {
//        m_keywordActions[args.text].Invoke();   //args.text gives us the string value of our dictionary and invoke the action associated with it. This goes into our dictionary
//                                                // which is m_keywordActions to look up recognized phrase (args.text) and invoke the action associated with it
//    }

//    public Transform m_spawnTransform;//spawn point set in Unity editor (empty game object)

//    public GameObject m_prefabCube; //Set in unity editor from a prefab
//    void SpawnCube()
//    {
//        Instantiate(m_prefabCube, m_spawnTransform.position, m_spawnTransform.rotation); //take a new prefab and parent it to these transform postions

//    }
//}
