using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LivesController : MonoBehaviour {

    public GameObject LivesPrefab;

    static private int vidas = 3;
    private int showingLives;
    private bool gameOver;

    // Use this for initialization
    void Start()
    {
        //vidas = 3;
        showingLives = 0;
        gameOver = false;
    }
	
	// Update is called once per frame
	void Update () {
        updatePanel();
	}

    void updatePanel()
    {
        if (vidas <= 0 && !gameOver)
        {
            gameOver = true;
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("GameOver");
        }
        else if (vidas > showingLives)
        {
            Instantiate(LivesPrefab,transform);
            showingLives++;
        }
        else if (vidas < showingLives)
        {
            showingLives--;
            StartVidaVisualFeedback(transform.GetChild(vidas).gameObject);
            //Destroy(transform.GetChild(vidas).gameObject);
            
        }      
    }

    public static void addVidas()
    {
        if (vidas < 3) //limite de vidas
        {
            vidas++;
        }
        
    }

    public static void RemVidas()
    {
        vidas--;
    }

    public static void InitVidas()
    {
        vidas = 3;
    }

    public static int GetVidas()
    {
        return vidas;
    }

    private void StartVidaVisualFeedback(GameObject vidaPraPerder)
    {
        StartCoroutine(VidaVisualFeedBack(vidaPraPerder));
    }

    IEnumerator VidaVisualFeedBack(GameObject vidaPraPerder)
    {
        float tempo = 1;
        inicio:
        vidaPraPerder.SetActive(false);
        yield return new WaitForSeconds(tempo);
        vidaPraPerder.SetActive(true);
        tempo -= 0.1f;
        yield return new WaitForSeconds(tempo);
        if(tempo > 0.2f)
        {
            goto inicio;
        }

        Destroy(vidaPraPerder);
    }
}
