using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform goal;
    public DataCollector dataCollector;
    public Navigator navigator;

    private bool pauseState = false;
    private bool worldReadyState = false;

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
        navigator = FindObjectOfType<Navigator>();
        //print("Game Script: started");
        

    }

    void setupWorld()
    {
        if(worldReadyState) return; //world is ready no need to set it up again
        Invoke("navigator.recalcPath",2);  //2 second delay so everything has a chance to load before calculating the path //http://answers.unity3d.com/questions/799637/delay-the-start-function.html
        worldReadyState = true;
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
        if(!worldReadyState) setupWorld();
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
