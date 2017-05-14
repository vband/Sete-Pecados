using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePreguiçaController : MonoBehaviour
{
    public Transform bedUpPrefab;
    public Transform bedDownPrefab;
    public Transform bedUpAndDownPrefab;
    public int numberOfBeds; // Número de camas que irão aparecer
    public float bedSpeed; // Velocidade de deslocamento das camas
    public float deltaDistance; // Intervalo de distância entre cada cama

    private List<Transform> beds; // Lista que armazena as camas
    private List<int> bedControls; // Lista que armazena os controles certos de cada cama: cima, baixo, ou cima e baixo
    private bool isTouchingBed; // True se o jogador estiver tocando em uma cama
    private int nBedsTouched; // Quantidade atual de camas que o jogador já passou
    private float buttonPressedTime = 0; // Tempo em que o jogador pressionou a seta para cima OU baixo
    private float timeWindow = 0.05f; // Intervalo de tempo em segundos entre uma tecla e outra, para que se considere
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

        // Vetor com os controles que o microgame reconhece
        int[] controls = { UP, DOWN, UPANDDOWN };
        
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
            
            // Cria as camas
            Transform instance = Instantiate(
                prefab,
                transform.position + new Vector3(deltaDistance * (i+1), 0, 0),
                new Quaternion(0, 0, 0, 0));
            beds.Add(instance);
        }
	}
	
	void Update ()
    {
        // Computa o tempo em que recebeu uma tecla
        ReceiveKeyPress();
        // Desloca as camas
        MoveBeds();

        if (isTouchingBed)
        {
            // Permite que o jogador acerte uma cama, caso esteja tocando em uma
            HitBed();
        }
        // Se o jogador apertar uma seta sem estar tocando numa cama, ele perde
        else if (PlayerPressedKey())
        {
            Debug.Log("Erooou!1");
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
                Debug.Log("Erooou!2");
                return;
            }

            // Se o jogador apertou apenas uma seta
            if (PlayerPressedKey())
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) && bedControls[nBedsTouched - 1] == UP)
                {
                    Debug.Log("Acertou");
                    beds[nBedsTouched - 1].gameObject.SetActive(false);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) && bedControls[nBedsTouched - 1] == DOWN)
                {
                    Debug.Log("Acertou");
                    beds[nBedsTouched - 1].gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("Erooou!3");
                    return;
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
                Debug.Log("Acertou");
                beds[nBedsTouched - 1].gameObject.SetActive(false);
                UpAndDownPressTime = 0;
            }
            
            // Se o jogador não apertou as duas setas ao mesmo tempo, ele errou
            if (UpAndDownPressTime != 0 && Time.time - UpAndDownPressTime > timeWindow)
            {
                Debug.Log("Erooou!4");
                UpAndDownPressTime = 0;
            }
        }
    }
}
