using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderReloader : MonoBehaviour
{
    public void OnGetMessage()
    {
        StartCoroutine(ReloadShader());
    }

    private IEnumerator ReloadShader()
    {
        GetComponent<ObjectMaskCamera>().enabled = false;

        yield return new WaitForEndOfFrame();

        GetComponent<ObjectMaskCamera>().enabled = true;
    }
}
