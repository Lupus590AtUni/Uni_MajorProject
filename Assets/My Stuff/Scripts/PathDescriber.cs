using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	
	// Update is called once per frame
	void Update()
    {
		
	}
}
