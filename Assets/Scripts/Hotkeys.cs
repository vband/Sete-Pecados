using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hotkeys : MonoBehaviour {
    public string nomeCenaJogo = "MainMenu";

    // Use this for initialization
    void Start () {
    
    }
	
	// Update is called once per frame
	void Update () {
        verificaEsc();
	}

    void verificaEsc()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene(nomeCenaJogo);
        }
    }
}
