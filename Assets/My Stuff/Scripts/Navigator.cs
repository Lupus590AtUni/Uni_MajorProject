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


        //HACK: or not I guess as this is the problem code
        string[] str = pathDescriber.convertPathToString(agent.path);


        print("agent.path.status: " + agent.path.status);
        print("agent.isPathStale: " + agent.isPathStale);

        print("agent.path.corners.Length: " + agent.path.corners.Length);
        guiController.directions.Clear();
        for(int i = 0; i<agent.path.corners.Length; i++)
        {
            guiController.directions.Add(str[i]); //goes with problem code

            //HACK this is the actual 'hack'
            //guiController.directions.Add(agent.path.corners[i].ToString());
        }
        
    }

	// Update is called once per frame
	void Update()
    {
        
    }


    
    private bool gotFirstPath = false;
    private bool hadfirstUpdate = false;

    void LateUpdate()
    {
        //HACK: first path generation - also this seems to be broken
        if(hadfirstUpdate && !gotFirstPath)
        {
            recalcPath();
            gotFirstPath = true;
        }

        hadfirstUpdate = true;
    }
}
