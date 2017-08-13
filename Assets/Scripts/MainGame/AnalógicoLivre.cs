using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AnalógicoLivre : MonoBehaviour
{
    public enum AxisOption
    {
        Both,
        OnlyHorizontal,
        OnlyVertical
    }

    public float movementMinRange = 20, movementMaxRange = 50;
    public AxisOption axesToUse = AxisOption.Both;
    public string horizontalAxisName = "Horizontal", verticalAxisName = "Vertical";
    public RectTransform bigCircle, smallCircle;

    private Vector2 startPosition, delta;
    private bool useX, useY;
    private CrossPlatformInputManager.VirtualAxis horizontalAxis, verticalAxis;

    public static bool hasCreatedAxes = false;

    void OnEnable()
    {
        if (!hasCreatedAxes)
        {
            CreateVirtualAxes();
            hasCreatedAxes = true;
        }
    }

    void Start ()
    {
        
    }

    void Update ()
    {
        ReceiveTouch();
        SendInput();
	}

    void CreateVirtualAxes()
    {
        useX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
        useY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

        if (useX)
        {
            horizontalAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
            CrossPlatformInputManager.RegisterVirtualAxis(horizontalAxis);
        }
        if (useY)
        {
            verticalAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
            CrossPlatformInputManager.RegisterVirtualAxis(verticalAxis);
        }
    }

    private void ReceiveTouch()
    {
#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x < Screen.width / 2)
        {
            startPosition = Input.mousePosition;
            bigCircle.gameObject.SetActive(true);
            bigCircle.position = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width / 2)
        {
            delta = (Vector2)Input.mousePosition - startPosition;
            if (delta.sqrMagnitude < movementMinRange * movementMinRange)
            {
                delta = Vector2.zero;
            }
        }
        else
        {
            delta = Vector2.zero;
            bigCircle.gameObject.SetActive(false);
        }

#else
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                // Se o jogador tocar na parte esquerda da tela
                if (touch.phase == TouchPhase.Began && touch.position.x < Screen.width / 2)
                {
                    startPosition = touch.position;
                    bigCircle.gameObject.SetActive(true);
                    bigCircle.position = Input.mousePosition;
                }

                // Se o jogador arrastar o dedo na tela
                else if (touch.phase == TouchPhase.Moved && touch.position.x < Screen.width / 2)
                {
                    delta = touch.position - startPosition;
                    if (delta.sqrMagnitude < movementMinRange)
                    {
                        delta = Vector2.zero;
                    }
                }
            }
        }
        else
        {
            delta = Vector2.zero;
            bigCircle.gameObject.SetActive(false);
        }
#endif
    }

    private void SendInput()
    {
        Vector2 movement = Vector2.ClampMagnitude(delta, movementMaxRange);

        if (movement != Vector2.zero && !smallCircle.gameObject.activeSelf)
        {
            smallCircle.gameObject.SetActive(true);
            smallCircle.position = bigCircle.position + (Vector3)movement;
        }
        else if (movement != Vector2.zero && smallCircle.gameObject.activeSelf)
        {
            smallCircle.position = bigCircle.position + (Vector3)movement;
        }
        else if (movement == Vector2.zero && smallCircle.gameObject.activeSelf)
        {
            smallCircle.gameObject.SetActive(false);
        }

        movement /= movementMaxRange;
        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            movement.y = 0;
        }
        else if (Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
        {
            movement.x = 0;
        }

        if (useX)
        {
            horizontalAxis.Update(movement.x);
        }
        if (useY)
        {
            verticalAxis.Update(movement.y);
        }
    }

    public void NeutralizeInput()
    {
        CrossPlatformInputManager.SetAxisZero(horizontalAxisName);
        CrossPlatformInputManager.SetAxisZero(verticalAxisName);
    }

    public static void UnRegisterAxis(string name)
    {
        CrossPlatformInputManager.UnRegisterVirtualAxis(name);
    }
}
