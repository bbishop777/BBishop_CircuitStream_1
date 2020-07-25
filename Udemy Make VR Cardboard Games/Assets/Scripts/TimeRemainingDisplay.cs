using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimeRemainingDisplay : MonoBehaviour
{
	LevelManager levelManager;
	Text text;


	// Use this for initialization
	void Start()
	{
		levelManager = FindObjectOfType<LevelManager>();
		text = GetComponent<Text>();

	}

	// Update is called once per frame
	void Update()
	{
		text.text = "Time Remaining: " + levelManager.timeTillNextLevel;
	}
}
