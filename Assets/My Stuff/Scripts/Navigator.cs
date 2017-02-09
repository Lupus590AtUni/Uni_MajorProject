using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigator : MonoBehaviour
{
    // https://docs.unity3d.com/Manual/nav-CreateNavMeshAgent.html

    public Transform goal;
    public GuiController guiController;
    private NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {

        //print("Navigator Script: started");
        agent = GetComponent<NavMeshAgent>();

        recalcPath();


    }

    void findLandmarks()
    {

    }

    void recalcPath()
    {

        agent.destination = goal.position;

        print("agent.path.status: " + agent.path.status);
        print("agent.isPathStale: " + agent.isPathStale);

        print("agent.path.corners.Length: " + agent.path.corners.Length);
        for(int i = 0; i<4 /*agent.path.corners.Length*/; i++)
        {
            guiController.directions.Add(agent.path.corners[i].ToString());
            i++;
        }
        
    }

	// Update is called once per frame
	void Update()
    {
        
	}
}
