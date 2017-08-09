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

    private bool isFlat = true;
    private Vector3 tilt;

    // Use this for initialization
    void Start()
    { 
        _Cylinder.position = Vector3.zero;
        _Ball.transform.position = new Vector3(_Radius, _StartAltitude, 0f);
        
    }

    void Update()
    {
        //obtem input
        tilt = Input.acceleration;

        if (isFlat)
            tilt = Quaternion.Euler(90, 0, 0) * tilt;

        _Ball.AddForce(tilt.x * 20 , 0, tilt.z * 20 );

        //cuida da trajetoria circular
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
}
