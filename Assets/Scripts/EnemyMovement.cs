using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

  // Start is called before the first frame update
  void Start()
  {
    Pathfinder pathfinder = FindObjectOfType<Pathfinder>();

    List<Waypoint> path = pathfinder.GetPath();

    StartCoroutine(FollowPath(path));
  }

  // Update is called once per frame
  void Update()
  {

  }

  IEnumerator FollowPath(List<Waypoint> pathToFollow)
  {
    print("Starting patrol...");

    foreach (Waypoint waypoint in pathToFollow)
    {
      // Enemy position becomes the current waypoint's position so the enemy moves on the path
      transform.position = waypoint.transform.position;

      yield return new WaitForSeconds(1f);
    }
    print("Ending patrol");
  }





}
