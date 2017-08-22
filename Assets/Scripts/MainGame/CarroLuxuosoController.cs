using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarroLuxuosoController : MonoBehaviour
{
    public float speed;
    public InvejosoController invejoso;

    private PointEffector2D pointEffector;
    private SpriteRenderer spriteRenderer;
    //private bool hasActivated = false;

	void Start ()
    {
        pointEffector = GetComponent<PointEffector2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        pointEffector.enabled = false;
	}
	
	void FixedUpdate ()
    {
        if (!SceneController.paused || SceneController.hasGameFinished)
        {
            if (spriteRenderer.isVisible)
            {
                Move();
            }
        }

        DestroyNearbyCars();
        //ActivateInvejoso();
    }

    private void Move()
    {
        GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + Vector2.left * speed * Time.fixedDeltaTime);
    }

    private void DestroyNearbyCars()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, 15f);
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, 15f);

        if (leftHit.collider != null && leftHit.collider.gameObject.tag == "car")
        {
            Destroy(leftHit.collider.gameObject);
        }

        if (rightHit.collider != null && rightHit.collider.gameObject.tag == "car")
        {
            Destroy(rightHit.collider.gameObject);
        }
    }

    /*
    // Ativa o invejoso quando o carro atinge metade da tela
    private void ActivateInvejoso()
    {
        if (!hasActivated && transform.position.x <= Camera.main.transform.position.x)
        {
            hasActivated = true;
            invejoso.OnActivate();
            pointEffector.enabled = true;
        }
    }
    */
    
    public void OnInvejosoVisible()
    {
        pointEffector.enabled = true;
    }
    
}
