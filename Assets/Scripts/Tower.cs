using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

  [SerializeField] Transform objectToPan;
  [SerializeField] float attackRange;
  [SerializeField] ParticleSystem projectileParticle;

  // State of each tower
  Transform targetEnemy;

  // Update is called once per frame
  void Update()
  {
    SetTargetEnemy();

    if (targetEnemy)
    {
      objectToPan.LookAt(targetEnemy);
      FireAtEnemy();
    }
    else
    {
      Shoot(false);
    }

  }

  private void SetTargetEnemy()
  {
    // find all enemies (by searching for their EnemyDamage script)
    EnemyDamage[] sceneEnemies = FindObjectsOfType<EnemyDamage>();

    if (sceneEnemies.Length == 0) { return; }

    Transform closestEnemy = sceneEnemies[0].transform;

    foreach (EnemyDamage testEnemy in sceneEnemies)
    {
      closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
    }

    targetEnemy = closestEnemy;
  }

  private Transform GetClosest(Transform transformA, Transform transformB)
  {
    // get distance from this Tower to the transform of the 1st argument you passed in
    var distToA = Vector3.Distance(transform.position, transformA.position);

    var distToB = Vector3.Distance(transform.position, transformB.position);

    if (distToA < distToB)
    {
      return transformA;
    }

    // otherwise, return transformB
    return transformB;


  }

  private void FireAtEnemy()
  {
    float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, this.gameObject.transform.position);

    if (distanceToEnemy <= attackRange)
    {
      Shoot(true);
    }
    else
    {
      Shoot(false);
    }
  }

  private void Shoot(bool isActive)
  {
    var emissionModule = projectileParticle.emission;
    emissionModule.enabled = isActive;
  }
}
