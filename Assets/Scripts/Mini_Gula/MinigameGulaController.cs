using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameGulaController : MonoBehaviour
{
    public MouthController[] mouths;
    public Sprite[] goodFoodSprites, badFoodSprites;
    public GameObject foodPrefab;
    public float foodDeltaDist; // Distância entre duas comidas seguidas

    private bool hasSetUpFoods; // True se já criou as comidas

    // Parâmetros de dificuldade do minigame
    private int nGoodFoods, nBadFoods, nTotalFoods;
    private float globalFoodSpeed;

    // Variáveis que contam quantas comidas o jogador comeu ou não comeu durante o jogo
    private int goodFoodsEaten, goodFoodsNotEaten, badFoodsNotEaten;

    // Lista com todas as comidas
    private List<GameObject>[] foods;

    // Dificuldade
    private static int difficulty = 1;

    // Constantes
    private const int MEIO = 0, ESQUERDA = 1, DIREITA = 2;

	void Start ()
    {
        // Inicialização
        hasSetUpFoods = false;
        goodFoodsEaten = 0;
        goodFoodsNotEaten = 0;
        badFoodsNotEaten = 0;

        // Ajusta os parâmetros de dificuldade
        AdjustParameters();
    }

    void Update ()
    {
        // Espera o minigame começar
        if (hasSetUpFoods == false && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PlayMinigameGula"))
        {
            hasSetUpFoods = true;
            SetUpFoods();
        }

        // Verifica se o jogador fez um Perfect
        // (se ele comeu todas as comidas boas e deixou de comer todas as ruins)
        if (goodFoodsEaten + badFoodsNotEaten == nTotalFoods && goodFoodsNotEaten == 0)
        {
            Perfect();
        }

        // Verifica se ele ganhou
        // (se deixou de comer todas as comidas ruins)
        else if (goodFoodsEaten + badFoodsNotEaten + goodFoodsNotEaten == nTotalFoods)
        {
            Win();
        }
	}

    // Baseado na dificuldade, ajusta os parâmetros do minigame, a saber: 
    // no. de alimentos saudáveis, no. de alimentos ruins, no. total de alimentos, velocidade global dos alimentos
    private void AdjustParameters()
    {
        switch (difficulty)
        {
            case 1:
                nTotalFoods = 5;
                nBadFoods = 1;
                nGoodFoods = 4;
                globalFoodSpeed = 20;
                break;
            case 2:
                nTotalFoods = 10;
                nBadFoods = 3;
                nGoodFoods = 7;
                globalFoodSpeed = 20;
                break;

            case 3:
                nTotalFoods = 20;
                nBadFoods = 5;
                nGoodFoods = 10;
                globalFoodSpeed = 30;
                break;
            case 4:
                nTotalFoods = 30;
                nBadFoods = 10;
                nGoodFoods = 20;
                globalFoodSpeed = 30;
                break;
            case 5:
                nTotalFoods = 40;
                nBadFoods = 10;
                nGoodFoods = 30;
                globalFoodSpeed = 40;
                break;
        }
    }

    // De acordo com os parâmetros de dificuldade, gera e configura as comidas
    private void SetUpFoods()
    {
        int _nGoodFoods = nGoodFoods, _nBadFoods = nBadFoods;

        // Três listas de comidas, uma para cada boca
        foods = new List<GameObject>[mouths.Length];
        for (int i = 0; i < mouths.Length; i++)
        {
            foods[i] = new List<GameObject>();
        }

        // Lista que guarda os índices de todas as comidas ruins em suas lanes
        List<int> badFoodIndexes = new List<int>();

        // Define a velocidade de cada lane, baseado na velocidade global, mas ainda mantendo uma certa aleatoriedade
        float[] foodSpeedByLane = new float[mouths.Length];
        for (int i = 0; i < mouths.Length; i++)
        {
            // A velocidade de uma dada lane será no mínimo 100% e no máximo 150% da velocidade global
            foodSpeedByLane[i] = Random.Range(globalFoodSpeed, globalFoodSpeed * 1.5f);
        }

        // Instancia as comidas
        int laneIndex = -1;
        for (int i = 0; i < nTotalFoods; i++)
        {
            // Seleciona a próxima boca (lane)
            laneIndex = (laneIndex + 1) % mouths.Length;

            // Sorteia se a comida será boa ou ruim
            bool type = FoodController.GOOD; // Valor padrão: comida boa

            // Primeiro, verifica se existe alguma comida ruim ao lado da comida que está para ser criada
            if (badFoodIndexes.Contains(foods[laneIndex].Count)
                || badFoodIndexes.Contains(foods[laneIndex].Count + 1)
                || badFoodIndexes.Contains(foods[laneIndex].Count - 1))
            {
                // Caso exista, a nova comida não pode ser ruim, para que o jogo não fique injusto
                type = FoodController.GOOD;
                _nGoodFoods--;
            }

            // Caso não exista, determina o tipo aleatoriamente
            else if (_nGoodFoods > 0 && _nBadFoods > 0)
            {
                type = (Random.Range(0f, 100f) >= 50f) ? (FoodController.GOOD) : (FoodController.BAD);
                if (type == FoodController.GOOD)
                {
                    _nGoodFoods--;
                }
                else
                {
                    _nBadFoods--;
                    badFoodIndexes.Add(foods[laneIndex].Count);
                }
            }
            else if (_nGoodFoods > 0)
            {
                type = FoodController.GOOD;
                _nGoodFoods--;
            }
            else
            {
                type = FoodController.BAD;
                _nBadFoods--;
                badFoodIndexes.Add(foods[laneIndex].Count);
            }

            // Cria a comida
            GameObject foodInstance = Instantiate
            (
                foodPrefab,
                foodPrefab.transform.position,
                Quaternion.identity,
                transform
            );

            // Ajusta o alinhamento da comida
            foodInstance.transform.position = new Vector3
            (
                mouths[laneIndex].transform.position.x,
                foodInstance.transform.position.y,
                foodInstance.transform.position.z
            );

            // Ajusta a altura da comida
            foodInstance.transform.position = new Vector3
            (
                foodInstance.transform.position.x,
                foodInstance.transform.position.y + foodDeltaDist * foods[laneIndex].Count,
                foodInstance.transform.position.z
            );

            // Ajusta a velocidade da comida
            foodInstance.GetComponent<FoodController>().Speed = foodSpeedByLane[laneIndex];

            // Ajusta a sprite da comida
            if (type == FoodController.GOOD)
            {
                foodInstance.GetComponent<FoodController>().Sprite = goodFoodSprites[Random.Range(0, goodFoodSprites.Length)];
            }
            else
            {
                foodInstance.GetComponent<FoodController>().Sprite = badFoodSprites[Random.Range(0, badFoodSprites.Length)];
            }

            // Ajusta o tipo da comida
            foodInstance.GetComponent<FoodController>().Type = type;

            // Adiciona a comida à lista
            foods[laneIndex].Add(foodInstance);
        }
    }

    // Perde o minigame
    private void Lose()
    {
        DestroyAllFoods();
        GetComponent<Animator>().SetTrigger("Lose");
        LivesController.RemVidas();
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
    }

    // Vence o minigame
    private void Win()
    {
        GetComponent<Animator>().SetTrigger("Win");
        GameObject.Find("Player").GetComponent<PlayerMovement>().StartDelaySobeCarinha();
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
    }

    // Vence o minigame com Perfect
    private void Perfect()
    {
        GetComponent<Animator>().SetTrigger("Perfect");
        LivesController.addVidas();
        GameObject.Find("Player").GetComponent<PlayerMovement>().StartDelaySobeCarinha();
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
    }

    // Abre todas as bocas
    public void OpenAllMouths()
    {
        foreach (MouthController mouth in mouths)
        {
            mouth.OpenMouth();
        }
    }

    // Destrói todas as comidas
    private void DestroyAllFoods()
    {
        for (int i = 0; i < foods.Length; i++)
        {
            foreach (GameObject food in foods[i])
            {
                food.SetActive(false);
            }
        }
    }

    // Chamada quando uma boca come uma comida ruim
    public void OnEatBadFood()
    {
        Lose();
    }

    // Chamada quando uma boca come uma comida boa
    public void OnEatGoodFood()
    {
        goodFoodsEaten++;
    }

    // Chamada quando uma boca se protege de uma comida ruim
    public void OnNotEatBadFood()
    {
        badFoodsNotEaten++;
    }

    // Chamada quando uma boca se protege de uma comida boa
    public void OnNotEatGoodFood()
    {
        goodFoodsNotEaten++;
    }

    public static void SetDifficulty(int dif)
    {
        difficulty = dif;
    }
}
