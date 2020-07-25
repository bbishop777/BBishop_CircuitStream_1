using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour //This is a static script and can be used if we have lots of GameObjects (like AI agents) who need to be aware of our player..we do this instead
                                            //of making m_player public on each GameObject script and dragging our player gameobject into the Unity Editor slot on each GameObject (AI agents)
                                            //Static means we won't be able to see it in the Unity Inspector but it is available to every script in the project
{
    public static PlayerSingleton s_instance; //so this variable is a static variable of the type "PlayerSingleton" which we just created above as a MonoBehaviour script...so a little
                                             //confusing declaring a variable of the same type of the class. A static class referencing itself

    private void Awake()
    {
        s_instance = this;  //now we assign the PlayerSingleton variable a value which  will reference itself by using the word "this"
    }

    public Transform m_player;  //We could create a GameObbject but since we need only the position, we create the Transform variable m_player
}
