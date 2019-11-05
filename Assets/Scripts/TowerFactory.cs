using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
  [SerializeField] int towerLimit = 5;

  [SerializeField] Tower towerPrefab;

  [SerializeField] Transform towerParentTransform;

  Queue<Tower> towerQueue = new Queue<Tower>();

  public void AddTower(Waypoint baseWaypoint)
  {
    Tower[] towers = FindObjectsOfType<Tower>();
    int numTowers = towerQueue.Count;

    if (numTowers < towerLimit)
    {
      InstantiateNewTower(baseWaypoint);
    }
    else
    {
      MoveExistingtower(baseWaypoint);
    }

  }

  private void MoveExistingtower(Waypoint newBaseWaypoint)
  {
    // take bottom tower off queue
    Tower oldTower = towerQueue.Dequeue();

    // set the old block the tower was on back to being placeable so we can place towers on it again
    oldTower.baseWaypoint.isPlaceable = true;

    // set the new block (which you'll be passing into this method) the tower is now on to be unplaceable
    newBaseWaypoint.isPlaceable = false;

    // set the tower's base Waypoint to the new base Waypoint
    oldTower.baseWaypoint = newBaseWaypoint;

    // now move the tower
    oldTower.transform.position = newBaseWaypoint.transform.position;

    //put the old tower on top of the queue
    towerQueue.Enqueue(oldTower);

  }

  private void InstantiateNewTower(Waypoint baseWaypoint)
  {
    Tower newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);

    // parent the new tower
    newTower.transform.parent = towerParentTransform;

    // don't let player place multiple towers on the same waypoint
    baseWaypoint.isPlaceable = false;

    // assign the baseWaypoint to the block it is now on
    newTower.baseWaypoint = baseWaypoint;
    // make the block it is now on unable to have more towers placed on it
    baseWaypoint.isPlaceable = false;

    towerQueue.Enqueue(newTower);


  }
}
