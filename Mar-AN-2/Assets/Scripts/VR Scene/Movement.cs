//The first 3 lines below are libraries.  Libraries bring in methods and operations to use within C# because it is not
//robust on its own.  These are created by other developers.  These are default when the script is created.  This is called
//a code stub.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;  //This library brings in stuff like transform, gameobject, position, etc

public class Movement : MonoBehaviour
#region PublicVsPrivate, Movement, and MonoBehaviour Functions Comments
//public means it is accessible outside this file scripts...other scripts can reach
//  it as well as Unity Inspector. For example, if we created another entire script
//for firing a canonball off a ship that "shoot" script will determine the speed of the
//canonball but we need to know the speed of our ship to add to that speed to
//determine impact etc.  That ship speed could be determined in this "Movement" script
//and be the "m_moveSpeed" variable.  Since it is public, our canonball script could
//access it and get it's value to determine the overall speed needed.
//"Private" restricts access to this script only. 
//"Movement" must be used here because it is the name of the script..this must match
//the name. "MonobeBehaviour" is called an inheritance and is important to functions such
//as Start, Update, FixedUpdate, Awake, OnTriggerEnter, OnJointBreak, etc and these know
//when to occur due to inheritance of MonoBehaviour.
//Class is basically the entire script here.
#endregion
{
    #region Scope,Coding Practice with "m_", structure of var declaration, Serialize Comments
    //Brackets define the scope
    //Coding practice...whatever we name a variable, first include "m_" to identify this as a "member variable".  This is
    //a variable that is available throughout your entire script/class. So only use this for public variables. Also use
    //camel case. Variables are first declared as to whether be public or private then the data type must be declared.
    //This prevents the wrong data type being put into your variable bucket.  Whenever we declare a variable we must put a
    //semicolon at the end to make it executable (any executable code needs this).

    //You can make a variable private but serialize the field and it will show up in the inspectorlike this
    //(uncomment to see):

    //[SerializeField]
    //private int m_turnSpeed;

    //Or you can take a Public variable and hide it from just the inspector like this (uncomment to see):

    //[HideInInspector]
    //public float m_rotateSpeed;
    #endregion
    #region Float/Double, use of "f", Inspector Value Comments
    //Alternative to float is "double" but Unity doesn't use it much
    //Whenever we define a float value (w/decimal), must put an "f" after number or will
    //get error. This is not necessary for whole integers. Defined here, it will show up
    //in Inspector under Movement Script but if change value there, it will override this
    //value and replace it for that use case
    #endregion
    public float m_moveSpeed = 3.5f;
    public float m_turnSpeed = 15;
    #region Optimizing repeat use functions like transform by caching as a variable Comments
    //Since we are using the 'transform" function so much in this example, to optimize
    //we could created a public variable "m_thisTransform" to cache it in and then
    //define it in the Start function like this:

    //public Transform m_thisTransform;
    #endregion
    #region Start Function Comments
    //The start function only runs on a gameobject that has the script on it AND is activated AND when the play button is hit.
    //If play button is hit but gmaeobject is not activated, its start function will not run...if it is enabled after play
    //button is hit, it will run this function.
    #endregion
    private void Start()
    {
        #region Continued Example of Caching function as variable Comments
        //m_moveSpeed = transform;    //Here we could define it or in the inspector, we could link "transform" to this
        //this variable by moving it into the field.  Now below, each transform should start
        //with this variable:
        //m_moveSpeed.Translate(Vector3.forward * m_Speed * Time.deltaTime) VS
        //transform.Translate(Vector3.forward * m_Speed * Time.deltaTime)
        #endregion
        #region To control mouse from leaving and clicking outside of game Comments
        //If user moves mouse outside game screen or clicks outside it, it will stop game or not have an effect. This
        //below locks our cursor to the game screen.  By hitting escape they can break out of this.
        #endregion
        Cursor.lockState = CursorLockMode.Locked;

    }


    #region Update Function Comments
    // Update is called once per frame.  Fixed Update is better for physics as it is calculated on regular interval not
    //frame rate
    #endregion
    void Update()
    {
        #region Translate & Vector3 Comments
        //transform.Translate(Vector3.forward * m_Speed * Time.deltaTime);

        //Vector 3 holds 3 values/coordinates (often used for math calculations. Translate fucntion takes the current
        //position of transform and moves it. If you are assigning a value (not creating it) there is no reason to use
        //"new Vector3". Here we are just taking the 3 coordinates of our transform and moving it forward by multiplying 
        //by m_Speed and then multiplying by Time.deltaTime.
        #endregion

        #region Translation
        if (Input.GetKey(KeyCode.W))
        #region GetKey Discussion Comments
        //"if" is followed by paranthesis that are called a "condition" we are checking for.
        //"Input" can refer to our buttons on pc or VR buttons on controllers, etc. The "GetKey"
        //is activated as long as the key is depressed. This will not activate on the first frame
        //when key IS BEING pressed down, but rather on every frame after that it is depressed.
        //We also have Input.GetKeyDown() and Input.GetKeyUp() both only are true on the frame the
        //specific key is pressed down or let up and will return false after even if the same state
        //is maintained (ex: the key remains depressed). So use Input.GetKey() to see if a key has
        //been released (this will be false when key has been released). GetKey and the like are
        //Unity library defined functions.  Also, we can also get buttons (VR controllers) and
        //axes (from joysticks and mouse and positioning of fingers on phone screen..also can
        //use "GetTouch".  "KeyCode." has a ton of options from keypads, top of keyboard numbers,
        //buttons from most controllers, and joystick axis.
        #endregion
        {
            transform.Translate(Vector3.forward * m_moveSpeed * Time.deltaTime);
            #region transform.Translate, Vector3.forward Comments
            //The "transform" will refer to the Transform on the gameobject the script is on. Translate() is a function
            //following the dot operator and it needs a direction.  Vector3.forward refers to the forward direction of the
            //gameobject script is on.  So whether we turn the player left or right, forward will be its local Z direction.
            //A Vector3 is like an arrow w/3 float values.
            //These behaviors can be accomplished in many other ways.  For example we could have written:

            //transform.postion = trasform.postion + Vector3.forward;

            //OR even shorter:

            //transform.postion += Vector3.forward;

            //Now for most movements we won't want to use translate as it is more a type of "teleporting" vs using physics
            //to move it.  Translate is not physics based. Example, if our object is translated to encounter a thick
            //wall object it will appear to be repelled.  With physics, each movement is calculated before object is moved,
            //so before moving into the wall the physics will calculate the movement and determine the collision and will
            //shorten the movement.  Translate teleports you across the distance determined. So it may teleport you into
            //the middle of the wall and then push you back out.  If wall has a rigidbody or object does, it may get a weird
            //result like tossing object to the other side of the wall.  Translate works because we use small increments
            //making it look smooth but it is a teleportation. With RigidBodies there is a velocity setting causing some
            //smoothing effects there or take the rigidbody.move to another position (look up this).  This all is important
            //because in VR the components we will use to track the controllers are called track pose drivers (these are
            //used by default for the Occulus touch controllers as well) and these open vr track pose drivers work for all
            //VR controllers (Valve Index, PSVR, Occulus, WMR, Vive, PyMax).  This is an agnostic system.  In VR the track
            //pose drivers use translate by default (and not physics and rigidbodies) and would pass your object through a
            //virtual wall when there is no real wall.  Instead of putting the track pose driver put on our hand or player
            //object, we will create an empty game object which will lead.  The player/hand/object will follow it using
            //physics.  The track pose drivers will be on the empty game object. The empty game object would pass through
            //the wall but the player/hand/object, using physics, will calculate a stop and struggle against the virtual wall.
            #endregion
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * m_moveSpeed * Time.deltaTime); //Here we translate back on local Z axis.
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * m_moveSpeed * Time.deltaTime); //Here we translate back on X axis.
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * m_moveSpeed * Time.deltaTime); //Here we translate forward on X axis.
        }
        #endregion
        #region Rotation Comments
        //For rotation, instead of an "if" statment we will use Mouse X & Mouse Y which tells us the change in position of
        //our mouse between each frame: left to right = Mouse X. Down to Up = Mouse Y. Mouse movement is based on frame
        //rate (actual movement made in each frame) so we don't need Time.deltaTime for this. This is typical of axes.
        //Rotate is another function that needs an axis to rotate around. To detemine this use the "LEFT THUMB RULE":
        //stiffen and straighten your thumb on your left hand. Rotate hand to have fingers curling in direction of desired
        //positive rotation.  So if curl to the right as in positive rotation to right on X axis, thumb is pointed up
        //toward ceiling. So positive rotation to right is Vector3.up.
        //
        //Now for rotating up and down: If I want to rotate up (positive on Y axis),I would turn my left hand so my fingers 
        //point up, and my thumb is pointing to the left, so I use Vector3.left for positive rotation on Y axis. To invert
        //these rotations, I would write them opposite.

        //Without multiplying Vector3.up (or left) would rotate us 1 degree per frame but would not be controlled by 
        //anything so need to multiply by a control Input.  Here we get an axis & GetAxis function takes a string instead 
        //of a Keycode. GetAxis and GetButton will be found in our Input Manager Directory. The string designates the
        //axis.

        //This works well until we begin to move the mouse in a circular motion.  Our object begins to tilt. The gimbal
        //effect. To fix this, we must have the transform ignore the Vector3.up (for example: for the X axis) of the
        //player or the local world of our object and instead base it on the World Vector3.up of the World X axis of the 
        //scene, so we include Space.World as a 2nd parameter for Rotate.  We include a turn speed as well.
        #endregion
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * m_turnSpeed, Space.World);
        transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * m_turnSpeed);
    }
}


#region FPS, Optimization, Minimums for Hardware, and Time.deltaTime Explanation Comments
//Notes on FPS: For Desktop VR you need 90 FPS (Valve Index can do up to 144 FPS so will need beefy PC for this).
//              For Mobile VR (Occulus Go, Samsung Gear, Google Daydream, Occulus Quest) min is 60 FPS (72 Occulus Quest)
//              (to get on Occulus store it must be 60 FPS...when developing on desktop the FPS will be MUCH higer than
//              what the mobile processor can do..so be sure to optimize app for mobile.
//              For mobile AR you need at least 30 FPS (no real requirement though).
//No matter how optimized app is, it will have lag spikes where it slows down.  For example, on average let's say FPS is
//good with 1/90th of a second between each frame for frames 1, 2, 3, 4, 5, and 6, but then between 6 and 7 the canvas has
//to redraw the whole thing and we get a big delay between 6 and 7.  With the Update function we are calculating every
//frame.  Example frames 1 thru 6 we move one unit.  But once we go from frame 6 to 7 and say the time is one second, the
//player will appear to slow way down.  Time.deltaTime calculates the time between frames and so it betweenn frames 6 and 7
//it calculates a full one second.  We then multiply this by the movement to keep the pace on our Translate function.  The
//"Time" is the time since app start and Time.deltaTime is change of time between a frame. So always use this when using
//Translate.
#endregion

