﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Transform player;
    public Image progressBar;
    public Transform beginningOfWorld;
    // Distância que o jogador precisa percorrer para ganhar
    public float distanceToWin;

    private float playerInitPos, playerEndPos;

	void Start ()
    {
        playerInitPos = beginningOfWorld.position.x;
        playerEndPos = playerInitPos + distanceToWin;

	}
	
	void Update ()
    {
        ProgressBar();
        UpdateDifficulty(); // Por enquanto, só funciona para o minigame da ira.
        WinGame();
	}

    // Ajusta a posição da barra de progresso com base a posição atual do jogador
    private void ProgressBar()
    {
        float delta = Mathf.Abs(playerEndPos - playerInitPos);
        float fillAmount = player.position.x / delta;
        progressBar.fillAmount = fillAmount;
    }

    private void UpdateDifficulty() // Por enquanto, só funciona para o minigame da ira.
    {
        // Divide o progresso do jogo em 5 segmentos, e
        // estabelece um nível de dificuldade para cada um.
        if (progressBar.fillAmount <= 0.2)
        {
            MinigameIraController.SetDifficulty(1);
        }
        else if (progressBar.fillAmount <= 0.4)
        {
            MinigameIraController.SetDifficulty(2);
        }
        else if (progressBar.fillAmount <= 0.6)
        {
            MinigameIraController.SetDifficulty(3);
        }
        else if (progressBar.fillAmount <= 0.8)
        {
            MinigameIraController.SetDifficulty(4);
        }
        else
        {
            MinigameIraController.SetDifficulty(5);
        }
    }

    private void WinGame()
    {
        if (player.position.x >= playerEndPos)
        {
            Debug.Log("Você venceu o jogo!");
        }
    }
}