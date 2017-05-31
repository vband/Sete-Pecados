using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgrejaBehaviour : MonoBehaviour
{
    public ParticleSystem fogos;
    public SceneController cenaPrincipal;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    // Caso o jogador entre na igrejaa...
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Começa a soltar fogos
            fogos.Play();
            // Chama a cena de gameover (futuramente, será trocada por uma cena mais adequada)
            //GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("GameOver");
            cenaPrincipal.WinGame();
        }
    }
}
