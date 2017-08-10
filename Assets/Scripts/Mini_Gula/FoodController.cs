using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    private bool type; // Tipo de comida: boa ou ruim

    // Getter e setter para a velocidade da queda
    public float Speed
    {
        get
        {
            return GetComponent<Rigidbody2D>().gravityScale;
        }
        set
        {
            GetComponent<Rigidbody2D>().gravityScale = value;
        }
    }

    // Getter e setter para a imagem
    public Sprite Sprite
    {
        get
        {
            return GetComponent<SpriteRenderer>().sprite;
        }
        set
        {
            GetComponent<SpriteRenderer>().sprite = value;
        }
    }

    // Getter e setter para o tipo de comida
    public bool Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }

    // Constantes públicas
    public static bool GOOD
    {
        get
        {
            return true;
        }
    }
    public static bool BAD
    {
        get
        {
            return false;
        }
    }
}
