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

    float xRotation;
    float yRotation;
    float zRotation;

    public float _StartAltitude;
    public float _Radius;

    public Rigidbody _Ball;
    public Transform _Cylinder;

    // Use this for initialization
    void Start()
    {
        Input.gyro.enabled = true;
        _Cylinder.position = Vector3.zero;
        _Ball.transform.position = new Vector3(_Radius, _StartAltitude, 0f);
        //_Ball.AddForce(Vector3.forward * 200);
    }

    void Update()
    {

        xRotation += -Input.gyro.rotationRateUnbiased.x;
        yRotation += -Input.gyro.rotationRateUnbiased.y;
        zRotation += -Input.gyro.rotationRateUnbiased.z;

        //Bolinha.GetComponent<Rigidbody>().AddForce(xRotation, 0, yRotation);
        Arena.transform.eulerAngles = new Vector3(xRotation, 0, yRotation);

        AccX.GetComponent<Text>().text = "x: " + xRotation;
        AccY.GetComponent<Text>().text = "y: " + yRotation;
        AccZ.GetComponent<Text>().text = "z: " + zRotation;


        Vector3 v = _Ball.velocity;
        // any vecotr from cylinders up axis to ball pos
        Vector3 radius = _Ball.transform.position - _Cylinder.transform.position;

        if (v.x == 0 && v.z == 0)
        {
            // ball only bouncing up & down. Can't calculate tangetial speed
        }
        else
        {
            // a vector perpendicular to both of those 
            // (a vector that would be an acceptable velocity for the ball sans the vertical component)
            Vector3 tangent = Vector3.Cross(Vector3.up, radius);

            // store the magnitude so we don't lose momentum when changing direction of speed
            float mag = _Ball.velocity.magnitude;

            // new speed is the old velocity projected on a plane defined by "up" and "tangent"
            Vector3 newVelo = (Vector3.Project(v, tangent) + Vector3.Project(v, Vector3.up)).normalized * mag;

            _Ball.GetComponent<Rigidbody>().velocity = newVelo;
        }
        // set the ball to the correct distance from the cylinder axis (assuming the vertical axis of cylinder is at X==0 && Z==0)
        radius.y = 0;
        radius = radius.normalized * _Radius;
        radius.y = _Ball.transform.position.y;

        _Ball.transform.position = radius;
    }




    /*
    float xRotation;
    float yRotation;
    float zRotation;
    */
    /*

    // Use this for initialization
    void Start()
    {
        NewPosition = new Vector3();
        Input.gyro.enabled = true;
        print(Input.gyro.enabled);
        circleCenter = Vector3.zero;
    }

    private void Update()
    {
        xRotation += -Input.gyro.rotationRateUnbiased.x;
        yRotation += -Input.gyro.rotationRateUnbiased.y;
        zRotation += -Input.gyro.rotationRateUnbiased.z;

        Arena.transform.eulerAngles = new Vector3(xRotation, 0, yRotation);

        AccX.GetComponent<Text>().text = "x: " + xRotation;
        AccY.GetComponent<Text>().text = "y: " + yRotation;
        AccZ.GetComponent<Text>().text = "z: " + zRotation; 

        

        /*
            void Update()
            {



                xRotation += -Input.gyro.rotationRateUnbiased.x;
                yRotation += -Input.gyro.rotationRateUnbiased.y;
                zRotation += -Input.gyro.rotationRateUnbiased.z;

                Arena.transform.eulerAngles = new Vector3(xRotation, 0, yRotation);

                AccX.GetComponent<Text>().text = "x: " + xRotation;
                AccY.GetComponent<Text>().text = "y: " + yRotation;
                AccZ.GetComponent<Text>().text = "z: " + zRotation;

                // Do all other movement first
                // Constrain to a circle with the following
                Vector3 offset = Bolinha.transform.position - circleCenter;
                offset.Normalize();
                offset = offset * radius;
                //Bolinha.transform.position = offset;
            }
            */
    /*
        //Update is called once per frame
        void Update()
        {

            //AccX.GetComponent<Text>().text = "x: " + Math.Round( Input.acceleration.x, 2) + "|" + Arena.transform.rotation.x;
            //AccY.GetComponent<Text>().text = "y: " + Math.Round( Input.acceleration.y, 2) + "|" + Arena.transform.rotation.y;
            //AccZ.GetComponent<Text>().text = "z: " + Math.Round( Input.acceleration.z, 2) + "|" + Arena.transform.rotation.x;

            //input = (float)Math.Round(Input.acceleration.x/0.25f, 3);

            input = (float)Math.Round(AdjustSensivity(0.25f), 3);

            sen = (float)Math.Sqrt(1 - Math.Pow(input, 2));


            if (Input.acceleration.y < 0)
            {
                NewPosition = new Vector3(input * 4, 0.7f, -sen * 4);
            }
            else
            {
                NewPosition = new Vector3(input * 4, 0.7f, sen * 4);
            }


            //Bolinha.transform.position = NewPosition;


            xRotation += -Input.gyro.rotationRateUnbiased.x;
            yRotation += -Input.gyro.rotationRateUnbiased.y;
            zRotation += -Input.gyro.rotationRateUnbiased.z;

            Arena.transform.eulerAngles = new Vector3(xRotation, 0, yRotation);

            AccX.GetComponent<Text>().text = "x: " + xRotation;
            AccY.GetComponent<Text>().text = "y: " + yRotation;
            AccZ.GetComponent<Text>().text = "z: " + zRotation;



            //Arena.transform.rotation = Input.gyro.attitude;

        }
    }
    */

    private float AdjustSensivity(float quociente)
    {
        float temp = Input.acceleration.x / quociente;

        if (temp >= 1)
            return 1;
        else
            return temp;
    }
}
