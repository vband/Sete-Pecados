using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MinigameIraController : MonoBehaviour
{
    public int nAggressiveComments;
    public int nPoliteComments;
    public RectTransform commentPrefab;
    public Text timer;
    public float maxTime;
    public Transform canvas;

    // Caminhos para os diretórios
    private string pathToAggressiveFolder;
    private string pathToPoliteFolder;
    private string fileExtension;

    // Temporizador
    private float timeLeft;

    void Start ()
    {
        // Inicializa os caminhos para o s diretórios
        float buttonHeight = commentPrefab.rect.height;
        //pathToAggressiveFolder = "Assets\\Text\\AggressiveComments";
        pathToAggressiveFolder = Application.dataPath + "\\Text\\AggressiveComments";
        //pathToPoliteFolder = "Assets\\Text\\PoliteComments";
        pathToPoliteFolder = Application.dataPath + "\\Text\\PoliteComments";
        fileExtension = ".txt";

        // Cria os espaços onde serão instanciados os comentários
        int total = nAggressiveComments + nPoliteComments;
        List<float> commentSlots = new List<float>();

        for (int i = 0; i < total; i++)
        {
            commentSlots.Add(commentPrefab.position.y + i * buttonHeight);
        }

        // Gera os comentários agressivos
        commentSlots = GenerateComments(buttonHeight, pathToAggressiveFolder, fileExtension, nAggressiveComments, true, commentSlots);
        // Gera os comentários educados
        GenerateComments(buttonHeight, pathToPoliteFolder, fileExtension, nPoliteComments, false, commentSlots);

        // Inicia o temporizador
        timeLeft = maxTime;

        // Inicia o temporizador da tela
        timer.text = timeLeft.ToString();
    }

    void Update()
    {
        // Atualiza o tempo
        if (canvas.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("2") && (timeLeft > 0))
        {
            timeLeft -= Time.deltaTime;
            int t = (int)timeLeft;
            timer.text = t.ToString();
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

    private List<float> GenerateComments(float buttonHeight, string pathToFolder, string fileExtension, int nComments, bool isAggressive, List<float> heights)
    {
        // Obtém os comentários
        for (int i = 1; i <= nComments; i++)
        {
            string path = "";

            if (isAggressive)
            {
                string file = "\\aggressive";
                file = string.Concat(file, i.ToString());
                file = string.Concat(file, fileExtension);
                path = string.Concat(pathToFolder, file);
            }
            else
            {
                string file = "\\polite";
                file = string.Concat(file, i.ToString());
                file = string.Concat(file, fileExtension);
                path = string.Concat(pathToFolder, file);
            }

            // Abre o arquivo
            using (FileStream fs = File.Open(path, FileMode.Open)) 
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);

                // Lê o arquivo
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    // Instancia o comentário
                    RectTransform instance = Instantiate(commentPrefab, transform);
                    // Escreve o comentário
                    instance.GetComponent<CommentController>().SetComment(temp.GetString(b));
                    // Define se o comentário é agressivo ou educado
                    instance.GetComponent<CommentController>().isAggressive = isAggressive;

                    // Posiciona o comentário a uma altura aleatória
                    float randomHeight = heights[Random.Range(0, heights.Count)];
                    instance.SetPositionAndRotation(
                        new Vector3(instance.position.x, randomHeight, instance.position.z),
                        new Quaternion(0, 0, 0, 0));
                    heights.Remove(randomHeight);
                }
            }
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

    
}
