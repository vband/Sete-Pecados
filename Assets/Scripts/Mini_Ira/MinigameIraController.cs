using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

    // Temporizador
    private float timeLeft;

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
        if (timeLeft <= 0)
        {
            canvas.GetComponent<Animator>().SetBool("Lost", true);
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("MainMenu");
        }

        // Checa se o minigame acabou
        if (canvas.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("5"))
        {
            // Volta para o jogo principal
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("MainMenu");
        }
    }

    private List<float> GenerateComments(float buttonHeight, string pathToFolder, int nComments, bool isAggressive, List<float> heights)
    {
        // Obtém os comentários
        for (int i = 1; i <= nComments; i++)
        {
            string path = "";

            if (isAggressive)
            {
                string file = "\\aggressive";
                file = string.Concat(file, i.ToString());
                path = string.Concat(pathToFolder, file);
            }
            else
            {
                string file = "\\polite";
                file = string.Concat(file, i.ToString());
                path = string.Concat(pathToFolder, file);
            }

            // Abre o arquivo
            TextAsset txtAsset = (TextAsset)Resources.Load(path);
            // Instancia o comentário
            RectTransform instance = Instantiate(commentPrefab, transform);
            // Escreve o comentário
            instance.GetComponent<CommentController>().SetComment(txtAsset.text);
            // Define se o comentário é agressivo ou educado
            instance.GetComponent<CommentController>().isAggressive = isAggressive;
            // Posiciona o comentário a uma altura aleatória
            float randomHeight = heights[Random.Range(0, heights.Count)];
            instance.SetPositionAndRotation(
                new Vector3(instance.position.x, randomHeight, instance.position.z),
                new Quaternion(0, 0, 0, 0));
            heights.Remove(randomHeight);
        }
        return heights;
    }
	
    public void AggressiveCommentOnClick()
    {
        canvas.GetComponent<Animator>().SetBool("Lost", true);
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("MainMenu");
    }

    public void PoliteCommentOnClick()
    {
        canvas.GetComponent<Animator>().SetBool("Won", true);
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
        
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
