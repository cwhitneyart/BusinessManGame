using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class will look after all the needs the player's businessman has. It will alert the player when a certain need is too low. 
public class playerNeeds : MonoBehaviour
{
    public float hungerNeed;
    public float thirstNeed;
    public float sleepNeed;
    public float loveNeed;

    float workDone;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkHunger();
        checkThirst();
        checksleep();
    }

    void checkHunger()
    {
        //check player's hunger level
    }

    void checkThirst()
    {
        //check player's thirst
    }

    void checksleep()
    {
        //check player's sleep
    }
}
