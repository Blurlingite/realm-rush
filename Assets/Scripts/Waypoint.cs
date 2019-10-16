using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
  // keeps track of which waypoint this waypoint was found from (making the path from the end to the start, so we know what the path is)
  public Waypoint exploredFrom;

  public bool isExplored = false;

  public bool isPlaceable = true;


  [SerializeField] Tower towerPrefab;

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
  public Vector2Int GetGridPos()
  {
    // used to calculate snapping of the cubes when we move them. 
    // The Mathf.RoundToInt() will take in the x position and divide it by 10 and then round it to the nearest int. So if the x was 6 than it would become 0.6 and then be rounded to 1
    // After we get 1 we multiply it by how much space should be between blocks (10)
    // This is how we snao the cubes to the nearest "10" position of x (10,20,30, etc)
    return new Vector2Int(
        Mathf.RoundToInt(transform.position.x / gridSize),

        Mathf.RoundToInt(transform.position.z / gridSize)
    );

  }


  void OnMouseOver()
  {
    // if left click
    if (Input.GetMouseButtonDown(0))
    {
      if (isPlaceable)
      {
        Instantiate(towerPrefab, transform.position, Quaternion.identity);

        // don't let player place multiple towers on the same waypoint
        isPlaceable = false;
      }
      else
      {
        print("Can't place here");
      }
    }
  }

  // public void SetTopColor(Color color)
  // {
  //   MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();

  //   topMeshRenderer.material.color = color;
  // }








}
