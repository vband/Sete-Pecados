using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverBehaviour : MonoBehaviour
{
    public Text scoreText;

    private GameObject cenaPrincipal;

	void Start ()
    {
        scoreText.text = "Você salvou " + PlayerMovement.getPessoasSalvas().ToString() + " almas";
    }
	
	void Update ()
    {
        
	}

    public void LoadMenu()
    {
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("MainMenu");
    }

    public void LoadGame()
    {
        LivesController.InitVidas();
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
    }
}
