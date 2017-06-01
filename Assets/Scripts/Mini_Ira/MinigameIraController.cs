using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MinigameIraController : MonoBehaviour
{
    //public int nAggressiveComments;
    //public int nPoliteComments;
    public RectTransform commentPrefab;
    public float maxTime;
    public Transform canvas;
    public Image timeBar;

    private static int difficulty = 1;

    // Caminhos para os diretórios
    private string pathToAggressiveFolder;
    private string pathToPoliteFolder;

    List<TextAsset> AgressiveComments;
    List<TextAsset> PoliteComments;

    // Temporizador
    private float timeLeft;

    private bool lost;

    void Start ()
    {
        // Inicializa os caminhos para os diretórios
        float buttonHeight = commentPrefab.rect.height;
        pathToAggressiveFolder = "AggressiveComments";
        pathToPoliteFolder = "PoliteComments";

        // Determina o número de comentários de acordo com a dificuldade
        int nAggressiveComments= difficulty;
        int nPoliteComments = (difficulty > 1)?(1):(2); // se dif = 1, nPoliteComments = 2. se dif > 1, nPoliteComments = 1

        // Cria os espaços onde serão instanciados os comentários
        int total = nAggressiveComments + nPoliteComments;
        List<float> commentSlots = new List<float>();

        //Carrega todos os comentarios para sortear depois
        AgressiveComments = Resources.LoadAll(pathToAggressiveFolder, typeof(TextAsset)).Cast<TextAsset>().ToList();
        PoliteComments = Resources.LoadAll(pathToPoliteFolder, typeof(TextAsset)).Cast<TextAsset>().ToList();

        for (int i = 0; i < total; i++)
        {
            commentSlots.Add(commentPrefab.position.y + i * buttonHeight);
        }

        // Gera os comentários agressivos
        commentSlots = GenerateComments(buttonHeight, pathToAggressiveFolder, nAggressiveComments, true, commentSlots);
        // Gera os comentários educados
        GenerateComments(buttonHeight, pathToPoliteFolder, nPoliteComments, false, commentSlots);

        // Desconta um certo tempo, dependendo da dificuldade
        maxTime -= (difficulty - 1) * 0.5f;

        // Inicia o temporizador
        timeLeft = maxTime;

        lost = false;
    }

    void Update()
    {
        // Atualiza o tempo
        if (canvas.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("2") && (timeLeft > 0))
        {
            timeLeft -= Time.deltaTime;
            timeBar.fillAmount = timeLeft / maxTime;
        }

        // Checa se o tempo acabou
        if (timeLeft <= 0 && !lost)
        {
            LoseGame();
        }
    }



    private List<float> GenerateComments(float buttonHeight, string pathToFolder, int nComments, bool isAggressive, List<float> heights)
    {
        
        // Obtém os comentários
        for (int i = 1; i <= nComments; i++)
        {
            TextAsset txtAsset;

            if (isAggressive)
            {
                txtAsset = AgressiveComments[Random.Range(0, AgressiveComments.Count)];
                //AgressiveComments.Remove(txtAsset);
            }
            else
            {
                txtAsset = PoliteComments[Random.Range(0, PoliteComments.Count)];
                //PoliteComments.Remove(txtAsset);
            }

            // Instancia o comentário
            RectTransform instance = Instantiate(commentPrefab, transform);
            // Escreve o comentário
            instance.GetComponent<CommentController>().SetComment(txtAsset.text);
            //print(txtAsset.text);
            // Define se o comentário é agressivo ou educado
            instance.GetComponent<CommentController>().isAggressive = isAggressive;
            // Posiciona o comentário a uma altura aleatória
            float randomHeight = heights[Random.Range(0, heights.Count)];
            instance.SetPositionAndRotation(
                new Vector3(instance.position.x, canvas.transform.position.y + randomHeight, instance.position.z), //modificado para evitar conflito com a cena do jogo principal
                new Quaternion(0, 0, 0, 0));
            heights.Remove(randomHeight);
        }
        return heights;
    }

    // Perde o jogo
    public void AggressiveCommentOnClick()
    {
        LoseGame();
    }

    // Ganha o jogo
    public void PoliteCommentOnClick()
    {
        WinGame();
    }

    private void LoseGame()
    {
        lost = true;
        LivesController.RemVidas();
        canvas.GetComponent<Animator>().SetBool("Lost", true);
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
    }

    private void WinGame()
    {
        // Checa se o jogador ganhou de Perfect (ganhar em menos de 1 segundo)
        if (maxTime - timeLeft <= 1)
        {
            LivesController.addVidas();
            canvas.GetComponent<Animator>().SetBool("Perfect", true);
            GameObject.Find("Player").GetComponent<PlayerMovement>().StartDelaySobeCarinha();
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
        }
        else
        {
            canvas.GetComponent<Animator>().SetBool("Won", true);
            GameObject.Find("Player").GetComponent<PlayerMovement>().StartDelaySobeCarinha();
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
        }

    }

    public static void SetDifficulty(int dif)
    {
        difficulty = dif;
    }

    public static int GetDifficulty()
    {
        return difficulty;
    }
}
