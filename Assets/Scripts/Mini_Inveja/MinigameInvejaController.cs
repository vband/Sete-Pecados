using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameInvejaController: MonoBehaviour {

    public GameObject Cam;
    public GameObject Target;
    public GameObject Certo;
    public GameObject Errado;
    public Text perfect;
    public Text ganhou;
    public List<GameObject> Rostos;

    //dificuldade
    private static int difficulty;
    private int tot_errado;
    private int tot_certo;

    private float zPosition = 0f;
    private float tempo = 3.5f;

    private List<GameObject> listaCerto = new List<GameObject>();
    private List<GameObject> listaErrado = new List<GameObject>();

    private List<GameObject> listaDeBotoesInstanciados = new List<GameObject>();

    [HideInInspector] public Vector2 screenSize;
    [HideInInspector] public bool syncBool = false; //variavel que controla o tempo certo para ativar a contagem e a acao do minigame
    [HideInInspector] public GameObject rosto_correto;
    [HideInInspector] public bool faceClick = false;


    private void Awake()
    {
        int RostoCerto = Random.Range(0, Rostos.Count);
        rosto_correto = Rostos[RostoCerto];
        Rostos.RemoveAt(RostoCerto);

        //obtendo tamanho da tela para gerar objetos dentro da area visivel
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
    }

    void Start()
    {

        configuraDificuldade(difficulty);

        //carregando quantidade de botoes nas listas corretas
        for (int i = 0; i < tot_certo; i++)
        {
            GameObject instance = Instantiate(rosto_correto, new Vector3(Random.Range(Cam.transform.position.x - (screenSize.x - 2), Cam.transform.position.x + (screenSize.x - 2)),
                                        Random.Range(Cam.transform.position.y - (screenSize.y - 2), Cam.transform.position.y + (screenSize.y - 2)),
                                        zPosition), Quaternion.identity, transform);
            

            listaCerto.Add(instance);
        }
        for (int i = 0; i < tot_errado; i++)
        {
            GameObject errado = Rostos[ Random.Range(0, Rostos.Count) ];
            GameObject instance = Instantiate(errado, new Vector3(Random.Range(Cam.transform.position.x - (screenSize.x - 2), Cam.transform.position.x + (screenSize.x - 2)),
                                        Random.Range(Cam.transform.position.y - (screenSize.y - 2), Cam.transform.position.y + (screenSize.y - 2)),
                                        zPosition), Quaternion.identity, transform);
            listaErrado.Add(instance);
        }

        StartCoroutine(timer(tempo));
    }

    IEnumerator timer(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        //aplicando rostos corretos em cada um dos objetos presentes na cena e definindo uma posicao aleatoria
        foreach (GameObject go in listaCerto)
        {
            
            go.SetActive(true);
            go.GetComponentInChildren<ParDeOlhosController>().SetTarget(Target);
        }

        foreach (GameObject go in listaErrado)
        {
            go.SetActive(true);
        }

        syncBool = true;
    }

    private void configuraDificuldade(int dif)
    {
        GetComponent<CountdownScriptInveja>().TempoContagem = 10 - dif;
        switch (dif)
        {
            case 1:
                tot_certo = 5;
                tot_errado = 30;
                break;
            case 2:
                tot_certo = 4;
                tot_errado = 35;
                break;
            case 3:
                tot_certo = 3;
                tot_errado = 40;
                break;
            case 4:
                tot_certo = 2;
                tot_errado = 45;
                break;
            case 5:
                tot_certo = 1;
                tot_errado = 50;
                break;
            default: //case sem entrada, ativa o mais facil
                tot_certo = 5;
                tot_errado = 30;
                break;
        }
    }

    public void RostoCerto()
    {
        faceClick = true;
        if (GetComponent<CountdownScript>().TempoContagem > 10 - difficulty - 1)
        {
            perfect.gameObject.SetActive(true);
            LivesController.addVidas();
        }
        else
        {
            ganhou.gameObject.SetActive(true);
        }

        foreach (GameObject botao in listaDeBotoesInstanciados)
        {
            botao.GetComponent<Button>().enabled = false;
        }

        GameObject.Find("Player").GetComponent<PlayerMovement>().StartDelaySobeCarinha();
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
    }

    public void RostoErrado()
    {
        faceClick = true;
        GameObject[] certos = GameObject.FindGameObjectsWithTag("Certo");
        foreach (GameObject certo in certos)
        {
            certo.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        LivesController.RemVidas();

        foreach (GameObject botao in listaDeBotoesInstanciados)
        {
            botao.GetComponent<Button>().enabled = false;
        }

        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
    }

    public static void SetDifficulty(int dif)
    {
        difficulty = dif;
    }

    public static int GetDifficulty()
    {
        return difficulty;
    }
}
