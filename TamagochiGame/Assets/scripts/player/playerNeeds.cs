using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//This class will look after all the needs the player's businessman has. It will alert the player when a certain need is too low. 
public class playerNeeds : MonoBehaviour
{
    public float hungerNeed = 1;
    public float thirstNeed = 1;
    public float sleepNeed = 1;
    public float loveNeed = 1;

    public float sleepDecreaseRate = 1f;
    public float thirstDecreaseRate = 1f;
    public float hungerDecreaseRate = 1f;

    public Slider hungerSlider;
    public Slider sleepSlider;

    bool hungerWarningCalled = false;

    #region
    public GameObject hungerWarningText;
    #endregion

    float workDone;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("decreaseSleep", 2f, 1f);
        InvokeRepeating("decreasesHunger", 2f, 1f);

        hungerWarningCalled = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkHunger();
        checkThirst();
        checksleep();
        hungerSlider.value = hungerNeed;
        sleepSlider.value = sleepNeed;
    }


    void decreaseSleep()
    {
        sleepNeed -= sleepDecreaseRate; //* Time.deltaTime;
    }

    void decreasesHunger()
    {
     
            hungerNeed -= hungerDecreaseRate; //* Time.deltaTime;
        
   
    }



    void checkHunger()
    {
        if(hungerWarningCalled == false)
        {
            //check player's hunger level
            if (hungerNeed <= 0.5)
            {
                StartCoroutine(hungerWarning());
            }
        }
     

       
    }

    IEnumerator hungerWarning()
    {
        hungerWarningText.SetActive(true);
        yield return new WaitForSeconds(2f);
        hungerWarningText.SetActive(false);
        hungerWarningCalled = true;
      
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
