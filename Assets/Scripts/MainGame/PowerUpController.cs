using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpController : MonoBehaviour {

    public GameObject Player;
    public GameObject Limite_camera;
    [Space(20)]
    public GameObject Pai;
    public GameObject Filho;
    public GameObject Espirito;
    public GameObject AguaBenta;
    [Space(20)]
    public Image pai_canvas;
    public Image filho_canvas;
    public Image espirito_canvas;
        

    private bool pai, filho, espirito, benzido;
    private float lastSpawn;
    private GameObject[] Pais;
    private GameObject[] Filhos;
    private GameObject[] Espiritos;
    private GameObject[] AguasBentas;

    // Use this for initialization
    void Start () {
        lastSpawn = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (!SceneController.paused)
        {
            SpawnPowerUp();
            verificaBencao();
            atualizaPainel();
        }
            
	}

    private void SpawnPowerUp() //a cada 5 segundos sorteia um power up para espawnar
    {
        if ((Time.fixedTime - lastSpawn) > 5 && Player.GetComponent<PlayerMovement>().imortal == false) {

            switch (Random.Range(0, 3))
            {
                case 0:
                    if (pai == false)
                    {
                        Instantiate(Pai, Limite_camera.transform.position + new Vector3(5, Random.Range(2, 7), 0), Quaternion.identity, GetComponent<Transform>());
                    }
                    else
                    {
                        Instantiate(AguaBenta, Limite_camera.transform.position + new Vector3(5, Random.Range(2, 7), 0), Quaternion.identity, GetComponent<Transform>());
                    }
                    break;
                case 1:
                    if (filho == false)
                    {
                        Instantiate(Filho, Limite_camera.transform.position + new Vector3(5, Random.Range(2, 7), 0), Quaternion.identity, GetComponent<Transform>());
                    }
                    else
                    {
                        Instantiate(AguaBenta, Limite_camera.transform.position + new Vector3(5, Random.Range(2, 7), 0), Quaternion.identity, GetComponent<Transform>());
                    }
                    break;
                case 2:
                    if (espirito == false)
                    {
                        Instantiate(Espirito, Limite_camera.transform.position + new Vector3(5, Random.Range(2, 7), 0), Quaternion.identity, GetComponent<Transform>());
                    }
                    else
                    {
                        Instantiate(AguaBenta, Limite_camera.transform.position + new Vector3(5, Random.Range(2, 7), 0), Quaternion.identity, GetComponent<Transform>());
                    }
                    break;
                case 3:
                    if (benzido == false)
                    {
                        Instantiate(AguaBenta, Limite_camera.transform.position + new Vector3(5, Random.Range(2, 7), 0), Quaternion.identity, GetComponent<Transform>());
                    }
                    break;
            }
            lastSpawn = Time.fixedTime;
            
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
                for(int i = 0; i < Pais.Length; i++) { Destroy(Pais[i], 0.1f); }
                break;
            case "filho":
                filho = true;
                Filhos = GameObject.FindGameObjectsWithTag("filho");
                for (int i = 0; i < Filhos.Length; i++) { Destroy(Filhos[i], 0.1f); }
                break;
            case "espirito":
                espirito = true;
                Espiritos = GameObject.FindGameObjectsWithTag("espirito");
                for (int i = 0; i < Espiritos.Length; i++) { Destroy(Espiritos[i], 0.1f); }
                break;
            case "aguabenta":
                benzido = true;
                AguasBentas = GameObject.FindGameObjectsWithTag("aguabenta");
                for (int i = 0; i < AguasBentas.Length; i++) { Destroy(AguasBentas[i], 0.1f); }
                break;
        }
    }

    private void verificaBencao()
    {
        if(pai == true && filho == true && espirito == true)
        {
            pai = false;
            filho = false;
            espirito = false;
            Player.GetComponent<PlayerMovement>().viraDeus();
        }

        if(benzido == true)
        {
            benzido = false;
            Player.GetComponent<PlayerMovement>().ficaBenzido();
        }
    }

    private void atualizaPainel()
    {
        if(pai)
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
    }
    
}
