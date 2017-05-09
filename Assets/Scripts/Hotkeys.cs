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

    }

    IEnumerator fading(string NextScene)
    {
        fade.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(NextScene);
    }
}
