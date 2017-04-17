using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

//https://docs.unity3d.com/Manual/UnityAnalyticsCustomEventScripting.html
//https://docs.unity3d.com/ScriptReference/Analytics.Analytics.CustomEvent.html

public class DataCollector : MonoBehaviour
{
    private List<Vector3> playerRoute;
    private List<Vector3> playerLook;
    private GameObject player;
    private GameController gameController;

    [HideInInspector] public bool recordMode;
    private float timeTaken;
    private int manualRecalcPathCount;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = FindObjectOfType<GameController>();
        //Analytics.CustomEvent();
        timeTaken = 0.0f;
        
	}
	
	// Update is called once per frame
	void Update()
    {
	    if(recordMode && !gameController.isPaused())
        {
            // Track the time taken
            timeTaken += Time.deltaTime;
        }
	}

    /*void FixedUpdate()
    {
        if(recordMode && !gameController.isPaused())
        {
            // Log player location
            playerRoute.Add(player.transform.position);

            // Log player look vector
            playerLook.Add(player.transform.forward); //http://answers.unity3d.com/questions/425734/which-direction-is-character-is-facing.html
        }
    }
    */

    public void sendData(int routeNumber, bool usingWaypoint)
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        string routeID = "Route "+routeNumber.ToString()+" using ";

        if(usingWaypoint)
        {
            routeID += "waypoint";
        }
        else
        {
            routeID += "landmarks";
        }

        data.Add("playerEndPos", player.transform.position.ToString());
        data.Add("timeTaken", timeTaken.ToString());
        data.Add("destinationPos", gameController.currentGoal.transform.position.ToString());
        data.Add("playerDestiationDistance", Mathf.Abs((gameController.currentGoal.transform.position - player.transform.position).magnitude).ToString());


        Analytics.CustomEvent("Session " + gameController.timesPlayed.ToString() + " "+routeID, data);
        timeTaken = 0;
    }
}
