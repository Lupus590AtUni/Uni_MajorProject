using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform goal;
    private DataCollector dataCollector;

    private bool pauseState = false;

    public bool isPaused()
    {
        return pauseState;
    }
    
    public void pause()
    {
        pauseState = true;
        Time.timeScale = 0;
    }

    public void resume()
    {
        pauseState = false;
        Time.timeScale = 1;
    }

    // Use this for initialization
    void Start ()
    {
        dataCollector = FindObjectOfType<DataCollector>();
        //print("Game Script: started");
        

    }
	
    void playerThinksTheyAreThere()
    {
        //Collect Data
        //Move Player
        //Set New Destination
    }

	// Update is called once per frame
	void Update ()
    {
		if(Input.GetButtonUp("Fire1"))
        {
            playerThinksTheyAreThere();
        }
        if(Input.GetButtonUp("Pause"))
        {
            if(isPaused())
            {
                resume();
            }
            else
            {
                pause();
            }
        }
	}
}
