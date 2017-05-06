using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hotkeys : MonoBehaviour {
    public string nomeCenaJogo = "MainMenu";

    [Space(20)]
    public Image black;
    public Animator fade;

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
            StartCoroutine(fading(nomeCenaJogo));
        }
    }

    IEnumerator fading(string NextScene)
    {
        fade.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(NextScene);
    }
}
