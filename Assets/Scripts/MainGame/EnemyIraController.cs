using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyIraController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float jumpCooldown;

    private Transform player;
    private Rigidbody2D rb2D;
    private SpriteRenderer sprite;
    private float currentCooldown;

    void Start ()
    {
        player = GameObject.Find("Player").transform;
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        currentCooldown = 0;
    }
	
	void Update ()
    {
        if (Time.timeScale != 0 && !SceneController.paused) //evita que atualize posicao enquanto jogo estiver pausado
        {
            Move();

            // Verifica se o player está acima e se existe umaplataforma dobre o inimigo
            if ((player.position.y > transform.position.y + 2) && IsBelowPlatform())
            {
                Jump();
            }

            // Diminui o cooldown de pulo
            if (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;
            }
        }
    }

    private void Move()
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

    private void Jump()
    {
        if ((currentCooldown <= 0) && IsGrounded())
        {
            rb2D.AddForce(Vector2.up * jumpForce);
            currentCooldown = jumpCooldown;
        }
    }

    private bool IsBelowPlatform()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, Vector2.up, 2f);
        GetComponent<BoxCollider2D>().enabled = true;

        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            return true;
        }
        return false;
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

    private bool IsGrounded()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, Vector2.down, 0.5f);
        GetComponent<BoxCollider2D>().enabled = true;

        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            return true;
        }
        return false;
    }

}
