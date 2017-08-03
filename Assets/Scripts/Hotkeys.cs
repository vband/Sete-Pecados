using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hotkeys : MonoBehaviour {

    void Update() {

        if (Input.GetKeyUp(KeyCode.M))
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Ganancia");
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Ira");
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Preguiça");
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Inveja");
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Gula");
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Soberba"); //Orgulho
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Luxuria");
        }

        //BotaoPausa
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            //se tiver em transicao, nao pausa o jogo.
            //se tiver em minigame nao pausa o jogo

            if (GameObject.Find("FadeImage").GetComponent<FadeController>().EmTransicao == true ||
                !SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("Main"))) { goto fim; }

            if (Time.timeScale == 0)//se já estiver pausado
            {
                GameObject.Find("PauseMenu").GetComponent<PauseMenuController>().AtivaMenu(false);
            }
        
        else //nao está pausado
        {
            
            GameObject.Find("PauseMenu").GetComponent<PauseMenuController>().AtivaMenu(true);
        }
            fim:
            ;//nop operation
        }
    }
}
