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

        //Invoke("recalcPath",2); //didn't work //2 second delay so everything has a chance to load before calculating the path //http://answers.unity3d.com/questions/799637/delay-the-start-function.html


    }

    void findLandmarks()
    {

    }

    void recalcPath()
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
        //recalcPath();
	}
}
