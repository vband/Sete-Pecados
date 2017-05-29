using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Transform player;
    public Animator introAnim;
    public Image progressBar;
    public Transform beginningOfWorld;
    public RectTransform progressBarFrame;
    public Transform PainelPowerUp;
    // Distância que o jogador precisa percorrer para ganhar
    public float distanceToWin;
    public RectTransform rostoFalamaia;

    private bool intro;
    private float playerInitPos, playerEndPos, deltaDistance;
    private float progressBarWidth;
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
        deltaDistance = Mathf.Abs(playerEndPos - playerInitPos);
        progressBarWidth = progressBar.GetComponent<RectTransform>().rect.width;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("PowerUps"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Enemies"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("AguaBentaPowerUp"), LayerMask.NameToLayer("PowerUps"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("FILHO-PANEL"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PowerUps"), LayerMask.NameToLayer("FILHO-PANEL"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Environment"), LayerMask.NameToLayer("AguaBentaPowerUp"));
        intro = true;
    }

    void Update ()
    {

        ProgressBar();
        UpdateDifficulty(); 
        WinGame();

        if (intro == true && introAnim != null && introAnim.GetCurrentAnimatorStateInfo(0).IsTag("1"))
        {
            paused = true;
        }
        else if (intro == true)
        {
            paused = false;
            intro = false;
            player.GetComponent<SpriteRenderer>().enabled = true;
        }

        // Desativa a barra de progresso e controle de poweUPs quando o jogo é pausado
        if (paused)
        {
            PainelPowerUp.gameObject.SetActive(false);
            progressBarFrame.gameObject.SetActive(false);
        }
        else
        {
            PainelPowerUp.gameObject.SetActive(true);
            progressBarFrame.gameObject.SetActive(true);
        }
	}

    // Ajusta a posição da barra de progresso com base a posição atual do jogador
    private void ProgressBar()
    {
        // Atualiza o enchimento da barra de progresso
        //float delta = Mathf.Abs(playerEndPos - playerInitPos);
        float fillAmount = player.position.x / deltaDistance;
        progressBar.fillAmount = fillAmount;

        // Move o rosto do Falamaia
        float correctPos = fillAmount * progressBarWidth;
        rostoFalamaia.anchoredPosition = new Vector2(correctPos, rostoFalamaia.anchoredPosition.y);
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
            //Debug.Log("Você venceu o jogo!");
        }
    }

    private void CheckExistance()
    {
        if (!created)
        {
            // this is the first instance - make it persist
            DontDestroyOnLoad(this.gameObject);
            created = true;
            paused = false;
        }
        else
        {
            // Determina que não é para mostrar a intro de novo
            introAnim.enabled = false;
            // this must be a duplicate from a scene reload - DESTROY!
            Destroy(this.gameObject);
        }
    }
}
