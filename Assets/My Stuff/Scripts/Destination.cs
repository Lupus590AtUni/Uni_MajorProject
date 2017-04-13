using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{

	// Use this for initialization
	void Start()
    {
		
	}
	
	// Update is called once per frame
	void Update()
    {
		
	}

}

public class DestionationSorter : IComparer<Destination>
{
    public int /*IComparer<Destination>.*/Compare(Destination x, Destination y)
    {
        //None of the landmarks used for this share and axis so the y part can probably be removed
        //https://support.microsoft.com/en-us/help/320727/how-to-use-the-icomparable-and-icomparer-interfaces-in-visual-c
        if(x.transform.position.x > y.transform.position.x)
            return 1;
        else if(x.transform.position.x < y.transform.position.x)
            return -1;
        else
        {
            if(x.transform.position.y > y.transform.position.y)
                return 1;
            else
                return -1;
        }
    }
}
