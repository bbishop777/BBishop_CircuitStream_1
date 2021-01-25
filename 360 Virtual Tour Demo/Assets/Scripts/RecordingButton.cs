using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecordingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isButtonBeingPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonBeingPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonBeingPressed = false;
    }
}
