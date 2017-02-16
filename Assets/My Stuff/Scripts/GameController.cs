using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform goal;
    [HideInInspector] public DataCollector dataCollector;
    [HideInInspector] public Navigator navigator;

    public enum Mode {menu, game, survey };
    [HideInInspector] public Mode mode; //set in start

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

        mode = Mode.game; //TODO: change to menu once menu is made

        //print("Game Script: started");
    }

    void playerThinksTheyAreThere()
    {
        //LOW: implement and test
        //Collect Data
        //Move Player
        //Set New Destination
        navigator.recalcPath(); //LOW: check for issues here
            
    }

	// Update is called once per frame
	void Update ()
    {
        //print("GameController.mode: " + mode.ToString()); //NOTE: printout current game mode
        
        switch(mode)
        {
            case Mode.game:
                {
                    if(isPaused())
                    {
                        // Paused
                        if(Input.GetButtonUp("Pause"))
                        {

                            resume();

                        }
                    }
                    else
                    {
                        // Play
                        if(Input.GetButtonUp("Arrival"))
                        {
                            playerThinksTheyAreThere();
                        }
                        if(Input.GetButtonUp("Pause"))
                        {
                            
                            pause();
                            
                        }
                    }
                    break;
                }
            case Mode.menu:
                {
                    // Main menu
                    break;
                }
            case Mode.survey:
                {
                    // Survey
                    break;
                }
            default:
                {
                    print("GameController.Update - unhandled GameController.Mode");
                    break;
                }
        }
	}
}
