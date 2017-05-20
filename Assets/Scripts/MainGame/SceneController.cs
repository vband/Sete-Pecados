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
    [HideInInspector] public static bool created = false;
    [HideInInspector] public static bool paused = false;
 
    private void Awake()
    {
        CheckExistance();
        
    }

    void Start ()
    {
        playerInitPos = beginningOfWorld.position.x;
        playerEndPos = playerInitPos + distanceToWin;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("PowerUps"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Enemies"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("AguaBentaPowerUp"), LayerMask.NameToLayer("PowerUps"));
    }

    void Update ()
    {
        ProgressBar();
        UpdateDifficulty(); // Por enquanto, só funciona para o minigame da ira.
        WinGame();
        print(paused);
	}

    // Ajusta a posição da barra de progresso com base a posição atual do jogador
    private void ProgressBar()
    {
        float delta = Mathf.Abs(playerEndPos - playerInitPos);
        float fillAmount = player.position.x / delta;
        progressBar.fillAmount = fillAmount;
    }

    private void UpdateDifficulty() //mecanica = MinigameGananciaController
    {
        // Divide o progresso do jogo em 5 segmentos, e
        // estabelece um nível de dificuldade para cada um.
        if (progressBar.fillAmount <= 0.2)
        {
            MinigameIraController.SetDifficulty(1);
            MinigamePreguiçaController.SetDifficulty(1);
            mecanica.SetDifficulty(1);
        }
        else if (progressBar.fillAmount <= 0.4)
        {
            MinigameIraController.SetDifficulty(2);
            MinigamePreguiçaController.SetDifficulty(2);
            mecanica.SetDifficulty(2);
        }
        else if (progressBar.fillAmount <= 0.6)
        {
            MinigameIraController.SetDifficulty(3);
            MinigamePreguiçaController.SetDifficulty(3);
            mecanica.SetDifficulty(3);
        }
        else if (progressBar.fillAmount <= 0.8)
        {
            MinigameIraController.SetDifficulty(4);
            MinigamePreguiçaController.SetDifficulty(4);
            mecanica.SetDifficulty(4);
        }
        else
        {
            MinigameIraController.SetDifficulty(5);
            MinigamePreguiçaController.SetDifficulty(5);
            mecanica.SetDifficulty(5);
        }
    }

    private void WinGame()
    {
        if (player.position.x >= playerEndPos)
        {
            Debug.Log("Você venceu o jogo!");
        }
    }

    private void CheckExistance()
    {
        if (!created)
        {
            // this is the first instance - make it persist
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            // this must be a duplicate from a scene reload - DESTROY!
            Destroy(this.gameObject);
        }
    }
}
