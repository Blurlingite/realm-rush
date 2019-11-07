using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

  [SerializeField] float movementPeriod = 0.5f;
  [SerializeField] ParticleSystem goalParticle;

  Pathfinder pathfinder;


  // Start is called before the first frame update
  void Start()
  {
    pathfinder = FindObjectOfType<Pathfinder>();

    List<Waypoint> path = pathfinder.GetPath();

    StartCoroutine(FollowPath(path));
  }

  // Update is called once per frame
  void Update()
  {

  }

  IEnumerator FollowPath(List<Waypoint> pathToFollow)
  {

    foreach (Waypoint waypoint in pathToFollow)
    {

      // The Breadth First Search had issues finding the exact positions of the waypoint & enemies were moving in between blocks. The z coordinate was off by 4.5f on all waypoints except the start & end waypoint b/c those were assigned in the inspector.
      // I got the start & end waypoints and see if the current waypoint had their coordinates. If yes, then I set the position of the enemy to that coordinate. Else, then I still set the enenmy position to the current waypoint's but I also add 4.5f to the z coordinate. This made the enemy travel on the blocks in the center instead of in between.
      Waypoint startPoint = pathfinder.getStartWaypoint();
      Waypoint endPoint = pathfinder.getEndWaypoint();

      if (waypoint.transform.position == startPoint.transform.position || waypoint.transform.position == endPoint.transform.position)
      {
        // Enemy position becomes the current waypoint's position so the enemy moves on the path
        transform.position = waypoint.transform.position;
      }
      else
      {
        transform.position = waypoint.transform.position;

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 4.5f);
      }


      yield return new WaitForSeconds(movementPeriod);
    }
    SelfDestruct();
  }


  private void SelfDestruct()
  {
    var vfx = Instantiate(goalParticle, transform.position, Quaternion.identity);

    vfx.Play();

    float destroyDelay = vfx.main.duration;

    // destroy particles
    Destroy(vfx.gameObject, destroyDelay);

    Destroy(this.gameObject);
  }



}
