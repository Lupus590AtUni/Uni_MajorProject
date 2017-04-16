using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


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
    private Vector2 surveyBoxSize;
    private Rect surveyBox;
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
                        // Normal Hud

                        // controls Hud
                        Rect controlsBox = new Rect(directionsBox);
                        controlsBox.x = Screen.width - (controlsBox.width + controlsBox.x);
                        GUI.BeginGroup(controlsBox);
                        GUI.Box(new Rect(0, 0, controlsBox.size.x, controlsBox.size.y), "Default Controls");
                        GUI.Label(new Rect(5, 20, controlsBox.size.x - 5, 1000), "W = Move forwards");
                        GUI.Label(new Rect(5, 35, controlsBox.size.x - 5, 1000), "Mouse = Turn");
                        GUI.Label(new Rect(5, 50, controlsBox.size.x - 5, 1000), "Left click = Indicate that you have arrived");
                        GUI.EndGroup();

                        if(!gameController.routeUseWaypoint)
                        {
                            // Landmark Text Hud
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
                            // Waypoint Hud
                            GUI.BeginGroup(mapMarkerInstuctionBox);
                            // Make a background box
                            GUI.Box(new Rect(0, 0, mapMarkerInstuctionBox.size.x, mapMarkerInstuctionBox.size.y), "Instructions");

                            
                            GUI.Label(new Rect(5, 20, directionsBox.size.x - 5, 1000), "Go to the green sphere");

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

    string mainMenuText = "Thankyou for downloading this and offering to help me with my project.\n"+
        "What you have here is a tech demo for an algorithm I have developed \n"+
        "You will be asked to move to locations within a virtual enviroment with a product of the algorithm as guidance.\n"+
        "Then, as a control to the experiment, you will be asked to go to more locations with the use of a nav-marker.\n"+
        "In both methods, you are expected to tell the demo when you think you have gotten to the destinations. This is done with a left click (unless you have changed the controlls).\n"+
        "Following this, you will be given a survey which will ask about your opinions of the algoithm.\n"+
        "It took less than 5 minutes to do a runthrough. There is a pause button (esc) if you can't get a full block of time.\n"+
        "\n" + 
        " - Nephi (or on the Internet, Lupus590)";

    void controlMenu()
    {
        int offset = 200;
        surveyBox = new Rect((Screen.width - (surveyBoxSize.x + offset)) / 2, (Screen.height - surveyBoxSize.y) / 2, surveyBoxSize.x+offset, surveyBoxSize.y);
        GUI.BeginGroup(surveyBox);
        GUI.Box(new Rect(0, 0, surveyBoxSize.x+offset, surveyBoxSize.y), "Main Menu");
        GUI.Label(new Rect(5, 20, surveyBoxSize.x + offset - 5, 1000), mainMenuText );
        if(GUI.Button(new Rect(5, buttonHeight + buttonSpacing + 225, surveyBoxSize.x + offset - 10, buttonHeight), "Play the Tech-Demo"))
            gameController.enterGame();

        if(GUI.Button(new Rect(5, (buttonHeight + buttonSpacing) * 2 + 225, surveyBoxSize.x + offset - 10, buttonHeight), "Quit"))
            Application.Quit();


        GUI.EndGroup();
    }

    private enum SurveyQuestion {longestTimeFelt, preference, mostImmersive, mostEviromentlyAware, clarity, playedBefore , satNavUser, gamer, rpgPlayer, complete, thankyou }

    private int currentQuestion = (int) SurveyQuestion.longestTimeFelt;
    
    private Dictionary<string, object> surveyResults = new Dictionary<string, object>();
    private string[] surveyQuestionsOnAlgorithm = { "Which method do you think took longer?", "Which method do you prefer overall?", "Which method would you consider more immersive?", "Which method made you more aware of the in-game enviroment?" };
    private string[] surveyQuestionsOnUser = { "Do you think the natual language directions were clear enough?", "Have you played this tech-demo before?", "Do you have any experience with using a sat-nav, TomTom or simular device?", "Do you consider yourself a gamer (a computer/video game player)?", "Do you play computer role playing games (CRPGs or RPGs)? This can be Japenise or Westen." };
    void controlSurvey()
    {
        
        surveyBox = new Rect((Screen.width - surveyBoxSize.x) / 2, (Screen.height - surveyBoxSize.y) / 2, surveyBoxSize.x, surveyBoxSize.y);
        GUI.BeginGroup(surveyBox);
        GUI.Box(new Rect(0, 0, surveyBoxSize.x, surveyBoxSize.y), "User Opinion Survey");

        if(currentQuestion >= (int) SurveyQuestion.longestTimeFelt && currentQuestion <= (int) SurveyQuestion.mostEviromentlyAware)
        {
            GUI.Label(new Rect(5, 20, surveyBoxSize.x - 5, 1000), surveyQuestionsOnAlgorithm[currentQuestion]);
            if(GUI.Button(new Rect(5, buttonHeight + buttonSpacing + 100, surveyBoxSize.x - 10, buttonHeight), "Written directions with landmarks"))
            {
                surveyResults.Add(surveyQuestionsOnAlgorithm[currentQuestion], "landmarks");
                currentQuestion++;
            }
            if(GUI.Button(new Rect(5, (buttonHeight + buttonSpacing) * 2 + 100, surveyBoxSize.x - 10, buttonHeight), "The green spherical waypoint"))
            {
                surveyResults.Add(surveyQuestionsOnAlgorithm[currentQuestion], "waypoint");
                currentQuestion++;
            }
        }
        else if(currentQuestion >= (int)SurveyQuestion.clarity && currentQuestion <= (int)SurveyQuestion.rpgPlayer)
        {
            GUI.Label(new Rect(5, 20, surveyBoxSize.x - 5, 1000), surveyQuestionsOnUser[currentQuestion - (int)SurveyQuestion.clarity]);
            if(GUI.Button(new Rect(5, buttonHeight + buttonSpacing + 100, surveyBoxSize.x - 10, buttonHeight), "Yes"))
            {
                surveyResults.Add(surveyQuestionsOnUser[currentQuestion - (int)SurveyQuestion.clarity], "true");
                currentQuestion++;
            }
            if(GUI.Button(new Rect(5, (buttonHeight + buttonSpacing) * 2 + 100, surveyBoxSize.x - 10, buttonHeight), "No"))
            {
                surveyResults.Add(surveyQuestionsOnUser[currentQuestion - (int)SurveyQuestion.clarity], "false");
                currentQuestion++;
            }
            if(GUI.Button(new Rect(5, (buttonHeight + buttonSpacing) * 3 + 100, surveyBoxSize.x - 10, buttonHeight), "Prefer not to answer"))
            {
                surveyResults.Add(surveyQuestionsOnUser[currentQuestion - (int)SurveyQuestion.clarity], "null");
                currentQuestion++;
            }
        }
        else if(currentQuestion == (int) SurveyQuestion.complete)
        {
            GUI.Label(new Rect(5, 20, surveyBoxSize.x - 5, 1000), "Sending survey data");
            Analytics.CustomEvent("Survey", surveyResults);
            currentQuestion++;
        }
        else if(currentQuestion == (int) SurveyQuestion.thankyou)
        {
            GUI.Label(new Rect(5, 20, surveyBoxSize.x - 5, 1000), "Thankyou for participating. Please share this with your friends.");
            if(GUI.Button(new Rect(5, buttonHeight + buttonSpacing + 100 , surveyBoxSize.x - 10, buttonHeight), "Return to Main Menu"))
                gameController.enterMenu();
        }
        else
        {
            print("GuiController::comtrolSurvey - unhandled question: "+currentQuestion.ToString());
        }

        GUI.EndGroup();
    }

    public void resetSurvey()
    {
        currentQuestion = (int)SurveyQuestion.longestTimeFelt;
        surveyResults = new Dictionary<string, object>();
    }

}
