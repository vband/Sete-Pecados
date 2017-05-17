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
      
        if (Input.GetKey(KeyCode.G))
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("MiniGame_Ganancia");
        }

        if (Input.GetKey(KeyCode.I))
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Ira");    
        }

        if (Input.GetKey(KeyCode.M))
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("MainMenu");
        }

        //DEBUG
        if (Input.GetKey(KeyCode.V))
        {
            enviroment.GetComponent<LivesController>().RemVidas();
        }
        if (Input.GetKey(KeyCode.B))
        {
            enviroment.GetComponent<LivesController>().addVidas();
        }

    }

    
}
