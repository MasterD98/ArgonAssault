using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("FX prefab")][SerializeField] GameObject deathFX;
    [Tooltip("in sec")][SerializeField] float LevelLoadDelay=1f;
    void OnTriggerEnter(Collider other)
    {
        deathFX.SetActive(true);
        StartDeathSequence();
        Invoke("ReloadScene",LevelLoadDelay);
        
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
    }
    void ReloadScene() { SceneManager.LoadScene(1);}
}
