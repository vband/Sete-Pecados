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
    //para garantir que o fade aconteca e a transicao nao quebre, associe a qualquer objeto fixo da cena
    //o script ativarFade.cs pra garantir que o objeto do fade esteja ativado quando acontecer a chamada.
    //nao esqueca de associar o fadeimage da cena no script ativarfade

    public void CallFading(string NextScene)
    {
        StartCoroutine(fadingDestroy(NextScene));
    }

    public void CallFadingAdditive(string NextScene)
    {
        StartCoroutine(fadingAdditive(NextScene));
    }

    IEnumerator fadingAdditive(string NextScene)
    {
        fade.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        GameObject.Find("FadeImage").SetActive(false);
        SceneManager.LoadScene(NextScene, LoadSceneMode.Additive);
    }

    IEnumerator fadingDestroy(string NextScene)
    {
        fade.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        GameObject.Find("FadeImage").SetActive(false);
        SceneManager.LoadScene(NextScene, LoadSceneMode.Single);
    }
}
