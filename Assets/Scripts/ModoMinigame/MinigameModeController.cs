using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameModeController : MonoBehaviour
{
    [HideInInspector] public static bool created = false;

    private int lives, difficulty, score, highScore, amountOfGamesWonInARow, amountOfGamesPlayed;
    private List<string> minigamePool = new List<string> { "Ganancia", "Gula", "Inveja", "Ira", "Luxuria", "Orgulho", "Preguiça" };
    private AsyncOperation loadScene;

    private const int MAX_LIVES = 3;

    private void Awake()
    {
        CheckExistance();
    }

    private void CheckExistance()
    {
        if (!created)
        {
            // Esta é a primeira instância
            DontDestroyOnLoad(gameObject);
            created = true;
            Init();
        }
        else
        {
            // Esta não é a primeira instância
            Destroy(gameObject);
        }
        return;
    }

    private void Init()
    {
        lives = MAX_LIVES;
        difficulty = 1;
        score = 0;
        highScore = PlayerPrefs.GetInt("ModoMinigameHighScore", 0);
        amountOfGamesWonInARow = 0;
        amountOfGamesPlayed = 0;
        UpdateDifficulty();
        UpdateDisplay();
        StartCoroutine(GoToNextMinigame(null));
        return;
    }

    private IEnumerator GoToNextMinigame(string lastMinigame)
    {
        string nextMinigame;
        if (lastMinigame == null)
        {
            // Sorteia o próximo minigame
            nextMinigame = minigamePool[Random.Range(0, minigamePool.Count)];
        }
        else
        {
            // Sorteia o próximo minigame, mas exclui o último jogado, para eliminar repetições sucessivas
            minigamePool.Remove(lastMinigame);
            nextMinigame = minigamePool[Random.Range(0, minigamePool.Count)];
            minigamePool.Add(lastMinigame);
        }

        // Espera x segundos
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(nextMinigame);
    }

    public void OnMinigameFinished(bool won, string lastMinigame)
    {
        StartCoroutine(LoadMyScene(won, lastMinigame));
    }

    private IEnumerator LoadMyScene(bool won, string lastMinigame)
    {
        yield return new WaitForSeconds(1f);
        loadScene = SceneManager.LoadSceneAsync("ModoMinigame_Transição");
        while (!loadScene.isDone)
        {
            yield return null;
        }

        UpdateGameState(won, lastMinigame);
    }

    private void UpdateGameState(bool won, string lastMinigame)
    {
        amountOfGamesPlayed++;

        if (!won)
        {
            amountOfGamesWonInARow = 0;
            lives--;

            if (lives <= 0)
            {
                PlayerMovement.pessoasSalvas = score;
                created = false;
                SceneManager.LoadScene("GameOver");
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            score++;

            if (lives < MAX_LIVES)
            {
                amountOfGamesWonInARow++;

                if (amountOfGamesWonInARow == 3)
                {
                    lives++;
                    amountOfGamesWonInARow = 0;
                }
            }
        }


        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("ModoMinigameHighScore", highScore);
        }
        
        if (amountOfGamesPlayed % 5 == 0)
        {
            difficulty++;
            UpdateDifficulty();
        }

        UpdateDisplay();
        StartCoroutine(GoToNextMinigame(lastMinigame));
    }

    
    private void UpdateDisplay()
    {
        MinigameModeDisplayController displayController = FindObjectOfType<MinigameModeDisplayController>();
        displayController.UpdateDisplay(lives, score, highScore);
    }

    private void UpdateDifficulty() //mecanica = MinigameGananciaController
    {
            MinigameIraController.SetDifficulty(difficulty);
            MinigamePreguiçaController.SetDifficulty(difficulty);
            mecanica.SetDifficulty(difficulty);
            MinigameGulaController.SetDifficulty(difficulty);
            MinigameOrgulhoController.SetDifficulty(difficulty);
            MiniGameGananciaController.SetDifficulty(difficulty);
            MinigameLuxuriaController.SetDifficulty(difficulty);
            MinigameInvejaController.SetDifficulty(difficulty);
    }
}
