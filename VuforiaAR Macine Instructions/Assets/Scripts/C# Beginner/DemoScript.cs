using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Our scripts start off by starting with the tools and libraries and things we need at the top.  We usually start off with
//declaring our variables.  We do this within our Class.  Our variables have public or private, a type, and a name
//public vs private. Public will allow other scripts and the Unity editor to be able to access it and read it.  Private
//means it can only be accessed by the script it is in. This has to do with "scope".  If a variable is defined within
//a function, then it can't be changed or seen by any place else in Unity. A variable defined in a class (even a public
//one) can either be public or private...private meaning they also will not be exposed outside this script.  Also, if it
//does not need to be modified, there is no reason to make it public so it shows in the inspector.  By default, if neither
//is declared, C# will make it private.
//Type must be declared because Unity is modifying our variables and it must know what type it is (things like Transform,
//Mesh Filter, Mesh Collider, Light, our Script, etc are all "types"...they are what define our component on each Game Object).
//Color, Camera, Rigidbody, int (full integer), float (decimal number), string, bool (boolean-true/false), etc.  Need to
//know types so Unity knows what it can combine together and how to manipulate it
//Name- can be any name except it can't be a number
//Coding convention: variables start with a lowercase letter and functions start with an Uppercase letter.  When public,
//Unity will rewrite the variable in the Inspector to make it more readable (Pascal case).


//Classes are the entire holder below.  The class name (in this case "DemoScript") must match the filename or script name.
//To be attached to a script, it then must "derive" from another class called "MonoBehaviour".  A class can be instantiated
//with a variable. See DataClass (and classes can be private or public too.  Custom classes created by us must be
//"serialized" () to show up in Unity

[System.Serializable] //This means it will convert our custom class below into simple data that Unity can read
public class DataClass   //In this case it doesn't need to derive from anything.  See within Demoscript class below
                         //where we create an instance of this.
{
    public int myInt;
    public float myFloat;
}

public class DemoScript : MonoBehaviour
{
    //Variables-like a box which hold values and objects
    //Functions-collections of code that compare and manipulate variables
    //Classes-wrap collections of variables and functions together for use and reuse if we need them
    //Scripting is primarily comparing these things or objects and their current states and values, and then based on logic
    //(not just math), determining an outcome or resolution.
    // Start is called before the first frame update
    // touch ... look this up to put touch input into scripting (like a finger touch vs a key stroke).
    public Light myLight;
    private Light myOtherLight; //this variable can't be seen anywhere but within this Class

    public DataClass[] myClass;  //Here we create a public variable of the type DataClass (established above). Here we
                                 //created an array we can store a series of integers and floats.  We may only want to
                                 //do this to keep things associated in this script. Only for organization purposes.

    public AnotherDataClass[] myOtherClass;  //Here is an example of calling another script that is has a custom class we
                                             //we created and made public

    //many functions run automatically in Unity such as:
    /*
     private void Awake()
     {
      Usually called first before anything else (before Start).  Called at first frame of game.  If the game
     object is active.  If it is not active, it will not be called
     }

     private void Start()
     {
      Called at beginning (after awake) unless game object is not active.
     }

     private void Update()
     {
     Called once per frame.  Unity will read all our codes/scripts then run update.  This is where we put things to be done

        
     }


     private void FixedUpdate()
     {
     This is called specifically when you want to do physics work.  This is different than Update in that it is called at a
     regular interval vs at the beginning of each frame.  For example, if you have a space game...at one point the ship may
     just be sitting idly and the frame rate will be high because there is little to process.  No need to calculate physics
     at every frame.  However, later you maybe in a space battle with other moving ships, moving astroids, particle
     explosions, etc.  Here, the frame rate may slow down but the physics calcualtions are critical so fixedUpdate solves this
     by keeping the regular timing based on percentages of a second to do the calculations.

     }

    private void LateUpdate()
    {
    This is similar to Update but is processed at the end of the frame after all Updates are completed.  For example, camera
    movement is best done here in case your character bumps off something...camera position is calcualted at end so it doesn't
    jiggle.
    }

   */

    //Functions are written starting with their "return" type first.  In the case of "void" it means no return will come
    //from this function.  Then name (like Update), parenthesis (for paremeters), and then brackets for enclosure where
    //the block of logic will go.  Since Unity calls the ones above, the functions we write must be located in one of the
    //scripts we write, or one of the functions Unity normally calls, or attached to a button click.  See MyFunction below:
    ////Functions can do calculations and return an answer to you.  Like declaring a type, the return type must be declared too.
    //This is what the parenthesis are for (to do something with parameters or to send arguments).  Again, void does not
    //return anything.  If we call a function in Awake to add 2 numbers it would look like this:

    private void Awake()
    {
        int myVar = AddTwo(9, 2);  //The 9 and 2 are arguments here
        Debug.Log(myVar);  //Just to show it return something in console
    }


    void Update()
    {
        if (Input.GetKeyDown ("space"))
        {
            //myLight.enabled = !myLight.enabled;  Instead:
            MyFunction();

        }
        
    }

    void MyFunction ()
    {
        myLight.enabled = !myLight.enabled;
    }
    //Now we set up our AddTwo function. In our paremeters we must declare the type of paremeters entering and give 
    //a name to the variable holder. The 9 and 2 that are sent are arguments
    //

    int AddTwo (int var1, int var2)
    {
        int returnValue = var1 + var2;
        return returnValue;
    }
}


//Q&A:
//"this." is generally not needed since the script is attached to a specific component or gameObject