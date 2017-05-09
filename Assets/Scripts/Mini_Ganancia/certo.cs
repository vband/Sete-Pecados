using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class certo : MonoBehaviour {
    private string maingame;


    [Space(20)]
    public Image black;
    public Animator fade;

    // Use this for initialization
    void Start () {
        maingame = "Main";

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Certo()
    {
        print("acertou");
        StartCoroutine(fading(maingame));
    }

    IEnumerator fading(string NextScene)
    {
        fade.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(NextScene);
    }


}
