using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Essa classe serve para tratar colisões do player com inimigos
public class PlayerColisionController : MonoBehaviour
{
    [SerializeField]
    private FadeController fadeImage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "EnemyIra":
                if (GetComponent<PlayerMovement>().isImortal())
                    goto imortal;

                GetComponent<Animator>().enabled = false;
                fadeImage.FadeFromColision("Ira", collision.transform.position, FadeController.IRA);
                Destroy(collision.gameObject);
                break;

            case "EnemyPreguica":
                if (GetComponent<PlayerMovement>().isImortal())
                    goto imortal;

                GetComponent<Animator>().enabled = false;
                fadeImage.FadeFromColision("Preguiça", collision.transform.position, FadeController.PREGUICA);
                Destroy(collision.gameObject);
                break;

            case "EnemyGanancia":
                if (GetComponent<PlayerMovement>().isImortal())
                    goto imortal;

                GetComponent<Animator>().enabled = false;
                //redireciona para inveja temporarimanete para nao quebrar o jogo
                fadeImage.FadeFromColision("Inveja", collision.transform.position, FadeController.INVEJA);
                //fadeImage.FadeFromColision("Ganancia", collision.transform.position, FadeController.GANANCIA);
                Destroy(collision.gameObject);
                break;

            case "EnemyInveja":
                if (GetComponent<PlayerMovement>().isImortal())
                    goto imortal;

                GetComponent<Animator>().enabled = false;
                fadeImage.FadeFromColision("Inveja", collision.transform.position, FadeController.INVEJA);
                Destroy(collision.gameObject);
                break;
            case "EnemyGula":
                if (GetComponent<PlayerMovement>().isImortal())
                    goto imortal;

                GetComponent<Animator>().enabled = false;
                fadeImage.FadeFromColision("Gula", collision.transform.position, FadeController.GULA);
                Destroy(collision.gameObject);
                break;
            case "EnemyOrgulho":
                if (GetComponent<PlayerMovement>().isImortal())
                    goto imortal;

                GetComponent<Animator>().enabled = false;
                fadeImage.FadeFromColision("Orgulho", collision.transform.position, FadeController.SOBERBA);
                Destroy(collision.gameObject);
                break;
            case "EnemyLuxuria":
                if (GetComponent<PlayerMovement>().isImortal())
                    goto imortal;

                GetComponent<Animator>().enabled = false;
                fadeImage.FadeFromColision("Luxuria", collision.transform.position, FadeController.LUXURIA);
                Destroy(collision.gameObject);
                break;
        }
        return;
        //caso esteja imortal
        imortal:
        GetComponent<PlayerMovement>().sobeCarinha();
        Destroy(collision.gameObject);
        return;
    }
}
