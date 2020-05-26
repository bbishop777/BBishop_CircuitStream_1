using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyboardClicker : MonoBehaviour
{
    [HideInInspector]
    public string m_textForScreen;

    public Button _buttonScreen;

    private char[] _screenList;

    private GameObject _capLetters;
    private GameObject _lowercaseLetters;

    private bool isShiftOn;

    private ColorBlock _shiftColors;
    private ColorBlock _screenColors;

    private Button _buttonShift;


    // Use this for initialization
    void Awake()
    {
        isShiftOn = false;
        _capLetters = GameObject.Find("CapitalLetters");
        _lowercaseLetters = GameObject.Find("LowerCaseLetters");
        _capLetters.gameObject.SetActive(false);
        _buttonShift = GameObject.Find("Shift").GetComponent<Button>();
        _shiftColors = _buttonShift.GetComponent<Button>().colors;
        ChangeColors(false);
        _screenColors = _buttonScreen.colors;
    }

    private void Start()
    {
        Invoke("ClearScreen", .5f);
    }

    void ClearScreen()
    {
        _buttonScreen.GetComponentInChildren<Text>().text = " ";
    }

    void Update()
    {


    }

   public  void ButtonPress()
    {
        string wat = EventSystem.current.currentSelectedGameObject.name;
        switch (wat)
        {
            case "Shift" when isShiftOn == false:
                isShiftOn = true;
                _capLetters.gameObject.SetActive(true);
                _lowercaseLetters.gameObject.SetActive(false);
                _buttonShift.GetComponentInChildren<Text>().text = "Shift On";
                ChangeColors(true);
                break;
            case "Shift" when isShiftOn == true:
                isShiftOn = false;
                _capLetters.gameObject.SetActive(false);
                _lowercaseLetters.gameObject.SetActive(true);
                _buttonShift.GetComponentInChildren<Text>().text = "Shift Off";
                ChangeColors(false);
                break;
            case "Space":
                m_textForScreen += " ";
                break;
            case "Delete":
                m_textForScreen = m_textForScreen.TrimEnd(m_textForScreen[m_textForScreen.Length - 1]);
                break;
            case "Comma":
                m_textForScreen += ",";
                break;
            case "Period":
                m_textForScreen += ".";
                break;
            case "QuestionMark/ForwardSlash" when isShiftOn == false:
                m_textForScreen += "/";
                break;
            case "QuestionMark/ForwardSlash" when isShiftOn == true:
                m_textForScreen += "?";
                break;
            case "Colon/Semicolon" when isShiftOn == false:
                m_textForScreen += ";";
                break;
            case "Colon/Semicolon" when isShiftOn == true:
                m_textForScreen += ":";
                break;
            case "\"":
                m_textForScreen += wat;
                break;
            case "Bracket/SqBracketStart" when isShiftOn == false:
                m_textForScreen += "[";
                break;
            case "Bracket/SqBracketStart" when isShiftOn == true:
                m_textForScreen += "{";
                break;
            case "Bracket/SqBracketEnd" when isShiftOn == false:
                m_textForScreen += "]";
                break;
            case "Bracket/SqBracketEnd" when isShiftOn == true:
                m_textForScreen += "}";
                break;
            case "Bar/Backslash" when isShiftOn == false:
                m_textForScreen += "\\";
                break;
            case "Bar/Backslash" when isShiftOn == true:
                m_textForScreen += "|";
                break;
            case "Enter":
                CheckPassPhrase();
                break;
            default:
                m_textForScreen += wat;
                break;
        }
       
        _buttonScreen.GetComponentInChildren<Text>().text = m_textForScreen;
       //buttonScreen.GetComponentInChildren<Text>().text = m_textForScreen;
    } 

   void ChangeColors(bool colorState)
    {
        if(colorState == true)
        {
            _shiftColors.normalColor = new Color(0, 9, 255);
            _shiftColors.selectedColor = new Color(0, 9, 255);
            _buttonShift.colors = _shiftColors;
        }
        else
        {
            _shiftColors.normalColor = new Color(233, 0, 208);
            _shiftColors.selectedColor = new Color(233, 0, 208);
            _buttonShift.colors = _shiftColors;
        }
    } 
    
    void CheckPassPhrase()
    {
        Debug.Log("Checking the phrase");
        if (m_textForScreen == "The Blue|Dog{[;]}. was sent\\?/, via the \"train\".")
        {
            _screenColors.normalColor = new Color32(24, 250, 4, 255);
            _buttonScreen.colors = _screenColors;
            m_textForScreen = "Access Granted.  Welcome Blue Dog";
        }
        else
        {
            _screenColors.normalColor = new Color32(250, 14, 4, 255);
            _buttonScreen.colors = _screenColors;
            m_textForScreen = "ACCESS DENIED!! Death Monkeys deployed to your location!";
        }
    }
}

