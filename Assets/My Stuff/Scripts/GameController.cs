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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void resume()
    {
        pauseState = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void initGame(int routeNumber)
    {
        player.transform.position = this.transform.position; //HACK: start position is always...
                                                             //the game controllers position for simplicity

        currentGoal.transform.position = routes[routeNumber].transform.position;

        if(routeUseWaypoint)
        {
            //Show waypoint
            currentGoal.gameObject.SetActive(true);
            
        }
        else
        {
            //Hide waypoint
            currentGoal.gameObject.SetActive(false);
            navigator.recalcPath();
        }
    }

    void playerThinksTheyAreThere()
    {

        //TODO: Collect Data

        //TODO: implement and test
        currentRouteNumber++;
        if(currentRouteNumber >= routes.Count) //BUG: null reference exception
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
        pause();
        
    }

    public void enterMenu()
    {
        mode = Mode.menu;
        pause();
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

        DestionationSorter d = new DestionationSorter();
        routes.Sort(d.Compare);

        //enterMenu();
        //enterGame();
        enterSurvey();

        //print("Game Script: started");
    }

    bool doInit = true;
    // Update is called once per frame
    void Update()
    {
        if(doInit)
        {
            
            doInit = false;
        }

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
