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

        if (Input.GetKey(KeyCode.P))
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Preguiça");
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("MainMenu");
        }

        //BotaoPausa
        if (Input.GetKeyUp(KeyCode.F1))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                AudioListener.pause = false;
            }
            else
            {
                Time.timeScale = 0;
                AudioListener.pause = true;
            }
            
        }
        
        //DEBUG
        if (Input.GetKeyUp(KeyCode.V))
        {
            enviroment.GetComponent<LivesController>().RemVidas();
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            enviroment.GetComponent<LivesController>().addVidas();
        }
    }
    private GameObject[] FindGameObjectsWithLayer(int layer)
    {
        GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        List<GameObject> goList = new List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        return goList.ToArray();
    }
}
