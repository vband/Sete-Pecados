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
    private static int difficulty = 3 ;
    private int tot_errado;
    private int tot_certo;
    private int modo;
    const int CORRETO = 0, FRENTE = 1, OPOSTO = 2, ALEATORIO = 3;

    private float zPosition = 90f;
    private float tempo = 3.5f;

    private List<GameObject> listaCerto = new List<GameObject>();
    private List<GameObject> listaErrado = new List<GameObject>();

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
            instance.GetComponentInChildren<ParDeOlhosController>().SetTarget(Target.transform);

            instance.GetComponent<Button>().onClick.AddListener(delegate { RostoCerto(); Vibration.Vibrate(30); });

            listaCerto.Add(instance);
        }
        for (int i = 0; i < tot_errado; i++)
        {
            GameObject errado = Rostos[ Random.Range(0, Rostos.Count) ];
            GameObject instance = Instantiate(errado, new Vector3(Random.Range(Cam.transform.position.x - (screenSize.x - 2), Cam.transform.position.x + (screenSize.x - 2)),
                                        Random.Range(Cam.transform.position.y - (screenSize.y - 2), Cam.transform.position.y + (screenSize.y - 2)),
                                        zPosition), Quaternion.identity, transform);

            instance.GetComponentInChildren<ParDeOlhosController>().SetTarget(Target.transform, modo);

            instance.GetComponent<Button>().onClick.AddListener(delegate { RostoErrado(); Vibration.Vibrate(30); });

            listaErrado.Add(instance);
        }

        StartCoroutine(Timer(tempo));
    }

    IEnumerator Timer(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        //aplicando rostos corretos em cada um dos objetos presentes na cena e definindo uma posicao aleatoria
        foreach (GameObject go in listaCerto)
        {
            
            go.SetActive(true);
            go.GetComponent<Animator>().SetBool("FadeIn", true);
        }

        foreach (GameObject go in listaErrado)
        {
            go.SetActive(true);
            go.GetComponent<Animator>().SetBool("FadeIn", true);
        }

        syncBool = true;
    }

    private void configuraDificuldade(int dif)
    {
        GetComponent<CountdownScriptInveja>().TempoContagem = 10 - dif;
        switch (dif)
        {
            case 1:
                tot_certo = 1;
                tot_errado = 10;
                modo = FRENTE;
                break;
            case 2:
                tot_certo = 1;
                tot_errado = 15;
                modo = OPOSTO;
                break;
            case 3:
                tot_certo = 1;
                tot_errado = 20;
                modo = ALEATORIO;
                break;
            case 4:
                tot_certo = 1;
                tot_errado = 25;
                modo = ALEATORIO;
                break;
            case 5:
                tot_certo = 1;
                tot_errado = 25;
                modo = ALEATORIO;
                break;
            default: //case sem entrada
                tot_certo = 1;
                tot_errado = 20;
                modo = ALEATORIO;
                break;
        }
    }

    public void RostoCerto()
    {
        if (!faceClick)
        {
            if (GameMode.Mode == GameMode.GameModes.Minigame)
            {
                MinigameModeController minigameModeController = FindObjectOfType<MinigameModeController>();
                minigameModeController.OnMinigameFinished(true, "Inveja");
            }
            else
            {
                if (GetComponent<CountdownScriptInveja>().TempoContagem > 10 - difficulty - 1)
                {
                    perfect.gameObject.SetActive(true);
                    LivesController.addVidas();
                }
                else
                {
                    ganhou.gameObject.SetActive(true);
                }

                foreach (GameObject errado in listaErrado)
                {
                    errado.GetComponent<Animator>().SetBool("FadeOut", true);
                    errado.GetComponent<Button>().enabled = false;
                }
                faceClick = true;

                GameObject.Find("Player").GetComponent<PlayerMovement>().StartDelaySobeCarinha();
                GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
            }
        }
        
    }

    public void RostoErrado()
    {

        if (!faceClick)
        {
            if (GameMode.Mode == GameMode.GameModes.Minigame)
            {
                MinigameModeController minigameModeController = FindObjectOfType<MinigameModeController>();
                minigameModeController.OnMinigameFinished(false, "Inveja");
            }
            else
            {
                foreach (GameObject errado in listaErrado)
                {
                    errado.GetComponent<Animator>().SetBool("FadeOut", true);
                    errado.GetComponent<Button>().enabled = false;
                }
                LivesController.RemVidas();

                faceClick = true;

                GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
            }
        }
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
