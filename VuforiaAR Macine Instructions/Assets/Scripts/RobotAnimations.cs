using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimations : MonoBehaviour {

	Vector3 rot = Vector3.zero;
	float rotSpeed = 40f;
	Animator anim;

	// Use this for initialization
	void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
		gameObject.transform.eulerAngles = rot;
	}

	// Update is called once per frame
	void Update()
	{
		CheckKey();
		gameObject.transform.eulerAngles = rot;
	}

	void CheckKey()
	{
		// Walk
		if (Input.GetKey(KeyCode.W))
		{
			anim.SetBool("Walk_Anim", true);
		}
		else if (Input.GetKeyUp(KeyCode.W))
		{
			anim.SetBool("Walk_Anim", false);
		}

		// Rotate Left
		if (Input.GetKey(KeyCode.A))
		{
			rot[1] -= rotSpeed * Time.fixedDeltaTime;
		}

		// Rotate Right
		if (Input.GetKey(KeyCode.D))
		{
			rot[1] += rotSpeed * Time.fixedDeltaTime;
		}

		// Roll
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			if (anim.GetBool("Roll_Anim") && anim.GetBool("AttackRoll_Anim") && !anim.GetBool("Open_Anim"))
			{
				anim.SetBool("AttackRoll_Anim", false);
				anim.SetBool("Open_Anim", true);
				anim.SetBool("Roll_Anim", false);
			}
			else if (anim.GetBool("Roll_Anim"))
            {
				anim.SetBool("Roll_Anim", false);
            }
            else
			{
				anim.SetBool("Roll_Anim", true);
			}
		}

		// Protective Close
		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			if (!anim.GetBool("Open_Anim"))
			{
				anim.SetBool("Open_Anim", true);
			}
			else
			{
				anim.SetBool("Open_Anim", false);
            }
		}

        //Go from protective close to roll
        if(Input.GetKeyDown(KeyCode.Mouse2))
        {
			Debug.Log("Middle mouse button pushed");
            if(!anim.GetBool("Open_Anim") && !anim.GetBool("AttackRoll_Anim"))
            {
				anim.SetBool("Roll_Anim", true);
				anim.SetBool("AttackRoll_Anim", true);	
            }
            else
            {
				anim.SetBool("AttackRoll_Anim", false);
            }
        }
	}
}
