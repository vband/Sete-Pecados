﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommentController : MonoBehaviour
{
    public bool isAggressive;

	void Start () { }

    void Update() { }

    public void SetComment(string c)
    {
        GetComponentInChildren<Text>().text = c;
    }

    public void OnClick()
    {
        if (isAggressive)
        {
            GetComponentInParent<MinigameIraController>().AggressiveCommentOnClick();
            Vibration.Vibrate(30);
        }
        else
        {
            GetComponentInParent<MinigameIraController>().PoliteCommentOnClick();
            Vibration.Vibrate(60);
        }
    }
}
