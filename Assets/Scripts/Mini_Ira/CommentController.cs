using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommentController : MonoBehaviour
{
    public bool isAggressive;

    public void SetComment(string c)
    {
        GetComponentInChildren<Text>().text = c;

        int rand = Random.Range(0, GetComponentInParent<MinigameIraController>().Users.Count);
        Sprite image = GetComponentInParent<MinigameIraController>().Users[rand];
        GetComponentInParent<MinigameIraController>().Users.RemoveAt(rand);

        GetComponent<Image>().sprite = image;
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
