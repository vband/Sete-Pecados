using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPreguiçaController : MonoBehaviour
{
    private void Start()
    {
        //debug para inimigo da preguiça spawnar deitado
        transform.Rotate(Vector3.forward, 90);
    }
}
