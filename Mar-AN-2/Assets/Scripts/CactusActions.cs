using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CactusActions : MonoBehaviour
{
    //public float m_turnSpeed = 15;
    

    [HideInInspector]
    public CactusButtonBehavior m_CBBScript;

    [HideInInspector]
    public string m_textForButton;

    [HideInInspector]
    public bool m_wallExtended;

    public GameObject m_Cam1;
    public GameObject m_Cam2;
    public Animator m_wallAnimator;

    private Animator anim;
    private Button _button;
    private Button _button1;
    private Button _button2;
    private Button _button3;
    private Button _button4;
    private Button _button5;
   // private Camera _camera1;
   //private Camera _camera2;
    
    private GameObject _brick;
    private Vector3 _scaleChanger;

    // Use this for initialization
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>(); 
        _button = GameObject.Find("Button").GetComponent<Button>(); //Was told that GameObject.Find is a very inefficient way to reference another GameObject...will clean this up by making
                                                                    //these variables public and assigning them in the Unity Inspector
        _button1 = GameObject.Find("Button1").GetComponent<Button>();
        _button2 = GameObject.Find("Button2a").GetComponent<Button>();
        _button3 = GameObject.Find("Button2b").GetComponent<Button>();
        _button4 = GameObject.Find("Button2c").GetComponent<Button>();
        _button5 = GameObject.Find("Button2d").GetComponent<Button>();
        _brick = GameObject.Find("brick433");
       //camera1 = GameObject.Find("Main Camera").GetComponent<Camera>();
       //camera2 = GameObject.Find("2ndCamera").GetComponent<Camera>();

        _button1.gameObject.SetActive(false);
        _button2.gameObject.SetActive(false);
        _button3.gameObject.SetActive(false);
        _button4.gameObject.SetActive(false);
        _button5.gameObject.SetActive(false);
        _scaleChanger = new Vector3(75.9951f, 1f, 1f);
        // gameObject.transform.eulerAngles = rot;
    }

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        KeyChecker();
        

    }

    public void Awaken()
    {
        _button.gameObject.SetActive(false);
        anim.SetBool("isAwake", true);
        Invoke("DisplayWallViewInstructions", 3);

    }

    public void DisplayWallViewInstructions()
    {
        _button1.gameObject.SetActive(true);
    }

    public void SeeWall()
    {
        _button1.gameObject.SetActive(false);
        anim.SetBool("isSeeingWall", true);
        Invoke("DisplaySideInstructions", 5);
    }

    public void DisplaySideInstructions()
    {
        _button2.gameObject.SetActive(true);
        _button3.gameObject.SetActive(true);
        _button4.gameObject.SetActive(true);
        _button5.gameObject.SetActive(true);
    }

    public void ChargeWall()
    {
        _button1.gameObject.SetActive(false);
        _button2.gameObject.SetActive(false);
        _button3.gameObject.SetActive(false);
        _button4.gameObject.SetActive(false);
        _button5.gameObject.SetActive(false);
        anim.SetBool("isCharging", true);
        m_textForButton = "Wow! Mr.Cactus will need to sleep that off! Try again!";
        StartCoroutine(TextChangeForButton(_button4, 2f, m_textForButton, false));
    }

    public void HitWall()
    {
        _button2.gameObject.SetActive(false);
        _button3.gameObject.SetActive(false);
        _button4.gameObject.SetActive(false);
        _button5.gameObject.SetActive(false);
        anim.SetBool("isHitting", true);
        Invoke("WallPunchBack", 1.5f);
    }
    public void WallPunchBack()
    {
        if (m_wallAnimator.enabled)
        {
            m_wallAnimator.enabled = !m_wallAnimator.enabled;
        }
        _brick.transform.localScale += _scaleChanger;
        m_wallExtended = true;
        anim.SetBool("isPunched", true);
        m_textForButton = "Oh Wow! Mr. Cactus wasn't expecting that! Better let him sleep that off and try again later!";
        StartCoroutine(TextChangeForButton(_button1, 2f, m_textForButton, m_wallExtended));
    }

    public void WaveToWall()
    {
        _button2.gameObject.SetActive(false);
        _button3.gameObject.SetActive(false);
        _button4.gameObject.SetActive(false);
        _button5.gameObject.SetActive(false);
        anim.SetBool("isWaving", true);
        Invoke("MakeWindow", 2f);
    }

    public IEnumerator TextChangeForButton(Button bttn, float delayTime, string textForButton, bool wallCondition)
    {
        yield return new WaitForSeconds(delayTime);
        if (wallCondition == true)
        {
            _brick.transform.localScale -= _scaleChanger;
            wallCondition = false;
            if (!m_wallAnimator.enabled)
            {
               m_wallAnimator.enabled = m_wallAnimator.enabled;
            }
        }
        bttn.gameObject.GetComponentInChildren<Text>().text = textForButton;
        bttn.gameObject.SetActive(true);
        //m_CBBScript.m_NewMessage = "Wow! Mr. Cactus will need to sleep that off! Try again!";
        //m_CBBScript.UpdateButton2b();

    }

    public void MakeWindow()
    {
        transform.Find("body_mesh").gameObject.SetActive(false);
        transform.Find("CATRigHub001").gameObject.SetActive(false);
        m_Cam2.SetActive(true);
        m_Cam1.SetActive(false);
        m_wallAnimator.SetBool("isWindowForming", true);
        Invoke("CameraPanToHappyEnd", 2f);
    }

    public void CameraPanToHappyEnd()
    {
        m_Cam2.SetActive(true);
        m_Cam1.SetActive(false);
        m_wallAnimator.SetBool("isCameraPanning", true);
        m_textForButton = "Ahhh! The sunlight feels good to Mr. Cactus!";
        StartCoroutine(TextChangeForButton(_button4, 2f, m_textForButton, false));
    }

    private void KeyChecker()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("isHopping", true);
            //Debug.Log("W key was held down! " + anim.GetBool("isHopping"));
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            anim.SetBool("isHopping", false);
            //Debug.Log("W key was held released! " + anim.GetBool("isHopping"));
        }

        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("isPunching", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("isPunching", false);
        }

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isWaving", true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("isWaving", false);
        }

        //else if (Input.GetKeyUp(KeyCode.W))
        //{
        //    anim.SetBool("isWaving", false);
        //}


        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.Translate(Vector3.left * m_moveSpeed * Time.deltaTime); //Here we translate back on X axis.
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.Translate(Vector3.right * m_moveSpeed * Time.deltaTime); //Here we translate forward on X axis.
        //}
        //transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * m_turnSpeed, Space.World);
        //transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * m_turnSpeed);
    }
}

