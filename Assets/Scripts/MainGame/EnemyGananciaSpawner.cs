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

    void SpawnerStart(float cooldown)
    {
        StartCoroutine(Spawner(cooldown));
    }

    IEnumerator Spawner(float cooldown)
    {
    inicio:
        yield return new WaitForSeconds(cooldown);
        if ((!SceneController.paused || Time.timeScale != 0) && GameObject.Find("EnemyGananciaEsquerda(Clone)") == null
            && !SceneController.winGame)
        {   
            Instantiate(InimigoGanancia, new Vector3(limiteDaCamera.transform.position.x + 15, 5.5f, 0), Quaternion.identity, transform.parent);
        }
        else if (SceneController.winGame)
        {
            DestroyAll();
        }
        goto inicio;
    }

    private void DestroyAll()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("EnemyGanancia");
        for (int i = 0; i < go.Length; i++)
        {
            Destroy(go[i]);

        }
    }
}
