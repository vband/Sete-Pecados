using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacaController : MonoBehaviour
{
    public float AutoDestructionTime;

    private Rigidbody2D rb2D;

    private void Start()
    {
        //AutoDestroyStart(AutoDestructionTime);
        rb2D = GetComponent<Rigidbody2D>();
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

            GameObject.Find("FadeImage").GetComponent<FadeController>().FadeFromColision("MiniGame_Ganancia", transform.position, FadeController.GANANCIA);
        Destruir:
            Destroy(gameObject);
        }
    }

    void AutoDestroyStart(float timer)
    {
        StartCoroutine(AutoDestroy(timer));
    }
    IEnumerator AutoDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

}
