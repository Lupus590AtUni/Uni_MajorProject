using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform goal;
    public DataCollector dataCollector;
    public Navigator navigator;

    public enum Mode {menu, game, survey };
    public Mode mode = Mode.menu;

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
        navigator = FindObjectOfType<Navigator>();
        //print("Game Script: started");
        //StartCoroutine("playerThinksTheyAreThere");

    }

    IEnumerator playerThinksTheyAreThere() //TODO: Call this
    {
        while(true)
        {
            if(mode == Mode.game && Input.GetButtonUp("Fire1"))
            {
                //Collect Data
                //Move Player
                //Set New Destination
                yield return new WaitForEndOfFrame();
                navigator.recalcPath();
            }
        }
    }

	// Update is called once per frame
	void Update ()
    {
		
        if(mode == Mode.game && Input.GetButtonUp("Pause"))
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
