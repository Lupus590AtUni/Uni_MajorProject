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

        // TODO: fix
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



    public List<customPath> convertPathToCustom(NavMeshPath oldPath)
    {
        //TODO: debug oldPathNodes, place a line or nodes or something
        List<customPath> newPath = new List<customPath>();

        for(int i = 0; i < oldPath.corners.Length; i++)
        {
            //print(oldPath.corners[i].ToString());

            //TODO: don't add if too close
            /*if(i != 0 && 5 > Mathf.Abs(oldPath.corners[i].magnitude - oldPath.corners[i-1].magnitude)) //i!=0 to prevent error on first index
            {
                continue;
            }*/

            newPath.Add(new customPath());

            newPath[i].position = oldPath.corners[i];

            //https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
            GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            marker.transform.position = new Vector3(newPath[i].position.x, newPath[i].position.y, newPath[i].position.z);


            //newPath[i].position.x = oldPath.corners[i].x;
            //newPath[i].position.y = oldPath.corners[i].y;
            //newPath[i].position.z = oldPath.corners[i].z;


            if(i + 1 < oldPath.corners.Length)
                newPath[i].heading = findCornerHeading(oldPath.corners[i], oldPath.corners[i + 1]);
            else
                newPath[i].heading = Vector3.zero;

            newPath[i].landmark = findLandmark(newPath[i].position);

            newPath[i].turnWord = convertCornerToTurn(newPath[i].position, newPath[i].heading);
        }

        return newPath;
    }

    private List<string> naturalLanguageConverter(List<customPath> path)
    {
        //LOW: Actually put stuff here
        //LOW: fancier natural language generation
        return null;
    }

    public List<string> convertPathToString(List<customPath> path)
    {
        List<string> str = new List<string>();

        for(int i = 0; i < path.Count; i++)
        {
            //TODO: find out how to combine strings
            

            str.Add(string.Concat("turn ", path[i].turnWord , " at the " , path[i].landmark.description));
        }

        return str; ;
    }

    public List<string> convertPathToString(NavMeshPath path)
    {
        return convertPathToString(convertPathToCustom(path));
    }
}
