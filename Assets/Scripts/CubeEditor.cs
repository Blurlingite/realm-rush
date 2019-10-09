using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this lets your code run before pressing the play button whenever you do anything
[ExecuteInEditMode]
[SelectionBase] // lets you select the parent object when you click on it in the Scene View instead of the child objects
public class CubeEditor : MonoBehaviour
{

  // Range will restrict this number in the Inspector to be only with this range (of 1-20 in this case), so you cannot assign this field a value less than 1 or gretaer than 20 in the Inspector
  [SerializeField] [Range(1f, 20f)] float gridSize = 10f;

  TextMesh textMesh;

  // Update is called once per frame
  void Start()
  {

  }



  // Update is called once per frame
  void Update()
  {



    Vector3 snapPos;

    // used to calculate snapping of the cubes when we move them. 
    // The Mathf.RoundToInt() will take in the x position and divide it by 10 and then round it to the nearest int. So if the x was 6 than it would become 0.6 and then be rounded to 1
    // After we get 1 we multiply it by how much space should be between blocks (10)
    // This is how we snao the cubes to the nearest "10" position of x (10,20,30, etc)

    snapPos.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;

    snapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;

    textMesh = GetComponentInChildren<TextMesh>();

    string labelText = snapPos.x / gridSize + "," + snapPos.z / gridSize;

    textMesh.text = labelText;

    gameObject.name = labelText;

    transform.position = new Vector3(snapPos.x, 0f, snapPos.z);

  }
}
