using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPreguiçaController : MonoBehaviour
{
    private FadeController FadeImage;

    private void Start()
    {
        //debug para inimigo da preguiça spawnar deitado
        transform.Rotate(Vector3.forward, 90);
        FadeImage = GameObject.Find("FadeImage").GetComponent<FadeController>();
    }

    // Essa função é chamada quando o inimigo colidir
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se o inimigo colidir com o jogador
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = collision.gameObject;

            // Se o jogador estava invulnerável
            if (player.GetComponent<PlayerMovement>().isImortal())
            {
                player.GetComponent<PlayerMovement>().sobeCarinha();
                goto Destruir;
            }
                        
            player.GetComponent<Animator>().enabled = false;

            FadeImage.FadeFromColision("Preguiça", transform.position, FadeController.PREGUICA);
            

            Destruir:
                Destroy(gameObject);
        }
    }
}
