using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigator : MonoBehaviour
{
    // https://docs.unity3d.com/Manual/nav-CreateNavMeshAgent.html

    public Transform goal;
    public Transform gameController;

	// Use this for initialization
	void Start()
    {
        //print("Navigator Script: started");
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
        //agent.path
    }

    void findLandmarks()
    {

    }

	// Update is called once per frame
	void Update()
    {
        
	}
}
