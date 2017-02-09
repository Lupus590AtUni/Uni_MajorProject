using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuiController : MonoBehaviour
{
    public List<string> directions;

    [SerializeField]
    private Rect directionsBox;

	// Use this for initialization
	void Start()
    {
        //print("GUI Script: started");
    }
	
	// Update is called once per frame
	void Update()
    {
		
	}

    void OnGUI()
    {

        GUI.BeginGroup(directionsBox);
        // Make a background box
        GUI.Box(new Rect(0, 0, directionsBox.size.x, directionsBox.size.y), "Directions");

        int yPos = 20;


        for(int i = 0; i< directions.Count; i++)
        {
            GUI.Label(new Rect(5, yPos, directionsBox.size.x - 5, 1000), directions[i]);
            yPos += 15;
        }

        GUI.EndGroup();

        
    }
}
