using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuiController : MonoBehaviour
{
    [HideInInspector]
    public List<string> directions;

    private float buttonHeight = 20;
    private float buttonSpacing = 5;
    
    [SerializeField]
    private Rect directionsBox;
    [SerializeField]
    private Rect mapMarkerInstuctionBox;
    [SerializeField]
    private Vector2 pauseBoxSize;
    private Rect PauseBox;

    private GameController gameController;

    // Use this for initialization
    void Start()
    {
        //print("GUI Script: started");
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {

        switch(gameController.mode)
        {
            case GameController.Mode.game:
                {

                    if(gameController.isPaused())
                    {
                        
                        PauseBox = new Rect((Screen.width - pauseBoxSize.x) / 2, (Screen.height - pauseBoxSize.y) / 2, pauseBoxSize.x, pauseBoxSize.y);
                        GUI.BeginGroup(PauseBox);
                        GUI.Box(new Rect(0,0,pauseBoxSize.x, pauseBoxSize.y), "Paused");
                        if(GUI.Button(new Rect(5, buttonHeight + buttonSpacing , pauseBoxSize.x - 10, buttonHeight), "Resume"))
                            gameController.resume();

                        if(GUI.Button(new Rect(5, buttonHeight * 2 + buttonSpacing * 2, pauseBoxSize.x - 10, buttonHeight), "Main Menu"))
                            gameController.enterMenu();


                        GUI.EndGroup();
                    }
                    else
                    {
                        if(!gameController.routeUseWaypoint)
                        {
                            // Normal Hud
                            GUI.BeginGroup(directionsBox);
                            // Make a background box
                            GUI.Box(new Rect(0, 0, directionsBox.size.x, directionsBox.size.y), "Instructions");

                            int yPos = 20;

                            //print("directions.Count: "+ directions.Count);
                            for(int i = 0; i < directions.Count; i++)
                            {
                                GUI.Label(new Rect(5, yPos, directionsBox.size.x - 5, 1000), directions[i]);
                                yPos += 15;
                            }

                            GUI.EndGroup();
                        }
                        else
                        {
                            // Normal Hud
                            GUI.BeginGroup(mapMarkerInstuctionBox);
                            // Make a background box
                            GUI.Box(new Rect(0, 0, mapMarkerInstuctionBox.size.x, mapMarkerInstuctionBox.size.y), "Instructions");

                            
                            GUI.Label(new Rect(5, 20, directionsBox.size.x - 5, 1000), "Go to the green sphere\n");

                            GUI.EndGroup();
                        }
                    }
                    break;
                }
            case GameController.Mode.menu:
                {
                    controlMenu();
                    break;
                }
            case GameController.Mode.survey:
                {
                    
                    controlSurvey();
                    break;
                }
            default:
                {
                    print("GuiController.OnGui - unhandled GameController.Mode");
                    break;
                }
        }

    }

    void controlMenu()
    {
        //TODO: Main menu
        PauseBox = new Rect((Screen.width - pauseBoxSize.x) / 2, (Screen.height - pauseBoxSize.y) / 2, pauseBoxSize.x, pauseBoxSize.y);
        GUI.BeginGroup(PauseBox);
        GUI.Box(new Rect(0, 0, pauseBoxSize.x, pauseBoxSize.y), "Paused");
        if(GUI.Button(new Rect(5, buttonHeight + buttonSpacing, pauseBoxSize.x - 10, buttonHeight), "Resume"))
            gameController.resume();

        if(GUI.Button(new Rect(5, buttonHeight * 2 + buttonSpacing * 2, pauseBoxSize.x - 10, buttonHeight), "Main Menu"))
            gameController.enterMenu();


        GUI.EndGroup();
    }

    void controlSurvey()
    {
        //TODO: Surveys
    }

}
