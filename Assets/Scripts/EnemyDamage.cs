using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }


  void OnParticleCollision(GameObject other)
  {
    print("I'm hit");
  }


}
