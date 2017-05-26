﻿using System.Collections;
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
            Destroy(transform.GetChild(vidas).gameObject);
            showingLives--;
        }      
    }

    public static void addVidas()
    {
        if(vidas < 3)
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
}
