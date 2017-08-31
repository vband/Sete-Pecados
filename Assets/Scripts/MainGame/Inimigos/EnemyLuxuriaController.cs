using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLuxuriaController : MonoBehaviour {

    [SerializeField] private GameObject Arremecavel;

	// Use this for initialization
	void Start () {
        StartCoroutine(Arremassador());
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    IEnumerator Arremassador()
    {
        inicio:
        yield return new WaitForSeconds(0.5f);
        //tem chance de arremessar os objetos indecentes
        if (Random.Range(0,100) > 50 && !SceneController.paused)
            Instantiate(Arremecavel, transform.position + Vector3.left, Quaternion.identity);        
        goto inicio;
    }
}
