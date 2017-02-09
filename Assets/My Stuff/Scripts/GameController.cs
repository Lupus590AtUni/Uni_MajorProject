using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform goal;


	// Use this for initialization
	void Start ()
    {
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
