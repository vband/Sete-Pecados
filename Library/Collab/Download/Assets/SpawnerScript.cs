using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerScript : MonoBehaviour
{

    public GameObject[] PowerUps;
    public GameObject[] InimigosTerrestres;
    public GameObject[] InimigosAlados;
    private bool[] tempConfig;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpawnPoint")
        {
            tempConfig = collision.GetComponent<SpawnOptions>().getConfig();

            if (tempConfig[0] && tempConfig[1])
            {
                if(Random.Range(0, 100) > 50)
                {
                    GetComponent<PowerUpController>().SpawnPowerUp(collision.transform.position);
                }
                else
                {
                    Instantiate(InimigosTerrestres[Random.Range(0, InimigosTerrestres.Length)],
                                   collision.transform.position, Quaternion.identity);
                }

            }
            else if (tempConfig[0] && tempConfig[2])
            {
                if (Random.Range(0, 100) > 50)
                {
                    GetComponent<PowerUpController>().SpawnPowerUp(collision.transform.position);
                }
                else
                {
                    Instantiate(InimigosAlados[Random.Range(0, InimigosAlados.Length)],
                                    collision.transform.position, Quaternion.identity);
                }
            }
            else
            {

                if (tempConfig[0]) //powerUp
                {
                    GetComponent<PowerUpController>().SpawnPowerUp(collision.transform.position);
                }
                else if (tempConfig[1]) //inimigo terrestre
                {
                    Instantiate(InimigosTerrestres[Random.Range(0, InimigosTerrestres.Length)],
                                collision.transform.position, Quaternion.identity);
                }
                else if (tempConfig[2]) //inimigo alado
                {
                    Instantiate(InimigosAlados[Random.Range(0, InimigosAlados.Length)],
                                collision.transform.position, Quaternion.identity);
                }
            }
        }
    }
}
