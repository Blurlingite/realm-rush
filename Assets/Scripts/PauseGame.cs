using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    Canvas gameOverCanvas;
    bool _isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && _isPaused == false)
        {
            _isPaused = true;
            gameOverCanvas.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else if (Input.GetKeyDown(KeyCode.P) && _isPaused == true)
        {
            _isPaused = false;
            gameOverCanvas.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}
