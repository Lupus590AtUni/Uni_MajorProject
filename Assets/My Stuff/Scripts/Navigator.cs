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

        agent.destination = gameController.currentGoal.position;
        this.transform.position = gameController.player.transform.position;



        //NOTE: debug
            //path state
        //print("agent.path.status: " + agent.path.status);
        //print("agent.isPathStale: " + agent.isPathStale);

        //print("agent.path.corners.Length: " + agent.path.corners.Length);

        
        List<string> dirStr = pathDescriber.convertPathToString(agent.path);
        guiController.directions = dirStr;
        
    }

	// Update is called once per frame
	void Update()
    {
        
    }


    
    private bool gotFirstPath = false;
    private bool hadfirstUpdate = false;

    void LateUpdate()
    {
        //BUG: many nodes
        //does this cause the multiple node making when 'that if' is active? 
        //Theory: an exception happens in recalcPath which crashes the thread, a new thread on the next update trys again (including placing a new node)

        //HACK: first path generation - also this seems to be broken
        //TODO: try remove when main menu in place
        if(hadfirstUpdate && !gotFirstPath)
        {
            recalcPath();
            gotFirstPath = true;
        }

        hadfirstUpdate = true;
    }
}
