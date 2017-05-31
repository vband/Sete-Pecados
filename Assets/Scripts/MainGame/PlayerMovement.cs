﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed; // Velocidade na qual o personagem anda para os lados
    public float jumpSpeed; // Velocidade na qual o personagem se move para o alto quando pula
    public float maxJumpTime; // Tempo máximo em segundos no qual o personagem pode se deslocar para o alto quando pula
    public LayerMask environmentLayer; // Layer do ambiente do jogo: chão, paredes, obstáculos, etc.

    public bool imortal;// estado de imortalidade do falamaia
    public float Tempo_imortal;
    private float Tempo_imortal_original;
    public GameObject aureola;

    public ParticleSystem carinhaSubindo;
    public static int pessoasSalvas = 0;

    //estado de bencao do falamaia
    public bool benzido;
    public float Tempo_benzido;
    private float Tempo_benzido_original;
    public GameObject simboloBencao;
    [HideInInspector] public bool inverte;
    

    public AudioClip jumpsound; //Som de pulo
    public AudioClip paaai; //som da imortalidade
    private float lastjump; //instante do ultimo pulo

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float currentJumpTime; // Tempo atual no qual o personagem está se deslocando para o alto enquanto pula
    public bool isJumping; // True se o personagem está se deslocando para o alto porque pulou
    private float horizontalInput;
    private float jumpInput;

    //private float spriteYExtent; // Distância entre o centro e a base da sprite do jogador
    //private float spriteXExtent; // Distância entre o centro e as laterais da sprite do jogador
    //private Vector3 spriteBottomCenter; // Centro da base da sprite
    //private Vector3 spriteBottomLeft; // Canto inferior esquerdo da base da sprite
    //private Vector3 spriteBottomRight; // Canto inferior direito da base da sprite



    // Inicializa os atributos privados da classe
    void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentJumpTime = 0;
        isJumping = false;

        //spriteYExtent = spriteRenderer.sprite.bounds.size.y + 0.2f;
        //spriteXExtent = spriteRenderer.sprite.bounds.size.x /*+ 0.2f*/;

        lastjump = Time.realtimeSinceStartup;

        Tempo_imortal_original = Tempo_imortal;
        Tempo_benzido_original = Tempo_benzido;
    }
	
	void FixedUpdate ()
    {
        //Debug.DrawRay(spriteBottomCenter, Vector3.down, Color.green);

        if (!SceneController.paused)
        {
            Move();
            //Jump();
            JumpNew();
            atualizaTempo();
        }
    }

    private void Move()
    {
        // Obtém input do teclado
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Se o jogador se mover para a direita
        if (horizontalInput > 0)
        {
            // Ajusta a orientação correta da sprite do personagem
            spriteRenderer.flipX = false;
            // Diz para o Animator ativar a animação do personagem correndo
            animator.SetBool("isRunning", true);
            inverte = false; //bool que controla inversao do powerup da aguabenta
        }
        // Se o jogador se mover para a esquerda
        else if (horizontalInput < 0)
        {
            // Ajusta a orientação correta da sprite do personagem
            spriteRenderer.flipX = true;
            // Diz para o Animator ativar a animação do personagem correndo
            animator.SetBool("isRunning", true);
            inverte = true; //bool que controla inversao do powerup da aguabenta
        }
        // Se o jogador não quiser andar
        else
        {
            // Diz para o Animator não ativar a animação do personagem correndo
            animator.SetBool("isRunning", false);
        }

        // Move o personagem
        Vector2 movement = new Vector2(horizontalInput, 0f);
        rb2D.AddForce(movement * speed);
    }

    private void JumpNew()
    {
        // Se o jogador pular
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            // Registra que o movimento do pulo deverá começar
            isJumping = true;
            currentJumpTime = 0;
        }

        // Se o movimento do pulo está acontecendo
        if (isJumping)
        {
            // Realiza o movimento
            rb2D.AddForce(new Vector2(0f, jumpSpeed));
            currentJumpTime += Time.fixedDeltaTime;
        }

        // Se chegou o fim do pulo
        if (currentJumpTime >= maxJumpTime)
        {
            // Determina que o jogador não mais deverá pular
            isJumping = false;
        }
    }

    private void Jump()
    {
        // Obtém input do teclado
        jumpInput = Input.GetAxisRaw("Jump");

        Vector2 movement = new Vector2(0f, 0f);

        // Se o jogador pular
        if (jumpInput > 0)
        {
            // Se o personagem estiver tocando o chão
            if (IsGrounded())
            {
                
                // Inicia o temporizador do pulo
                currentJumpTime = 0;
                // Registra que o personagem está pulando
                isJumping = true;
                // Move o personagem para cima
                movement = new Vector2(0f, jumpSpeed);
                rb2D.AddForce(movement);

                //testa o tempo para evitar que o som se sobreponha
                if ((Time.realtimeSinceStartup - lastjump) > 0.5f)
                {
                    GetComponent<AudioSource>().PlayOneShot(jumpsound);
                    lastjump = Time.realtimeSinceStartup;
                }
                
            }
            // Se a força do pulo ainda não tiver acabado
            else if (isJumping && currentJumpTime < maxJumpTime)
            {
                // Continua a contar o tempo do pulo
                currentJumpTime += Time.fixedDeltaTime;
                // Move o personagem para cima
                movement = new Vector2(0f, jumpSpeed);
                rb2D.AddForce(movement);
            }
            // Se a força do pulo tiver acabado
            else if (isJumping && currentJumpTime >= maxJumpTime)
            {
                // Registra que o personagem não está mais pulando. Agora, ele estará caindo
                isJumping = false;
            }
        }
        // Se o jogador não estiver pulando
        else
        {
            // Zera o temporizador do pulo
            currentJumpTime = 0;
            // Registra que o personagem não está pulando
            isJumping = false;
        }
    }
    
    // Retorna true se o personagem estiver tocando o chão
    private bool IsGrounded()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        //spriteBottomCenter = transform.position + new Vector3(0f, -spriteYExtent, 0f);
        //spriteBottomLeft = transform.position + new Vector3(-spriteXExtent + 0.2f, -spriteYExtent, 0f);
        //spriteBottomRight = transform.position + new Vector3(spriteXExtent - 0.2f, -spriteYExtent, 0f);

        RaycastHit2D hitCenter, hitLeft, hitRight;

        float distToGround = collider.bounds.extents.y;
        float colliderWidth = collider.bounds.extents.x;
        Vector3 originCenter = transform.position + new Vector3(0, -distToGround, 0);
        Vector3 originLeft = transform.position + new Vector3(-colliderWidth, -distToGround, 0);
        Vector3 originRight = transform.position + new Vector3(colliderWidth, -distToGround, 0);
        float distance = 0.2f;
        //collider.enabled = false;
        //Faz três raycasts para saber se o jogador está no chão
        hitCenter = Physics2D.Raycast(originCenter, Vector2.down, distance, environmentLayer);
        hitLeft = Physics2D.Raycast(originLeft, Vector2.down, distance, environmentLayer);
        hitRight = Physics2D.Raycast(originRight, Vector2.down, distance, environmentLayer);
        //collider.enabled = true;

        // Testa se algum dos raycasts acertaram o chão
        if (hitCenter.collider != null || hitLeft.collider != null || hitRight.collider != null)
        {
            //Debug.Log(hitCenter.collider.gameObject.name);
            return true;
        }

        return false;
    }

    public void viraDeus()
    {
        imortal = true;
        Instantiate(aureola, transform);
        GetComponent<AudioSource>().PlayOneShot(paaai);
        //print("Sou Deus!!!");

    }

    public void ficaBenzido()
    {
        benzido = true;
        Instantiate(simboloBencao, transform);
        //print("Estou Benzido!!!");
    }

    void atualizaTempo()
    {
        if (Tempo_imortal > 0.0f && imortal == true)
        {
            Tempo_imortal -= Time.deltaTime;
        }
        else if (imortal == true)
        {
            imortal = false;
            Destroy(GameObject.Find("aureola(Clone)"));
            //print("Virei humano :-(");
            Tempo_imortal = Tempo_imortal_original;
        }

        if (Tempo_benzido > 0.0f && benzido == true)
        {
            Tempo_benzido -= Time.deltaTime;
        }
        else if (benzido == true)
        {
            benzido = false;
            Destroy(GameObject.FindGameObjectWithTag("aguabentapowerup"));
            Tempo_benzido = Tempo_benzido_original;
        }

    }

    public void sobeCarinha()
    {
        carinhaSubindo.Play();
        pessoasSalvas++;
    }

    public static int getPessoasSalvas()
    {
        return pessoasSalvas;
    }

    public void StartDelaySobeCarinha()
    {
        //atraso padrao de 3 segundos
        StartCoroutine(DelaySobeCarinha(3));
    }

    IEnumerator DelaySobeCarinha(float delay)
    {
        yield return new WaitForSeconds(delay);
        sobeCarinha();
    }
}