﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvejosoController : MonoBehaviour
{
    public CarroLuxuosoController carro;
    public float jumpForce;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;
    private Animator animator;

    private bool hasJumped = false;
    private bool hasGrippedCar = false;
    //private Vector2 tempSpeed;

	void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb2D.constraints = RigidbodyConstraints2D.FreezePosition;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Invejoso"), LayerMask.NameToLayer("Enemies"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Invejoso"), LayerMask.NameToLayer("Environment"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Invejoso"), LayerMask.NameToLayer("LeftCollider"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Invejoso"), LayerMask.NameToLayer("PowerUps"));
    }

    private void Update()
    {
        // Pausa a movimentação quando o jogo estiver pausado
        if (SceneController.paused && rb2D.constraints == RigidbodyConstraints2D.None)
        {
            //tempSpeed = rb2D.velocity;
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if (!SceneController.paused && rb2D.constraints == RigidbodyConstraints2D.FreezeAll && hasJumped && !hasGrippedCar)
        {
            //rb2D.velocity = tempSpeed;
            rb2D.constraints = RigidbodyConstraints2D.None;
        }
    }

    private void FixedUpdate ()
    {
        
        // Quando o invejoso aparecer na tela...
		if (!hasJumped && spriteRenderer.isVisible)
        {
            hasJumped = true;

            // Pula para agarrar o carro luxuoso
            Jump();
        }
        

        // Após o pulo...
        if (hasJumped && !hasGrippedCar)
        {
            // Se agarra ao carro
            if (Vector2.Distance(transform.position, carro.transform.position) <= 1f)
            {
                animator.SetTrigger("Gripped");
                rb2D.bodyType = RigidbodyType2D.Kinematic;
                transform.parent = carro.transform;
                hasGrippedCar = true;
            }
        }

        FreezeRotation();
	}

    private void Jump()
    {
        animator.SetTrigger("Jumped");
        rb2D.constraints = RigidbodyConstraints2D.None;
        rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb2D.AddForce(new Vector2(0, jumpForce));
        carro.OnInvejosoVisible();
    }

    private void FreezeRotation()
    {
        if (rb2D.constraints != RigidbodyConstraints2D.FreezeRotation)
        {
            rb2D.constraints = rb2D.constraints | RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
