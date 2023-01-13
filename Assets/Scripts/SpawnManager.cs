using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float elapsed;
    private float timerSpeed = 3f;
    public GameObject enemyPrefab;
    private Vector2 spawnPos = new Vector3(-10, 4, 1);



    void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed >= timerSpeed)
        {
            elapsed = 0f;
            timerSpeed = Random.Range(1f, 3f);

            GameController.Instance.CriarNovoInimigo();
        }
    }
}
