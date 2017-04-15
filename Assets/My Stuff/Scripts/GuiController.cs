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

    private enum SurveyQuestion {longestTimeFelt, preference, mostImmersive, mostEviromentlyAware, clarity, playedBefore , satNavUser, gamer, rpgPlayer, complete, thankyou }

    private int currentQuestion = (int) SurveyQuestion.longestTimeFelt;
    
    private Dictionary<string, object> surveyResults = new Dictionary<string, object>();
    private string[] surveyQuestionsOnAlgorithm = { "Which method do you think took longer?", "Which method do you prefer overall?", "Which method would you consider more immersive?", "Which method made you more aware of the in-game enviroment?" };
    private string[] surveyQuestionsOnUser = { "Do you think the natual language directions were clear enough?", "Have you played this tech-demo before?", "Do you have any experience with using a sat-nav, TomTom or simular device?", "Do you consider yourself a gamer (a computer/video game player)?", "Do you play computer role playing games (CRPGs or RPGs)? This can be Japenise or Westen." };
    void controlSurvey()
    {
        surveyBox = new Rect((Screen.width - surveyBoxSize.x) / 2, (Screen.height - surveyBoxSize.y) / 2, surveyBoxSize.x, surveyBoxSize.y);
        GUI.BeginGroup(surveyBox);
        //TODO: Surveys
        GUI.Box(new Rect(0, 0, surveyBoxSize.x, surveyBoxSize.y), "User Opinion Survey");

        if(currentQuestion >= (int) SurveyQuestion.longestTimeFelt || currentQuestion <= (int) SurveyQuestion.mostEviromentlyAware)
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
        else if(currentQuestion >= (int)SurveyQuestion.clarity || currentQuestion <= (int)SurveyQuestion.rpgPlayer)
        {
            GUI.Label(new Rect(5, 20, surveyBoxSize.x - 5, 1000), surveyQuestionsOnUser[currentQuestion]);
            if(GUI.Button(new Rect(5, buttonHeight + buttonSpacing + 100, surveyBoxSize.x - 10, buttonHeight), "Yes"))
            {
                surveyResults.Add(surveyQuestionsOnUser[currentQuestion - (int)SurveyQuestion.mostEviromentlyAware], "true");
                currentQuestion++;
            }
            if(GUI.Button(new Rect(5, (buttonHeight + buttonSpacing) * 2 + 100, surveyBoxSize.x - 10, buttonHeight), "No"))
            {
                surveyResults.Add(surveyQuestionsOnUser[currentQuestion - (int)SurveyQuestion.mostEviromentlyAware], "false");
                currentQuestion++;
            }
            if(GUI.Button(new Rect(5, (buttonHeight + buttonSpacing) * 3 + 100, surveyBoxSize.x - 10, buttonHeight), "Prefer not to answer"))
            {
                surveyResults.Add(surveyQuestionsOnUser[currentQuestion - (int)SurveyQuestion.mostEviromentlyAware], "null");
                currentQuestion++;
            }
        }

        else if(currentQuestion == (int) SurveyQuestion.complete)
        {
            Analytics.CustomEvent("Survey", surveyResults);
            currentQuestion++;
        }
        else if(currentQuestion == (int) SurveyQuestion.thankyou)
        {
            GUI.Label(new Rect(5, 20, surveyBoxSize.x - 5, 1000), "Thankyou for participating. Please share this with your friends.");
            if(GUI.Button(new Rect(5, buttonHeight + buttonSpacing , surveyBoxSize.x - 10, buttonHeight), "Return to Main Menu"))
                gameController.enterMenu();
        }
        else
        {
            print("GuiController::comtrolSurvey - unhandled question");
        }

        GUI.EndGroup();
    }

}
