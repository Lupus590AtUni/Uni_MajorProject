using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class customPath
{
    public Vector3 position;
    public Vector3 heading; // points towards next position 
    public Landmark landmark;
    public string turnWord;
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
        if(cornerHeading == Vector3.zero)
            return "arrived";

        Vector3 normal = Vector3.Cross(cornerPos, Vector3.up);

        float dot = Vector3.Dot(normal, cornerHeading);

        if(dot > 0)
            return "right";
        else
            return "left";

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

    public customPath[] convertPathToCustom(NavMeshPath oldPath)
    {
        customPath[] newPath = new customPath[oldPath.corners.Length];

        for(int i = 0; i < oldPath.corners.Length; i++)
        {
            newPath[i].position = oldPath.corners[i];

            if(i + 1 < oldPath.corners.Length)
                newPath[i].heading = findCornerHeading(oldPath.corners[i], oldPath.corners[i + 1]);
            else
                newPath[i].heading = Vector3.zero;

            newPath[i].landmark = findLandmark(newPath[i].position);

            newPath[i].turnWord = convertCornerToTurn(newPath[i].position, newPath[i].heading);
        }

        return newPath;
    }

    private string[] naturalLanguageConverter(customPath[] path)
    {
        //LOW: Actually put stuff here
        //LOW: fancier natural language generation
        return null;
    }

    public string[] convertPathToString(customPath[] path)
    {
        string[] str = new string[path.Length];

        for(int i = 0; i < path.Length; i++)
        {
            str[i] = "turn " + path[i].turnWord + " at the " + path[i].landmark.description;
        }

        return null;
    }

    public string[] convertPathToString(NavMeshPath path)
    {
        return convertPathToString(convertPathToCustom(path));
    }
}
