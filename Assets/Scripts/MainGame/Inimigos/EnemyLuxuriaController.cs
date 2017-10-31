using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLuxuriaController : MonoBehaviour {

    [SerializeField] private GameObject Arremecavel;

    private Animator animator;

	// Use this for initialization
	void Start () {
        //StartCoroutine(Arremassador());
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    /*
    IEnumerator Arremassador()
    {
        inicio:
            yield return new WaitForSeconds(0.8f);
            //tem chance de arremessar os objetos indecentes
            if (Random.Range(0,100) > 50 && !SceneController.paused)
                //Instantiate(Arremecavel, transform.position + Vector3.left, Quaternion.identity);
                
        goto inicio;
    }
    */

    private void InstantiateBullet()
    {
        Instantiate(Arremecavel, transform.position + Vector3.left, Quaternion.identity);
    }

    private IEnumerator StopRandomly()
    {
        // Tem uma chance de 60% de pausar os arremessos de projéteis por 0.8 segundos
        if (Random.Range(0f, 100f) < 60f)
        {
            animator.speed = 0;
            yield return new WaitForSeconds(0.8f);
            animator.speed = 1;
        }
    }
}
