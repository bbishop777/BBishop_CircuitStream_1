using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CactusButtonBehavior : MonoBehaviour
{
    public void Start()
    {
        Debug.Log("YYOU HAVE REACHED ME");
    }
    [SerializeField]
    private Text m_buttonText;

    [HideInInspector]
    public string m_NewMessage;

    public void UpdateButton2b()
    {
        m_buttonText.text = m_NewMessage; 
    }
}
