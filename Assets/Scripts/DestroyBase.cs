using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyBase : MonoBehaviour
{

    [SerializeField] PlayerHealth friendlyBase;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField]
    Canvas gameOverCanvas;
    bool stopLooping = false;

    void Update()
    {
        int playerHealth = friendlyBase.getPlayerHealth();

        if (playerHealth == 0 && stopLooping == false)
        {
            StartCoroutine(DestroyFriendlyBase());
        }

    }


    IEnumerator DestroyFriendlyBase()
    {
        stopLooping = true;

        Vector3 explosionPosition = new Vector3(-0.3f, 5f, 6.4f);

        Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);

        friendlyBase.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);

        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(1.5f);

        GameObject.Find("AA Theme Reversed").GetComponent<AudioSource>().Stop();

        Time.timeScale = 0.0f;

        gameOverCanvas.gameObject.SetActive(true);

        this.enabled = false;
    }
}
