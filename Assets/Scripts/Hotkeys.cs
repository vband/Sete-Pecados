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

<<<<<<< HEAD
=======
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


    IEnumerator fading(string NextScene)
    {
        fade.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(NextScene);
>>>>>>> f89e37802e0bca52c167967b3288509099d0351d
    }

    
}
