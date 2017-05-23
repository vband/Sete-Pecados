using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacaController : MonoBehaviour {

    public float AutoDestructionTime;

    private void Start()
    {
        AutoDestroyStart(AutoDestructionTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameObject.Find("Player").GetComponent<PlayerMovement>().imortal || SceneController.paused)
            {
                //caso o player esteja imortal ou o jogo estiver pausado, destroi o panfleto.
                goto Destruir;
            }
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("MiniGame_Ganancia");
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
