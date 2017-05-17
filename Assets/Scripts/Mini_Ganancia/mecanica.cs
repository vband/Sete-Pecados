using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mecanica : MonoBehaviour {

    
    public GameObject minigame;
    public GameObject Certo;
    public GameObject Errado;
    public float tempo;
    public int tot_errado;
    public int tot_certo;
    
    public float zPosition = 0f;

    public List<Sprite> album_faces;
    
        
    private List<GameObject> listaCerto = new List<GameObject>();
    private List<GameObject> listaErrado = new List<GameObject>();
    private Vector2 screenSize;
    [HideInInspector]
    public bool syncBool = false;
    public Sprite rosto_correto;

    private void Awake()
    {
        int RostoCerto = Random.Range(0, album_faces.Count);
        rosto_correto = album_faces[RostoCerto];
        album_faces.RemoveAt(RostoCerto);
    }

    void Start () {
                
        //obtendo tamanho da tela para gerar obejtos dentro da area visivel
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

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
        //aplicando rostos corretos em cada um dos objetos presentes na cena
        foreach (GameObject go in listaCerto)
        {
            go.GetComponent<Image>().sprite = rosto_correto;
            Instantiate(go, new Vector3(Random.Range(0 - (screenSize.x - 2), 0 + (screenSize.x - 2)),
                                        Random.Range(0 - (screenSize.y - 2), 0 + (screenSize.y - 2)),
                                        zPosition), Quaternion.identity, minigame.transform);
        }

        foreach (GameObject go in listaErrado)
        {
            go.GetComponent<Image>().sprite = album_faces[Random.Range(0, album_faces.Count)];
            Instantiate(go, new Vector3(Random.Range(0 - (screenSize.x - 2), 0 + (screenSize.x - 2)),
                                        Random.Range(0 - (screenSize.y - 2), 0 + (screenSize.y - 2)),
                                        zPosition), Quaternion.identity, minigame.transform);
        }
        
        syncBool = true;
    }

}