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

    private string convertCornerToTurn(Vector3 currentHeading, Vector3 newHeading)
    {
        if(newHeading == Vector3.zero)
            return "arrived";

        Vector3 normal = Vector3.Cross(currentHeading, Vector3.up);

        float dot = Vector3.Dot(normal, newHeading);

        if(dot < 0)
            return "right";
        else
            return "left";

        return null;
    }

    private Landmark findLandmark(Vector3 position) //find the nearest landmark
    {

        Landmark nearest = landmarks[0]; //BUG: init Null reference exception
                                            //only happens once per run, might be on init
        foreach( Landmark current in landmarks)
        {
            //TODO: add better criteria
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

    //HACK: this entire function is to hide a bug
    List<customPath> pruneList(List<customPath> oldPath)
    {
        //return oldPath; 

        List<customPath> newPath = new List<customPath>();

        newPath.Add(oldPath[0]); //Ensure first node is added

        //NOTE: potential bug
            //may have issues if oldPath.count is less than two
        for(int i = 1; i < oldPath.Count-1; i++)
        {
            //add if not too close
            if(1 < Mathf.Abs((oldPath[i].position.magnitude - oldPath[i-1].position.magnitude))) //i!=0 to prevent error on first index
            {
                newPath.Add(oldPath[i]);
            }
        }

        //Ensure last node is added
        newPath.Add(oldPath[oldPath.Count-1]); 

        return newPath;
    }

    public List<customPath> convertPathToCustom(NavMeshPath oldPath)
    {
        List<customPath> newPath = new List<customPath>();

        for(int i = 0; i < oldPath.corners.Length; i++)
        {
            //print(oldPath.corners[i].ToString());

            //BUG: many nodes
                //many things when trying to not add node if too close another node
            if(i != 0 && 1 > Mathf.Abs((oldPath.corners[i] - oldPath.corners[i-1]).magnitude)) //i!=0 to prevent error on first index
            {
                //continue;
            }

            newPath.Add(new customPath());

            newPath[i].position = oldPath.corners[i]; //BUG: many nodes
                                                        //argument out of range exception - only heppens when nodes get skipped with above if

            //when removeing nodes this spam creates
            //NOTE: debug
                //node visulaiser
            //https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
            //GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere); marker.transform.position = new Vector3(newPath[i].position.x, newPath[i].position.y, newPath[i].position.z);


            //newPath[i].position.x = oldPath.corners[i].x;
            //newPath[i].position.y = oldPath.corners[i].y;
            //newPath[i].position.z = oldPath.corners[i].z;


            if(i + 1 < oldPath.corners.Length)
                newPath[i].heading = findCornerHeading(oldPath.corners[i], oldPath.corners[i + 1]);
            else
                newPath[i].heading = Vector3.zero;

            newPath[i].landmark = findLandmark(newPath[i].position);

            if(i==0)
            {
                newPath[i].turnWord = "go";
            }
            else
            {
                newPath[i].turnWord = convertCornerToTurn(newPath[i - 1].heading, newPath[i].heading);
            }
            
        }

        newPath = pruneList(newPath);

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
            str.Add(string.Concat("turn ", path[i].turnWord , " at the " , path[i].landmark.description));
        }

        return str; ;
    }

    public List<string> convertPathToString(NavMeshPath path)
    {
        return convertPathToString(convertPathToCustom(path));
    }
}
