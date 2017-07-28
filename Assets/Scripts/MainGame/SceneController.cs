using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class SceneController : MonoBehaviour
{
    public Transform player;
    public RectTransform mobileVirtualButtonsController;
    public RectTransform mobileJoystickController, mobileJoystick, mobileJumpButton;
    public RectTransform pauseButton;
    public Animator introAnim;
    public Image progressBar;
    public Transform beginningOfWorld;
    public RectTransform progressBarFrame;
    public Transform PainelPowerUp;
    // Distância que o jogador precisa percorrer para ganhar
    public float distanceToWin;
    public RectTransform rostoFalamaia;
    public Camera mainCamera;
    public Button VoltarParaMenu;
    public Button JogarDeNovo;
    public Text almasSalvasText;
    public RectTransform lowLivesWarning;
    public float warningTimeInterval;
    public int warningTimesLimit;

    private bool intro;
    private float playerInitPos, playerEndPos, deltaDistance;
    private float progressBarWidth;
    private float warningTimer;
    private int warningCounter;
    
    private GameObject igreja;
    [HideInInspector] public static bool winAnimation = false;
    [HideInInspector] public static bool hasGameFinished = false;
    [HideInInspector] public static bool created = false;
    [HideInInspector] public static bool paused = false;
    [HideInInspector] public bool isWarning = false; // True se o aviso de vida baixa está ocorrendo

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
        progressBarWidth = progressBar.GetComponent<RectTransform>().rect.width;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("PowerUps"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Enemies"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("AguaBentaPowerUp"), LayerMask.NameToLayer("PowerUps"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("FILHO-PANEL"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PowerUps"), LayerMask.NameToLayer("FILHO-PANEL"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("FILHO-PANEL"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Environment"), LayerMask.NameToLayer("AguaBentaPowerUp"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("UI"));
        intro = true;
        warningCounter = 0;

#if UNITY_STANDALONE_WIN
        pauseButton.gameObject.SetActive(false);
        mobileVirtualButtonsController.gameObject.SetActive(false);
#endif
#if UNITY_ANDROID
        pauseButton.gameObject.SetActive(true);

        // Verifica as configurações e ajusta os controles
        if (PlayerPrefs.GetInt("INPUTCONFIG") == JOYSTICK)
        {
            mobileJoystickController.gameObject.SetActive(true);
            mobileVirtualButtonsController.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("INPUTCONFIG") == VIRTUAL)
        {
            mobileJoystickController.gameObject.SetActive(false);
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
            lowLivesWarning.gameObject.SetActive(false);

#if UNITY_ANDROID
            pauseButton.gameObject.SetActive(false);

            if (PlayerPrefs.GetInt("INPUTCONFIG") == JOYSTICK && mobileJoystick.GetComponent<Image>().enabled == true)
            {
                mobileJoystick.GetComponent<Image>().enabled = false;
                mobileJumpButton.GetComponent<Image>().enabled = false;
            }

            else if (PlayerPrefs.GetInt("INPUTCONFIG") == VIRTUAL && mobileVirtualButtonsController.gameObject.activeSelf)
            {
                mobileVirtualButtonsController.gameObject.SetActive(false);
            }
#endif

        }
        else
        {
            PainelPowerUp.gameObject.SetActive(true);
            progressBarFrame.gameObject.SetActive(true);

#if UNITY_ANDROID
            pauseButton.gameObject.SetActive(true);

            if (PlayerPrefs.GetInt("INPUTCONFIG") == JOYSTICK && mobileJoystick.GetComponent<Image>().enabled == false)
            {
                mobileJoystick.GetComponent<Image>().enabled = true;
                mobileJumpButton.GetComponent<Image>().enabled = true;
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
        if (isWarning)
        {
            // Avisa o jogador
            WarnPlayer();
        }
	}

    // Registra que o aviso de vida baixa deve começar agora
    public void StartWarning()
    {
        if (warningCounter == warningTimesLimit)
        {
            return;
        }

        isWarning = true;
        warningTimer = 0;
    }

    // Ativa e desativa o aviso de vida baixa
    private void WarnPlayer()
    {
        if (warningTimer == 0) // Essa é a primeira vez que essa chamada é feita
        {
            // Registra o tempo atual
            warningTimer = Time.time;

            // Ativa o texto
            lowLivesWarning.gameObject.SetActive(true);

            warningCounter++;
        }
        else // Essa não é a primeira vez que essa chamada é feita
        {
            // Verifica se está na hora de pulsar o texto
            if (Time.time - warningTimer >= warningTimeInterval)
            {
                // Verifica se já terminou de avisar
                if (warningCounter == warningTimesLimit)
                {
                    StopWarning();
                }

                // Ainda não terminou...
                else
                {
                    // Registra o tempo atual
                    warningTimer = Time.time;

                    // Pulsa o texto
                    lowLivesWarning.gameObject.SetActive(!lowLivesWarning.gameObject.activeSelf);

                    if (lowLivesWarning.gameObject.activeSelf)
                    {
                        warningCounter++;
                    }
                }
            }
        }
    }

    // Registra que o aviso de vida baixa deve parar
    public void StopWarning()
    {
        warningTimer = 0;
        isWarning = false;
        lowLivesWarning.gameObject.SetActive(false);
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
        hasGameFinished = true;
        winAnimation = true;
        // Avisa que a câmera vai parar de andar sozinha
        mainCamera.GetComponent<CameraMovement>().enabled = false;
        // Impede que o jogo prossiga
        paused = true;

        player.GetComponent<Animator>().SetBool("isRunning", false);
        player.GetComponent<Animator>().SetBool("isJumping", false);

#if UNITY_ANDROID
        mobileJoystick.GetComponent<Image>().enabled = false;
        mobileJumpButton.GetComponent<Image>().enabled = false;
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
}
