using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPreguiçaController : MonoBehaviour
{
	void Start () { }

    void Update () {}

    // Essa função é chamada quando o inimigo colidir
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se o inimigo colidir com o jogador
        if (collision.gameObject.tag == "Player")
        {
            // Se o jogador estava invulnerável
            if (GameObject.Find("Player").GetComponent<PlayerMovement>().imortal)
            {
                goto Destruir;
            }

            //GameObject.Find("Vidas").GetComponent<LivesController>().RemVidas();
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Preguiça");

            Destruir:
                Destroy(gameObject);
        }
    }
}
