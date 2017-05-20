using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mecanica : MonoBehaviour {

    public GameObject Cam;
    public GameObject minigame;
    public GameObject Certo;
    public GameObject Errado;
    public List<Sprite> album_faces;

    //dificuldade
    private static int difficulty;
    private int tot_errado;
    private int tot_certo;

    private float zPosition = 0f;
    private float tempo = 3.5f;

    private List<GameObject> listaCerto = new List<GameObject>();
    private List<GameObject> listaErrado = new List<GameObject>();

    [HideInInspector] public Vector2 screenSize;
    [HideInInspector] public bool syncBool = false;
    [HideInInspector] public Sprite rosto_correto;


    private void Awake()
    {
        int RostoCerto = Random.Range(0, album_faces.Count);
        rosto_correto = album_faces[RostoCerto];
        album_faces.RemoveAt(RostoCerto);

        //obtendo tamanho da tela para gerar objetos dentro da area visivel
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
    }

    void Start () {

        configuraDificuldade(difficulty);

        //carregando quantidade de botoes nas listas corretas
        for (int i = 0; i < tot_certo; i++)
        {
            listaCerto.Add(Certo);
        }
        for (int i=0;i < tot_errado; i++)
        {
            listaErrado.Add(Errado);
        }

        StartCoroutine(timer(tempo));
    }
	
	IEnumerator timer(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        //aplicando rostos corretos em cada um dos objetos presentes na cena e definindo uma posicao aleatoria
        foreach (GameObject go in listaCerto)
        {
            go.GetComponent<Image>().sprite = rosto_correto;
            Instantiate(go, new Vector3(Random.Range(Cam.transform.position.x - (screenSize.x - 2), Cam.transform.position.x + (screenSize.x - 2)),
                                        Random.Range(Cam.transform.position.y - (screenSize.y - 2), Cam.transform.position.y + (screenSize.y - 2)),
                                        zPosition), Quaternion.identity, minigame.transform);
        }

        foreach (GameObject go in listaErrado)
        {
            go.GetComponent<Image>().sprite = album_faces[Random.Range(0, album_faces.Count)];
            Instantiate(go, new Vector3(Random.Range(Cam.transform.position.x - (screenSize.x - 2), Cam.transform.position.x + (screenSize.x - 2)),
                                        Random.Range(Cam.transform.position.y - (screenSize.y - 2), Cam.transform.position.y + (screenSize.y - 2)),
                                        zPosition), Quaternion.identity, minigame.transform);
        }
        
        syncBool = true;
    }

    private void configuraDificuldade(int dif)
    {
        GetComponent<CountdownScript>().TempoContagem = 10 - dif;
        switch (dif)
        {
            case 1:
                tot_certo = 5;
                tot_errado = 25;
                break;
            case 2:
                tot_certo = 4;
                tot_errado = 26;
                break;
            case 3:
                tot_certo = 3;
                tot_errado = 27;
                break;
            case 4:
                tot_certo = 2;
                tot_errado = 28;
                break;
            case 5:
                tot_certo = 1;
                tot_errado = 29;
                break;
            default: //case sem entrada, ativa o mais facil
                tot_certo = 5;
                tot_errado = 25;
                break;
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