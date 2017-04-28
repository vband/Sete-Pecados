using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityWorld : MonoBehaviour {

    //gameobjects usados como referencia
    public GameObject player;
    public GameObject prank;
    public GameObject ground;
    //prefabs para clonar
    public Transform prankpf;
    public Transform groundpf;
    //variaveis que armazeram a posicao x do ultimo prefab clonado
    private Vector3 lastpos_prank;
    private Vector3 lastpos_ground;
    //vetor unitario para calculo das distancias
    private Vector3 unitario = new Vector3(1, 0, 0);

    void Start () {
        //inicializa as variaveis com os prefabs do inicio
        lastpos_prank = prank.transform.position;
        lastpos_ground = ground.transform.position;
    }
	

	void Update () {     
        //os dois condicionais testam a distancia do ultimo prefab gerado e instancia os novos com base na distancia, garantindo q isso aconteca fora da visao do jogador
        if (Vector3.Distance(player.transform.position, lastpos_prank) > 4 && Vector3.Distance(player.transform.position, lastpos_prank) < 50)
        {
            Instantiate(prankpf, (lastpos_prank + 4*unitario), new Quaternion(0,0,0,0));
            lastpos_prank += (4 * unitario);
            Instantiate(prankpf, (lastpos_prank + 4 * unitario), new Quaternion(0, 0, 0, 0));
            lastpos_prank += (4 * unitario);
            Instantiate(prankpf, (lastpos_prank + 4 * unitario), new Quaternion(0, 0, 0, 0));
            lastpos_prank += (4 * unitario);
        }

        if (Vector3.Distance(player.transform.position, lastpos_ground) > 1 && Vector3.Distance(player.transform.position, lastpos_ground) < 50)
        {
            Instantiate(groundpf, (lastpos_ground + 10 * unitario), new Quaternion(0, 0, 0, 0));
            lastpos_ground += (10 * unitario);
            Instantiate(groundpf, (lastpos_ground + 10 * unitario), new Quaternion(0, 0, 0, 0));
            lastpos_ground += (10 * unitario);
        }
	}
}