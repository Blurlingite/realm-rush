using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this lets your code run before pressing the play button whenever you do anything
[ExecuteInEditMode]
[SelectionBase] // lets you select the parent object when you click on it in the Scene View instead of the child objects
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{


  Waypoint waypoint;

  private void Awake()
  {
    waypoint = GetComponent<Waypoint>();
  }

  // Update is called once per frame
  void Update()
  {
    SnapToGrid();

    UpdateLabel();

  }



  void UpdateLabel()
  {
    TextMesh textMesh = GetComponentInChildren<TextMesh>();

    string labelText =

    waypoint.GetGridPos().x
    + ","
    + waypoint.GetGridPos().y;

    textMesh.text = labelText;

    gameObject.name = labelText;
  }

  void SnapToGrid()
  {
    int gridSize = waypoint.GetGridSize();


    // The reason we use the .y in the z position is b/c the Vector2Int we got this GetGridPos() from uses (x,y)
    // x is the x and y is the z coordinate
    transform.position = new Vector3(waypoint.GetGridPos().x * gridSize, 0f, waypoint.GetGridPos().y * gridSize);

  }






}
