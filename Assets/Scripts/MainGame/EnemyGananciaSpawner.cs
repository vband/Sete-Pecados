using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGananciaSpawner : MonoBehaviour {
    public GameObject limiteDaCamera;
    public GameObject InimigoGanancia;
    public float cooldown;

	// Use this for initialization
	void Start () {
        SpawnerStart(cooldown);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnerStart(float cooldown)
    {
        StartCoroutine(Spawner(cooldown));
    }

    IEnumerator Spawner(float cooldown)
    {
    inicio:
        yield return new WaitForSeconds(cooldown);
        if (!SceneController.paused)
        {   
            Instantiate(InimigoGanancia, new Vector3(limiteDaCamera.transform.position.x + 5, 5, 0), Quaternion.identity, transform.parent);
        }
        goto inicio;
    }
}
