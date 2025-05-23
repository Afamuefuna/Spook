using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    public UIManager uIManager;

    void Start(){
        uIManager = FindAnyObjectByType<UIManager>();
    }
    public virtual void Show(){
        gameObject.SetActive(true);
    }

    public virtual void Hide(){
        gameObject.SetActive(false);
    }
}
