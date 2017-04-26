﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed; // Velocidade na qual o personagem anda para os lados
    public float jumpSpeed; // Velocidade na qual o personagem se move para o alto quando pula
    public float maxJumpTime; // Tempo máximo em segundos no qual o personagem pode se deslocar para o alto quando pula
    public LayerMask environmentLayer; // Layer do ambiente do jogo: chão, paredes, obstáculos, etc.

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float currentJumpTime; // Tempo atual no qual o personagem está se deslocando para o alto enquanto pula
    private bool isJumping; // True se o personagem está se deslocando para o alto porque pulou
    private float horizontalInput;
    private float jumpInput;

    private float spriteYExtent; // Distância entre o centro e a base da sprite do jogador
    private float spriteXExtent; // Distância entre o centro e as laterais da sprite do jogador
    private Vector3 spriteBottomCenter; // Centro da base da sprite
    private Vector3 spriteBottomLeft; // Canto inferior esquerdo da base da sprite
    private Vector3 spriteBottomRight; // Canto inferior direito da base da sprite

    // Inicializa os atributos privados da classe
    void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentJumpTime = 0;
        isJumping = false;

        spriteYExtent = spriteRenderer.sprite.bounds.extents.y - 0.2f;
        spriteXExtent = spriteRenderer.sprite.bounds.extents.x - 0.2f;

    }
	
	void FixedUpdate ()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        // Obtém input do teclado
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Se o jogador se mover para a direita
        if (horizontalInput > 0)
        {
            // Ajusta a orientação correta da sprite do personagem
            spriteRenderer.flipX = true;
            // Diz para o Animator ativar a animação do personagem correndo
            animator.SetBool("isRunning", true);
        }
        // Se o jogador se mover para a esquerda
        else if (horizontalInput < 0)
        {
            // Ajusta a orientação correta da sprite do personagem
            spriteRenderer.flipX = false;
            // Diz para o Animator ativar a animação do personagem correndo
            animator.SetBool("isRunning", true);
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
        spriteBottomCenter = transform.position + new Vector3(0f, -spriteYExtent, 0f);
        spriteBottomLeft = transform.position + new Vector3(-spriteXExtent, -spriteYExtent, 0f);
        spriteBottomRight = transform.position + new Vector3(spriteXExtent, -spriteYExtent, 0f);

        RaycastHit2D hitCenter, hitLeft, hitRight;

        // Desabilita temporariamente o collider do jogador
        collider.enabled = false;
        //Faz três raycasts para saber se o jogador está no chão
        hitCenter = Physics2D.Raycast(spriteBottomCenter, Vector2.down, Mathf.Epsilon, environmentLayer);
        hitLeft = Physics2D.Raycast(spriteBottomLeft, Vector2.down, Mathf.Epsilon, environmentLayer);
        hitRight = Physics2D.Raycast(spriteBottomRight, Vector2.down, Mathf.Epsilon, environmentLayer);
        collider.enabled = true;

        // Testa se algum dos raycasts acertaram o chão
        if (hitCenter.collider != null || hitLeft.collider != null || hitRight.collider != null)
        {
            return true;
        }

        return false;
    }
}

/*
 * Presets para o pulo do jogador
 * 
 * Pulo rápido e curto:
 *  Gravity Scale 19
 *  Jump Speed 200
 *  Max Jump Time 0.2
 * 
 * 
 * Pulo lento e longo:
 *  Gravity Scale 14
 *  Jump Speed 150
 *  Max Jump Time 0.3
*/