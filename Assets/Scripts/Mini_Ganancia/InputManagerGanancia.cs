using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InputManagerGanancia : MonoBehaviour
{
    [SerializeField]
    private GameObject Arena;
    [SerializeField]
    private GameObject Bolinha;

    public Text AccX;
    public Text AccY;
    public Text AccZ;

    private Vector3 NewPosition;

    private float sen, input;
    
    // Use this for initialization
    void Start()
    {
        NewPosition = new Vector3();
    }

    // Update is called once per frame
    void Update()
    { 

        AccX.GetComponent<Text>().text = "x: " + Math.Round( Input.acceleration.x, 2) + "|" + Arena.transform.rotation.x;
        AccY.GetComponent<Text>().text = "y: " + Math.Round( Input.acceleration.y, 2) + "|" + Arena.transform.rotation.y;
        AccZ.GetComponent<Text>().text = "z: " + Math.Round( Input.acceleration.z, 2) + "|" + Arena.transform.rotation.x;


        input = (float)Math.Round(Input.acceleration.x * 4, 2);
        if (input > 1f)
            input = 1f;
        else if (input < -1f)
            input = -1f;
        
        sen = (float)Math.Sqrt(1 - Math.Pow(input, 2));
               
        
        if (Input.acceleration.y < 0 )
        {
            NewPosition = new Vector3(input * 4, 0, -sen * 4);
        }
        else
        {
            NewPosition = new Vector3(input * 4, 0, sen * 4);
        }
        
        
        

        
        Bolinha.transform.position = NewPosition;
        
    }
}
