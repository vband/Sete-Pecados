using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanfletoController : MonoBehaviour {
    public float autoDestroyTimer;

    private void Start()
    {
        AutoDestroyStart(autoDestroyTimer);
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
