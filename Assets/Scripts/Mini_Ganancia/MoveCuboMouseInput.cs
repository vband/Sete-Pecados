using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCuboMouseInput : MonoBehaviour {

    [SerializeField] private GameObject Cam;

    public Vector3 mousePosition;
    public Vector3 temp;
    public float dist;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

       
        mousePosition = Input.mousePosition + new Vector3(0,0,dist);
        temp = Cam.GetComponent<Camera>().ScreenToWorldPoint(mousePosition);

        transform.position = new Vector3(temp.x, temp.y, temp.z);
	}
}
