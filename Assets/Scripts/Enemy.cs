using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] GameObject deathFX;
    [SerializeField] int scorePerHit = 12;
    [SerializeField] int hits = 10;
    ScoreBoard scoreBoard;
    // Start is called before the first frame update
    void Start()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider()
    {
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hits < 1) { KillEnemy(); }
    }

    private void ProcessHit()
    {
        scoreBoard.ScoreHit(scorePerHit);
        hits--;
        explode();
    }

    private void KillEnemy()
    {
        //explode();
        Destroy(gameObject);
    }

    private void explode()
    {
        GameObject explosion = Instantiate(deathFX, transform.position, Quaternion.identity);
        explosion.transform.parent = parent;
    }
}
