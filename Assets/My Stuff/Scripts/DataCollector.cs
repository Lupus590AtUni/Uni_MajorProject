using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

//https://docs.unity3d.com/Manual/UnityAnalyticsCustomEventScripting.html
//https://docs.unity3d.com/ScriptReference/Analytics.Analytics.CustomEvent.html

public class DataCollector : MonoBehaviour
{
    private List<Vector3> playerRoute;
    [SerializeField] private GameObject player;
    [SerializeField] private GameController gameController;

    public bool recordMode;
    private float timeTaken;
    private int manualRecalcPathCount;

    // Use this for initialization
    void Start()
    {
        //Analytics.CustomEvent();
        timeTaken = 0.0f;
        
	}
	
	// Update is called once per frame
	void Update()
    {
	    if(recordMode)
        {
            // Track the time taken
            timeTaken += Time.deltaTime;
        }
	}

    void FixedUpdate()
    {
        if(recordMode)
        {
            // Log player location
            playerRoute.Add(player.transform.position);
        }
    }


}
