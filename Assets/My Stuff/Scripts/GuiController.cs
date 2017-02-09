using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuiController : MonoBehaviour
{

	// Use this for initialization
	void Start()
    {
        print("GUI Script: started");
    }
	
	// Update is called once per frame
	void Update()
    {
		
	}

    void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(10, 10, 100, 90), "Loader Menu");

        // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        if(GUI.Button(new Rect(20, 40, 80, 20), "Level 1"))
        {
            print(1);
        }

        // Make the second button.
        if(GUI.Button(new Rect(20, 70, 80, 20), "Level 2"))
        {
            print(2);
        }
    }
}
