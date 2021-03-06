﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

    public float speed; // Velocidade na qual o personagem anda para os lados
    public float jumpSpeed; // Velocidade na qual o personagem se move para o alto quando pula
    public float maxJumpTime; // Tempo máximo em segundos no qual o personagem pode se deslocar para o alto quando pula
    public float timeBetweenArrowDownPresses; // Intervalo máximo de tempo entre dois toques no botão para descer de plataformas
    public LayerMask environmentLayer; // Layer do ambiente do jogo: chão, paredes, obstáculos, etc.

    public bool imortal;// estado de imortalidade do falamaia
    public float Tempo_imortal;
    private float Tempo_imortal_original;

    public bool imortal_backMinigame;
    public float Tempo_imortal_backMinigame;
    private float Tempo_imortal_backMinigame_original;
    public GameObject aureola;
    public float PFES_SpeedMultiplier; // Multiplicador de velocidade que é posto em uso quando o jogador está com a auréola

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
    public AudioClip SobeCara;
    private float lastjump; //instante do ultimo pulo

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float currentJumpTime; // Tempo atual no qual o personagem está se deslocando para o alto enquanto pula
    public bool isJumping; // True se o personagem está se deslocando para o alto porque pulou
    private float horizontalInput;
    private float verticalInput = 0, negativeVerticalInputTime = 0, startLeavingPlatformTime;
    private float jumpInput;

    private bool simulateJump;

    public bool isCollidingWithScreenBorder; // True se o jogador estiver colidindo com a borda esquerda da tela
    private bool isCollidingWithObstacle; // True se o jogador estiver colidindo com oalgum obstáculo

    private bool hasLetGoOfJumpButton;

    private float currentSpeedMultiplier;

    private GameObject CenaPrincipal;

    // Inicializa os atributos privados da classe
    void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentJumpTime = 0;
        isJumping = false;

        lastjump = Time.realtimeSinceStartup;

        Tempo_imortal_original = Tempo_imortal;
        Tempo_imortal_backMinigame_original = Tempo_imortal_backMinigame;
        Tempo_benzido_original = Tempo_benzido;

        isCollidingWithScreenBorder = false;
        isCollidingWithObstacle = false;

        hasLetGoOfJumpButton = true;

        horizontalInput = 0;
        jumpInput = 0;

        simulateJump = false;

        currentSpeedMultiplier = 1;

        CenaPrincipal = GameObject.Find("CenaPrincipal");
    }

    private void Awake()
    {
        // Se estiver ignorando colisões com inimigos
        if (Physics2D.GetIgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies")))
        {
            // Para de ignorar
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);
        }
    }

    private void Update()
    {
        ReceiveInput();
        atualizaTempo();
    }

    void FixedUpdate ()
    {
        if (!SceneController.paused)
        {

            // Despausar movimentação
            if (rb2D.bodyType == RigidbodyType2D.Kinematic)
            {
                rb2D.bodyType = RigidbodyType2D.Dynamic;
            }
            if (animator.enabled == false)
            {
                animator.enabled = true;
            }

            Move();
            Jump();
            Crouch();
            LeavePlatform();
            SimulateJump();
        }

        // Pausar movimentação numa colisão com inimigo
        else if (!SceneController.hasGameFinished)
        {
            rb2D.bodyType = RigidbodyType2D.Kinematic;
            rb2D.velocity = new Vector2(0, 0);
        }

        if (jumpInput == 0)
        {
            hasLetGoOfJumpButton = true;
            isJumping = false;
        }
    }

    private void ReceiveInput()
    {
        // Obtém input do teclado
        horizontalInput = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        jumpInput = CrossPlatformInputManager.GetAxisRaw("Jump");

        // Registra o tempo em que o jogador soltou o botão para baixo
        if (verticalInput < 0 && CrossPlatformInputManager.GetAxisRaw("Vertical") == 0)
        {
            negativeVerticalInputTime = Time.time;
        }

        verticalInput = CrossPlatformInputManager.GetAxisRaw("Vertical");
    }

    // Falamaia corre para os lados
    private void Move()
    {
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
        rb2D.AddForce(movement * speed * currentSpeedMultiplier * Time.fixedDeltaTime);
    }

    // Falamaia pula
    private void Jump()
    {
        bool isGrounded = IsGrounded();

        // Se o jogador pular
        if (jumpInput > 0 && isGrounded && (Time.realtimeSinceStartup - lastjump) > 0.3f && hasLetGoOfJumpButton)
        {
            hasLetGoOfJumpButton = false;

            // Registra que o movimento do pulo deverá começar
            isJumping = true;
            currentJumpTime = 0;

            //testa o tempo para evitar que o som se sobreponha
            if ((Time.realtimeSinceStartup - lastjump) > 0.5f)
            {
                GetComponent<AudioSource>().PlayOneShot(jumpsound);
                lastjump = Time.realtimeSinceStartup;
            }
        }

        // Se o movimento do pulo está acontecendo
        if ((isJumping && jumpInput > 0 && !hasLetGoOfJumpButton) // (com pulo variável)
            || simulateJump)
        {
            // Realiza o movimento
            rb2D.AddForce(new Vector2(0f, jumpSpeed * Time.fixedDeltaTime));
            currentJumpTime += Time.fixedDeltaTime;
        }

        // Se chegou o fim do pulo
        if (currentJumpTime >= maxJumpTime)
        {
            // Determina que o jogador não mais deverá pular
            isJumping = false;
        }

        if (!isGrounded)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }

    // Falamaia se agacha
    private void Crouch()
    {
        if (verticalInput < 0 && Time.time - negativeVerticalInputTime > timeBetweenArrowDownPresses && !animator.GetBool("isCrouching"))
        {
            // Se agacha
            animator.SetBool("isCrouching", true);
        }

        if (verticalInput >= 0 && animator.GetBool("isCrouching"))
        {
            animator.SetBool("isCrouching", false);
        }
    }

    // Falamaia desce da plataforma
    private void LeavePlatform()
    {
        if (verticalInput < 0 && Time.time - negativeVerticalInputTime <= timeBetweenArrowDownPresses)
        {
            // Começa a ignorar colisões entre o jogador e as plataformas
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Environment"), true);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;

            // Registra o tempo em que isso ocorreu
            startLeavingPlatformTime = Time.time;
        }

        // Verifica se acabou o tempo de ignorar colisões
        if (Physics2D.GetIgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Environment")) && Time.time - startLeavingPlatformTime >= 0.2f)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Environment"), false);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    
    // Retorna true se o personagem estiver tocando o chão
    private bool IsGrounded()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        RaycastHit2D hitCenter, hitLeft, hitRight;

        float distToGround = collider.bounds.extents.y;
        float colliderWidth = collider.bounds.extents.x;
        Vector3 originCenter = transform.position + new Vector3(0, -distToGround, 0);
        Vector3 originLeft = transform.position + new Vector3(-colliderWidth, -distToGround, 0);
        Vector3 originRight = transform.position + new Vector3(colliderWidth, -distToGround, 0);
        float distance = 0.2f;

        //Faz três raycasts para saber se o jogador está no chão
        hitCenter = Physics2D.Raycast(originCenter, Vector2.down, distance, environmentLayer);
        hitLeft = Physics2D.Raycast(originLeft, Vector2.down, distance, environmentLayer);
        hitRight = Physics2D.Raycast(originRight, Vector2.down, distance, environmentLayer);

        // Testa se algum dos raycasts acertaram o chão
        if (hitCenter.collider != null || hitLeft.collider != null || hitRight.collider != null)
        {
            return true;
        }

        return false;
    }

    public void viraDeus()
    {
        imortal = true;
        Instantiate(aureola, transform);
        GetComponent<AudioSource>().PlayOneShot(paaai);

        // Põe em uso o multiplicador de velocidade
        currentSpeedMultiplier = PFES_SpeedMultiplier;
        CenaPrincipal.GetComponent<SceneController>().SpeedUpMusic(PFES_SpeedMultiplier);
    }

    public void viraDeus_Backminigame()
    {
        imortal_backMinigame = true;
        StartInvencibilidadeVisualFeedback();
        //completar com animacao
    }

    public void ficaBenzido()
    {
        benzido = true;
        Instantiate(simboloBencao, transform);
    }

    void atualizaTempo()
    {
        // Atualiza o tempo da auréola
        if (Tempo_imortal > 0.0f && imortal == true)
        {
            Tempo_imortal -= Time.deltaTime;
        }
        // Se terminou o tempo da auréola
        else if (imortal == true)
        {
            imortal = false;
            Destroy(GameObject.Find("aureola(Clone)"));
            Tempo_imortal = Tempo_imortal_original;
            currentSpeedMultiplier = 1;
            CenaPrincipal.GetComponent<SceneController>().SpeedDownMusic();
        }

        // Atualiza o tempo da invencibilidade piscante
        if (Tempo_imortal_backMinigame > 0.0f && imortal_backMinigame == true)
        {
            Tempo_imortal_backMinigame -= Time.deltaTime;
        }
        // Se terminou o tempo da invencibilidade piscante
        else if (imortal_backMinigame == true)
        {
            imortal_backMinigame = false;
            Tempo_imortal_backMinigame = Tempo_imortal_backMinigame_original;
        }

        // Atualiza o tempo da água benta
        if (Tempo_benzido > 0.0f && benzido == true)
        {
            Tempo_benzido -= Time.deltaTime;
        }
        //Se terminou o tempo sa água benta
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
        GetComponent<AudioSource>().PlayOneShot(SobeCara,0.5f);
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Registra se o jogador está colidindo com a borda da tela
        if (collision.gameObject.tag == "LeftCollider")
        {
            isCollidingWithScreenBorder = true;
        }
        // Registra se o jogador está colidindo com algum obstáculo
        else if (collision.gameObject.tag == "Obstacle")
        {
            isCollidingWithObstacle = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        // Registra se o jogador parou de colidir com a borda da tela
        if (collision.gameObject.tag == "LeftCollider")
        {
            isCollidingWithScreenBorder = false;
        }
        // Registra se o jogador parou de colidir com algum obstáculo
        else if (collision.gameObject.tag == "Obstacle")
        {
            isCollidingWithObstacle = false;
        }
    }

    private void SimulateJump()
    {
        // Se o jogador ficar preso entre a borda da câmera e um obstáculo
        if (isCollidingWithObstacle
            && isCollidingWithScreenBorder
            //&& IsGrounded()
            && Time.realtimeSinceStartup - lastjump > 0.3f)
        {
            // Simula um pulo
            currentJumpTime = 0;
            simulateJump = true;

            //testa o tempo para evitar que o som se sobreponha
            if ((Time.realtimeSinceStartup - lastjump) > 0.5f)
            {
                GetComponent<AudioSource>().PlayOneShot(jumpsound);
                lastjump = Time.realtimeSinceStartup;
            }
        }
        else
        {
            simulateJump = false;
        }
    }

    private void StartInvencibilidadeVisualFeedback()
    {
        StartCoroutine(InvencibilidadeVisualFeedback());

        // Começa a ignorar colisões entre o jogador e os inimigos
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Invejoso"));
    }

    IEnumerator InvencibilidadeVisualFeedback()
    {
        float tempo = Tempo_imortal_backMinigame_original;
        inicio:
        GetComponent<SpriteRenderer>().enabled = false;
        tempo -= 0.1f;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = true;
        tempo -= 0.1f;
        yield return new WaitForSeconds(0.1f);
        if (tempo > 0.0f)
        {
            goto inicio;
        }
        GetComponent<SpriteRenderer>().enabled = true;
        
        // Para de ignorar colisões entre o jogador e os inimigos
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Invejoso"), false);
    }

    public bool isImortal()
    {
        if (imortal || imortal_backMinigame)
            return true;
        else
            return false;
    }
}