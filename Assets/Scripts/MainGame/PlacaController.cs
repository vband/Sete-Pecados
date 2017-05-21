using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacaController : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameObject.Find("Player").GetComponent<PlayerMovement>().imortal)
            {
                //print("MATOU ENQUANTO DIVINO!");
                goto Destruir;
            }
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("MiniGame_Ganancia");
        Destruir:
            Destroy(gameObject);
        }
    }
}
