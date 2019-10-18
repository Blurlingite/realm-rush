using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
  [SerializeField] int towerLimit = 5;

  [SerializeField] Tower towerPrefab;


  public void AddTower(Waypoint baseWaypoint)
  {
    Tower[] towers = FindObjectsOfType<Tower>();
    int numTowers = towers.Length;

    if (numTowers < towerLimit)
    {
      InstantiateNewTower(baseWaypoint);
    }
    else
    {
      MoveExistingtower();
    }

  }

  private static void MoveExistingtower()
  {
    Debug.Log("Max towers reached");
  }

  private void InstantiateNewTower(Waypoint baseWaypoint)
  {
    Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);

    // don't let player place multiple towers on the same waypoint
    baseWaypoint.isPlaceable = false;

  }
}
