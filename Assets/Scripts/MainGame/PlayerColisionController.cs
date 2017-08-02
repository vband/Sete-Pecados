using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Essa classe serve para tratar colisões do player com inimigos
public class PlayerColisionController : MonoBehaviour
{
    public FadeController fadeImage;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "EnemyIra":

                if (GetComponent<PlayerMovement>().isImortal())
                {
                    GetComponent<PlayerMovement>().sobeCarinha();
                    Destroy(collision.gameObject);
                    return;
                }

                GetComponent<Animator>().enabled = false;
                fadeImage.FadeFromColision("Ira", collision.transform.position, FadeController.IRA);
                Destroy(collision.gameObject);
                break;

            case "EnemyPreguica":

                if (GetComponent<PlayerMovement>().isImortal())
                {
                    GetComponent<PlayerMovement>().sobeCarinha();
                    Destroy(collision.gameObject);
                    return;
                }

                GetComponent<Animator>().enabled = false;
                fadeImage.FadeFromColision("Preguiça", collision.transform.position, FadeController.PREGUICA);
                Destroy(collision.gameObject);
                break;

            case "EnemyGanancia":
                if (GetComponent<PlayerMovement>().isImortal() || SceneController.paused)
                {
                    GetComponent<PlayerMovement>().sobeCarinha();
                    Destroy(collision.gameObject);
                    return;
                }

                GetComponent<Animator>().enabled = false;
                fadeImage.FadeFromColision("MiniGame_Ganancia", collision.transform.position, FadeController.GANANCIA);
                Destroy(collision.gameObject);
                break;
        }
    }
}
