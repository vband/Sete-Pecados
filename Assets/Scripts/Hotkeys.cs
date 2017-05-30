using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hotkeys : MonoBehaviour {
   
    

    //debug
    public GameObject enviroment;

    // Use this for initialization
    void Start () {
    
    }
	
	// Update is called once per frame
	void Update () {
      
        if (Input.GetKeyUp(KeyCode.G))
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading( "MiniGame_Ganancia");
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Ira");    
        }

        if (Input.GetKeyUp(KeyCode.M))
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Preguiça");
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().sobeCarinha();
        }

        //BotaoPausa
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            //se tiver em transicao, nao pausa o jogo.
            if(GameObject.Find("FadeImage").GetComponent<FadeController>().EmTransicao == true) { goto fim; }
            if (Time.timeScale == 0)//se já estiver pausado
            {
                Time.timeScale = 1;
                AudioListener.pause = false;
                GameObject.Find("PauseMenu").GetComponent<PauseMenuController>().AtivaMenu(false);
            }
            else //nao está pausado
            {
                Time.timeScale = 0;
                AudioListener.pause = true;
                GameObject.Find("PauseMenu").GetComponent<PauseMenuController>().AtivaMenu(true);
            }
            fim:
            ;//nop operation
        }
        
        //DEBUG
        if (Input.GetKeyUp(KeyCode.V))
        {
            LivesController.RemVidas();
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            LivesController.addVidas();
        }
    }
}
