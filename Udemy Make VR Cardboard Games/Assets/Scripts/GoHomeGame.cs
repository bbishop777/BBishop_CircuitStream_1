using UnityEngine;
using System.Collections;

public class GoHomeGame : MonoBehaviour //Class is ConsolePrinter and it is a blueprint..Here we find the Class Name
{
	public Vector2 playerLocation;  //these are variables of a Class Type called Vector2 (Class Types are usually in capital letters while types..like "float" are lowercase)
	public Vector2 homeLocation;
	bool gameIsOVer = false;

	// Use this for initialization
	void Start () //Start is a method or a function 
	{
		print("Hello and welcome to Go Home!");    //These few lines are all "Statements" or commands to our computer
		print("Steven has gotten lost and needs your help to get home!!");
		print("In this game, your challenge will be to navigate Steven  home as quickly as possible!");
		print("Good Luck!");		
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!gameIsOVer)
        {
			UpdateMovement(KeyCode.LeftArrow, new Vector2(-1, 0));
			UpdateMovement(KeyCode.RightArrow, new Vector2(1, 0));
			UpdateMovement(KeyCode.UpArrow, new Vector2(0, 1));
			UpdateMovement(KeyCode.DownArrow, new Vector2(0, -1));
		}
 		
	}

	private void UpdateMovement(KeyCode kc, Vector2 movementVector)  //This is a method which is like a factory that can take input and make output
    {
		if (Input.GetKeyDown(kc)) //This block of code within the "if" statement is a Compound Statement containing sub-statements and expressions
        {
            playerLocation = playerLocation + movementVector; //This is an "Expression" as it evaluates to a value
            PrintDistanceToHome();
        }

    }

    private void PrintDistanceToHome()
    {
        Vector2 pathToHome = homeLocation - playerLocation;
        print("Distance to home: " + pathToHome.magnitude); //Magnitude is calculated using Pythagorian theory (both sides or right triangle squared and added together equals 3rd side or hypotenuse
                                                            //squared..so take the square root of this to get last side) and tells us the size of the 3rd side or distance from home w/out the direction
                                                            //pathToHome.magnitude is an object .magnitude is a method within this object
        if (playerLocation == homeLocation)
        {
            print("Steven is home!");
			gameIsOVer = true;
        }
    }
}
