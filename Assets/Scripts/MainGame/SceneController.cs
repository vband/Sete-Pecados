using System.Collections;
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
        WinGame();
	}

    // Ajusta a posição da barra de progresso com base a posição atual do jogador
    private void ProgressBar()
    {
        float delta = Mathf.Abs(playerEndPos - playerInitPos);
        float fillAmount = player.position.x / delta;
        progressBar.fillAmount = fillAmount;
    }

    private void WinGame()
    {
        if (player.position.x >= playerEndPos)
        {
            Debug.Log("Você venceu o jogo!");
        }
    }
}
