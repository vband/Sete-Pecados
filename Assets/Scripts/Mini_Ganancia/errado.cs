using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class errado : MonoBehaviour
{
    public void Errado()
    {
        LivesController.RemVidas();
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
    }
}
