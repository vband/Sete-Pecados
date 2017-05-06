using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_MoveCamera : MonoBehaviour {

    public Camera cam;

    private bool move_cam = false;

	// Use this for initialization
	void Start () {
        InstructionsTimer();
	}
	
	// Update is called once per frame
	void Update () {
        if (move_cam && cam.transform.position.y > 0) {
            cam.transform.Translate(new Vector3(0, -1));
        }
            
        
	}

    void InstructionsTimer()
    {

       StartCoroutine(timer(5));
       
    }

    IEnumerator timer(float segundos)
    {
        print(Time.time);
        yield return new WaitForSecondsRealtime(segundos);
        move_cam = true;
        print(Time.time);
    }
}
