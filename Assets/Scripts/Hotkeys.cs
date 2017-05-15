using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hotkeys : MonoBehaviour {
   
    public Image black;
    public Animator fade;

    // Use this for initialization
    void Start () {
    
    }
	
	// Update is called once per frame
	void Update () {
      
        if (Input.GetKey(KeyCode.G))
        {
            StartCoroutine(fading("MiniGame_Ganancia"));
        }

        if (Input.GetKey(KeyCode.I))
        {
            StartCoroutine(fading("Ira"));
        }

        if (Input.GetKey(KeyCode.M))
        {
            StartCoroutine(fading("Main"));
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            StartCoroutine(fading("MainMenu"));
        }

        if (Input.GetKey(KeyCode.P))
        {
            StartCoroutine(fading("Preguiça"));
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


    IEnumerator fading(string NextScene)
    {
        fade.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(NextScene);
    }
}
