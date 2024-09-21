using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Action
{
    Jump,
    MoveLeft,
    MoveRight
}

public class UIHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public Action action;
    [SerializeField] private PlayerController PlayerController;

    private void Start()
    {
        PlayerController = FindObjectOfType<PlayerController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");

        switch (action)
        {
            case Action.Jump:
                PlayerController.Jump();
                break;
            case Action.MoveLeft:
                PlayerController.MoveLeft();
                break;
            case Action.MoveRight:
                PlayerController.MoveRight();
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");

        PlayerController.movementDirection = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");

        PlayerController.movementDirection = 0;
    }
}
