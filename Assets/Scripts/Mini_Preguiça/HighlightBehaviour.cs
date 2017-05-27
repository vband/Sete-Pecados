﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightBehaviour : MonoBehaviour {

    private Transform highlight;
    private float originalScale;

	void Start ()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.name ==  "Highlight")
            {
                highlight = child;
            }
        }
        originalScale = highlight.localScale.x;
	}
	
	void Update ()
    {
        
    }

    public void Rescale(float percent)
    {
        if (highlight.localScale.x > 1)
        {
            float newScale = originalScale * percent + 1;
            highlight.localScale = new Vector3(newScale, newScale, 1);
        }
    }

    public float GetScale()
    {
        return highlight.localScale.x;
    }

    public bool IsGreat()
    {
        if (Mathf.Abs(highlight.localScale.x - 1) <= 0.1f)
        {
            return true;
        }
        return false;
    }
}
