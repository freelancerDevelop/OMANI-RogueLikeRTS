﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MetaAudioController : MonoBehaviour
{

    public AudioSource loopingSFX;
    public GameObject[] waveSfxPrefabs;
    public GameObject[] explosionSfxPregabs;
    public GameObject[] smallExplosionSfxPregabs;
    public float globalProgressSpeed = 1f, startTimer;


    private float globalProgress;

    void Start()
    {

        globalProgress = 0f;

    }

    // Spawn Empty with Sound FX
    public void EmitParticleExplosion(Vector3 pos, bool big)
    {
        if (big == true)
        {
            Instantiate(explosionSfxPregabs[Random.Range(0, explosionSfxPregabs.Length)], pos, transform.rotation);
        }
        else
        {
            Instantiate(smallExplosionSfxPregabs[Random.Range(0, smallExplosionSfxPregabs.Length)], pos, transform.rotation);
        }
    }

    void Update()
    {
        
        if (globalProgress >= 0f)
        {
            globalProgress -= Time.deltaTime * globalProgressSpeed;
        }

        loopingSFX.volume = globalProgress -0.1f ;

        if (Time.time - startTimer > 0.5)
        {
            loopingSFX.pitch = 1;
        }
    }

    public void ResetLaserProgress()
    {
        globalProgress = 1f;
    }

    public void StartSound()
    {
        Instantiate(waveSfxPrefabs[Random.Range(0, waveSfxPrefabs.Length)], transform.position, transform.rotation);
    }

    public void energyTransmisionSound(float value)
    {
        startTimer = Time.time;
        loopingSFX.pitch += (value / 8) * Time.unscaledDeltaTime;
        Debug.Log((value / 8) * Time.unscaledDeltaTime);
        loopingSFX.pitch = Mathf.Clamp(loopingSFX.pitch, 1, 3f);
    }
}
