using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform currentGoal;
    [HideInInspector] public DataCollector dataCollector;
    [HideInInspector] public Navigator navigator;
    [HideInInspector] public GameObject player;

    public enum Mode {menu, game, survey };
    [HideInInspector] public Mode mode; //set in start

    private bool pauseState = false;


    public bool routeUseWaypoint = false; //else use new tech
    private List<Destination> routes; //Not really routes on their own
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
        player.transform.position = this.transform.position; //HACK: start position is always...
                                                                //the game controllers position for simplicity
        currentGoal.position = routes[routeNumber].transform.position;
        if(routeUseWaypoint)
        {
            //Show waypoint
            currentGoal.gameObject.SetActive(true);
            //TODO: disable directions hud eliment
        }
        else
        {
            //Hide waypoint
            currentGoal.gameObject.SetActive(false);
            //TODO: enable directions hud eliment
            navigator.recalcPath();
        }
    }

    void playerThinksTheyAreThere()
    {

        //TODO: Collect Data

        //TODO: implement and test
        currentRouteNumber++;
        if(currentRouteNumber > routes.Count) //BUG: null reference exception - I have no routes yet
        {
            if(routeUseWaypoint)
            {
                enterSurvey(); //player has done both routes

            }
            currentRouteNumber = 0;
            routeUseWaypoint = true;
        }
       
        initGame(currentRouteNumber);


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

        Destination[] destinations = FindObjectsOfType<Destination>();

        routes = new List<Destination>();

        for(int i = 0;i < destinations.Length; i++)
        {
            routes.Add(destinations[i]);
        }

        routes.Sort(); 
                       //TODO: sort destinations
                       //TODO: add to routes

        enterGame();

        //print("Game Script: started");
    }

    
    // Update is called once per frame
    void Update()
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
