using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanfletoController : MonoBehaviour
{
    

    private Rigidbody2D rb2D;
    private FadeController FadeImage;

    private void Start()
    {
  
        rb2D = GetComponent<Rigidbody2D>();
        FadeImage = GameObject.Find("FadeImage").GetComponent<FadeController>();
    }

    void FixedUpdate()
    {
        if (!SceneController.paused)
        {

            // TESTE - despausar movimentação
            if (rb2D.bodyType == RigidbodyType2D.Kinematic)
            {
                rb2D.bodyType = RigidbodyType2D.Dynamic;
                rb2D.freezeRotation = false;
            }
        }

        // TESTE - pausar movimentação
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

        if (collision.gameObject.tag == "Player")
        {
            GameObject player = collision.gameObject;
            if (player.GetComponent<PlayerMovement>().isImortal() || SceneController.paused)
            {
                //caso o player esteja imortal ou o jogo estiver pausado, destroi o panfleto.
                player.GetComponent<PlayerMovement>().sobeCarinha();
                goto Destruir;
            }

            player.GetComponent<Animator>().enabled = false;

            FadeImage.FadeFromColision("MiniGame_Ganancia", transform.position,FadeController.GANANCIA);
        Destruir:
            Destroy(gameObject);
        }

    }
}
