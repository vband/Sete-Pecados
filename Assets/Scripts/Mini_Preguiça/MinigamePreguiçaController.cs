using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePreguiçaController : MonoBehaviour
{
    public Animator animator;
    public Transform mainCamera;
    public Transform background;
    public Transform bedUpPrefab;
    public Transform bedDownPrefab;
    public Transform bedUpAndDownPrefab;
    public int difficulty;

    private int numberOfBeds; // Número de camas que irão aparecer
    private float bedSpeed; // Velocidade de deslocamento das camas
    private float deltaDistance; // Intervalo de distância entre cada cama
    private List<Transform> backgrounds; // Lista que armazena as imagens dos planos de fundo
    private float bgWidth; // Largura do plano de fundo
    private float cameraWidth; // Largura do quadro da câmera
    private List<Transform> beds; // Lista que armazena as camas
    private List<int> bedControls; // Lista que armazena os controles certos de cada cama: cima, baixo, ou cima e baixo
    private List<bool> wasBedHit; // Lista que armazena em quais camas o jogador já bateu atualmente
    private bool isTouchingBed; // True se o jogador estiver tocando em uma cama
    private int nBedsTouched; // Quantidade atual de camas que o jogador já passou
    private float buttonPressedTime = 0; // Tempo em que o jogador pressionou a seta para cima OU baixo
    private float timeWindow = 0.02f; // Intervalo de tempo em segundos entre uma tecla e outra, para que se considere
                                      // que elas foram tecladas ao mesmo tempo
    private float UpAndDownPressTime = 0; // Tempo em que o jogador pressionou a seta para cima E para baixo

    // Constantes
    const int UP = 1;
    const int DOWN = 2;
    const int UPANDDOWN = 3;

	void Start ()
    {
        // Inicialização
        isTouchingBed = false;
        nBedsTouched = 0;
        beds = new List<Transform>();
        bedControls = new List<int>();
        wasBedHit = new List<bool>();
        backgrounds = new List<Transform>();
        bgWidth = background.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        cameraWidth = mainCamera.GetComponent<Camera>().rect.size.x;

        // Ajusta os parâmetros do jogo de acordo com a dificuldade
        SetUpDifficulty();

        // Vetor com os controles que o microgame reconhece
        int[] controls = { UP, DOWN, UPANDDOWN };

        // Cria as camas
        for (int i = 0; i < numberOfBeds; i++)
        {
            // Define um controle aleatório para cada cama criada
            int index = Random.Range(0, controls.Length);
            bedControls.Add(controls[index]);

            // Seleciona a prefab adequada
            Transform prefab;
            if (index == 0)
                prefab = bedUpPrefab;
            else if (index == 1)
                prefab = bedDownPrefab;
            else
                prefab = bedUpAndDownPrefab;
            
            // Instancia
            Transform instance = Instantiate(
                prefab,
                new Vector3(transform.position.x + deltaDistance * (i + 1), prefab.position.y),
                new Quaternion(0, 0, 0, 0));
            beds.Add(instance);

            // Determina que essa cama ainda não foi atingida
            wasBedHit.Add(false);
        }

        // Cria os planos de fundo
        float lastBedPos = beds[beds.Count - 1].position.x;
        float lastBgPos = background.position.x;

        while(lastBgPos + cameraWidth < lastBedPos + cameraWidth)
        {
            Transform instance = Instantiate(
                background,
                new Vector3(lastBgPos + bgWidth, background.position.y),
                new Quaternion());
            lastBgPos = lastBgPos + bgWidth;
            backgrounds.Add(instance);
        }
	}
	
	void Update ()
    {
        // Se o jogo já começou, permite que ele aconteça
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("2"))
        {
            // Computa o tempo em que recebeu uma tecla
            ReceiveKeyPress();
            // Desloca as camas
            MoveBeds();
            // Desloca o plano de fundo
            MoveBackground();

            if (isTouchingBed)
            {
                // Permite que o jogador acerte uma cama, caso esteja tocando em uma
                HitBed();
            }
            // Se o jogador apertar uma seta sem estar tocando numa cama, ele perde
            else if (PlayerPressedKey())
            {
                //Debug.Log("Erooou!1");
                LoseGame();
            }
        }
	}

    private void SetUpDifficulty()
    {
        switch (difficulty)
        {
            case 1:
                numberOfBeds = 3;
                bedSpeed = 0.1f;
                deltaDistance = 5;
                break;

            case 2:
                numberOfBeds = 6;
                bedSpeed = 0.2f;
                deltaDistance = 5;
                break;

            case 3:
                numberOfBeds = 8;
                bedSpeed = 0.3f;
                deltaDistance = 5;
                break;

            case 4:
                numberOfBeds = 10;
                bedSpeed = 0.4f;
                deltaDistance = 8;
                break;

            case 5:
                numberOfBeds = 15;
                bedSpeed = 0.45f;
                deltaDistance = 8;
                break;
        }
    }

    // Desloca as camas
    private void MoveBeds()
    {
        foreach (Transform bed in beds)
        {
            bed.GetComponent<Rigidbody2D>().MovePosition(
                (Vector2)bed.position - new Vector2(bedSpeed, 0));
        }
    }

    // Desloca o plano de fundo
    private void MoveBackground()
    {
        foreach (Transform bg in backgrounds)
        {
            bg.GetComponent<Rigidbody2D>().MovePosition(
                (Vector2)bg.position + Vector2.left * bedSpeed);
        }
    }

    private void WinGame()
    {
        DespawnBeds();
        animator.SetTrigger("win");
    }

    private void LoseGame()
    {
        DespawnBeds();
        animator.SetTrigger("lose");
    }

    private void DespawnBeds()
    {
        foreach (Transform bed in beds)
        {
            bed.gameObject.SetActive(false);
        }
    }

    // Checa se o player começou a colidir com alguma cama
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Animator>().SetBool("isRunning", true);
        isTouchingBed = true;
        nBedsTouched++;
    }

    // Checa se o player parou de colidir com alguma cama
    public void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<Animator>().SetBool("isRunning", false);
        isTouchingBed = false;
        UpAndDownPressTime = 0;

        // Checa se o jogador deixou de bater nessa cama
        if (!wasBedHit[nBedsTouched - 1])
        {
            //Debug.Log("Deixou passar");
            LoseGame();
        }

        // Checa se o jogador ganhou
        if (nBedsTouched == numberOfBeds && wasBedHit[nBedsTouched - 1])
        {
            WinGame();
        }
    }

    // Computa o tempo em que recebeu uma tecla
    private void ReceiveKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            buttonPressedTime = Time.time;
        }
    }

    // Retorna true se, no frame atual, o jogador pressionou no mínimo uma tecla
    private bool PlayerPressedKey()
    {
        return (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow));
    }

    // Retorna true se, em um tempo muito rápido, o jogador pressionou exatamente duas teclas
    private bool PlayerPressedDoubleKeys()
    {
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            if (Time.time - buttonPressedTime <= timeWindow)
            {
                return true;
            }
        }

        return false;
    }

    // Verifica se o jogador apertou os botões certos de acordo com a cama em que está tocando
    private void HitBed()
    {
        // Se a cama for de seta para cima ou baixo
        if (bedControls[nBedsTouched - 1] != UPANDDOWN)
        {
            // Se o jogador apertou as duas setas ao mesmo tempo
            if (PlayerPressedDoubleKeys())
            {
                //Debug.Log("Erooou!2");
                LoseGame();
            }

            // Se o jogador apertou apenas uma seta
            if (PlayerPressedKey())
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) && bedControls[nBedsTouched - 1] == UP)
                {
                    //Debug.Log("Acertou");
                    //beds[nBedsTouched - 1].gameObject.SetActive(false);
                    beds[nBedsTouched - 1].GetComponent<Animator>().SetTrigger("hitBed");
                    wasBedHit[nBedsTouched - 1] = true;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) && bedControls[nBedsTouched - 1] == DOWN)
                {
                    //Debug.Log("Acertou");
                    //beds[nBedsTouched - 1].gameObject.SetActive(false);
                    beds[nBedsTouched - 1].GetComponent<Animator>().SetTrigger("hitBed");
                    wasBedHit[nBedsTouched - 1] = true;
                }
                else
                {
                    //Debug.Log("Erooou!3");
                    LoseGame();
                }
            }
        }
        // Se a cama for das duas setas
        else
        {
            if (PlayerPressedKey())
            {
                // Computa o tempo em que o jogador apertou a primeira seta
                UpAndDownPressTime = Time.time;
            }
            
            if (PlayerPressedDoubleKeys())
            {
                //Debug.Log("Acertou");
                //beds[nBedsTouched - 1].gameObject.SetActive(false);
                beds[nBedsTouched - 1].GetComponent<Animator>().SetTrigger("hitBed");
                UpAndDownPressTime = 0;
                wasBedHit[nBedsTouched - 1] = true;
            }

            // Se o jogador não apertou as duas setas ao mesmo tempo, ele errou
            if (UpAndDownPressTime != 0 && Time.time - UpAndDownPressTime > timeWindow)
            {
                //Debug.Log("Erooou!4");
                LoseGame();
            }
        }
    }
}
