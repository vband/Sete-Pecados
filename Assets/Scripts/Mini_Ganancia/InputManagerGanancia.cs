using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InputManagerGanancia : MonoBehaviour
{
    [SerializeField]
    private GameObject Arena;

    public Text AccX;
    public Text AccY;
    public Text AccZ;

    public Quaternion to;
    public float speed = 2f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 

        AccX.GetComponent<Text>().text = "x: " + Input.acceleration.x;
        AccY.GetComponent<Text>().text = "y: " + Input.acceleration.y;
        AccZ.GetComponent<Text>().text = "z: " + Input.acceleration.z;
        if(Input.acceleration.x < -0.1 )
        {
            Arena.transform.Rotate(Vector3.forward);
        }
        else if(Input.acceleration.x > 0.1 )
        {
            Arena.transform.Rotate(Vector3.back);
        }


        if (Input.acceleration.y < -0.1 )
        {
            Arena.transform.Rotate(Vector3.left);
        }
        else if (Input.acceleration.y > 0.1 )
        {
            Arena.transform.Rotate(Vector3.right);
        }


        Arena.transform.rotation = new Quaternion(Arena.transform.rotation.x, 0, Arena.transform.rotation.z, 1);

    }
}
