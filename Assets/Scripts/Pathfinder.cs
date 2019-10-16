using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
  // key = position of block, value = block
  Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

  Queue<Waypoint> queue = new Queue<Waypoint>();

  // the shortest path we found (will be descending order until we reverse it so our Enemy can travel on this path)
  [SerializeField]
  List<Waypoint> path = new List<Waypoint>();

  bool isRunning = true;

  Waypoint searchCenter; // the current waypoint we are searching from

  // specifies which direction we can move
  Vector2Int[] directions = {
    Vector2Int.up,
    Vector2Int.right,
    Vector2Int.down,
    Vector2Int.left
};

  [SerializeField] Waypoint startWaypoint, endWaypoint;


  private void CreatePath()
  {
    SetAsPath(endWaypoint);

    // now set where the destination was explored from
    Waypoint previous = endWaypoint.exploredFrom;

    while (previous != startWaypoint)
    {
      // add intermediate waypoints
      SetAsPath(previous);

      // set previous to the waypoint before the original previous
      // So before, previous was the waypoint stored on the endWaypoint. Now, it is the waypoint stored on the waypoint that was stored on the endWaypoint.
      // when the while loop executes again, previous will be re-assigned the waypoint before until we get to the startWaypoint
      previous = previous.exploredFrom;
    }

    // Add the waypoint you started from
    SetAsPath(startWaypoint);

    // reverse the list to get the correct path the enemy needs to travel
    path.Reverse();
  }

  private void SetAsPath(Waypoint waypoint)
  {
    // first add the destination
    path.Add(waypoint);

    waypoint.isPlaceable = false;
  }

  private void BreadthFirstSearch()
  {
    // add the start to the queue
    queue.Enqueue(startWaypoint);

    // while there is something in the queue and it is running
    while (queue.Count > 0 && isRunning)
    {
      // where you will search from (the first time it will be the startWaypoint since that's the only thing you added above with Enqueue())
      // Dequeue() will also remove the item it is on from the queue and starts from the next available item, otherwise we'd be searching from the same point forever. It is used to retrieve the top most element in a queue collection
      searchCenter = queue.Dequeue();

      HaltIfEndFound();

      ExploreNeighbors();

      // marked that you've explored this waypoint's neighbors already
      searchCenter.isExplored = true;
    }

  }

  private void HaltIfEndFound()
  {
    if (searchCenter == endWaypoint)
    {
      print("Searched from The End");
      isRunning = false;
    }
  }

  private void ExploreNeighbors()
  {
    if (!isRunning) { return; }

    foreach (Vector2Int direction in directions)
    {
      Vector2Int neighborCoordinates = searchCenter.GetGridPos() + direction;

      // if the dictionary has the neighbor coordinated, add it to the queue. This is to prevent queueing coordinated that do not exist (like empty spaces between blocks)
      if (grid.ContainsKey(neighborCoordinates))
      {
        QueueNewNeighbors(neighborCoordinates);
      }

    }
  }

  private void QueueNewNeighbors(Vector2Int neighborCoordinates)
  {
    // look up neighbor by coordinates from dictionary
    Waypoint neighbor = grid[neighborCoordinates];

    // if the neighboring waypoint has been explored or this neighbor was already explored (prevent duplicate queuing of waypoints), do nothing (do not add it to the queue)
    // else, add it to the queue so we can search from there at some point
    if (neighbor.isExplored || queue.Contains(neighbor))
    {
      // do nothing
    }
    else
    {

      // add this neighbor waypoint to the queue so we keep searching until we find the end (once the queue has nothing in it, it stops searching the path)
      queue.Enqueue(neighbor);

      // when we add the neighbor waypoint to the queue that neighbor waypoint needs to know where it was found from. It was found from the waypoint we were searching from at the time, which is stored in searchCenter in this line: searchCenter = queue.Dequeue();

      neighbor.exploredFrom = searchCenter;

      // todo make a method that rounds the x and z positions when w is the endpoint, b/c the z coordinate is off and that's why the if statement would not work
      // I was getting a null error because the endwaypoint was not getting it's exploredFrom set. This is b/c I added the endWaypoint prefab into the path and not the actual end waypoint that gets found during runtme
      Waypoint w = grid[neighborCoordinates];

      float wx = RoundFloat(w.transform.position.x);
      float wz = RoundFloat(w.transform.position.z);


      if (wx == endWaypoint.transform.position.x && wz == endWaypoint.transform.position.z)
      {
        endWaypoint.exploredFrom = searchCenter;
      }

    }

  }

  // private void ColorStartAndEnd()
  // {
  //   startWaypoint.SetTopColor(Color.green);
  //   endWaypoint.SetTopColor(Color.red);
  // }

  void LoadBlocks()
  {
    var waypoints = FindObjectsOfType<Waypoint>();

    foreach (Waypoint waypoint in waypoints)
    {
      var gridPos = waypoint.GetGridPos();

      if (grid.ContainsKey(gridPos))
      {
        // Debug.LogWarning("Skipping overlapping block " + waypoint);
      }
      else
      {
        grid.Add(gridPos, waypoint);
      }
    }

  }

  public List<Waypoint> GetPath()
  {
    // if path was calculated, do not calulate it again when multiple enemies spawn it will cause an error
    if (path.Count == 0)
    {
      CalculatePath();
    }

    return path;
  }

  private void CalculatePath()
  {
    LoadBlocks();
    // ColorStartAndEnd();
    BreadthFirstSearch();
    CreatePath();
  }

  float RoundFloat(float roundThis)
  {
    // 2.6
    roundThis = roundThis / 10;
    // 3 -> 30
    roundThis = Mathf.Round(roundThis) * 10;

    return roundThis;
  }



}
