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
    public Camera mainCamera;

    private bool intro;
    private float playerInitPos, playerEndPos, deltaDistance;
    private float progressBarWidth;
    private bool winGame = false;
    private GameObject igreja;
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
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("FILHO-PANEL"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Environment"), LayerMask.NameToLayer("AguaBentaPowerUp"));
        intro = true;
    }

    void Update ()
    {

        ProgressBar();
        UpdateDifficulty(); 

        // Impede que o jogo prossiga enquanto ainda estiver na introdução
        if (intro == true && introAnim != null && introAnim.GetCurrentAnimatorStateInfo(0).IsTag("1"))
        {
            paused = true;
        }
        // Ao final da intro, permite que o jogo prossiga
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

        // Verifica se o jogador ganhou
        if (winGame)
        {
            PlayWinAnimation();
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

    public void WinGame()
    {
        // Avisa que o jogo acabou
        winGame = true;
        // Avisa que a câmera vai parar de andar sozinha
        mainCamera.GetComponent<CameraMovement>().enabled = false;
        // Impede que o jogo prossiga
        paused = true;

        igreja = GameObject.FindGameObjectWithTag("Igreja");
    }

    // Executa a animação de vitória
    private void PlayWinAnimation()
    {
        // Move a câmera até centralizar a igreja
        if (mainCamera.transform.position.x < igreja.transform.position.x)
        {
            Rigidbody2D cameraRB = mainCamera.GetComponent<Rigidbody2D>();
            Vector3 cameraPos = mainCamera.transform.position;
            Vector2 cameraSpeed = new Vector2(0.05f, 0f);

            cameraRB.MovePosition((Vector2)cameraPos + cameraSpeed);
        }
        else if (mainCamera.transform.position.y < igreja.transform.position.y - 1.37f)
        {
            Rigidbody2D cameraRB = mainCamera.GetComponent<Rigidbody2D>();
            Vector3 cameraPos = mainCamera.transform.position;
            Vector2 cameraSpeed = new Vector2(0f, 0.05f);

            cameraRB.MovePosition((Vector2)cameraPos + cameraSpeed);
        }
        // Quando tiver centralizado a igreja...
        else
        {
            // Chama a cena de gameover (futuramente, será trocada por uma cena mais adequada)
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("GameOver");
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
