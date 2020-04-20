using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnotherDataClass
{
    public string myString;  //Because string is the first element in this array, when it is name in the inspector, it will
                               //name the Element whatever you name the strings.
    public Color myColor;
    public GameObject myGameObject;
}