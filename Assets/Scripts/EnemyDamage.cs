using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab, deathParticlePrefab;

    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;

    AudioSource myAudioSource;


    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
    void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (hitPoints <= 0)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        hitPoints = hitPoints - 1;
        hitParticlePrefab.Play();
        myAudioSource.PlayOneShot(enemyHitSFX);
    }

    private void KillEnemy()
    {
        var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);

        vfx.Play();

        float destroyDelay = vfx.main.duration;

        // destroy enemy death particles
        Destroy(vfx.gameObject, destroyDelay);

        // The AudioSource here is Unity's global Audio Source (NOT the one attached to this script's object). We use that here because right after this line, the object this script is on & this script will be destroyed, so the audio won't have time to play
        // We still couldn't hear the death sound effect because in 3D games, audio dies down depending how far away the death was from the Main Camera. So we just needed to pass in the Main Camera's position into the PlayClipAtPoint() method. This will play the death sound effect at the main camera's postion (and since the Main Camera has the Audio Listener component, it can listen to audio being played by an Audio Source)
        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position);

        Destroy(this.gameObject);
    }


}
