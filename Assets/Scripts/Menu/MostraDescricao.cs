using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MostraDescricao : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

    public Text Descricao;

    public void OnPointerEnter(PointerEventData eventData)
    {
#if UNITY_STANDALONE
        Descricao.gameObject.SetActive(true);
#endif
    }

    public void OnPointerExit(PointerEventData eventData)
    {
#if UNITY_STANDALONE
        Descricao.gameObject.SetActive(false);
#endif
    }
}
