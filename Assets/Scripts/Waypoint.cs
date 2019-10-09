﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

  // a pair of ints (x,y)
  Vector2Int gridPos;

  const int gridSize = 10;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public int GetGridSize()
  {
    return gridSize;
  }

  // calculates where in the world this waypoint is
  public Vector2 GetGridPos()
  {
    // used to calculate snapping of the cubes when we move them. 
    // The Mathf.RoundToInt() will take in the x position and divide it by 10 and then round it to the nearest int. So if the x was 6 than it would become 0.6 and then be rounded to 1
    // After we get 1 we multiply it by how much space should be between blocks (10)
    // This is how we snao the cubes to the nearest "10" position of x (10,20,30, etc)
    return new Vector2Int(
        Mathf.RoundToInt(transform.position.x / gridSize) * gridSize,

        Mathf.RoundToInt(transform.position.z / gridSize) * gridSize
    );

  }
}
