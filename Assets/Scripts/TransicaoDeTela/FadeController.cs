using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class FadeController : MonoBehaviour {

    //proibir pause na transicao de cena
    [HideInInspector] public bool EmTransicao = false;
    private bool enemyColision = false;
    private GameObject Player;
    public GameObject cam;
    public GameObject circleImage;
    public static Color IRA, GULA, PREGUICA, GANANCIA, LUXURIA, INVEJA, SOBERBA, PADRAO, BRANCO;
    //vermelho, laranja, azul, amarelo, rosa, verde, purpura

    private void Awake()
    {
        InicializaCores();
    }

    private void Start()
    {
        Player = GameObject.Find("Player");
        //cam = GameObject.Find("Main Camera");
        SetColor(PADRAO);
    }

    //metodo padrao para chamar fade e alteracao de cenas a partir de qualquer gameobject
    //GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");

    public void FadeFromColision(string NextScene, Vector3 ColisionLocal)
    {
        setCircleFadePosition(ColisionLocal);
        CallFading(NextScene);
    }

    public void FadeFromColision(string NextScene, Vector3 ColisionLocal, Color cor)
    {
        setCircleFadePosition(ColisionLocal);
        SetColor(cor);
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

            SceneManager.LoadScene(NextScene, LoadSceneMode.Single);

            if (NextScene == "MainMenu" || NextScene == "GameOver")
            {
                SceneController.created = false;
                SceneController.paused = false;
                Destroy(GameObject.Find("CenaPrincipal"));
                AnalógicoLivre.hasCreatedAxes = false;
                AnalógicoLivre.UnRegisterAxis("Horizontal");
                AnalógicoLivre.UnRegisterAxis("Vertical");
            }

            GetComponent<Animator>().SetBool("AtivaFade", false);
            enemyColision = false;

        }
        else
        {
            if((NextScene == "Main" || NextScene == "MainEndless") && SceneManager.GetActiveScene().name != "MainMenu")
                SetColor(BRANCO);

            if (NextScene == "MainMenu" || NextScene == "GameOver")
                SetColor(PADRAO);
            
            GetComponent<Animator>().SetBool("Fade", true);
            yield return new WaitUntil(() => GetComponent<Image>().color.a == 1);

            SceneManager.LoadScene(NextScene, LoadSceneMode.Single);

            if (NextScene == "MainMenu" || NextScene == "GameOver")
            {
                SceneController.created = false;
                SceneController.paused = false;
                Destroy(GameObject.Find("CenaPrincipal"));
                AnalógicoLivre.hasCreatedAxes = false;
                AnalógicoLivre.UnRegisterAxis("Horizontal");
                AnalógicoLivre.UnRegisterAxis("Vertical");
            }

            GetComponent<Animator>().SetBool("Fade", false);
        }
        yield return new WaitUntil(() => GetComponent<Image>().color.a == 0);
        if (NextScene == "Main")
        {
            SceneController.paused = false;
            Player.GetComponent<PlayerMovement>().viraDeus_Backminigame();
        }
        EmTransicao = false;
       
    }
    
    void SetColor(Color cor)
    {
        GetComponent<Image>().color = cor;
        circleImage.GetComponent<Image>().color = cor;
    }

    private void InicializaCores()
    {
        IRA = new Color(1, 0, 0);
        GULA = new Color(1, 0.749f, 0);
        PREGUICA = new Color(0, 0, 1);
        GANANCIA = new Color(1, 1, 0);
        LUXURIA = new Color(1, 0, 1);
        INVEJA = new Color(0, 1, 0);
        SOBERBA = new Color(0.501f, 0, 0.501f);

        PADRAO = new Color(0, 0, 0);
        BRANCO = new Color(1, 1, 1, 0);
       
    }

    public void LoadedPointer()
    {
        Debug.Log("loaded");
    }
}
