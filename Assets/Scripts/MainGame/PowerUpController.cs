using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour {

    public GameObject Player;
    public GameObject Limite_camera;
    [Space(20)]
    public GameObject Pai;
    public GameObject Filho;
    public GameObject Espirito;
    public GameObject AguaBenta;
    [Space(20)]
        

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
        }
            
	}

    private void SpawnPowerUp()
    {
        if ((Time.fixedTime - lastSpawn) > 5 && Player.GetComponent<PlayerMovement>().imortal == false) {
            if (pai == false)
            {
                Instantiate(Pai, Limite_camera.transform.position + new Vector3(Random.Range(5, 25), Random.Range(2, 7), 0), Quaternion.identity, GetComponent<Transform>());
            }
            if (filho == false)
            {
                Instantiate(Filho, Limite_camera.transform.position + new Vector3(Random.Range(5, 25), Random.Range(2, 7), 0), Quaternion.identity, GetComponent<Transform>());
            }
            if (espirito == false)
            {
                Instantiate(Espirito, Limite_camera.transform.position + new Vector3(Random.Range(5, 25), Random.Range(2, 7), 0), Quaternion.identity, GetComponent<Transform>());
            }
            lastSpawn = Time.fixedTime;
            if (benzido == false)
            {
                Instantiate(AguaBenta, Limite_camera.transform.position + new Vector3(Random.Range(5, 25), Random.Range(2, 7), 0), Quaternion.identity, GetComponent<Transform>());
            }
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
                for(int i = 0; i < Pais.Length; i++) { Destroy(Pais[i], 1.5f); }
                break;
            case "filho":
                filho = true;
                Filhos = GameObject.FindGameObjectsWithTag("filho");
                for (int i = 0; i < Filhos.Length; i++) { Destroy(Filhos[i], 1.5f); }
                break;
            case "espirito":
                espirito = true;
                Espiritos = GameObject.FindGameObjectsWithTag("espirito");
                for (int i = 0; i < Espiritos.Length; i++) { Destroy(Espiritos[i], 1.5f); }
                break;
            case "aguabenta":
                benzido = true;
                AguasBentas = GameObject.FindGameObjectsWithTag("aguabenta");
                for (int i = 0; i < AguasBentas.Length; i++) { Destroy(AguasBentas[i], 1.5f); }
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
    
}
