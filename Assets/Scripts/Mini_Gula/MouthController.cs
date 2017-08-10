using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouthController : MonoBehaviour
{
    public Sprite openMouth, closedMouth;
    public MinigameGulaController gameController;

    private Image imageController;
    private bool state;

    // Constantes públicas
    public static bool OPEN
    {
        get
        {
            return true;
        }
    }
    public static bool CLOSED
    {
        get
        {
            return false;
        }
    }

    // Getter para o estado
    public bool State
    {
        get
        {
            return state;
        }
    }

    void Start ()
    {
        imageController = GetComponent<Image>();
        state = OPEN;
	}
	
	void Update ()
    {
        
	}

    public void OnClick()
    {
        // Se a boca estiver aberta, fecha
        if (state == OPEN)
        {
            // Manda o script controlador do minigame abrir todas as bocas
            gameController.OpenAllMouths();

            // Fecha essa boca
            CloseMouth();
        }

        // Se estiver fechada, abre
        else if (state == CLOSED)
        {
            // Abre essa boca
            OpenMouth();
        }
    }

    // Abre a boca
    public void OpenMouth()
    {
        imageController.sprite = openMouth;
        state = OPEN;
    }

    // Fecha a boca
    public void CloseMouth()
    {
        imageController.sprite = closedMouth;
        state = CLOSED;
    }

    // Controla a colisão com comidas
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Se a comida for ruim
        if (collision.gameObject.GetComponent<FoodController>().Type == FoodController.BAD)
        {
            // Se a boca estiver aberta
            if (state == OPEN)
            {
                gameController.OnEatBadFood();
            }

            // Se estiver fechada
            if (state == CLOSED)
            {
                gameController.OnNotEatBadFood();
            }
        }

        // Se a comida for boa
        if (collision.gameObject.GetComponent<FoodController>().Type == FoodController.GOOD)
        {
            // Se a boca estiver aberta
            if (state == OPEN)
            {
                gameController.OnEatGoodFood();
            }

            // Se estiver fechada
            if (state == CLOSED)
            {
                gameController.OnNotEatGoodFood();
            }
        }

        // Desativa a comida
        collision.gameObject.SetActive(false);
    }
}
