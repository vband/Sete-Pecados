using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameLuxuriaController : MonoBehaviour {

    [SerializeField] private RectTransform Player;
    [SerializeField] private RectTransform Perseguidor;
    [SerializeField] private Camera Cam;
    [SerializeField] private float tempo;

    [SerializeField] private Text fujadoputao;
    [SerializeField] private Text instrucoes;
    [SerializeField] private Text instrucoesPC;
    [SerializeField] private Text ganhou;
    [SerializeField] private Text perdeu;
    [SerializeField] private Text perfect;

    public Text DebugPosition;
    
    //private Vector3 PlayerOffset;
    private Vector3 PlayerScaleOffset;
    //private Vector3 PerseguidorOffset;
    private Vector3 PerseguidorScaleOffset;
    //private Color BackGroundColorOffset;

    private float DistanciaFinal = 0f;
    
    private bool running = false;
    private float parcialPlayer = 0, parcialPerseguidor;
    // Dificuldade
    private static int difficulty = 5;
    private float velocidadeDoPutao = 2f;

    private bool FimDeJogo = false;

    private KeyCode lastPress = KeyCode.None;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void Start () {

#if UNITY_ANDROID
        instrucoes.gameObject.SetActive(true);
#elif UNITY_STANDALONE
        instrucoesPC.gameObject.SetActive(true);
#endif
        //PlayerOffset = Player.position;//y=220
        PlayerScaleOffset = Player.localScale;
        //PerseguidorOffset = Perseguidor.position;
        PerseguidorScaleOffset = Perseguidor.localScale;
        //BackGroundColorOffset = Cam.backgroundColor;

        AdjustParameters();
        

        StartCoroutine(TelaDePreparacao(tempo));
        StartCoroutine(ControlaVibrador());
    }

    private void Update()
    {
        
        //debug only
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Player.transform.position = PlayerOffset;
            Perseguidor.transform.position = PerseguidorOffset;
            Cam.backgroundColor = BackGroundColorOffset;
            running = true;
        }
        */
        if (running)
        {
            if (ShakeDetection.shakeEvent || KeyboardInput())
            {
                MovePlayer();
            }

            MovePutao();
        }

        WinCondition();

    }

    private bool KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.A) && (lastPress == KeyCode.D || lastPress == KeyCode.None))
        {
            lastPress = KeyCode.A;
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && (lastPress == KeyCode.A || lastPress == KeyCode.None))
        {
            lastPress = KeyCode.D;
            return true;
        }
        else
            return false;
    } 

    private void VibraDeAcordoComAProximidadeDeGanhar()
    {
        Vibration.Cancel();

        if (parcialPlayer < 0.2f)
            Vibration.Vibrate(new long[2] {40, 50}, 0);
        else if (parcialPlayer < 0.4f)
            Vibration.Vibrate(new long[2] {40, 45}, 0);
        else if (parcialPlayer < 0.6f)
            Vibration.Vibrate(new long[2] {40, 40}, 0);
        else if (parcialPlayer < 0.8f)
            Vibration.Vibrate(new long[2] {40, 35}, 0);
        else
            Vibration.Vibrate(new long[2] {40, 30}, 0);
    }

    IEnumerator ControlaVibrador()
    {
        yield return new WaitUntil(() => running == true);
        VibraDeAcordoComAProximidadeDeGanhar();
        yield return new WaitUntil(() => parcialPlayer > 0.2f);
        VibraDeAcordoComAProximidadeDeGanhar();
        yield return new WaitUntil(() => parcialPlayer > 0.4f);
        VibraDeAcordoComAProximidadeDeGanhar();
        yield return new WaitUntil(() => parcialPlayer > 0.6f);
        VibraDeAcordoComAProximidadeDeGanhar();
        yield return new WaitUntil(() => parcialPlayer > 0.8f);
        VibraDeAcordoComAProximidadeDeGanhar();
    }

    private void MovePlayer()
    {
#if UNITY_ANDROID
        Player.localPosition += Vector3.down * Time.deltaTime * 100f;
#else
        Player.localPosition += Vector3.down * Time.deltaTime * 200f;
#endif
        //Player.transform.position += Vector3.down * Time.deltaTime * 20f;
        DebugPosition.text = Player.localPosition.ToString();
        //ajusta tamanho da sprite para simular proximidade
        parcialPlayer = ((Player.localPosition.y - 220) / -410);
        Player.localScale = Vector3.Lerp(PlayerScaleOffset, new Vector3(2.5f, 2.5f, 2.5f), parcialPlayer);

        
    }

    private void MovePutao()
    { 
        Perseguidor.localPosition += Vector3.down * Time.deltaTime * velocidadeDoPutao;
        //ajusta tamanho da sprite para simular proximidade
        parcialPerseguidor = ((Perseguidor.localPosition.y - 220) / -410);
        Perseguidor.localScale = Vector3.Lerp(PerseguidorScaleOffset, new Vector3(2.5f, 2.5f, 2.5f), parcialPerseguidor);
    }

    private void WinCondition()
    {
        if (!FimDeJogo)
        {
            
            //vitoria
            if (Player.localPosition.y < -190)
            {
                FimDeJogo = true;
                running = false;
                DistanciaFinal = Vector2.Distance(Player.localPosition, Perseguidor.localPosition);
                //Debug.Log(DistanciaFinal);
                StopCoroutine(ControlaVibrador());

                if (DistanciaFinal > 250)
                {
                    Cam.backgroundColor = new Color(1, 1, 1);
                    Vibration.Vibrate(2500);
                    perfect.gameObject.SetActive(true);
                    GetComponent<WinOrLoseScript>().Perfect("Luxuria");
                } 
                else
                {
                    Cam.backgroundColor = new Color(0, 1, 0);
                    Vibration.Vibrate(1500);
                    ganhou.gameObject.SetActive(true);
                    GetComponent<WinOrLoseScript>().Venceu("Luxuria");
                }
                    
            }
            //derrota
            if (Perseguidor.localPosition.y <= Player.localPosition.y)
            {
                FimDeJogo = true;
                StopCoroutine(ControlaVibrador());
                Vibration.Cancel();
                running = false;
                Cam.backgroundColor = new Color(1, 0, 0);
                perdeu.gameObject.SetActive(true);
                GetComponent<WinOrLoseScript>().Perdeu("Luxuria");
            }
        }

    }

    IEnumerator TelaDePreparacao(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        fujadoputao.gameObject.SetActive(false);
        instrucoes.gameObject.SetActive(false);
        instrucoesPC.gameObject.SetActive(false);
        Vibration.Vibrate(100);
        running = true;
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
                velocidadeDoPutao = 2f;
                break;
            case 2:
                velocidadeDoPutao = 4f;
                break;

            case 3:
                velocidadeDoPutao = 6f;
                break;
            case 4:
                velocidadeDoPutao = 8f;
                break;
            case 5:
                velocidadeDoPutao = 10f;
                break;
        }
    }
}
