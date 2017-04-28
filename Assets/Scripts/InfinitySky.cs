using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitySky : MonoBehaviour {

    //gameobjects usados como referencia
    public GameObject player;
<<<<<<< HEAD
    //public GameObject sky;
=======
    public GameObject sky;
>>>>>>> 2a96d3d6aeaa8316e1a6853358aa764eb104f329
   
    //prefabs para clonar
    public Transform skypf;
    //vetor unitario para calculo das distancias
<<<<<<< HEAD
    //private Vector3 unitario = new Vector3(1, 0, 0);
    //variaveis que armazeram a posicao xy do ultimo prefab clonado
    private Vector3 currentPos;
    private float skyWidth;
=======
    private Vector3 unitario = new Vector3(1, 0, 0);
    //variaveis que armazeram a posicao xy do ultimo prefab clonado
    private Vector3 lastpos_sky;
>>>>>>> 2a96d3d6aeaa8316e1a6853358aa764eb104f329

    void Start()
    {
        //inicializa as variaveis com os prefabs do inicio
<<<<<<< HEAD
        currentPos = skypf.transform.position;
        //Player = GameObject.FindWithTag("Player");
        //Obtém o tamanho da sprite, em coordenadas de mundo
        skyWidth = skypf.GetComponent<SpriteRenderer>().bounds.size.x;
=======
        lastpos_sky = sky.transform.position;
        //Player = GameObject.FindWithTag("Player");
>>>>>>> 2a96d3d6aeaa8316e1a6853358aa764eb104f329
    }


    void Update()
    {
        //condicionais testam a distancia do ultimo prefab gerado e instancia os novos com base na distancia, garantindo q isso aconteca fora da visao do jogador
<<<<<<< HEAD
        if (Vector3.Distance(player.transform.position, currentPos) < 50)
        {
            currentPos += skyWidth * Vector3.right;
            Instantiate(skypf, currentPos, new Quaternion(0, 0, 0, 0));
            //lastpos_sky += (skyWidth * Vector3.right);
=======
        if (Vector3.Distance(player.transform.position, lastpos_sky) > 1 && Vector3.Distance(player.transform.position, lastpos_sky) < 50)
        {
            Instantiate(skypf, (lastpos_sky + 20.48f * unitario), new Quaternion(0, 0, 0, 0));
            lastpos_sky += (20.48f * unitario);
            
>>>>>>> 2a96d3d6aeaa8316e1a6853358aa764eb104f329
        }
    }
}
