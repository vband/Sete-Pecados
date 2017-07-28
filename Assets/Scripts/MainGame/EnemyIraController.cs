using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyIraController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float jumpMaxTime;
    public float jumpCooldown;
    public float chaseDistance;
    public float idleDistance;
    public LayerMask environmentLayer;

    private Transform player;
    private Rigidbody2D rb2D;
    private SpriteRenderer sprite;
    private Animator animator;
    private float currentJumpCooldown;
    private float jumpTimer;
    private float originPosition;
    private int currentState;
    private float distanceToPlayer;
    private bool isIdlingRight, isIdlingLeft;
    private bool isJumping;

    private const int CHASE = 1;
    private const int IDLE = 2;
    private const int RETURN = 3;

    void Start ()
    {
        player = GameObject.Find("Player").transform;
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentJumpCooldown = 0;
        originPosition = transform.position.x;
        currentState = IDLE;
        isIdlingRight = true;
        isIdlingLeft = false;
        isJumping = false;
    }
	
	void FixedUpdate ()
    {
        //Debug.Log(currentJumpCooldown);
        if (Time.timeScale != 0 && !SceneController.paused) //evita que atualize posicao enquanto jogo estiver pausado
        {
            // Diminui o cooldown de pulo
            if (currentJumpCooldown > 0)
            {
                currentJumpCooldown -= Time.fixedDeltaTime;
            }

            // Realiza o movimento do pulo, caso esteja pulando
            if (isJumping)
            {
                KeepJumping();
            }

            // Atualiza o estado do inimigo baseado na sua posição e na posição do jogador
            UpdateState();

            // Realiza a sua ação dependendo do estado atual
            switch(currentState)
            {
                // Se o inimigo estiver perseguindo o jogador...
                case CHASE:
                    // Corre atrás do jogador
                    //MoveToPlayer();

                    // Anda para a esquerda
                    MoveLeft();

                    // Verifica se o player está acima e se existe uma plataforma sobre o inimigo
                    if ((player.position.y > transform.position.y + 2) && IsBelowPlatform())
                    {
                        StartJumping();
                    }
                    break;

                // Se o inimigo estiver parado, sem haver ainda detectado o jogador...
                case IDLE:
                    // Move-se em volta do seu "spawn point", ocioso
                    IdleAround();

                    //MoveLeft();

                    break;
            }

            // Se o inimigo ficar preso em algum obstáculo...
            if (IsStuck())
            {
                // Pula, para tentar soltá-lo
                StartJumping();
            }

            // Atualiza a animação
            if (rb2D.velocity.x != 0)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
    }

    // Atualiza o estado do inimigo baseado na sua posição e na posição do jogador
    private void UpdateState()
    {
        // Calcula a distância entre o inimigo e o jogador
        distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x);

        // Se o jogador estiver dentro das vistas do inimigo...
        if (distanceToPlayer <= chaseDistance)
        {
            // O inimigo perseguirá o jogador
            currentState = CHASE;
        }
        // Senão, o inimigo fica ocioso
        else
        {
            currentState = IDLE;
        }
    }

    // Se desloca na direção do jogador
    private void MoveToPlayer()
    {
        // Verifica se o player está para a direita
        if (player.position.x > transform.position.x)
        {
            // Anda para a direita
            rb2D.AddForce(Vector2.right * speed * Time.fixedDeltaTime);
            sprite.flipX = false;
        }
        // Verifica se o player está para a esquerda
        if (player.position.x < transform.position.x)
        {
            // Anda para a esquerda
            rb2D.AddForce(Vector2.left * speed * Time.fixedDeltaTime);
            sprite.flipX = true;
        }
    }

    // Se desloca para a esquerda, não importando a posição do jogador
    private void MoveLeft()
    {
        rb2D.AddForce(Vector2.left * speed * Time.fixedDeltaTime);
        sprite.flipX = true;
    }

    // Inicia a ação de pular
    private void StartJumping()
    {
        if (/*(currentJumpCooldown <= 0) &&*/ IsGrounded())
        {
            isJumping = true;
            currentJumpCooldown = jumpCooldown;
            jumpTimer = 0;
        }
    }

    // Continua a ação de pular
    private void KeepJumping()
    {
        if (jumpTimer < jumpMaxTime)
        {
            rb2D.AddForce(Vector2.up * jumpForce * Time.fixedDeltaTime);
            jumpTimer += Time.fixedDeltaTime;
        }
        else
        {
            isJumping = false;
        }
    }

    // Move-se em volta do seu "spawn point", ocioso
    private void IdleAround()
    {
        // Atualiza o estado de se mover para a esquerda ou direita
        if (isIdlingRight)
        {
            if (transform.position.x > originPosition + idleDistance)
            {
                isIdlingRight = false;
                isIdlingLeft = true;
            }
        }
        else if (isIdlingLeft)
        {
            if (transform.position.x < originPosition - idleDistance)
            {
                isIdlingRight = true;
                isIdlingLeft = false;
            }
        }

        // Move-se
        if (isIdlingRight)
        {
            // Anda para a direita
            rb2D.AddForce(Vector2.right * speed * Time.fixedDeltaTime);
            sprite.flipX = false;
        }
        else if (isIdlingLeft)
        {
            // Anda para a esquerda
            rb2D.AddForce(Vector2.left * speed * Time.fixedDeltaTime);
            sprite.flipX = true;
        }
    }
    
    // Essa função é chamada quando o inimigo colidir
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se o inimigo colidir com o player...
              
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = collision.gameObject;
            if (player.GetComponent<PlayerMovement>().isImortal())
            {
                player.GetComponent<PlayerMovement>().sobeCarinha();
                goto Destruir;
            }

            player.GetComponent<Animator>().enabled = false;

            GameObject.Find("FadeImage").GetComponent<FadeController>().FadeFromColision("Ira", transform.position);

            

            Destruir:
                Destroy(gameObject);
        }
    }

    // Retorna true se o inimigo estiver tocando o chão
    private bool IsGrounded()
    {
        float distanceToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
        Vector3 origin = transform.position - new Vector3(0, distanceToGround, 0);
        float distance = 0.2f;
        //GetComponent<BoxCollider2D>().enabled = false;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, distance, environmentLayer);
        //GetComponent<BoxCollider2D>().enabled = true;

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    // Retorna true quando existe uma plataforma sobre o inimigo
    private bool IsBelowPlatform()
    {
        //GetComponent<BoxCollider2D>().enabled = false;
        RaycastHit2D hit1 = Physics2D.Raycast((Vector2)transform.position, Vector2.up, 4f, environmentLayer);
        RaycastHit2D hit2 = Physics2D.Raycast((Vector2)transform.position, Vector2.up + Vector2.left, 4f, environmentLayer);
        //GetComponent<BoxCollider2D>().enabled = true;

        if (hit1.collider != null || hit2.collider != null)
        {
            return true;
        }
        return false;
    }

    // Returna true se o inimigo estiver parado
    private bool IsStuck()
    {
        if (GetComponent<Rigidbody2D>().velocity.x == 0)
        {
            return true;
        }
        return false;
    }
}
