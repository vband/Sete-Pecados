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
    public float colDepth = 4f;
    public float zPosition = 0f;

    private List<GameObject> lista = new List<GameObject>();
    private Vector2 screenSize;
    [HideInInspector]
    public bool syncBool = false;


    private void Awake()
    {

    }

    // Use this for initialization
    void Start () {

        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        for (int i=0;i < tot_errado; i++)
        {
            lista.Add(Errado);
        }
        for (int i = 0; i < tot_certo; i++)
        {
            lista.Add(Certo);
        }

        StartCoroutine(timer(tempo));
        
    }
	
	// Update is called once per frame
	void Update () {

    }

    IEnumerator timer(float segundos)
    {

        yield return new WaitForSeconds(segundos);
        //set_active = true;
        foreach (GameObject go in lista)
        {
            Instantiate(go, new Vector3(Random.Range(0 - (screenSize.x - 2), 0 + (screenSize.x - 2)),
                                        Random.Range(0 - (screenSize.y - 2), 0 + (screenSize.y - 2)),
                                        zPosition), Quaternion.identity, minigame.transform);
        }

        syncBool = true;
    }

}
