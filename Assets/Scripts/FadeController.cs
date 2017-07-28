using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class FadeController : MonoBehaviour {

    //proibir pause na transicao de cena
    [HideInInspector] public bool EmTransicao = false;
    //[HideInInspector] 
    private bool enemyColision = false;
    private GameObject Player;
    private GameObject cam;
    public GameObject circleImage;

    private void Start()
    {
        Player = GameObject.Find("Player");
        cam = GameObject.Find("Main Camera");
    }

    //metodo padrao para chamar fade e alteracao de cenas a partir de qualquer gameobject
    //GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");

    public void FadeFromColision(string NextScene, Vector3 ColisionLocal)
    {
        setCircleFadePosition(ColisionLocal);
        CallFading(NextScene);
    }

    private void setCircleFadePosition(Vector3 ColisionLocal)
    {
        enemyColision = true;        
        circleImage.GetComponent<RectTransform>().position = cam.GetComponent<Camera>().WorldToScreenPoint(ColisionLocal);
    }
   
    public void CallFading(string NextScene)
    {
        StartCoroutine(fadingDestroy(NextScene));
    }

    IEnumerator fadingDestroy(string NextScene)
    {
        EmTransicao = true;
        SceneController.paused = true;

        if (enemyColision)
        {
            GetComponent<Animator>().SetBool("AtivaFade", true);
            yield return new WaitUntil(() => circleImage.GetComponent<RectTransform>().localScale.x == 1900);
            //yield return new WaitForSeconds(1.0f);

            SceneManager.LoadScene(NextScene, LoadSceneMode.Single);

            if (NextScene == "MainMenu" || NextScene == "GameOver")
            {
                SceneController.created = false;
                SceneController.paused = false;
                Destroy(GameObject.Find("CenaPrincipal"));
                Joystick.hasCreatedAxes = false;
            }

            GetComponent<Animator>().SetBool("AtivaFade", false);
            enemyColision = false;
        }
        else
        {
           
            GetComponent<Animator>().SetBool("Fade", true);
            yield return new WaitUntil(() => GetComponent<Image>().color.a == 1);

            SceneManager.LoadScene(NextScene, LoadSceneMode.Single);

            if (NextScene == "MainMenu" || NextScene == "GameOver")
            {
                SceneController.created = false;
                SceneController.paused = false;
                Destroy(GameObject.Find("CenaPrincipal"));
                Joystick.hasCreatedAxes = false;
            }

            GetComponent<Animator>().SetBool("Fade", false);
        }
        yield return new WaitForSeconds(1.5f);

        if (NextScene == "Main")
        {
            SceneController.paused = false;
            Player.GetComponent<PlayerMovement>().viraDeus_Backminigame();
        }
        EmTransicao = false;
       
    }
    
}
