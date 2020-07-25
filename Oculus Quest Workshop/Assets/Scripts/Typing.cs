using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Typing : MonoBehaviour
{
    public Text m_typingDisplay;

    public void Type(string letter)
    {
        m_typingDisplay.text += letter;
    }
}
