using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndSwitch : MonoBehaviour
{
    [SerializeField]
    Canvas gameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
