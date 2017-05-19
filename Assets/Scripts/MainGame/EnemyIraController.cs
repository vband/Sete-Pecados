using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyIraController : MonoBehaviour
{
    public Transform player;
    public float speed;
    public float jumpForce;
    public float jumpCooldown;

    private Rigidbody2D rb2D;
    private SpriteRenderer sprite;
    private float currentCooldown;
    private bool isgrounded;

    void Start ()
    {
        player = GameObject.Find("Player").transform;
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        currentCooldown = 0;
        isgrounded = false;
    }
	
	void Update ()
    {
        Move();

        // Verifica se o player está acima
        if (player.position.y > transform.position.y + 2)
        {
            Jump();
        }

        // Diminui o cooldown de pulo
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
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
        if ((currentCooldown <= 0) && isgrounded)
        {
            rb2D.AddForce(Vector2.up * jumpForce);
            currentCooldown = jumpCooldown;
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

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            isgrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isgrounded = false;
    }

}
