using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverBehaviour : MonoBehaviour
{
    public Text scoreText, gameOverText, carregandoText;
    public Button jogarButton, voltarButton;

    private GameObject cenaPrincipal;

    public static bool restart = false;

	void Start ()
    {
        if (restart)
        {
            carregandoText.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(false);
            jogarButton.gameObject.SetActive(false);
            voltarButton.gameObject.SetActive(false);
            LoadGame();
        }
        else
        {
            scoreText.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
            jogarButton.gameObject.SetActive(true);
            voltarButton.gameObject.SetActive(true);
            scoreText.text = "Você salvou " + PlayerMovement.getPessoasSalvas().ToString() + " almas";
        }
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
        if (GameMode.Mode == GameMode.GameModes.Classic)
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
        }
        else if (GameMode.Mode == GameMode.GameModes.Minigame)
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("ModoMinigame_Transição");
        }
    }
}
