using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] string buttonSound;


    public void OnPointerClick(PointerEventData eventData){
        AudioManager.Instance.PlaySound(buttonSound);
    }
}
