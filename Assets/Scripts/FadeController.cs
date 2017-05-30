using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeController : MonoBehaviour {

    public Image black;
    public Animator fade;
    //resolvendo problema dos listeners
    public GameObject MainCamera;
    //proibir pause na transicao de cena
    [HideInInspector] public bool EmTransicao = false;

    //metodo padrao para chamar fade e alteracao de cenas a partir de qualquer gameobject
    //GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
    
    public void CallFading(string NextScene)
    {
        StartCoroutine(fadingDestroy(NextScene));
        
    }

    IEnumerator fadingDestroy(string NextScene)
    {
        EmTransicao = true;
        SceneController.paused = true;
        fade.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
               
        SceneManager.LoadScene(NextScene, LoadSceneMode.Single);

        if (NextScene == "MainMenu" || NextScene == "GameOver")
        {
            SceneController.created = false;
            SceneController.paused = false;
            Destroy( GameObject.Find("CenaPrincipal") );
        }
        
        fade.SetBool("Fade", false);
        
        yield return new WaitForSeconds(1.5f);
        
        if (NextScene == "Main") {
            SceneController.paused = false;
        }
        EmTransicao = false;
    }
}
