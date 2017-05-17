using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LivesController : MonoBehaviour {

    public GameObject LivesPrefab;

    private int vidas;
    private int showingLives;

    // Use this for initialization
    void Start()
    {
        vidas = 3;
        showingLives = 0;
    }
	
	// Update is called once per frame
	void Update () {
        updatePanel();
	}

    void updatePanel()
    {
        if (vidas < 0)
        {
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("MainMenu");   
        }
        else if (vidas > showingLives)
        {
            Instantiate(LivesPrefab,transform);
            showingLives++;
        }
        else if (vidas < showingLives)
        {
            Destroy(transform.GetChild(vidas).gameObject);
            showingLives--;
        }      
    }

    public void addVidas()
    {
        vidas++;
    }

    public void RemVidas()
    {
        vidas--;
    }

}
