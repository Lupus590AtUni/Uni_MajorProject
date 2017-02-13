using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigator : MonoBehaviour
{
    // https://docs.unity3d.com/Manual/nav-CreateNavMeshAgent.html

    private GameController gameController;
    private GuiController guiController;
    private NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        guiController = FindObjectOfType<GuiController>();

        //print("Navigator Script: started");
        agent = GetComponent<NavMeshAgent>();


    }

    public void findLandmarks()
    {

    }

    public void recalcPath() //recalculate the path and send to gui controller
    {

        agent.destination = gameController.goal.position;


        print("agent.path.status: " + agent.path.status);
        print("agent.isPathStale: " + agent.isPathStale);

        print("agent.path.corners.Length: " + agent.path.corners.Length);
        guiController.directions.Clear();
        for(int i = 0; i<agent.path.corners.Length; i++)
        {
            guiController.directions.Add(agent.path.corners[i].ToString());
        }
        
    }

	// Update is called once per frame
	void Update()
    {
        
    }


    
    private bool gotFirstPath = false;

    void LateUpdate()
    {
        if(!gotFirstPath)
        {
            recalcPath();
            gotFirstPath = true;
        }
    }
    
}
