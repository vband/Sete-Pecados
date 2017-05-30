using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPreguiçaController : MonoBehaviour
{
    // Essa função é chamada quando o inimigo colidir
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se o inimigo colidir com o jogador
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = GameObject.Find("Player");
            // Se o jogador estava invulnerável
            if (player.GetComponent<PlayerMovement>().imortal)
            {
                player.GetComponent<PlayerMovement>().sobeCarinha();
                goto Destruir;
            }
                        
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Preguiça");

            Destruir:
                Destroy(gameObject);
        }
    }
}
