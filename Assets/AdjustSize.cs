using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSize : MonoBehaviour {

    private RectTransform canvas;

	// Use this for initialization
	void Start () {
        canvas = GetComponentInParent<RectTransform>();
        
	}

    private void OnRectTransformDimensionsChange()
    {
        
    }

    // Update is called once per frame
    void Update () {
        GetComponent<RectTransform>().sizeDelta = new Vector2(canvas.sizeDelta.x,
                                                              GetComponent<RectTransform>().sizeDelta.y);
    }
}
