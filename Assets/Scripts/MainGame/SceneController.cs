using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class SceneController : MonoBehaviour
{
    public Transform player;
    public RectTransform mobileVirtualButtonsController;
    public RectTransform freeJoystick;
    public RectTransform pauseButton;
    public Animator introAnim;
    public Image progressBar;
    public Transform beginningOfWorld;
    public RectTransform progressBarFrame;
    public Transform PainelPowerUp;
    public Animator CanvasHud;
    public GameObject Background;
    // Distância que o jogador precisa percorrer para ganhar
    public float distanceToWin;
    public RectTransform rostoFalamaia;
    public Camera mainCamera;
    public Button VoltarParaMenu;
    public Button JogarDeNovo;
    public Text almasSalvasText;
    public RectTransform lowLivesWarning;

    private bool intro;
    private float playerInitPos, playerEndPos, deltaDistance;

    private float fillAmount = 0;


    private GameObject igreja;
    [HideInInspector] public static bool winAnimation = false;
    [HideInInspector] public static bool hasGameFinished = false;
    [HideInInspector] public static bool created = false;
    [HideInInspector] public static bool paused = false;

    private const int JOYSTICK = 0, VIRTUAL = 1;


    private void Awake()
    {
        CheckExistance();
    }

    void Start ()
    {
        playerInitPos = beginningOfWorld.position.x;
        playerEndPos = playerInitPos + distanceToWin;
        deltaDistance = Mathf.Abs(playerEndPos - playerInitPos);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("PowerUps"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Enemies"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("AguaBentaPowerUp"), LayerMask.NameToLayer("PowerUps"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("FILHO-PANEL"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PowerUps"), LayerMask.NameToLayer("FILHO-PANEL"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("FILHO-PANEL"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Environment"), LayerMask.NameToLayer("AguaBentaPowerUp"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("UI"));
        intro = true;

#if UNITY_STANDALONE_WIN
        pauseButton.gameObject.SetActive(false);
        mobileVirtualButtonsController.gameObject.SetActive(false);
#endif
#if UNITY_ANDROID
        pauseButton.gameObject.SetActive(true);

        // Verifica as configurações e ajusta os controles
        if (PlayerPrefs.GetInt("INPUTCONFIG") == JOYSTICK)
        {
            freeJoystick.gameObject.SetActive(true);
            mobileVirtualButtonsController.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("INPUTCONFIG") == VIRTUAL)
        {
            freeJoystick.gameObject.SetActive(false);
            mobileVirtualButtonsController.gameObject.SetActive(true);
        }
#endif
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
            Background.gameObject.SetActive(false);

#if UNITY_ANDROID

            pauseButton.gameObject.SetActive(false);

            if (PlayerPrefs.GetInt("INPUTCONFIG") == JOYSTICK && freeJoystick.gameObject.activeSelf)
            {
                freeJoystick.GetComponent<AnalógicoLivre>().NeutralizeInput();
                freeJoystick.gameObject.SetActive(false);
            }

            else if (PlayerPrefs.GetInt("INPUTCONFIG") == VIRTUAL && mobileVirtualButtonsController.gameObject.activeSelf)
            {
                CrossPlatformInputManager.SetAxisZero("Horizontal");
                CrossPlatformInputManager.SetAxisZero("Vertical");
                mobileVirtualButtonsController.gameObject.SetActive(false);
            }
#endif

        }
        else
        {
            PainelPowerUp.gameObject.SetActive(true);
            progressBarFrame.gameObject.SetActive(true);
            Background.gameObject.SetActive(true);

#if UNITY_ANDROID
            pauseButton.gameObject.SetActive(true);

            if (PlayerPrefs.GetInt("INPUTCONFIG") == JOYSTICK && !freeJoystick.gameObject.activeSelf)
            {
                freeJoystick.gameObject.SetActive(true);
            }

            else if (PlayerPrefs.GetInt("INPUTCONFIG") == VIRTUAL && !mobileVirtualButtonsController.gameObject.activeSelf)
            {
                mobileVirtualButtonsController.gameObject.SetActive(true);
            }
#endif
        }

        // Verifica se o jogador ganhou
        if (winAnimation)
        {
            // Executa a animação de vitória
            PlayWinAnimation();
        }

        // Verifica se deve avisar o jogador
	}

    // Ativa animacao de warning na tela

    public IEnumerator WarnPlayer()
    {
        yield return new WaitForSeconds(3);
        yield return new WaitUntil(() => LivesController.GetVidas() == 1);
        CanvasHud.SetBool("Warning", true);
        yield return new WaitUntil(() => CanvasHud.GetCurrentAnimatorStateInfo(0).IsTag("piscando"));
        CanvasHud.SetBool("Warning", false);
    }

    // Ajusta a posição da barra de progresso com base a posição atual do jogador
    private void ProgressBar()
    {
        // Atualiza o enchimento da barra de progresso
        fillAmount = player.position.x / deltaDistance;
        float preenchimento = 800 - 800 * fillAmount;
        float correctPos = -preenchimento + 380;
        //ajusta preenchimento da barra
        if(correctPos < 380)
            SetRect(progressBar.GetComponent<RectTransform>(), 0, 0, preenchimento, -32);        
        //ajusta posicao da cabeca
        if (correctPos <= -380)
            rostoFalamaia.localPosition = new Vector3(-380, rostoFalamaia.localPosition.y);
        else if (correctPos >= 380)
            rostoFalamaia.localPosition = new Vector3(380, rostoFalamaia.localPosition.y);
        else
            rostoFalamaia.localPosition = new Vector2(correctPos, rostoFalamaia.localPosition.y);
    }
    //metodo para acessar as propriedades do recttransform
    public static void SetRect(RectTransform trs, float left, float top, float right, float bottom)
    {
        trs.offsetMin = new Vector2(left, bottom);
        trs.offsetMax = new Vector2(-right, -top);
    }

    private void UpdateDifficulty() //mecanica = MinigameGananciaController
    {
        // Divide o progresso do jogo em 5 segmentos, e
        // estabelece um nível de dificuldade para cada um.
        if (fillAmount <= 0.2)
        {
            MinigameIraController.SetDifficulty(1);
            MinigamePreguiçaController.SetDifficulty(1);
            mecanica.SetDifficulty(1);
            MinigameGulaController.SetDifficulty(1);
            MinigameOrgulhoController.SetDifficulty(1);
            MiniGameGananciaController.SetDifficulty(1);
            MinigameLuxuriaController.SetDifficulty(1);
            MinigameInvejaController.SetDifficulty(1);
        }
        else if (fillAmount <= 0.4)
        {
            MinigameIraController.SetDifficulty(2);
            MinigamePreguiçaController.SetDifficulty(2);
            mecanica.SetDifficulty(2);
            MinigameGulaController.SetDifficulty(2);
            MinigameOrgulhoController.SetDifficulty(2);
            MiniGameGananciaController.SetDifficulty(2);
            MinigameLuxuriaController.SetDifficulty(2);
            MinigameInvejaController.SetDifficulty(2);
        }
        else if (fillAmount <= 0.6)
        {
            MinigameIraController.SetDifficulty(3);
            MinigamePreguiçaController.SetDifficulty(3);
            mecanica.SetDifficulty(3);
            MinigameGulaController.SetDifficulty(3);
            MinigameOrgulhoController.SetDifficulty(3);
            MiniGameGananciaController.SetDifficulty(3);
            MinigameLuxuriaController.SetDifficulty(3);
            MinigameInvejaController.SetDifficulty(3);
        }
        else if (fillAmount <= 0.8)
        {
            MinigameIraController.SetDifficulty(4);
            MinigamePreguiçaController.SetDifficulty(4);
            mecanica.SetDifficulty(4);
            MinigameGulaController.SetDifficulty(4);
            MinigameOrgulhoController.SetDifficulty(4);
            MiniGameGananciaController.SetDifficulty(4);
            MinigameLuxuriaController.SetDifficulty(4);
            MinigameInvejaController.SetDifficulty(4);
        }
        else
        {
            MinigameIraController.SetDifficulty(5);
            MinigamePreguiçaController.SetDifficulty(5);
            mecanica.SetDifficulty(5);
            MinigameGulaController.SetDifficulty(5);
            MinigameOrgulhoController.SetDifficulty(5);
            MiniGameGananciaController.SetDifficulty(5);
            MinigameLuxuriaController.SetDifficulty(5);
            MinigameInvejaController.SetDifficulty(5);
        }
    }

    public void WinGame()
    {
        // Avisa que o jogo acabou
        hasGameFinished = true;
        winAnimation = true;
        // Avisa que a câmera vai parar de andar sozinha
        mainCamera.GetComponent<CameraMovement>().enabled = false;
        // Impede que o jogo prossiga
        paused = true;

        player.GetComponent<Animator>().SetBool("isRunning", false);
        player.GetComponent<Animator>().SetBool("isJumping", false);

#if UNITY_ANDROID

        if (PlayerPrefs.GetInt("INPUTCONFIG") == VIRTUAL)
        {
            mobileVirtualButtonsController.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("INPUTCONFIG") == JOYSTICK)
        {
            freeJoystick.gameObject.SetActive(false);
        }
#endif


        igreja = GameObject.FindGameObjectWithTag("Igreja");
    }

    // Executa a animação de vitória
    private void PlayWinAnimation()
    {
        float cameraFinalSize = 7f;
        float cameraExpandSpeed = 0.02f;
        float cameraMoveSpeed = 0.07f;

        // Move a câmera até centralizar a igreja
        // Para a direita
        if (mainCamera.transform.position.x < igreja.transform.position.x)
        {
            Rigidbody2D cameraRB = mainCamera.GetComponent<Rigidbody2D>();
            Vector3 cameraPos = mainCamera.transform.position;
            Vector2 cameraSpeedVector = new Vector2(cameraMoveSpeed, 0f);

            cameraRB.MovePosition((Vector2)cameraPos + cameraSpeedVector);
        }
        // Para a esquerda
        else if (mainCamera.transform.position.x > igreja.transform.position.x + cameraMoveSpeed)
        {
            Rigidbody2D cameraRB = mainCamera.GetComponent<Rigidbody2D>();
            Vector3 cameraPos = mainCamera.transform.position;
            Vector2 cameraSpeedVector = new Vector2(cameraMoveSpeed, 0f);

            cameraRB.MovePosition((Vector2)cameraPos - cameraSpeedVector);
        }
        // Para cima
        else if (mainCamera.transform.position.y < igreja.transform.position.y - 1.37f)
        {
            Rigidbody2D cameraRB = mainCamera.GetComponent<Rigidbody2D>();
            Vector3 cameraPos = mainCamera.transform.position;
            Vector2 cameraSpeedVector = new Vector2(0f, cameraMoveSpeed);

            cameraRB.MovePosition((Vector2)cameraPos + cameraSpeedVector);

            // Afasta a câmera enquanto ela se move
            if (mainCamera.orthographicSize < cameraFinalSize)
            {
                mainCamera.orthographicSize += cameraExpandSpeed;
            }
        }
        // Termina de afastar a câmera depois que ela se moveu
        else if (mainCamera.orthographicSize < cameraFinalSize)
        {
            mainCamera.orthographicSize += cameraExpandSpeed;
        }

        // Quando tiver centralizado a igreja...
        else
        {
            VoltarParaMenu.gameObject.SetActive(true);
            JogarDeNovo.gameObject.SetActive(true);
            almasSalvasText.text = "Você salvou " + PlayerMovement.getPessoasSalvas().ToString() + " almas!";
            almasSalvasText.gameObject.SetActive(true);
            winAnimation = false;
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

    public void SpeedUpMusic(float Multiplier)
    {
        GetComponent<AudioSource>().pitch = Multiplier;
    }

    public void SpeedDownMusic()
    {
        GetComponent<AudioSource>().pitch = 1.0f;
    }

    public void ButtonVibrate()
    {
        Vibration.Vibrate(30);
    }
}
