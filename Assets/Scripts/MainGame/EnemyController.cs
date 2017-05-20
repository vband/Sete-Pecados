using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float speed;

    private Rigidbody2D rb2D;
    private SpriteRenderer sprite;

    void Start ()
    {
        player = GameObject.Find("Player").transform;
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	void Update ()
    {
        if (Time.timeScale != 0 && !SceneController.paused) //evita que atualize posicao enquanto jogo estiver pausado
        {
            // Verifica se o player está para a direita
            if (player.position.x > transform.position.x)
            {
                // Anda para a direita
                rb2D.AddForce(Vector2.right * speed);
                sprite.flipX = false;
            }
            // Verifica se o player está para a esquerda
            else if (player.position.x < transform.position.x)
            {
                // Anda para a esquerda
                rb2D.AddForce(Vector2.left * speed);
                sprite.flipX = true;
            }
        }
    }

    // Essa função é chamada quando o inimigo colidir
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se o inimigo colidir com o player...
              
        if (collision.gameObject.tag == "Player")
        {
            if (GameObject.Find("Player").GetComponent<PlayerMovement>().imortal)
            {
                //print("MATOU ENQUANTO DIVINO!");
                goto Destruir;
            }

            GameObject.Find("Vidas").GetComponent<LivesController>().RemVidas();
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Ira");
        Destruir:
            Destroy(gameObject);
        }
    }
    
}
