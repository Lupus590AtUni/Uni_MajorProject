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
    private PathDescriber pathDescriber;

    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        guiController = FindObjectOfType<GuiController>();
        pathDescriber = FindObjectOfType<PathDescriber>();

        //print("Navigator Script: started");
        agent = GetComponent<NavMeshAgent>();


    }

    public void recalcPath() //recalculate the path and send to gui controller
    {

        agent.destination = gameController.goal.position;

        guiController.directions = pathDescriber.convertPathToString(agent.path);

        
        
        
    }

	// Update is called once per frame
	void Update()
    {
        
    }


    
    private bool gotFirstPath = false;

    void LateUpdate()
    {
        //HACK
        if(!gotFirstPath)
        {
            recalcPath();
            gotFirstPath = true;
        }
    }
    
}
