using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanfletoController : MonoBehaviour
{
    

    private Rigidbody2D rb2D;

    private void Start()
    {
  
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!SceneController.paused)
        {

            // Despausar movimentação
            if (rb2D.bodyType == RigidbodyType2D.Kinematic)
            {
                rb2D.bodyType = RigidbodyType2D.Dynamic;
                rb2D.freezeRotation = false;
            }
        }

        // Pausar movimentação
        else
        {
            rb2D.bodyType = RigidbodyType2D.Kinematic;
            rb2D.velocity = new Vector2(0, 0);
            rb2D.freezeRotation = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //desativa queda suave ao encostar no chao
        if(transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

    }
}
