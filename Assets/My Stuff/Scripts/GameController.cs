using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform goal;
    private DataCollector dataCollector;

	// Use this for initialization
	void Start ()
    {
        dataCollector = FindObjectOfType<DataCollector>();
        //print("Game Script: started");
    }
	
    void playerThinksTheyAreThere()
    {
        //Collect Data
        //Move Player
        //Set New Destination
    }

	// Update is called once per frame
	void Update ()
    {
		if(Input.GetButtonUp("Fire1"))
        {
            playerThinksTheyAreThere();
        }
	}
}
