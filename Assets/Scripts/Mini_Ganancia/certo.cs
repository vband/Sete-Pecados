using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class certo : MonoBehaviour {
   
    public void Certo()
    {
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
    }

    


}
