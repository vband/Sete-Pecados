﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameGananciaController : MonoBehaviour {

    public GameObject Cam;
    public Text display_Tempo;
    [HideInInspector] public bool isPlaying = false;

    private bool perdeu = false;
    private bool ganhou = false;
    private bool perfect = true;
    [SerializeField] private float tempo;
    private static int difficulty = 5;

    // Use this for initialization
    void Start() {
        AdjustParameters();
        //inicializa o contador de tempo
        display_Tempo.GetComponent<Text>().text = tempo.ToString("F1");

        // Trava a rotação de tela
        if (Screen.orientation == ScreenOrientation.LandscapeLeft)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else if (Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;
        }
    }

    // Update is called once per frame
    void Update() {

        if (isPlaying)
        {
            AtualizaTempo();

            if (perdeu)
                Cam.GetComponent<Animator>().SetBool("perdeu", true);
            else if (ganhou && perfect)
                Cam.GetComponent<Animator>().SetBool("perfect", true);
            else if (ganhou)
                Cam.GetComponent<Animator>().SetBool("ganhou", true);
        }
    }

    public void SetPerdeu()
    {
        perdeu = true;
    }

    public bool GetPerdeu()
    {
        return (perdeu | ganhou) ;
    }

    public void SetUnperfect()
    {
        perfect = false;
    }

    private void AtualizaTempo()
    {
        if (tempo < 0)
        {
            ganhou = true;
            display_Tempo.GetComponent<Text>().text = "0.0";
        }
            
        else if (!ganhou && !perdeu)
        {
            tempo -= Time.deltaTime;
            display_Tempo.GetComponent<Text>().text = tempo.ToString("F1");
        }
    }

    public static void SetDifficulty(int dif)
    {
        difficulty = dif;
    }

    // Baseado na dificuldade, ajusta os parâmetros do minigame, a saber: 
    // multiplicador de velocidade do putao
    private void AdjustParameters()
    {
        switch (difficulty)
        {
            case 1:
                MovimentaZoiudoScript.rotationTime = 2f;
                break;
            case 2:
                MovimentaZoiudoScript.rotationTime = 1.7f;
                break;

            case 3:
                MovimentaZoiudoScript.rotationTime = 1.4f;
                break;
            case 4:
                MovimentaZoiudoScript.rotationTime = 1.1f;
                break;
            case 5:
                MovimentaZoiudoScript.rotationTime = 0.8f;
                break;
        }
    }

}
