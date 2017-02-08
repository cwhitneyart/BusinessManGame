using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class needsManager : MonoBehaviour
{

    public GameObject needsWindow;


    public void needsWindowActivation()
    {
        if (needsWindow.activeInHierarchy == false)
        {
            needsWindow.SetActive(true);
        }
        else if (needsWindow.activeInHierarchy == true)
        {
            needsWindow.SetActive(false);
        }
       
       
    }

	// Use this for initialization
	void Start ()
    {
        needsWindow.SetActive(false);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
