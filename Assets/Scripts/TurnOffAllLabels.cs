using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffAllLabels : MonoBehaviour
{
  void Start()
  {
    TextMesh[] allLabels = FindObjectsOfType<TextMesh>();

    foreach (TextMesh t in allLabels)
    {
      t.gameObject.SetActive(false);
    }

  }
}
