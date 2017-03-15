using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route
{
    public Transform startPos;
    public Transform goalPos;

}

public class GameController : MonoBehaviour
{
    public Transform currentGoal;
    [HideInInspector] public DataCollector dataCollector;
    [HideInInspector] public Navigator navigator;
    [HideInInspector] public GameObject player;

    public enum Mode {menu, game, survey };
    [HideInInspector] public Mode mode; //set in start

    private bool pauseState = false;


    
    private List<Route> routes;
    private int currentRouteNumber;

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

    public void initGame(int routeNumber)
    {
        //player.transform.position = routes[routeNumber].startPos.position;
        //currentGoal.position = routes[routeNumber].goalPos.position;
        navigator.recalcPath();
    }

    public void enterGame()
    {
        mode = Mode.game;
        resume();

        currentRouteNumber = 0;
        initGame(currentRouteNumber);
        

    }

    public void enterSurvey()
    {
        mode = Mode.survey;
        
        
    }

    public void enterMenu()
    {
        mode = Mode.menu;
        
        


    }

    // Use this for initialization
    void Start ()
    {
        dataCollector = FindObjectOfType<DataCollector>();
        navigator = FindObjectOfType<Navigator>();
        player = GameObject.FindGameObjectWithTag("Player");

        enterGame();

        //print("Game Script: started");
    }

    void playerThinksTheyAreThere()
    {
        //TODO: implement and test
        //TODO: Collect Data

        //currentRouteNumber++;
        initGame(currentRouteNumber);
        
            
    }

	// Update is called once per frame
	void Update ()
    {
        //NOTE: debug
            //printout current game mode
        //print("GameController.mode: " + mode.ToString()); 

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
