using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

  [SerializeField] List<Waypoint> path;
  // Start is called before the first frame update
  void Start()
  {
    // StartCoroutine(FollowPath());
    // print("Hey, I'm back at Start");

  }

  // Update is called once per frame
  void Update()
  {

  }

  IEnumerator FollowPath()
  {
    print("Starting patrol...");

    foreach (Waypoint waypoint in path)
    {
      // Enemy position becomes the current waypoint's position so the enemy moves on the path
      transform.position = waypoint.transform.position;

      print("Visiting: " + waypoint);

      yield return new WaitForSeconds(1f);
    }
    print("Ending patrol");
  }





}
