using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLuxuriaController : MonoBehaviour {

    [SerializeField] private GameObject Arremecavel;

    private Animator animator;
    private bool shouldThrow = true;

	// Use this for initialization
	void Start () {
        //StartCoroutine(Arremassador());
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        // Pausa a animação quando o jogo estiver pausado
        if (SceneController.paused)
        {
            animator.speed = 0;
        }
        else if (!SceneController.paused && shouldThrow && animator.speed == 0)
        {
            animator.speed = 1;
        }
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
        Instantiate(Arremecavel, transform.position + Vector3.left, Quaternion.identity, transform.parent);
    }

    private IEnumerator StopRandomly()
    {
        // Tem uma chance de 60% de pausar os arremessos de projéteis por 0.8 segundos
        if (Random.Range(0f, 100f) < 60f)
        {
            shouldThrow = false;
            animator.speed = 0;
            yield return new WaitForSeconds(0.8f);
            animator.speed = 1;
            shouldThrow = true;
        }
    }
}
