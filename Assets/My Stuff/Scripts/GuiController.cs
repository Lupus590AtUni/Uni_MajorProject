using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuiController : MonoBehaviour
{
    [HideInInspector]
    public List<string> directions;

    [SerializeField]
    private Rect directionsBox;
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
                        //LOW: Pause Menu temp code in place - doens't work
                        PauseBox = new Rect((Screen.width - pauseBoxSize.x) / 2, (Screen.height - pauseBoxSize.y) / 2, pauseBoxSize.x, pauseBoxSize.y);
                        GUI.BeginGroup(PauseBox);
                        GUI.Box(PauseBox, "Paused");

                        GUI.EndGroup();
                    }
                    else
                    {

                        // Normal Hud
                        //LOW: improve
                        GUI.BeginGroup(directionsBox);
                        // Make a background box
                        GUI.Box(directionsBox, "Directions");

                        int yPos = 20;

                        //print("directions.Count: "+ directions.Count);
                        for(int i = 0; i < directions.Length; i++)
                        {
                            GUI.Label(new Rect(5, yPos, directionsBox.size.x - 5, 1000), directions[i]);
                            yPos += 15;
                        }

                        GUI.EndGroup();
                    }
                    break;
                }
            case GameController.Mode.menu:
                {
                    //LOW: Main menu

                    break;
                }
            case GameController.Mode.survey:
                {
                    //LOW: Survey

                    break;
                }
            default:
                {
                    print("GuiController.OnGui - unhandled GameController.Mode");
                    break;
                }
        }

    }
}
