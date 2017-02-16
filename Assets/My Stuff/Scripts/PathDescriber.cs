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

    private Landmark findLandmark(Vector3 position) //find the nearest landmark
    {

        Landmark nearest = landmarks[0];
        foreach( Landmark current in landmarks)
        {
            //LOW: add better criteria
            if(Vector3.Distance(nearest.transform.position, position) > Vector3.Distance(current.transform.position, position))
            {
                nearest = current;
            }
        }

        return nearest;
    }

    private Vector3 findCornerHeading(Vector3 current, Vector3 next)
    {
        Vector3 heading = next - current; //hopefully this is pointing the right way
            
        return heading;
    }

    private customPath[] convertPathToCustom(NavMeshPath oldPath)
    {
        //TODO: Actually put stuff here
        // call the above stuff in the correct order with 'glue code'

        return null;
    }

    private string[] naturalLanguageConverter(customPath[] path)
    {
        //LOW: Actually put stuff here
        //LOW: fancier natural language generation
        return null;
    }

    public string[] convertPath(NavMeshPath oldPath)
    {
        //TODO: Actually put stuff here
        return null;
    }
}
