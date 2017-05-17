using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeController : MonoBehaviour {

    public Image black;
    public Animator fade;

    //metodo padrao para chamar fade e alteracao de cenas a partir de qualquer gameobject
    //GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");

    public void CallFading(string NextScene)
    {
        StartCoroutine(fading(NextScene));
    }

    IEnumerator fading(string NextScene)
    {
        fade.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(NextScene);
    }
}
