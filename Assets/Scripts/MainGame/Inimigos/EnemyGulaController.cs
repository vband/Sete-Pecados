using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGulaController : MonoBehaviour
{
    public float distanceToActivate; // Distância até o jogador, a partir da qual o inimigo será ativado
    //public float fallDuration; // Duração, em segundos, da animação que faz o fluido escorrer
    //public int totalNumberOfExtensions; // Número de vezes que o collider terá que ser estendido

    private Transform player;
    //private BoxCollider2D boxCollider;
    //private float deltaFallAnimation; // Intervalo de tempo entre as vezes em que o tamanho do fluido aumenta, devido à sua viscosidade
    private bool isActive; // True se o inimigo já foi ativado (engatilhado) pelo jogador
    private float timer; // Cronômetro
    //private float colliderInitialHeight; // Altura inicial do colisor. Ao longo do processo, a altura dele irá aumentar
    //private int currentNumberOfExtensions; // Quantidade atual de vezes em que o colisor foi estendido

	void Start ()
    {
        // Inicialização
        player = GameObject.Find("Player").transform;
        //boxCollider = GetComponent<BoxCollider2D>();
        isActive = false;
        //deltaFallAnimation = fallDuration / totalNumberOfExtensions;
        //colliderInitialHeight = boxCollider.size.y;
        //currentNumberOfExtensions = 0;
    }
	
	void Update ()
    {
        if (!isActive)
        {
            Activate();
        }

		if (isActive)
        {
            timer += Time.deltaTime;

            /*
            // Verifica se chegou a hora de estender o collider
            if (timer >= deltaFallAnimation && currentNumberOfExtensions < totalNumberOfExtensions)
            {
                ExtendCollider();
            }
            */
        }
	}

    // Engatilha o inimigo quando o jogador se aproxima
    private void Activate()
    {
        // Verifica se o jogador se aoriximou
        if (Mathf.Abs(player.position.x - transform.position.x) <= distanceToActivate)
        {
            // Ativa o Animator, e ajusta as variáveis que controlam o procedimento
            GetComponent<Animator>().enabled = true;
            isActive = true;
            timer = 0;
        }
    }

    // Estende o colisor, acompanhando a animação que faz o fluido escorrer até o chão
    private void ExtendCollider()
    {
        /*
        // Incrementa a altura do collider
        boxCollider.size = new Vector2(boxCollider.size.x, boxCollider.size.y + colliderInitialHeight);

        // Ajusta o offset do collider
        boxCollider.offset = new Vector2(boxCollider.offset.x, boxCollider.offset.y - colliderInitialHeight / 2);

        // Atualiza as variáveis usadas para controlar o procedimento
        timer = 0;
        currentNumberOfExtensions++;
        */
    }
}
