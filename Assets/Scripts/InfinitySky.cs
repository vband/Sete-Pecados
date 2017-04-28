using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitySky : MonoBehaviour {

    //gameobjects usados como referencia
    public GameObject player;
    public GameObject sky;
   
    //prefabs para clonar
    public Transform skypf;
    //vetor unitario para calculo das distancias
    private Vector3 unitario = new Vector3(1, 0, 0);
    //variaveis que armazeram a posicao xy do ultimo prefab clonado
    private Vector3 lastpos_sky;

    void Start()
    {
        //inicializa as variaveis com os prefabs do inicio
        lastpos_sky = sky.transform.position;
        //Player = GameObject.FindWithTag("Player");
    }


    void Update()
    {
        //condicionais testam a distancia do ultimo prefab gerado e instancia os novos com base na distancia, garantindo q isso aconteca fora da visao do jogador
        if (Vector3.Distance(player.transform.position, lastpos_sky) > 1 && Vector3.Distance(player.transform.position, lastpos_sky) < 50)
        {
            Instantiate(skypf, (lastpos_sky + 20.48f * unitario), new Quaternion(0, 0, 0, 0));
            lastpos_sky += (20.48f * unitario);
            
        }
    }
}
