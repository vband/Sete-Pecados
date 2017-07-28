using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LivesController : MonoBehaviour {

    public GameObject LivesPrefab;
    public SceneController sceneController;

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
        WarnPlayer();
	}

    private void WarnPlayer()
    {
        if (!SceneController.paused && vidas == 1 && !sceneController.isWarning)
        {
            sceneController.StartWarning();
        }
        else if (vidas != 1 && sceneController.isWarning)
        {
            sceneController.StopWarning();
        }
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
        float totalTime = 3, timeInterval = 0.25f;

        for (float i = totalTime; i > 0; i -= 2*timeInterval)
        {
            vidaPraPerder.SetActive(false);
            yield return new WaitForSeconds(timeInterval);
            vidaPraPerder.SetActive(true);
            yield return new WaitForSeconds(timeInterval);
        }
        Destroy(vidaPraPerder);
    }
}
