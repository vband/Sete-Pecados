﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InputManagerGanancia : MonoBehaviour
{
    [SerializeField] private GameObject Arena;
    [SerializeField] private Camera cam;


    public float _StartAltitude;
    public float _Radius;

    public Rigidbody _Ball;
    public Transform _Cylinder;
#if UNITY_ANDROID
    private bool isFlat = true;
#endif
    private Vector3 tilt, mousePosition, temp;

    // Use this for initialization
    void Start()
    {
        _Ball.transform.position = new Vector3(_Radius + _Cylinder.position.x, _Cylinder.position.y + _StartAltitude, _Cylinder.position.z);
    }

    void Update()
    {

#if UNITY_ANDROID
        //obtem input
        tilt = Input.acceleration;

        if (isFlat)
            tilt = Quaternion.Euler(90, 0, 0) * tilt;

        _Ball.AddForce(tilt.x * 30, 0, tilt.z * 30);

#elif UNITY_STANDALONE

        mousePosition = Input.mousePosition + new Vector3(0, 0, 10);
        temp = cam.ScreenToWorldPoint(mousePosition);
        _Ball.transform.transform.position = new Vector3(temp.x, temp.y + 0.2f, temp.z);
#endif
        //cuida da trajetoria circular
        
        Vector3 v = _Ball.velocity;
        // any vector from cylinders up axis to ball pos
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
        // set the ball to the correct distance from the cylinder axis 
        radius.y = 0;
        radius = radius.normalized * _Radius + _Cylinder.transform.position;
        radius.y = _Ball.transform.position.y;

        _Ball.transform.position = radius;
        
    }
}
