using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpController : MonoBehaviour
{
    private GameObject Player;
    public GameObject Pai;
    public GameObject Filho;
    public GameObject Espirito;
    public GameObject AguaBenta;
    [Space(20)]
    public Image pai_canvas;
    public Image filho_canvas;
    public Image espirito_canvas;


    private bool pai, filho, espirito, benzido;
    private GameObject[] Pais;
    private GameObject[] Filhos;
    private GameObject[] Espiritos;
    private GameObject[] AguasBentas;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!SceneController.paused && !SceneController.hasGameFinished)
        {
            verificaBencao();
            atualizaPainel();
        }

    }

    public void SpawnPowerUp(Vector3 position, Transform segmentoPai)
    {
        if(Random.Range(0, 100) > 50)
        {
            if (!pai)
            {
                Instantiate(Pai, position, Quaternion.identity, segmentoPai);
            }
            else if (!filho)
            {
                Instantiate(Filho, position, Quaternion.identity, segmentoPai);
            }
            else if (!espirito)
            {
                Instantiate(Espirito, position, Quaternion.identity, segmentoPai);
            }
            else
            {
                Instantiate(AguaBenta, position, Quaternion.identity, segmentoPai);
            }
        }
        else
        {
            Instantiate(AguaBenta, position, Quaternion.identity, segmentoPai);
        }
    }
    // evita spawnar palavras ja coletadas e elimina palavras ja coletadas presentes na tela
    public void coleta(string nome)
    {
        switch (nome)
        {
            case "pai":
                pai = true;
                Pais = GameObject.FindGameObjectsWithTag("pai");
                for (int i = 0; i < Pais.Length; i++)
                {
                    if (Pais[i].GetComponent<Renderer>().isVisible)
                    {
                        Pais[i].layer = LayerMask.NameToLayer("FILHO-PANEL");
                    }
                    else
                    {
                        Destroy(Pais[i]);
                    }

                }
                break;
            case "filho":
                filho = true;
                Filhos = GameObject.FindGameObjectsWithTag("filho");
                for (int i = 0; i < Filhos.Length; i++)
                {
                    if (Filhos[i].GetComponent<Renderer>().isVisible)
                    {
                        Filhos[i].layer = LayerMask.NameToLayer("FILHO-PANEL");
                    }
                    else
                    {
                        Destroy(Filhos[i]);
                    }
                }
                break;
            case "espirito":
                espirito = true;
                Espiritos = GameObject.FindGameObjectsWithTag("espirito");
                for (int i = 0; i < Espiritos.Length; i++)
                {
                    if (Espiritos[i].GetComponent<Renderer>().isVisible)
                    {
                        Espiritos[i].layer = LayerMask.NameToLayer("FILHO-PANEL");
                    }
                    else
                    {
                        Destroy(Espiritos[i]);
                    }
                }
                break;
            case "aguabenta":
                benzido = true;
                AguasBentas = GameObject.FindGameObjectsWithTag("aguabenta");
                for (int i = 0; i < AguasBentas.Length; i++) { Destroy(AguasBentas[i]); }
                break;
        }
    }

    private void verificaBencao()
    {
        if (pai == true && filho == true && espirito == true)
        {
            pai = false;
            filho = false;
            espirito = false;
            Player.GetComponent<PlayerMovement>().viraDeus();

        }

        if (benzido == true)
        {
            benzido = false;
            Player.GetComponent<PlayerMovement>().ficaBenzido();
        }
    }

    private void atualizaPainel()
    {
        if (Player.GetComponent<PlayerMovement>().imortal)
        {
            pai_canvas.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            filho_canvas.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            espirito_canvas.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            pai = false;
            filho = false;
            espirito = false;
            goto fim;
        }

        if (pai)
        {
            pai_canvas.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            pai_canvas.GetComponent<Image>().color = new Color(1, 1, 1, 0.125f);
        }
        if (filho)
        {
            filho_canvas.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            filho_canvas.GetComponent<Image>().color = new Color(1, 1, 1, 0.125f);
        }
        if (espirito)
        {
            espirito_canvas.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            espirito_canvas.GetComponent<Image>().color = new Color(1, 1, 1, 0.125f);
        }

        fim:
        ;
    }
}
