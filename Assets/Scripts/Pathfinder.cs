using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{


  // key = position of block, value = block
  Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

  Queue<Waypoint> queue = new Queue<Waypoint>();

  bool isRunning = true;

  // specifies which direction we can move
  Vector2Int[] directions = {
    Vector2Int.up,
    Vector2Int.right,
    Vector2Int.down,
    Vector2Int.left
};

  [SerializeField] Waypoint startWaypoint, endWaypoint;

  // Start is called before the first frame update
  void Start()
  {
    LoadBlocks();
    ColorStartAndEnd();
    Pathfind();

    // ExploreNeighbors();

  }

  private void Pathfind()
  {
    // add the start to the queue
    queue.Enqueue(startWaypoint);

    // while there is something in the queue
    while (queue.Count > 0 && isRunning)
    {
      // where you will search from (the first time it will be the startWaypoint since that's the only thing you added above with Enqueue())
      // Dequeue() will also remove the item it is on from the queue and starts from the next available item, otherwise we'd be searching from the same point forever. It is used to retrieve the top most element in a queue collection
      Waypoint searchCenter = queue.Dequeue();

      print("Searching from " + searchCenter);

      HaltIfEndFound(searchCenter);

      ExploreNeighbors(searchCenter);

      // marked that you've explored this waypoint's neighbors already
      searchCenter.isExplored = true;

    }

    print("Finished pathfinding?");

  }

  private void HaltIfEndFound(Waypoint searchCenter)
  {
    if (searchCenter == endWaypoint)
    {
      print("Searched from The End");
      isRunning = false;
    }
  }

  private void ExploreNeighbors(Waypoint searchFrom)
  {

    if (!isRunning) { return; }

    foreach (Vector2Int direction in directions)
    {
      Vector2Int neighborCoordinates = searchFrom.GetGridPos() + direction;

      try
      {
        QueueNewNeighbors(neighborCoordinates);
      }
      catch
      {

      }

    }
  }

  private void QueueNewNeighbors(Vector2Int neighborCoordinates)
  {
    // look up neighbor by coordinates from dictionary
    Waypoint neighbor = grid[neighborCoordinates];


    // if the neighboring waypoint has been explored, do nothing (do not add it to the queue)
    // else, add it to the queue so we can search from there at some point
    if (neighbor.isExplored)
    {
      // do nothing
    }
    else
    {
      // find the waypoint by it's coordinate and color it
      neighbor.SetTopColor(Color.blue);

      // add this neighbor waypoint to the queue so we keep searching until we find the end (once the queue has nothing in it, it stops searching the path)
      queue.Enqueue(neighbor);

      print("Queueing " + neighbor);
    }

  }

  private void ColorStartAndEnd()
  {
    startWaypoint.SetTopColor(Color.green);
    endWaypoint.SetTopColor(Color.red);

  }

  void LoadBlocks()
  {
    var waypoints = FindObjectsOfType<Waypoint>();

    foreach (Waypoint waypoint in waypoints)
    {
      var gridPos = waypoint.GetGridPos();


      if (grid.ContainsKey(gridPos))
      {
        Debug.LogWarning("Skipping overlapping block " + waypoint);
      }
      else
      {
        grid.Add(gridPos, waypoint);

      }
    }

  }


  // Update is called once per frame
  void Update()
  {

  }
}
