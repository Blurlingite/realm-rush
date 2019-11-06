using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

  [SerializeField] float movementPeriod = 0.5f;
  [SerializeField] ParticleSystem goalParticle;

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

    foreach (Waypoint waypoint in pathToFollow)
    {
      // Enemy position becomes the current waypoint's position so the enemy moves on the path
      transform.position = waypoint.transform.position;

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
