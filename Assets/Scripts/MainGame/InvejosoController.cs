using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvejosoController : MonoBehaviour
{
    public CarroLuxuosoController carro;
    public float jumpForce;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;

    private bool hasJumped = false;
    private bool hasGrippedCar = false;

	void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.constraints = RigidbodyConstraints2D.FreezePosition;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Invejoso"), LayerMask.NameToLayer("Enemies"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Invejoso"), LayerMask.NameToLayer("Environment"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Invejoso"), LayerMask.NameToLayer("LeftCollider"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Invejoso"), LayerMask.NameToLayer("PowerUps"));
    }

    void Update ()
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
                rb2D.bodyType = RigidbodyType2D.Kinematic;
                transform.parent = carro.transform;
                hasGrippedCar = true;
            }
        }
	}

    private void Jump()
    {
        rb2D.constraints = RigidbodyConstraints2D.None;
        rb2D.AddForce(new Vector2(0, jumpForce));
        carro.OnInvejosoVisible();
    }

    /*
    // Chamada quando o carro manda o invejoso se ativar
    public void OnActivate()
    {
        hasJumped = true;

        // Pula para agarrar o carro luxuoso
        Jump();
    }
    */
}
