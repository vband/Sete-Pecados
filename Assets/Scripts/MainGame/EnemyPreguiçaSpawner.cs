﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPreguiçaSpawner : MonoBehaviour {

    public GameObject limiteDaCamera;
    public GameObject InimigoPreguiça;
    public float cooldown;

    // Use this for initialization
    void Start()
    {
        SpawnerStart(cooldown);
    }

    void SpawnerStart(float cooldown)
    {
        StartCoroutine(Spawner(cooldown));
    }

    IEnumerator Spawner(float cooldown)
    {
        inicio:
        yield return new WaitForSeconds(cooldown);
        if (!SceneController.paused || Time.timeScale != 0)
        {
            Instantiate(InimigoPreguiça, new Vector3(limiteDaCamera.transform.position.x + 7, Random.Range(2, 3), 0), Quaternion.AngleAxis(90, Vector3.forward), transform.parent);
        }
        goto inicio;
    }
}
