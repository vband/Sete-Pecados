using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverBehaviour : MonoBehaviour
{
    private GameObject cenaPrincipal;

	void Start ()
    {

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
