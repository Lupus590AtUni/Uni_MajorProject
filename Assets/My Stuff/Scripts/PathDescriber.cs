﻿using System.Collections;
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

    public string[] convertCornersToTurns(Vector3[] corners)
    {
        //TODO: dot product stuff on vectors to identify left and right turns

        return null;
    }

    public Landmark findNearestLandmark(Vector3 position)
    {
        return null;
    }

    public customPath linkTurnToLandmarks(Vector3 corner)
    {
        return null;
    }

    // Update is called once per frame
    void Update()
    {
		
	}
}