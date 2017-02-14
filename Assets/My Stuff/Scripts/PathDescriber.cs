using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class customPath
{
    public Vector3 position;
    public Vector3 heading; // points towards next position 
    public Landmark landmark;
    public string turnWord; //TODO: how best to do this
}

public class PathDescriber : MonoBehaviour
{

    private GameController gameController;
    private Landmark[] landmarks;

    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        landmarks = FindObjectsOfType<Landmark>();
    }

    private string convertCornerToTurn(Vector3 cornerPos, Vector3 cornerHeading)
    {
        //TODO: dot product stuff on vectors to identify left and right turns

        return null;
    }

    private Landmark findNearestLandmark(Vector3 position)
    {
        return null;
    }

    private Vector3 findCornerHeading(Vector3 current, Vector3 next)
    {
        return null;
    }

    private customPath[] convertPathToCustom(NavMeshPath oldPath)
    {
        return null;
    }

    //TODO: fancier natural language generation

    public string[] convertPath(NavMeshPath oldPath)
    {
        return null;
    }

    // Update is called once per frame
    void Update()
    {
		
	}
}
