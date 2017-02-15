using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuiController : MonoBehaviour
{
    public List<string> directions;

    [SerializeField] private Rect directionsBox;

    private GameController gameController;

    // Use this for initialization
    void Start()
    {
        print("GUI Script: started");
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
                        //TODO: Pause Menu
                        GUI.Box(new Rect(0, 0, directionsBox.size.x, directionsBox.size.y), "Paused");
                    }
                    else
                    {
                        // Normal Hud
                        GUI.BeginGroup(directionsBox);
                        // Make a background box
                        GUI.Box(new Rect(0, 0, directionsBox.size.x, directionsBox.size.y), "Directions");

                        int yPos = 20;

                        //print("directions.Count: "+ directions.Count);
                        for(int i = 0; i < directions.Count; i++)
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
                    //TODO: Main menu
                    break;
                }
            case GameController.Mode.survey:
                {
                    //TODO: Survey
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
